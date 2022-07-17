using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    public GameObject encounterUI;
    public TMP_Text textBox;
    float delayPerCharacter;
    bool canGoNext;

    // Start is called before the first frame update
    void Start(){

        encounterUI.SetActive(false);

    }

    public void AttackClick(){

        Debug.Log("Attack Clicked");
        //Roll Dice

        //Show Dice Rolling animation

    

    }

    public void RunClick(){

        Debug.Log("Run Clicked");

    }

    public void StartEncouncter(EnemyStats enemy){

        encounterUI.SetActive(true);

    }

    public void EndEncounter(){

        encounterUI.SetActive(false);

    }

    public void ChangeText(string script){

        textBox.text = script;
        //Play a sound?

    }


}
