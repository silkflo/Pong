using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public Text timeText;           //Time display
    public GameManager gm;          //Use Game Manager
    public Ball ball;               //Use ball script
    private float timeValue;        //Time value

    private void Start()
    {
        timeValue = gm.timeChallenge - 1;  //Set the time value on start
    }
    
    private void Update()
    {
        if(timeValue > 0 )
        {
            if (ball.inPlay && gm.numberOfBricks > 0)
            {
                timeValue -= Time.deltaTime;    //Reduce the time by 1 each second, as long the game is in play and time not out
            }
        }
        else
        {
            timeValue = 0;  //Time below 0 set to 0
        }

        TimeManagement(timeValue);  //Display the time or set to game over on time out
    }

    void TimeManagement(float timeToDisplay)
    {
        //Time out 
        if(timeToDisplay <= 0)
        {
            timeToDisplay = 0;  //Time display is 0
            gm.GameOver();      //Display game over panel, disable ball and paddle

        } else
        {
            timeToDisplay += 1;     //Time to display
        }
        //Convert a decimal number to a time
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds); //{0:00} , the first 2 digits are the type, the second 2 digits are the display
    }
}
