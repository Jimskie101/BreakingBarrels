using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace RGVA
{
    public class CustomAnimator : MonoBehaviour
    {
        [BoxGroup("Settings"), PropertySpace(0, 5)] public bool PlayOnEnable;
        [BoxGroup("Settings"), FoldoutGroup("Settings/Loop")] public int LoopCount = 0;
        [BoxGroup("Settings"), FoldoutGroup("Settings/Loop"), ShowIf(nameof(LoopCount), -1)] public LoopType LoopType;
        [Space]
        [BoxGroup("Settings")] public bool IsList = false;
        [Space]
        [BoxGroup("Settings"), HideIf(nameof(IsList))] public CustomAnimation CustomAnimation = new CustomAnimation();
        [Space]
        [BoxGroup("Settings"), ShowIf(nameof(IsList)), OnValueChanged(nameof(OnListValueChanged), InvokeOnInitialize = true)] public List<CustomAnimation> CustomAnimations = new List<CustomAnimation>();
        [Space]
        [BoxGroup("Settings"), FoldoutGroup("Settings/Events")] public UnityEvent OnStart;
        [BoxGroup("Settings"), FoldoutGroup("Settings/Events")] public UnityEvent OnComplete;

        Sequence m_Sequence;
        CustomAnimation m_DummyCustomAnimation;
        [Button]
        private void EditorPlay()
        {
            if (IsList)
            {
                if (CustomAnimations.Count > 0)
                    CustomAnimations[0].Play(transform);
                else
                    Debug.LogWarning("Empty Custom Animations");
            }
            else
                CustomAnimation.Play(transform);
        }

        private void OnListValueChanged()
        {
            int previousIndex = 0;
            CustomAnimation.eAnimationType previousAnimationType = CustomAnimation.eAnimationType.Move;
            for (int i = 0; i < CustomAnimations.Count; i++)
            {
                if (CustomAnimations[i] != null)
                {
                    CustomAnimations[i].Index = i;

                    CustomAnimations[i].OnValueChanged.RemoveAllListeners();
                    CustomAnimations[i].OnValueChanged.AddListener(OnListValueChanged);

                    if (i == 0)
                    {
                        CustomAnimations[i].PlayWithPreviousAnimation = false;
                        CustomAnimations[i].IndexToAnimate = previousIndex;
                        previousAnimationType = CustomAnimations[i].AnimationType;
                    }
                    else
                    {
                        CustomAnimations[i].PreviousAnimationType = previousAnimationType;
                        if (CustomAnimations[i].PlayWithPreviousAnimation && previousAnimationType != CustomAnimations[i].AnimationType)
                        {
                            CustomAnimations[i].IndexToAnimate = previousIndex;
                        }
                        else
                        {
                            previousIndex++;
                            CustomAnimations[i].PlayWithPreviousAnimation = false;
                            CustomAnimations[i].IndexToAnimate = previousIndex;
                        }
                        previousAnimationType = CustomAnimations[i].AnimationType;
                    }
                }


            }
        }

        private void OnEnable()
        {
            OnListValueChanged();
            if (PlayOnEnable)
                Play();
        }

        private void OnAnimationComplete(Transform i_Target)
        {
            if (LoopCount == 0)
            {
                OnComplete?.Invoke();
                Debug.Log("Animation Completed");
            }

        }

        public void Play()
        {
            m_Sequence = DOTween.Sequence();

            int LastPreviousIndex = 0;

            if (IsList)
            {
                if (CustomAnimations.Count > 0)
                {
                    for (int i = 0; i < CustomAnimations.Count; i++)
                    {
                        switch (CustomAnimations[i].Axis)
                        {
                            case CustomAnimation.eAxis.All:
                            case CustomAnimation.eAxis.X:
                            case CustomAnimation.eAxis.Y:
                            case CustomAnimation.eAxis.Z:
                                if (CustomAnimations[i].PlayWithPreviousAnimation)
                                {
                                    m_Sequence.Join(CustomAnimations[i].Play(transform));
                                }
                                else
                                {
                                    m_Sequence.Append(CustomAnimations[i].Play(transform));
                                    LastPreviousIndex = i;
                                }
                                break;
                            case CustomAnimation.eAxis.Forward:
                            case CustomAnimation.eAxis.Back:
                            case CustomAnimation.eAxis.Left:
                            case CustomAnimation.eAxis.Right:
                                m_DummyCustomAnimation = CustomAnimations[i];
                                float value = 1f;
                                if (CustomAnimations[i].PlayWithPreviousAnimation)
                                {
                                    m_Sequence.Join(DOTween.To(() => value, x => value = x, 0, CustomAnimations[i].Duration).OnStart(() =>
                                    {
                                        m_DummyCustomAnimation.Play(transform);
                                    }));
                                }
                                else
                                {
                                    m_Sequence.Append(DOTween.To(() => value, x => value = x, 0, CustomAnimations[i].Duration).OnStart(() =>
                                    {
                                        m_DummyCustomAnimation.Play(transform);
                                    }));
                                }
                                break;
                        }
                    }
                    m_Sequence.SetLoops(LoopCount, LoopType);
                }
                else
                {
                #if UNITY_EDITOR
                    Debug.LogWarning("Empty Custom Animations");
                    //return null;
                #endif
                }
            }
            else
            {
                CustomAnimation.PlayOnEnable = PlayOnEnable;
                CustomAnimation.LoopCount = LoopCount;
                CustomAnimation.LoopType = LoopType;
                m_Sequence.Append(CustomAnimation.Play(transform));

            }
            m_Sequence.OnComplete(() =>
            {
                OnAnimationComplete(transform);
            });
            m_Sequence.OnStart(() =>
            {
                OnStart?.Invoke();
            });
            //return sequence;
        }
    }
}

