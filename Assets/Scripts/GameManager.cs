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
    public Text highScoreText;
    public bool gameOver;
    public GameObject gameOverPanel;
    public GameObject loadLevelPanel;
    public int numberOfBricks;
    public Transform[] levels;
    public int currentLevelIndex = 0;

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
        if(numberOfBricks <= 0){
            Debug.Log("currentLevelIndex : " + currentLevelIndex + "levels Length : " + levels.Length);
            if (currentLevelIndex >= levels.Length - 1)
            {
                GameOver();
            } else
            {
                loadLevelPanel.SetActive(true);
                loadLevelPanel.GetComponentInChildren<Text>().text = "level " + (currentLevelIndex + 2);
                gameOver = true;
                Invoke("LoadLevel", 3f);
               // LoadLevel();
            }
        }
    }


    void LoadLevel()
    {
        currentLevelIndex++;
        Instantiate(levels[currentLevelIndex], Vector2.zero, Quaternion.identity);
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;
        gameOver = false;
        loadLevelPanel.SetActive(false);
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
        int highScore = PlayerPrefs.GetInt("HIGHSCORE");
        if(score > highScore)
        {
            PlayerPrefs.SetInt("HIGHSCORE", score);
            highScoreText.text = "New High Score " + score;
        }
        else
        {
            highScoreText.text = "High Score " + score;
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Main");
    }

    public void Menu()
    {
        SceneManager.LoadScene("StartMenu");
    }

}
