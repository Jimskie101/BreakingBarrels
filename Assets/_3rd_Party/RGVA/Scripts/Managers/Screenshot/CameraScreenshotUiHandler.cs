using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace RGVA.SreenShot
{
    public class CameraScreenshotUiHandler : MonoBehaviour
    {
        [Header("References")]
        [SerializeField, ReadOnly] private RectTransform m_ShutterMaskable;

        [Button]
        public void SetRef()
        {
            m_ShutterMaskable = transform.FindDeepChild<RectTransform>("Shutter_Maskable");
        }

        public void Shot(float i_ShotSpeed = 0.5f, bool i_AnimateXAxis = true)
        {
            Sequence shotSequence = DOTween.Sequence();

            shotSequence.Append(m_ShutterMaskable.DOScaleY(0, i_ShotSpeed / 2).OnComplete(() =>
            {

            }));

            if (i_AnimateXAxis)
                shotSequence.Join(m_ShutterMaskable.DOScaleX(0, i_ShotSpeed / 2));

            shotSequence.Append(m_ShutterMaskable.DOScaleY(1, i_ShotSpeed / 2).OnComplete(() =>
            {

            }));

            if (i_AnimateXAxis)
                shotSequence.Join(m_ShutterMaskable.DOScaleX(1, i_ShotSpeed / 2));
        }
    }

}
