using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        //The power up is falling down
        transform.Translate(new Vector2(0f, -1f) * Time.deltaTime * speed);

        //The powerup up is lost
        if(transform.position.y < -5f)
        {
            Destroy(gameObject);
        }
    }

    
}
