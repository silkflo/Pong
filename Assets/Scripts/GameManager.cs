using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public enum SceneName { Endless , Challenge1 }
    public SceneName sceneName;
    public int lives;
    public int score;
    public int numberOfBricks;
    public int currentLevelIndex = 0;
    public float brickLeftMargin;
    public Text livesText;
    public Text scoreText;
    public Text highScoreText;
    public bool gameOver;
    public GameObject gameOverPanel;
    public GameObject loadLevelPanel;
    public GameObject blueBrick;
    public GameObject yellowBrick;
   

    void Start()
    {
        //UI Text

        livesText.text = "Lives: " + lives;

        if (scoreText)
        {
            scoreText.text = score.ToString();
        }
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;
      }

    public void UpdateLives(int changeInLives)
    {
        lives += changeInLives;
        //Game Over trigger
        if(lives <= 0)
        {
            lives = 0;
            GameOver();
        }
        livesText.text = "Lives: " + lives;
    }

    public void UpdateNumberOfBricks()
    {
        //Game acition when no brick left
        numberOfBricks--;
        if(numberOfBricks <= 0){
            //Game Over
            if (lives==0)
            {
                GameOver();
            } 
            //Load new level
            else if (sceneName == SceneName.Endless)
            {
                loadLevelPanel.SetActive(true);
                loadLevelPanel.GetComponentInChildren<Text>().text = "level " + (currentLevelIndex + 2);
                gameOver = true; //it pauses and init the game
                Invoke("LoadLevel", 3f);
            } else
            {
                loadLevelPanel.SetActive(true);
            }
        }
    }

    void LoadLevel()
    {

        currentLevelIndex++; //Keep track on the player level

        // load random bricks for the new level
        GameObject brick;
        float initBrickMargin = brickLeftMargin;
        for(int i = 0; i < 5; i++)
        {
            //Increase the range for new color brick
            int brickChoosen = Random.Range(1, 3);

            brick = brickChoosen switch
            {
                1 => yellowBrick,
                2 => blueBrick,
                _ => yellowBrick,
            };
            brickLeftMargin += 0.76f;
            Instantiate(brick, new Vector2(brickLeftMargin, 3.0f), Quaternion.identity);
            Instantiate(brick, new Vector2(brickLeftMargin, 2.70f), Quaternion.identity);
            Instantiate(brick, new Vector2(brickLeftMargin, 2.40f), Quaternion.identity);
        }
         brickLeftMargin = initBrickMargin;

        //Init the game back
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;
        gameOver = false;
        loadLevelPanel.SetActive(false);
    }

    public void UpdateScore(int points)
    {
        //Score Text
        if (scoreText)
        {
            score += points;
            scoreText.text = score.ToString();
        }
    }

    void GameOver()
    {
        gameOver = true;  //gameOver true ruturn other scripts null
        gameOverPanel.SetActive(true); //End game display
        //Set the highscore
        int highScore = PlayerPrefs.GetInt("HIGHSCORE"); 
        if(score > highScore && sceneName == SceneName.Endless)
        {
            PlayerPrefs.SetInt("HIGHSCORE", score);
            highScoreText.text = "New High Score " + score;
        }
        else if(sceneName == SceneName.Endless)
        {
            highScoreText.text = "High Score " + score;
        }
    }

    public void PlayAgain()
    {
        if (sceneName == SceneName.Endless)
        {
            SceneManager.LoadScene("Endless");
        } else if (sceneName == SceneName.Challenge1)
        {
            SceneManager.LoadScene("Challenge_1");
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("StartMenu");
    }

}
