using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public int startLife = 3;       //Default life quantity for a new game
    
    //Quit the game
    public void QuitGame()
    {
        Application.Quit();
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
