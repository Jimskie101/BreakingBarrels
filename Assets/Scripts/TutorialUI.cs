using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
using RGVA;

public class TutorialUI : Singleton<TutorialUI>
{
    [SerializeField] 
    RectTransform hand;
    [SerializeField] 
    Text tipText;
    [SerializeField]
    GameObject ttc, tips;

    [SerializeField] Shooting shooting;

    bool once = false;
    
    //Animate the hand for shooting
    private void Animate()
    {
        Sequence handMove = DOTween.Sequence();
        //seq.Append(hand.transform.DOLocalMoveX(-82f, 1f));
        handMove.Append(hand.transform.DOScale(0.75f, 0.3f));
        handMove.Append(hand.transform.DOScale(1f, 0.3f));
        //seq.Append(hand.transform.DOLocalMoveX(1000f, 1f));
        //seq.Append(hand.transform.DOLocalMoveX(-800f, 0f));
        handMove.SetDelay(1f);
        handMove.OnComplete(() => handMove.Restart());
        InputManager.OnInputDown += Second;
        

    }

    //Animate the hand to point at the cooldown icon
    private void Second(Vector2 i_points)
    {
        
        DOTween.KillAll();
        InputManager.OnInputDown -= Second;
        tipText.fontSize = 60;
        ttc.SetActive(true);
        tipText.text = "Wait for the bullet to fill \nto shoot another bullet";

        //Sequence tipsSeq = DOTween.Sequence();
        //tipsSeq.Append(tips.transform.DOLocalMoveY(-100f - 90f, 0.5f));
        //tipsSeq.Append(tips.transform.DOLocalMoveY(-90f, 0.3f));

        
        Sequence seq = DOTween.Sequence();
        seq.Append(hand.transform.DOLocalMove(new Vector3(14f, 590f, 0), 0.5f));
        //seq.OnComplete(() => )


        Sequence seq2 = DOTween.Sequence();
        seq2.Append(hand.transform.DOLocalMoveY(580, 0.3f));
        seq2.Append(hand.transform.DOLocalMoveY(590, 0.3f));
        seq2.SetDelay(1f);
        seq2.OnComplete(() => seq2.Restart());
        
        StartCoroutine(CoSecond());

        
    }

    //Wait 2 seconds to initiate tap to next/continue tutorial
    IEnumerator CoSecond() { yield return new WaitForSeconds(2f); InputManager.OnInputDown += Third; shooting.enabled = false; }

    
    //Activate the dragging animation for hand
    private void Third(Vector2 i_points)
    {
        DOTween.KillAll();
        InputManager.OnInputDown -= Third;
        Sequence seq = DOTween.Sequence();
        seq.Append(hand.transform.DOLocalMove(new Vector3(-100, 0, 0), 0.5f));
        //seq.OnComplete(() => DOTween.KillAll());

        //Sequence tipsSeq = DOTween.Sequence();
        //tipsSeq.Append(tips.transform.DOLocalMoveY(667f + 100f - 90f, 0.5f));
        //tipsSeq.Append(tips.transform.DOLocalMoveY(667f, 0.3f));

        Sequence seq2 = DOTween.Sequence();
        seq2.Append(hand.transform.DOLocalMove(new Vector3(-100, 0, 0), 0.5f));
        seq2.Append(hand.transform.DOLocalMove(new Vector3(0, 100, 0), 0.5f));
        seq2.Append(hand.transform.DOLocalMove(new Vector3(100, 0, 0), 0.5f));
        seq2.Append(hand.transform.DOLocalMove(new Vector3(0, -100, 0), 0.5f));
        seq2.Append(hand.transform.DOLocalMove(new Vector3(-100, 0, 0), 0.5f));
        seq2.SetDelay(1f);
        ttc.SetActive(false);
        tipText.text = "Drag to aim\nwhile shooting";
        seq2.OnComplete(() => seq2.Restart());
        StartCoroutine(CoThird());
        shooting.enabled = true;
    }

    //Wait 2 seconds to initiate drag to next/continue tutorial
    IEnumerator CoThird() { yield return new WaitForSeconds(2f); InputManager.OnDrag += ThirdPointOne; shooting.enabled = false; }


    //Hand point to clock/timer
    private void ThirdPointOne(Vector2 i_points)
    {
        DOTween.KillAll();
        InputManager.OnDrag -= ThirdPointOne;
        Sequence seq = DOTween.Sequence();
        seq.Append(hand.transform.DOLocalMove(new Vector3(355, 600, 0), 0.5f));



        Sequence seq2 = DOTween.Sequence();
        seq2.Append(hand.transform.DOLocalMoveY(590, 0.3f));
        seq2.Append(hand.transform.DOLocalMoveY(600, 0.3f));
        seq2.SetDelay(1f);
        seq2.OnComplete(() => seq2.Restart());
        ttc.SetActive(true);
        tipText.text = "Don't let the time run out";
        seq2.OnComplete(() => seq2.Restart());
        StartCoroutine(CoThirdPointOne());
        shooting.enabled = true;
    }

    //Wait 2 seconds to initiate drag to next/continue tutorial
    IEnumerator CoThirdPointOne() { yield return new WaitForSeconds(2f); InputManager.OnInputDown += Fourth; shooting.enabled = false; }



    //Fades out the hand and change the Tutorial Text
    private void Fourth(Vector2 i_points)
    {
        InputManager.OnInputDown -= Fourth;
        hand.GetComponent<Image>().DOFade(0, 1f);
        tipText.text = "Getting two different tiles\n returns them to being boxed again";
        ttc.SetActive(true);
        StartCoroutine(CoFourth());
    }

    //Wait 2 seconds to initiate tap to next/continue tutorial
    IEnumerator CoFourth() { yield return new WaitForSeconds(2f); InputManager.OnInputDown += Fifth; }


    //Activate the last Tutorial Text
    private void Fifth(Vector2 i_points)
    {
        InputManager.OnInputDown -= Fifth;
        tipText.text = "Match all tiles to win the game\nHappy Shooting!";
        StartCoroutine(CoFifth());
    }

    //Wait 2 seconds to initiate tap to next/continue tutorial
    IEnumerator CoFifth() { yield return new WaitForSeconds(2f); InputManager.OnInputDown += Last; }

    //Slides out and disable the Tutorial UI
    private void Last(Vector2 i_points)
    {
        
        InputManager.OnInputDown -= Last;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMoveY(-100f, 0.3f));
        seq.Append(transform.DOLocalMoveY(2000f, 0.5f));
        seq.OnComplete(() => DOTween.KillAll());
        StartCoroutine(CoLast());

    }
    //Enables the shooting mechanics and timer after tutorlal
    IEnumerator CoLast() { yield return new WaitForSeconds(1f); shooting.enabled = true; GameEvents.Instance.timerOn = true; }


    


    private void OnEnable()
    {
        shooting.enabled = true;

        StartCoroutine(DelayTimeTutorial());
        if(!once)
        {
            once = true;
            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOLocalMoveY(-100f, 0.5f));
            seq.Append(transform.DOLocalMoveY(0f, 0.3f));

            seq.OnComplete(() => Animate());


        }
        

        

    }

    //Stop the timer on tutorial
    IEnumerator DelayTimeTutorial()
    {
        yield return new WaitForSeconds(0f);
        GameEvents.Instance.timerOn = false;
    }
}
