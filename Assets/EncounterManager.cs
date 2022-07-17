using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BattleState {

    START,
    PLAYER,
    ENEMY,
    WON,
    LOST,
    EXIT

}

public class EncounterManager : MonoBehaviour
{

    public PlayerStats player;
    public EnemyStats enemy;
    public BattleState currentState;
    UIManager uiManager;
    int unitLength;

    // Start is called before the first frame update
    void Start()
    {

        player = FindObjectOfType<PlayerStats>();
        unitLength = FindObjectOfType<DungeonGen>().unitLength;
        uiManager = FindObjectOfType<UIManager>();
        
    }

    int[] RollDice(int numDice){

        int[] values = new int[numDice];

        for(int i = 0; i < numDice;i++){

            values[i] = Random.Range(1,6);

        }

        return values;

    }

    public void StartEncounter(EnemyStats e,BoardSpace tile){

        enemy = e;
        StartCoroutine(StartEncounterCoro(enemy,tile));

    }

    IEnumerator StartEncounterCoro(EnemyStats e,BoardSpace tile){

        currentState = BattleState.START;
        uiManager.ChangeText("A " + e.name + " attacked");

        enemy.currentHealth = enemy.maxHealth;
        player.currentHealth = player.maxHealth;

        player.GetComponent<GridPlayerMovement>().actionable = false;
        player.transform.position = tile.transform.position - new Vector3(unitLength/2,0,0);
        enemy.transform.position += new Vector3(unitLength/2,0,0);

        uiManager.StartEncounter(player,e);

        yield return new WaitForSeconds(2f);

        PlayerTurn();

    }

    void PlayerTurn(){

        uiManager.ChangeText("PLAYER TURN");
        currentState = BattleState.PLAYER;

    }

    public void PlayerAttack(){

        if(currentState != BattleState.PLAYER) return;
        StartCoroutine(PlayerAttackCoro());

    }

    IEnumerator PlayerAttackCoro(){

        int playerDmg = 0;
        int[] playerDiceRoll = RollDice(player.numberOfDice);
        uiManager.UpdateDice(playerDiceRoll,true);

        for(int i = 0; i < playerDiceRoll.Length;i++){

            playerDmg += playerDiceRoll[i];

        }

        uiManager.ChangeText("Player did " + playerDmg + " damage!");

        bool isDead = enemy.TakeDamage(playerDmg);
        uiManager.enemyHP.value = enemy.currentHealth / enemy.maxHealth;

        yield return new WaitForSeconds(2f);

        if(isDead){

            Destroy(enemy.gameObject);
            Win();

        } else {

            currentState = BattleState.ENEMY;
            StartCoroutine(EnemyAttackCoro());

        }

    }

    
    IEnumerator EnemyAttackCoro(){

        int enemyDmg = 0;
        int[] enemyDiceRoll = RollDice(1);
        uiManager.UpdateDice(enemyDiceRoll,false);

        for(int i = 0; i < enemyDiceRoll.Length;i++){

            enemyDmg += enemyDiceRoll[i];

        }

        uiManager.ChangeText(enemy.name + " did " + enemyDmg + " damage!");

        bool isDead = player.TakeDamage(enemyDmg);
        uiManager.playerHP.value = player.currentHealth / player.maxHealth;

        yield return new WaitForSeconds(2f);

        if(isDead){

            uiManager.ShowLoseScreen();
            Lose();

        } else {

            PlayerTurn();

        }

    }

    public void Run(){

        if(currentState != BattleState.PLAYER) return;
        StartCoroutine(RunCoro());

    }

    IEnumerator RunCoro(){

        uiManager.ChangeText("ESCAPED");
        currentState = BattleState.EXIT;
        yield return new WaitForSeconds(2f);

        uiManager.EndEncounter();
        player.GetComponent<GridPlayerMovement>().actionable = true;
        
    }

    public void Win(){

        currentState = BattleState.WON;

        uiManager.EndEncounter();
        player.GetComponent<GridPlayerMovement>().actionable = true;

    }

    public void Lose(){

        currentState = BattleState.LOST;

        uiManager.EndEncounter();
        player.GetComponent<GridPlayerMovement>().actionable = true;

    }

}
