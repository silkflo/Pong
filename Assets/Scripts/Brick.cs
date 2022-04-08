using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int points;                      //Brick point value
    public int hitsToBreak;                 //Hit amount requirement to destroy the brick
    public Sprite brickCracked;             //Brick cracked image
    public List<Transform> explosion;       //list of Explosion position

    private string brickColor;              //brick color 
    public void Start()
    {
        //Brick color detection and rename
        string brickSpriteName = brickCracked.name;
        switch (brickSpriteName)
        {
            case "Yellow Brick - Crack":
                brickColor = "yellow";
                break;
            case "Blue Brick - Crack":
                brickColor = "blue";
                break;
            default:
                brickColor = "yellow";
                break;
        }
    }
    public void BreakBrick()
    {
        //Update the brick solidity
        hitsToBreak--;
        //Render the cracked brick
        GetComponent<SpriteRenderer>().sprite = brickCracked;
    }

    //Instantiate the correct explosion effect according the brick color
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
