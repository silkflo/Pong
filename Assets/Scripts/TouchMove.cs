using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMove : MonoBehaviour
{
    private float deltax, deltay;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.touchCount > 0)
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

    }


}
