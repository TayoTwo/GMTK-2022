using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject instructions;


    public void OnClickStart(){

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void OnClickInstructions(){

        mainMenu.SetActive(false);
        instructions.SetActive(true);

    }

    public void OnClickBack(){

        mainMenu.SetActive(true);
        instructions.SetActive(false);

    }

}
