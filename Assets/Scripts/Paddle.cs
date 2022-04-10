using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public GameManager gm;                                   //Use Game Manager

    private Rigidbody2D rb;                                  //Import the paddle body to the script
    private readonly float rightScreenEdge = 1.49f;          //Right Edge of the screen 
    private readonly float leftScreenEdge = -1.49f;          //Left Edge of the screen
  
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (gm.gameOver) {
            return;     //The paddle couldn't move on game over
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
