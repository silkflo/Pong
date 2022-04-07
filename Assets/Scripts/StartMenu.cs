using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void EndlessGame()
    {
        SceneManager.LoadScene("Endless");
    }
    public void ChallengeGame()
    {
        PlayerPrefs.SetInt("LIFE", 3);
        SceneManager.LoadScene("Challenge_1");
    }

}
