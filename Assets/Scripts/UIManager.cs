using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RGVA;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public RectTransform winScreen, loseScreen,
        clock, timeRect, pauseScreen, transitionPanel, tutorial;

    RawImage bgImg;
    RawImage waveImg;


    bool isPaused = false;


    private void OnEnable()
    {
        GameEvents.Instance.timerOn = false;
        waveImg = transitionPanel.GetComponent<RawImage>();
        StartCoroutine(TransitionNewGame());
    }

    //Animates the loading screen
    IEnumerator TransitionNewGame()
    {
        yield return new WaitForSeconds(1.5f);
        Sequence seq = SlideTween(transitionPanel, 0, 860f, 0.5f, 0, 2880f, 0.2f);
        seq.OnComplete(() => AnimateClock());

    }

    //Disables the loading screen and starts the clock
    public void AnimateClock()
    {
        transitionPanel.gameObject.SetActive(false);
        if (tutorial != null)
        {
            tutorial.gameObject.SetActive(true);
        }
        if (!Mode.inCasualMode)
        {

            ClockAnimation();
            GameEvents.Instance.timerOn = true;

        }

    }


    //Opens and Closes the Pause UI
    public void PauseScreen()
    {
        if (!isPaused)
        {
            if (!Mode.inCasualMode) GameEvents.Instance.timerOn = false;
            pauseScreen.gameObject.SetActive(true);
            Sequence seq = SlideTween(pauseScreen, 0, -100f + (1920f / 2f), 0.5f, 0, (1920f / 2f), 0.2f);
            bgImg = pauseScreen.GetComponentInChildren<RawImage>();
            isPaused = true;
        }
        else if (isPaused)
        {
            if (!Mode.inCasualMode) GameEvents.Instance.timerOn = true;
            Sequence seq = SlideTween(pauseScreen, 0, -100f + (1920f / 2f), 0.2f, 0, 3840f, 0.5f);
            seq.OnComplete(() => pauseScreen.gameObject.SetActive(false));
            isPaused = false;


        }


    }



    //Slide in the clock and timer and add a pulsing effect to the clock icon
    private void ClockAnimation()
    {

        SlideTween(timeRect, 245f, 875f, 0.5f, 410f, 875f, 0.5f);

        Sequence clockAnim = DOTween.Sequence();
        clockAnim.Append(clock.DOScale(1.10f, 0.7f));
        clockAnim.SetLoops(-1, LoopType.Yoyo);

    }

    //Enable and Animate the Winning UI
    public void Win()
    {
        AudioManager.Instance.Play("Win");
        AudioManager.Instance.Stop("BGM");
        winScreen.gameObject.SetActive(true);
        Sequence seq = SlideTween(winScreen, 0, -100f + (1920f / 2f), 0.5f, 0, (1920f / 2f), 0.2f);
        bgImg = winScreen.GetComponentInChildren<RawImage>();


    }


    //Enable and Animate the Winning UI
    public void Lose()
    {
        AudioManager.Instance.Play("Lose");
        AudioManager.Instance.Stop("BGM");
        Timer.Instance.timerOn = false;
        loseScreen.gameObject.SetActive(true);
        Sequence seq = SlideTween(loseScreen, 0, -100f + (1920f / 2f), 0.5f, 0, (1920f / 2f), 0.2f);
        bgImg = loseScreen.GetComponentInChildren<RawImage>();
    }


    //Method for sliding UI objects to screen
    
    private Sequence SlideTween(RectTransform uiObject, float x1, float y1, float duration1, float x2, float y2, float duration2)
    {
        Sequence tween = DOTween.Sequence();
        tween.Append(uiObject.DOAnchorPos(new Vector2(x1, y1), duration1));
        tween.Append(uiObject.DOAnchorPos(new Vector2(x2, y2), duration2));
        return tween;
    }



    private void Update()
    {
        //Animates the scrolling background
        if (bgImg && bgImg.transform.parent.gameObject.activeSelf)
        {
            bgImg.uvRect = new Rect(bgImg.uvRect.position + new Vector2(0.5f,0 ) * Time.deltaTime, bgImg.uvRect.size);
        }

        if (transitionPanel.gameObject.activeSelf)
        {
            waveImg.uvRect = new Rect(waveImg.uvRect.position + new Vector2(0.5f,0 ) * Time.deltaTime, waveImg.uvRect.size);
        }
    }

}
