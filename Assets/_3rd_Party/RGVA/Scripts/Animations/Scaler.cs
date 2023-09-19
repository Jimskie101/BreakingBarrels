using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;

namespace RGVA
{
	public class Scaler : AnimatorBase
	{
		private Vector3 m_InitialScale;
		public Vector3 FinalScale;

		public bool SetInitialScaleStart = false;
		[ShowIf(nameof(SetInitialScaleStart))]
		public Vector3 InitialScale;

		protected override void Awake()
		{
			base.Awake();
			m_InitialScale = transform.localScale;
		}

		[Button]
		public override void StartAnimation()
		{
			base.StartAnimation();

			if (SetInitialScaleStart)
			{
				transform.localScale = InitialScale;
			}

			Tween = transform.DOScale(FinalScale, Duration)
				.SetEase(Ease)
				.SetRelative(IsRelative)
				.SetLoops(LoopCount, LoopType)
				.SetUpdate(UpdateType, UpdateFrameIndependant);

			base.SetId(Tween);

			if (StartTime > 0 && StartTime < 1)
				this.Tween.Goto(Mathf.Lerp(0, Duration, StartTime), true);
		}

		public override void ResetValues()
		{
			base.ResetValues();

			transform.localScale = m_InitialScale;
		}
	}
}