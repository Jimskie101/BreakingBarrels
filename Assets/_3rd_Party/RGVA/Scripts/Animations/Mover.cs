using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;

namespace RGVA
{
	public class Mover : AnimatorBase
	{
        public bool IsLocal = false;

        public Vector3 m_InitialPosition;
		public Vector3 FinalPosition;

		protected override void Awake()
		{
            base.Awake();
            if (IsLocal)
                m_InitialPosition = transform.localPosition;
            else
                m_InitialPosition = transform.position;
        }

        [Button]
		public override void StartAnimation()
		{
			base.StartAnimation();

            ResetValues();

            if (IsLocal)
            {
                Tween = transform.DOLocalMove(FinalPosition, Duration)
                                .SetEase(Ease)
                                .SetRelative(IsRelative)
                                .SetLoops(LoopCount, LoopType);
            }
            else
            {
                Tween = transform.DOMove(FinalPosition, Duration)
                                .SetEase(Ease)
                                .SetRelative(IsRelative)
                                .SetLoops(LoopCount, LoopType);
            }

            base.SetId(Tween);

            if (StartTime > 0 && StartTime < 1)
				this.Tween.Goto(Mathf.Lerp(0, Duration, StartTime), true);
		}

        

		public override void ResetValues()
		{
			base.ResetValues();

            if(IsLocal)
			    transform.localPosition = m_InitialPosition;
            else
                transform.position = m_InitialPosition;
        }
	}
}
