using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int lives;
    public int score;
    public Text livesText;
    public Text scoreText;
    public bool gameOver;
    public GameObject gameOverPanel;
    public int numberOfBricks;

    private string lve = "Lives: ";
    private string scr = "Score: ";

    void Start()
    {
        livesText.text = lve + lives;
        scoreText.text = scr + score;
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;
      }

    public void UpdateLives(int changeInLives)
    {
        lives += changeInLives;

        if(lives <= 0)
        {
            lives = 0;
            GameOver();
        }


        livesText.text = lve + lives;
    }

    public void UpdateNumberOfBricks()
    {
        numberOfBricks--;
        if(numberOfBricks <=-0){
            GameOver();
        }
    }


    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = scr + score;

    }

    void GameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Main");
    }

    public void quit()
    {
        Application.Quit();
        Debug.Log("Game quit");
    }

}
