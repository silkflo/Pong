using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    
    public Rigidbody2D rb;
    public bool inPlay;
    public Transform paddle;
    public float speed;
    public Transform explosion;
    public Transform powerUp;
    private GameObject powerUpTag;
    AudioSource audio;
    public GameManager gm;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
     
       
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
            rb.AddForce(Vector2.up * speed);
        }


    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        //ball lost
        if (collision.CompareTag("bottom"))
        {
            rb.velocity = Vector2.zero;
            inPlay = false;
            powerUpTag = GameObject.FindGameObjectWithTag("extraLife");
            Destroy(powerUpTag);
            gm.UpdateLives(-1);
           
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

                //Explosion Effect
                Transform newExplosion = Instantiate(explosion, collision.transform.position, collision.transform.rotation);
                Destroy(newExplosion.gameObject, 2.5f);
                //Score
                gm.UpdateScore(brick.points);
                //Remove bricks
                gm.UpdateNumberOfBricks();
                Destroy(collision.gameObject);
            }

            audio.Play();
        }
    }


}
