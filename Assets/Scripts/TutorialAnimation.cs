using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class TutorialAnimation : MonoBehaviour
{
    public RectTransform hand, tips;

    [Button]
    private void Animate()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(hand.transform.DOMoveX(450f, 1f));
        seq.Append(hand.transform.DOScale(0.75f, 0.3f));
        seq.Append(hand.transform.DOScale(1f, 0.3f));
        seq.Append(hand.transform.DOMoveX(1500f, 1f));
        seq.Append(hand.transform.DOMoveX(-600f, 0f));
        seq.OnComplete(() =>  Animate());
    }


    private void OnEnable()
    {
        GameEvents.Instance.timerOn = false;

    }


    

}
