using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public int startLife = 3;       //Default life quantity for a new game
    public Text highScoreText;      //High Score Display

    private int highScore;          //Get the highScore
    
    //Quit the game
    public void QuitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
       highScore = PlayerPrefs.GetInt("HIGHSCORE");
    }

    private void Update()
    {
        highScoreText.text = "High Score " + highScore.ToString();      //High score display
    }

    //Go to Endless game
    public void EndlessGame()
    {
        PlayerPrefs.SetInt("LIFE", startLife);
        SceneManager.LoadScene("Endless");
    }
   
    //Go to the 1st challenge game
    public void ChallengeGame()
    {
        PlayerPrefs.SetInt("LIFE", startLife);
        SceneManager.LoadScene("Challenge_1");
    }

}
