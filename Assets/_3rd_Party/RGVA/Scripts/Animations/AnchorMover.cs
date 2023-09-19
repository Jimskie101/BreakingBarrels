using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;

namespace RGVA
{
	public class AnchorMover : AnimatorBase
	{
		public bool IsLocal = false;

		public Vector3 m_InitialPosition;
		public Vector3 FinalPosition;

		private RectTransform m_rectTransform;

		protected override void Awake()
		{
			base.Awake();
			initIfNeed();
		}

		[Button]
		public override void StartAnimation()
		{
			initIfNeed();
			base.StartAnimation();

			ResetValues();

			Tween = m_rectTransform.DOAnchorPos3D(FinalPosition, Duration)
							.SetEase(Ease)
							.SetRelative(IsRelative)
							.SetLoops(LoopCount, LoopType);

			base.SetId(Tween);

			if (StartTime > 0 && StartTime < 1)
				this.Tween.Goto(Mathf.Lerp(0, Duration, StartTime), true);
		}

		public override void ResetValues()
		{
			initIfNeed();
			base.ResetValues();

			m_rectTransform.anchoredPosition3D = m_InitialPosition;
		}

		private bool m_WasInit = false;
		private void initIfNeed()
		{
			if (!m_WasInit)
			{
				m_WasInit = true;

				m_rectTransform = GetComponent<RectTransform>();

				m_InitialPosition = m_rectTransform.anchoredPosition3D;
			}
		}
	}
}
