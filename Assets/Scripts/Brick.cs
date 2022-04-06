using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int points;
    public int hitsToBreak;
    public Sprite hitSprite;
    public List<Transform> explosion;

    private string brickColor;
    public void Start()
    {
        switch (hitsToBreak)
        {
            case 1:
                brickColor = "yellow";
                break;
            case 2:
                brickColor = "blue";
                break;

        }       
    }
    public void BreakBrick()
    {
        hitsToBreak--;
        GetComponent<SpriteRenderer>().sprite = hitSprite;

    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
      
        if (brickColor == "yellow")
        {
            Transform newExplosion = Instantiate(explosion[0], collision.transform.position, collision.transform.rotation);
            Destroy(newExplosion.gameObject, 2.5f);
        } 
        
        if (brickColor == "blue" && hitsToBreak == 1)
        {
          Transform newExplosion = Instantiate(explosion[1], collision.transform.position, collision.transform.rotation);
          Destroy(newExplosion.gameObject, 2.5f);
        }
    }
}
