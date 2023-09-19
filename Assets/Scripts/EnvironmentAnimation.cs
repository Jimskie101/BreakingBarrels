using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RGVA;
using DG.Tweening;
using Sirenix.OdinInspector;

public class EnvironmentAnimation : MonoBehaviour
{
    [System.Serializable]
    public class AnimatedObject
    {
        public GameObject targetObject;
        public float duration;
        public Vector3 endPos;
        public Vector3 endRot;
    }

    [SerializeField] AnimatedObject[] m_animatedObjects;

    private void Start()
    {
        if(m_animatedObjects.Length != 0)
        foreach (AnimatedObject a in m_animatedObjects)
        {
            AnimationSequence(a);
        }
    }
    private void AnimationSequence(AnimatedObject animatedObject)
    {
        // animatedObject.targetObject.transform.DOLocalMove(animatedObject.endPos, animatedObject.duration).
        // SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        animatedObject.targetObject.transform.DORotate(animatedObject.endRot, animatedObject.duration).
        SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

}
