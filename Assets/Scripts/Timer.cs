using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    
    public Text timeText;
    public GameManager gm;
    public Ball ball;
    private float timeValue;

    private void Start()
    {
        timeValue = gm.timeChallenge - 1;
    }
    
    private void Update()
    {
        if(timeValue > 0 )
        {
            if (ball.inPlay)
            {
                timeValue -= Time.deltaTime;
            }
        }
        else
        {
            timeValue = 0;
        }

        DisplayTime(timeValue);
    }

    void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay <= 0)
        {
            timeToDisplay = 0;
            gm.GameOver();

        } else
        {
            timeToDisplay += 1;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds); //{0:00} , the first 2 digits select the type, the second 2 digits is the display
    }
}
