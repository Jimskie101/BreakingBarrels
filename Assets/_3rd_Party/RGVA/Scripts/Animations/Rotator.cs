using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;

namespace RGVA
{
	public class Rotator : AnimatorBase
	{
        public bool IsLocal = true;

        private Quaternion m_InitialRotation;
		public Vector3 FinalLocalRotation;

		protected override void Awake()
		{
            base.Awake();

            SetInitialValues();
        }
	    
        public void SetInitialValues()
        {
            if (IsLocal)
                m_InitialRotation = transform.localRotation;
            else
                m_InitialRotation = transform.rotation;
        }

        [Button]
		public override void StartAnimation()
		{
			base.StartAnimation();

            if(IsLocal)
            {
                Tween = transform.DOLocalRotate(FinalLocalRotation, Duration, RotateMode.LocalAxisAdd)
                                .SetEase(Ease)
                                .SetRelative(IsRelative)
                                .SetLoops(LoopCount, LoopType);
            }
            else
            {
                Tween = transform.DORotate(FinalLocalRotation, Duration, RotateMode.LocalAxisAdd)
                                .SetEase(Ease)
                                .SetRelative(IsRelative)
                                .SetLoops(LoopCount, LoopType);
            }

            base.SetId(Tween);

			if(StartTime > 0 && StartTime < 1)
				this.Tween.Goto(Mathf.Lerp(0, Duration, StartTime), true);
		}

		public override void ResetValues()
		{
			base.ResetValues();

            if (IsLocal)
                transform.localRotation = m_InitialRotation;
            else
                transform.rotation = m_InitialRotation;
        }
	}
}
