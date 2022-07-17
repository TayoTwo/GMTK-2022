using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    public int currentScore;
    public int highScore;
    public TMP_Text scoreText;
    public TMP_Text highscoreText;

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("Highscore",0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        scoreText.text = "Score - " + currentScore;
        highscoreText.text = "Highscore - " + highScore;

        if(currentScore > highScore){

            highScore = currentScore;
            PlayerPrefs.SetInt("Highscore",highScore);
            PlayerPrefs.Save();

        }

    }
}
