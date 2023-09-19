using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace RGVA
{
    [System.Serializable]
    public class CustomAnimation
    {
        public delegate void AnimationEvent(Transform i_Target);
        public event AnimationEvent OnComplete;
        public event AnimationEvent OnStart;

        [HideInInspector] public UnityEvent OnValueChanged = new UnityEvent();

        [FoldoutGroup("$GetBoxGroupName"), OnValueChanged(nameof(OnChangeAnimationType)), OnValueChanged(nameof(OnValueChangedPlayWithPreviousAnimation))] public eAnimationType AnimationType;
        [FoldoutGroup("$GetBoxGroupName"), HideIf("$DisablePreviousAnimationToggler"), OnValueChanged(nameof(OnValueChangedPlayWithPreviousAnimation))] public bool PlayWithPreviousAnimation;
        [FoldoutGroup("$GetBoxGroupName")] public float DelayOnStart = 0f;
        [FoldoutGroup("$GetBoxGroupName")] public float Duration = 1f;
        [FoldoutGroup("$GetBoxGroupName")] public Ease Ease = Ease.Linear;
        [FoldoutGroup("$GetBoxGroupName")] public bool IsRelative;
        [FoldoutGroup("$GetBoxGroupName"), ShowIf("$IsLocalEnabled")] public bool IsLocal = false;
        [FoldoutGroup("$GetBoxGroupName")] public eAxis Axis;
        [FoldoutGroup("$GetBoxGroupName"), ShowIf("$IsAxisValueEnabled")] public float Value;
        [FoldoutGroup("$GetBoxGroupName"), ShowIf("$IsAxisVectorValueEnabled")] public Vector3 VectorValue;

        [HideInInspector] public bool PlayOnEnable;
        [HideInInspector] public int Index = 0;
        [HideInInspector] public int LoopCount = 0;
        [HideInInspector] public int IndexToAnimate = 0;
        [HideInInspector] public eAnimationType PreviousAnimationType;
        [HideInInspector] public LoopType LoopType;

        private Transform m_Target;
        private bool DisablePreviousAnimationToggler => (Index == 0) || (Index != 0 && PreviousAnimationType == AnimationType);
        private bool IsLocalEnabled => !(Axis == eAxis.Forward || Axis == eAxis.Back || Axis == eAxis.Left || Axis == eAxis.Right) && AnimationType != eAnimationType.Scale;
        private bool IsAxisValueEnabled => Axis != eAxis.All;
        private bool IsAxisVectorValueEnabled => Axis == eAxis.All;
        private string GetBoxGroupName => "(" + IndexToAnimate + ") " + AnimationType.ToString() + (Axis != eAxis.All ? (" " + Axis.ToString()) : "") + ": " + (Axis != eAxis.All ? (" " + Value) : (" Vector3(" + VectorValue.x + ", " + VectorValue.y + ", " + VectorValue.z + ")"));

        private Vector3 TargetDirection;
        private void OnChangeAnimationType()
        {
            PlayWithPreviousAnimation = false;
            VectorValue = AnimationType == eAnimationType.Scale ? Vector3.one : Vector3.zero;
        }

        private void OnValueChangedPlayWithPreviousAnimation()
        {
            OnValueChanged?.Invoke();
        }

        private void OnStarted()
        {
            Debug.Log("On Started :" + GetBoxGroupName);
            OnStart?.Invoke(m_Target);
        }

        private void OnCompleted()
        {
            Debug.Log("On Completed :" + GetBoxGroupName);
            OnComplete?.Invoke(m_Target);
        }

        private Sequence Move(Transform i_Target)
        {
            Sequence sequence = DOTween.Sequence();

            switch (Axis)
            {
                case eAxis.All:
                case eAxis.X:
                case eAxis.Y:
                case eAxis.Z:
                    switch (Axis)
                    {
                        case eAxis.All:
                            if (IsLocal)
                                sequence.Append(i_Target.DOLocalMove(VectorValue, Duration));
                            else
                                sequence.Append(i_Target.DOMove(VectorValue, Duration));
                            break;
                        case eAxis.X:
                            if (IsLocal)
                                sequence.Append(i_Target.DOLocalMoveX(Value, Duration));
                            else
                                sequence.Append(i_Target.DOMoveX(Value, Duration));
                            break;
                        case eAxis.Y:
                            if (IsLocal)
                                sequence.Append(i_Target.DOLocalMoveY(Value, Duration));
                            else
                                sequence.Append(i_Target.DOMoveY(Value, Duration));
                            break;
                        case eAxis.Z:
                            if (IsLocal)
                                sequence.Append(i_Target.DOLocalMoveZ(Value, Duration));
                            else
                                sequence.Append(i_Target.DOMoveZ(Value, Duration));
                            break;
                    }
                    break;
                case eAxis.Forward:
                case eAxis.Back:
                case eAxis.Left:
                case eAxis.Right:
                    switch (Axis)
                    {
                        case eAxis.Forward:
                            sequence.Append(i_Target.DOMove(i_Target.forward * Value, Duration));
                            break;
                        case eAxis.Back:
                            sequence.Append(i_Target.DOMove(-i_Target.forward * Value, Duration));
                            break;
                        case eAxis.Left:
                            sequence.Append(i_Target.DOMove(-i_Target.right * Value, Duration));
                            break;
                        case eAxis.Right:
                            sequence.Append(i_Target.DOMove(i_Target.right * Value, Duration));
                            break;
                    }
                    break;
            }
            sequence.SetEase(Ease).SetRelative(IsRelative).SetLoops(LoopCount, LoopType).OnComplete(OnCompleted).SetDelay(DelayOnStart);
            return sequence;
        }

        private Sequence Rotate(Transform i_Target)
        {
            Sequence sequence = DOTween.Sequence();
            switch (Axis)
            {
                case eAxis.All:
                case eAxis.X:
                case eAxis.Y:
                case eAxis.Z:
                    switch (Axis)
                    {
                        case eAxis.All:
                            if (IsLocal)
                                sequence.Append(i_Target.DOLocalRotate(VectorValue, Duration, RotateMode.LocalAxisAdd));
                            else
                                sequence.Append(i_Target.DORotate(VectorValue, Duration, RotateMode.LocalAxisAdd));
                            break;
                        case eAxis.X:
                            if (IsLocal)
                                sequence.Append(i_Target.DOLocalRotate(Vector3.right * Value, Duration, RotateMode.LocalAxisAdd));
                            else
                                sequence.Append(i_Target.DORotate(Vector3.right * Value, Duration, RotateMode.LocalAxisAdd));
                            break;
                        case eAxis.Y:
                            if (IsLocal)
                                sequence.Append(i_Target.DOLocalRotate(Vector3.up * Value, Duration, RotateMode.LocalAxisAdd));
                            else
                                sequence.Append(i_Target.DORotate(Vector3.up * Value, Duration, RotateMode.LocalAxisAdd));
                            break;
                        case eAxis.Z:
                            if (IsLocal)
                                sequence.Append(i_Target.DOLocalRotate(Vector3.forward * Value, Duration, RotateMode.LocalAxisAdd));
                            else
                                sequence.Append(i_Target.DORotate(Vector3.forward * Value, Duration, RotateMode.LocalAxisAdd));

                            break;
                    }
                    break;
                case eAxis.Forward:
                case eAxis.Back:
                case eAxis.Left:
                case eAxis.Right:
                    switch (Axis)
                    {
                        case eAxis.Forward:
                            sequence.Append(i_Target.DORotate(i_Target.forward * Value, Duration));
                            break;
                        case eAxis.Back:
                            sequence.Append(i_Target.DORotate(-i_Target.forward * Value, Duration));
                            break;
                        case eAxis.Left:
                            sequence.Append(i_Target.DORotate(-i_Target.right * Value, Duration));
                            break;
                        case eAxis.Right:
                            sequence.Append(i_Target.DORotate(i_Target.right * Value, Duration));
                            break;
                    }
                    break;
            }

            sequence.SetEase(Ease).SetRelative(IsRelative).SetLoops(LoopCount, LoopType).SetDelay(DelayOnStart).OnComplete(OnCompleted);
            return sequence;
        }

        private Sequence Scale(Transform i_Target)
        {
            Sequence sequence = DOTween.Sequence();
            switch (Axis)
            {
                case eAxis.All:
                case eAxis.X:
                case eAxis.Y:
                case eAxis.Z:
                    switch (Axis)
                    {
                        case eAxis.All:
                            sequence.Append(i_Target.DOScale(VectorValue, Duration));
                            break;
                        case eAxis.X:
                            sequence.Append(i_Target.DOScaleX(Value, Duration));
                            break;
                        case eAxis.Y:
                            sequence.Append(i_Target.DOScaleY(Value, Duration));
                            break;
                        case eAxis.Z:
                            sequence.Append(i_Target.DOScaleZ(Value, Duration));
                            break;
                    }
                    break;
                case eAxis.Forward:
                case eAxis.Back:
                case eAxis.Left:
                case eAxis.Right:
                    switch (Axis)
                    {
                        case eAxis.Forward:
                            sequence.Append(i_Target.DOScale(i_Target.forward * Value, Duration));
                            break;
                        case eAxis.Back:
                            sequence.Append(i_Target.DOScale(-i_Target.forward * Value, Duration));
                            break;
                        case eAxis.Left:
                            sequence.Append(i_Target.DOScale(-i_Target.right * Value, Duration));
                            break;
                        case eAxis.Right:
                            sequence.Append(i_Target.DOScale(i_Target.right * Value, Duration));
                            break;
                    }
                    break;
            }


            sequence.SetEase(Ease).SetRelative(IsRelative).SetLoops(LoopCount, LoopType).SetDelay(DelayOnStart).OnComplete(OnCompleted);
            return sequence;
        }

        public Sequence Play(Transform i_Target)
        {
            Sequence sequence = DOTween.Sequence();
            m_Target = i_Target;
            switch (AnimationType)
            {
                case eAnimationType.Move:
                    sequence = Move(i_Target);
                    break;
                case eAnimationType.Rotate:
                    sequence = Rotate(i_Target);
                    break;
                case eAnimationType.Scale:
                    sequence = Scale(i_Target);
                    break;
            }
            sequence.OnStart(OnStarted);
            return sequence;
        }


        public enum eAnimationType
        {
            Move,
            Rotate,
            Scale
        }

        public enum eAxis
        {
            All,
            X,
            Y,
            Z,
            Forward,
            Back,
            Left,
            Right
        }
    }
}


