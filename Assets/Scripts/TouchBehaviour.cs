using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;                                  //Import the paddle body to the script
    private float deltaX;                                    //Distance between the touch and the paddle
    public float maxDoubbleTapTime;
    public GameManager gm;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        if (gm.gameOver)
        {
           return;
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
           
    }
}
