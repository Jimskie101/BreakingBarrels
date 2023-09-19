using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace RGVA
{
	public class AnimatorBase : MonoBehaviour
	{
		
		[ReadOnly] public bool m_IsPlaying;
		[ReadOnly] public Tween Tween;

		[Space]

		public bool PlayOnEnable;
		public bool RandomStartTime = false;
		[Range(0, 1)] public float StartTime = 0;

		[Space]
		public bool UseDotweenID = false;

		[HideIf(nameof(UseDotweenID))]
		public int Id = k_NoId;
		protected const int k_NoId = -9999;


		public float Duration = 1;
		public Ease Ease = Ease.Linear;
		public bool IsRelative;

		[Space]

		public UpdateType UpdateType = UpdateType.Normal;
		public bool UpdateFrameIndependant = false;

		[Space]

		public int LoopCount = 0;
		private bool m_UsingLoops => LoopCount > 0 || LoopCount == -1;
		[ShowIf(nameof(m_UsingLoops))]
		public LoopType LoopType;


		protected virtual void Awake()
		{
			if (RandomStartTime)
			{
				StartTime = Random.Range(0.0f, 1.0f);
			}
		}

		protected virtual void Start()
		{
			SetRestartAction();
		}

		protected virtual void SetRestartAction()
		{
		}

		protected virtual void OnEnable()
		{
			ResetValues();
			if (PlayOnEnable)
				StartAnimation();
		}

		protected virtual void OnDisable()
		{
			StopAnimation();
		}

		[Button]
		public virtual void ResetValues()
		{
			StopAnimation();
		}

		public virtual void StartAnimation()
		{
			Tween?.Kill(true);
			m_IsPlaying = true;
		}

		public virtual void StopAnimation()
		{
			m_IsPlaying = false;
			Tween?.Kill(true);
		}

		public virtual void RestartAnimation()
		{
			ResetValues();
			StartAnimation();
		}

		public virtual void PauseAnimation()
        {
			Tween?.Pause();
        }

		public void ContinueAnimation()
        {
			Tween?.Play();
        }

		public virtual void SetId(Tween i_Tween)
        {
			
        }
	}
}