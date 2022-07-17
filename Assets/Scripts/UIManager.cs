using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{

    public GameObject encounterUI;
    public GameObject loseScreen;
    public TMP_Text stateText;
    public Slider playerHP;
    public Slider enemyHP;
    public Transform playerUI;
    public Transform enemyUI;
    public Vector3 UIOffset;
    public Die[] playerDice;
    public Die enemyDie;
    EncounterManager em;
    float delayPerCharacter;
    bool canGoNext;

    // Start is called before the first frame update
    void Start(){

        em = FindObjectOfType<EncounterManager>();
        encounterUI.SetActive(false);

    }

    public void UpdateDice(int[] diceRoll,bool isPlayer){

        if(isPlayer){

            for(int i = 0; i < diceRoll.Length;i++){

                playerDice[i].value = diceRoll[i];

            }

        } else {

            enemyDie.value = diceRoll[0];

        }

    }

    public void AttackClick(){

        if(em.midAction) return;
        Debug.Log("Attack Clicked");
        em.PlayerAttack();

    }

    public void RunClick(){

        Debug.Log("Run Clicked");
        em.Run();

    }

    public void StartEncounter(PlayerStats player,EnemyStats enemy){

        encounterUI.SetActive(true);
        playerHP.value = player.currentHealth / player.maxHealth;
        enemyHP.value = enemy.currentHealth / enemy.maxHealth;

    }

    public void EndEncounter(){

        encounterUI.SetActive(false);

    }

    public void ShowLoseScreen(){

        loseScreen.SetActive(true);

    }

    public void ChangeText(string text){

        stateText.text = text;
        //Play a sound?

    }

    public void OnRestart(){

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

}
