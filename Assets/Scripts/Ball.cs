using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    
    public Rigidbody2D rigidBody;
    public bool inPlay;
    public Transform paddle;
    public float speed;
    public int speedAcceleration = 50;
    public bool acceleration ;
    public Transform powerUp;
    public GameObject ChallengeLevelPanel;
    public GameManager gm;
    AudioSource AudioSource;
    private GameObject powerUpTag;
    //private float timer = 0;
    private int paddleHitCount = 0;
    Vector2 accelerate;

    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        AudioSource = GetComponent<AudioSource>();
          
    }

    void Update()
    {
        //Ball Freeze on Game Over
        if (gm.gameOver) {
            //Don't execute Ball script
            //gameObject.SetActive(false);
            transform.position = paddle.position;
            return;
        }

        //Init ball position on game start
        if (!inPlay)
        {
          transform.position = paddle.position;
        }

        //launch the ball on game start
        if (Input.GetButtonDown("Jump") && !inPlay)
        {
            inPlay = true;
            rigidBody.AddForce(Vector2.up * speed);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //ball lost
        if (collision.CompareTag("bottom"))
        {
            rigidBody.velocity = Vector2.zero;
            inPlay = false;
            powerUpTag = GameObject.FindGameObjectWithTag("extraLife");
            Destroy(powerUpTag);
            paddleHitCount = 0;
            //Lost Lives
            if (!ChallengeLevelPanel || !ChallengeLevelPanel.activeSelf)
            {
                gm.UpdateLives(-1);
                int challengeLife = PlayerPrefs.GetInt("LIFE");
                PlayerPrefs.SetInt("LIFE", challengeLife--);

            }
            
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ball collision with brick
        if (collision.transform.CompareTag("brick"))
        {
            Brick brick = collision.gameObject.GetComponent<Brick>();
            //brick break
            if (brick.hitsToBreak > 1)
            {
                brick.BreakBrick();
            }
            else
            {
                //set powerup
                int randChance = Random.Range(1, 101);
                if (randChance < 50)
                {
                    Instantiate(powerUp, collision.transform.position, collision.transform.rotation);
                }

                gm.UpdateScore(brick.points);
                //Remove bricks
                gm.UpdateNumberOfBricks();
                Destroy(collision.gameObject);
            }

            AudioSource.Play();
        }

        if (collision.transform.CompareTag("paddle"))
        {
            if (acceleration && inPlay)
            {
                paddleHitCount++;

                if (paddleHitCount % 2 == 0)
                {
                    Debug.Log("Speed!!");
                    rigidBody.AddForce(Vector2.up * speedAcceleration);
                }


            }

            Debug.Log(paddleHitCount);

        }
    }


}
