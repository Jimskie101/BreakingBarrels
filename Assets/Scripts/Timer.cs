using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RGVA;
using Sirenix.OdinInspector;

public class Timer : Singleton<Timer>
{
    public float timeLeft = 100;
    public bool timerOn = false;

    public float presentTime = 100;

    public Text timerText;

    [Button]
    private void InitializeVariables()
    {
        timerText = GetComponentInChildren<Text>();
    }

    private void OnEnable()
    {
        timeLeft = GameEvents.Instance.timeGiven;
        timerOn = true;
    }

    private void Update()
    {
        if (timerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                presentTime = UpdateTimer(timeLeft);
            }
            else
            {
                Debug.Log("Time is up");
                timeLeft = 0;
                timerOn = false;
            }
        
        }
    }

    private float UpdateTimer(float currentTime)
    {


        timerText.text = string.Format("{0:00}", currentTime);
        return currentTime;
    }

}
