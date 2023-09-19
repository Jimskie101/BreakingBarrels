using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RGVA;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class GameEvents : Singleton<GameEvents>
{
    [SerializeField] Timer timeScript;

    public Image cooldownCircle;

    [SerializeField] float cooldownSpeed = 10;

    public float timeGiven = 10;



    [HideInInspector]
    bool lost = false;
    //[HideInInspector]
    //bool won = false;

    public int matchedPair = 0;


    public bool timerOn = true;

    private void OnEnable()
    {
        if (Mode.inCasualMode) timerOn = false;
    }

    [Button]
    private void InitializeVariables()
    {
        timeScript = GameObject.Find("Time").GetComponent<Timer>();

    }


    private void Update()
    {
        CooldownRegen();
        Timer.Instance.timerOn = timerOn;
        if (timeScript != null)
        {
            if (timeScript.presentTime < 0 && !lost)
            {
                lost = true;
                //Time.timeScale = 0f;
                UIManager.Instance.Lose();

            }
        }




    }


    private void CooldownRegen()
    {
        if (cooldownCircle != null)
        {
            if (cooldownCircle.fillAmount < 1)
            {
                cooldownCircle.fillAmount += (1f * cooldownSpeed) * Time.deltaTime;

            }
        }
    }




}
