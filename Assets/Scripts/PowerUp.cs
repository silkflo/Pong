using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        //fall of the powerup
        transform.Translate(new Vector2(0f, -1f) * Time.deltaTime * speed);

        //powerup lost
        if(transform.position.y < -5f)
        {
            Destroy(gameObject);
        }
    }

    
}
