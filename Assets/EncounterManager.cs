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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartEncounter(EnemyStats e,BoardSpace tile){

        currentState = BattleState.START;
        enemy = e;

        player.GetComponent<GridPlayerMovement>().actionable = false;
        player.transform.position -= new Vector3(unitLength/2,0,0);
        enemy.transform.position += new Vector3(unitLength/2,0,0);

        uiManager.StartEncouncter(e);

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
