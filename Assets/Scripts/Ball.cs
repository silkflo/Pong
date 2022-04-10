using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    
    public bool inPlay;                             //Check if the game status in currently in play
    public float speed;                             //Ball speed
    public int speedAcceleration = 50;              //On challenge mode, force added to accelerate the ball
    public bool accelerationRule ;                  //If the game is in speed up mode, it need to be true
    public int powerUpRate;                         //Set the power up falling rate
    public Transform paddle;                        //Import paddle position to init the ball position
    public Transform powerUp;                       //Import power up position to spawn them
    public GameObject successPanel;                 //Import challenge level to allow lost ball without losing live when this panel is active
    public GameManager gm;                          //Use Game Manager
    AudioSource AudioSource;                        //Audio of the brick

    private Rigidbody2D rb;                         //Import the ball body to the script
    private GameObject[] powerUpTag;                //Retrieve the power up
    private int paddleHitCount = 0;                 //Count paddle hit for the ball acceleration
    
    void Start()
    {
        rb  = GetComponent<Rigidbody2D>();
        AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //On game over, return 
        if (gm.gameOver) {
            //set ball positon
             transform.position = paddle.position;
             return;
        }

        //Init ball position on the paddle before the game start
        if (!inPlay)
        {
            Time.timeScale = 1.0f;  //Debug the retry from the pause menu when double tap at that moment
            transform.position = paddle.position;
        }

        //Launch the ball on click to start the game
        if (Input.GetMouseButtonUp(0) && !inPlay)
        {
            inPlay = true;
            rb.AddForce(Vector2.up * speed);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Ball lost
        if (collision.CompareTag("bottom"))
        {
            //Stop the game
            rb.velocity = Vector2.zero;
            inPlay = false;
            powerUpTag = GameObject.FindGameObjectsWithTag("extraLife");
            for (int i = 0;i < powerUpTag.Length; i++)
            {
                Destroy(powerUpTag[i]);    //Destroy all the power up spawn if life is lost meanwhile
            }

            paddleHitCount = 0;     //Reset the paddle count to 0 to reset the ball speed for the speed up ball challenge
            
            //Lost Lives not in a level transition during challenge game mode
            if (!successPanel || !successPanel.activeSelf)
            {
                gm.LivesManagement(-1); //life lost
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Ball collision with brick
        if (collision.transform.CompareTag("brick"))
        {
            //Get brick gameObject
            Brick brick = collision.gameObject.GetComponent<Brick>();
            //Brick breaking
            if (brick.hitsToBreak > 1)
            {
               //Reduce the paddle hit counted for brick with multiple hit to destroy ; get the explosion component
                brick.BreakBrick();
            }
            else
            {
                //Set and spawn powerup
                int randChance = Random.Range(1, 101);  //Random number to set the power up rate
                if (randChance < powerUpRate)
                {
                    Instantiate(powerUp, collision.transform.position, collision.transform.rotation);
                }
                //Update the score
                gm.UpdateScore(brick.points);
                //Update the number of bricks and manage the end of the level if no brick left
                gm.BrickManagement();
                //Destroy the brick
                Destroy(collision.gameObject);
            }
            //Brick breaking sound
            AudioSource.Play();
        }

        //Use for the acceleration in the speed up challenge 
        if (collision.transform.CompareTag("paddle"))
        {
            if (accelerationRule && inPlay)
            {
                paddleHitCount++;
               //Every 2 paddle hit is adding force to the ball
               if (paddleHitCount % 2 == 0)
               {
                    rb.AddForce(Vector2.up * (speedAcceleration));
               }
            }
        }
    }


}
