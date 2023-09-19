using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;

namespace RGVA
{
	public class TextBlinker : AnimatorBase
	{
        public Color Initial;
        public Color Final;

        public Text Text;
		public TextMeshProUGUI TextTMP;

		protected override void Awake()
		{
            base.Awake();
			ResetValues();
		}
	    
        [Button]
		public override void StartAnimation()
		{
			base.StartAnimation();

			if (Text != null)
            {
				Tween = Text.DOColor(Final, Duration)
				.SetEase(Ease)
				.SetLoops(LoopCount, LoopType);
			}
			else if (TextTMP != null)
            {
				Tween = TextTMP.DOColor(Final, Duration)
								.SetEase(Ease)
								.SetLoops(LoopCount, LoopType);
			}

			base.SetId(Tween);

			if (StartTime > 0 && StartTime < 1)
				this.Tween.Goto(Mathf.Lerp(0, Duration, StartTime), true);
		}

		public override void ResetValues()
		{
			base.ResetValues();

			if (Text != null)
            {
				Text.color = Initial;
			}
			else if (TextTMP != null)
            {
				TextTMP.color = Initial;
			}
		}
	}
}
