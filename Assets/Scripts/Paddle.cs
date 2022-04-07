using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    public float speed;
    public float rightScreenEdge;
    public float leftScreenEdge;
    public GameManager gm;
    private Vector3 direction;
    private Vector3 touchPosition;
    private Rigidbody2D rb;
    private float deltax, deltay;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (gm.gameOver) {
            return;
        }


        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    deltax = touchPos.x - transform.position.x;
                    break;
                case TouchPhase.Moved:
                    rb.MovePosition(new Vector2(touchPos.x - deltax, transform.position.y));
                    break;
                case TouchPhase.Ended:
                    rb.velocity = Vector2.zero;
                    break;
            }
        }


        float horizontal = Input.GetAxis("Horizontal");
      
        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed);
        if(transform.position.x < leftScreenEdge)
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
        if (collision.CompareTag("extraLife"))
        {
            gm.UpdateLives(1);
            Destroy(collision.gameObject);

        }
    }

}
