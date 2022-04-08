using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public enum SceneName { Endless , Challenge }   //Get the type of game play
    public SceneName sceneName;                         
    public int currentLevelIndex = 0;               //Endless game Level number
    public int bricksColumnQty = 5;                 //Bricks column quantity
    public int brickRowQty = 3;
    public float brickPositionX = -2.2f;           //Adjust the brick block position from on X
    public float timeChallenge;                     //Max time allowed for the time challenge
    public Text livesText;                          //Lives display text
    public Text scoreText;                          //Score display text
    public Text highScoreText;                      //Highscore Display text in the game over panel
    public bool gameOver;                           //Set to true, the gameplay is disabled
    public GameObject gameOverPanel;                //Display of the game over panel
    public GameObject successPanel;                 //Panel displayed between levels
    public GameObject[] bricks;                     //Available bricks for the endless game

    private int score;                              //Score
    private int numberOfBricks;                     //Keep track on the brick amount left
    private int lives;                              //lives

    void Start()
    {
        //Lives Text Display
        lives = PlayerPrefs.GetInt("LIFE");
        livesText.text = "Lives: " + lives;
                
        //1st Endless Mode level load
        if (sceneName == SceneName.Endless)
        {
            scoreText.text = score.ToString(); //Score display
            LoadLevel();    //Add the bricks
        }
      
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;      //Get level bricks quantity
    }

    //Manage the lives
    public void LivesManagement(int changeInLives)
    {
        PlayerPrefs.SetInt("LIFE", lives += changeInLives);     //Update lives quantity

        //Game Over, no more life left
        if (lives <= 0)
            {
            lives = 0;
            GameOver(); //Stop the gameplay and show the game over panel
            }
            livesText.text = "Lives: " + lives;     //Display the lives
    }

    //Manage the bricks
    public void BrickManagement()
    {
        numberOfBricks--;      //Keep track of bricks quantity 
        //No more bricks left in the game
        if (numberOfBricks <= 0){
            //Endless Mode
            if (sceneName == SceneName.Endless)
            {
                //Display successPanel
                successPanel.SetActive(true);
                successPanel.GetComponentInChildren<Text>().text = "level " + (currentLevelIndex + 1);
                gameOver = true; //It pauses and init the game
                Invoke("LoadLevel", 3f); //Wait 3 sec before load level
            } else //Challenge Mode
            {
                successPanel.SetActive(true); //Display success panel
            }
        }
    }

    //Load a new Level of random bricks
    void LoadLevel()
    {
         currentLevelIndex++;   //Keep track of the game level

        //Add random bricks for the new level
        GameObject brick;
        float initBrickPositionX = brickPositionX;    //Save the init position on X the 1st brick
        float brickPositionY = 2.10f;     //Brick position on Y axis
        //Add 3 rows of random bricks 
        for (int i = 0; i < bricksColumnQty; i++)
        {
            brickPositionX += 0.76f;   //Add margin for the next brick column position

            //re-loop for each row to set each bricks
            for (int j = 0; j < brickRowQty; j++)
            {
                int brickChoosen = Random.Range(0, bricks.Length);  //Choose the brick
                brickPositionY += 0.3f;  //Add margin for the next row
                //Call the brick
                brick = brickChoosen switch
                {
                    1 => bricks[brickChoosen],
                    2 => bricks[brickChoosen],
                    _ => bricks[0],
                };
                Instantiate(brick, new Vector2(brickPositionX, brickPositionY), Quaternion.identity);
            }
            brickPositionY = 2.10f;    //init back the position for the next brick spawn on Y
        }

        brickPositionX = initBrickPositionX; //Set the original position on X, for the next level loading

        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;  //Get the new bricks quantity
        gameOver = false;  //Enable gameplay
        successPanel.SetActive(false);  //Hide the success panel 
    }
    
    //Update the score
    public void UpdateScore(int points)
    {
        if (scoreText)
        {
            score += points;   //add points
            scoreText.text = score.ToString();   //Score display update
        }
    }

    //Game Over Managment
    public void GameOver()
    {
        gameOver = true;     //GameOver true reveive what is returned from the other scripts using this variable
        gameOverPanel.SetActive(true);  //Game over panel display
        //Set the highscore
        int highScore = PlayerPrefs.GetInt("HIGHSCORE"); 
        if(score > highScore && sceneName == SceneName.Endless)
        {
            //High score beaten set new highscore
            PlayerPrefs.SetInt("HIGHSCORE", score);
            highScoreText.text = "New High Score " + score;     //Display high score
        }
        else if(sceneName == SceneName.Endless)
        {
            highScoreText.text = "High Score " + score;     //Display high score
        }
    }
  
//GAME NAVIGUATION
    public void GoToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
    public void GoToEndless(bool gameOver)
    {
        if (gameOver)
        {
            SceneManager.LoadScene("Endless");
            PlayerPrefs.SetInt("LIFE", 3);
        } else
        {
            SceneManager.LoadScene("Endless");
        }
    }
    public void GoToChallenge1(bool gameOver)
    {
        if (gameOver)
        {
            SceneManager.LoadScene("Challenge_1");
            PlayerPrefs.SetInt("LIFE", 3);
        }
        else
        {
            SceneManager.LoadScene("Challenge_1");
        };
    } 
    public void GoToChallenge2(bool gameOver)
    {
        if (gameOver)
        {
            SceneManager.LoadScene("Challenge_2");
            PlayerPrefs.SetInt("LIFE", 3);
        }
        else
        {
            SceneManager.LoadScene("Challenge_2");
        }
    }
    public void GoToChallenge3(bool gameOver)
    {
        if (gameOver)
        {
            SceneManager.LoadScene("Challenge_3");
            PlayerPrefs.SetInt("LIFE", 3);
        }
        else
        {
            SceneManager.LoadScene("Challenge_3");
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
