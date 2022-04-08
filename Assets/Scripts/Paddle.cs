using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public GameManager gm;                                   //Use Game Manager

    private Rigidbody2D rb;                                  //Import the paddle body to the script
    private readonly float rightScreenEdge = 1.49f;          //Right Edge of the screen 
    private readonly float leftScreenEdge = -1.49f;          //Left Edge of the screen
    private float deltaX;                                    //Distance between the touch and the paddle
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (gm.gameOver) {
            return;     //The paddle couldn't move on game over
        }

        //On touch
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);    //Get the touch
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);  //Anywhere from the screen

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    deltaX = touchPos.x - transform.position.x;     //Get the distance between the object and the touch
                    break;
                case TouchPhase.Moved:
                    rb.MovePosition(new Vector2(touchPos.x - deltaX, transform.position.y));    //Object follow the touch on X (because it always keep the same distance "DeltaX" between your touch and the object)
                    break;
                case TouchPhase.Ended:
                    rb.velocity = Vector2.zero;     //Object not moving
                    break;
            }
        }

        //Not allowing the paddle to cross through the edge by reseting him position
        if (transform.position.x < leftScreenEdge)
        {
            transform.position = new Vector2(leftScreenEdge, transform.position.y);
        }
        if (transform.position.x > rightScreenEdge)
        {
            transform.position = new Vector2(rightScreenEdge, transform.position.y);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Power up reception
        if (collision.CompareTag("extraLife"))
        {
            gm.LivesManagement(1);              //Add a new life
            Destroy(collision.gameObject);
        }
    }

}
