using UnityEngine;

namespace MyUtilities.GUI
{
    [RequireComponent(typeof(RectTransform))]
    public class PositionEffect : EffectBase
    {
        private Vector3EffectSO v3EffectSo;
        private RectTransform rectTransform;


        protected override void Awake()
        {
            if (!(effectSO is Vector3EffectSO))
            {
                Debug.LogWarning("The reference EffectSO is of wrong type. This component requires EffectSO to be of type Vector3EffectSO", this);
                enabled = false;
                return;
            }

            v3EffectSo = (Vector3EffectSO)effectSO;
            rectTransform = GetComponent<RectTransform>();

            base.Awake();
        }

        protected override void ApplyEffect()
        {
            rectTransform.anchoredPosition = GetNextValue();
        }

        private Vector3 GetNextValue()
        {
            Vector3 nextValue = Vector3.Lerp(v3EffectSo.startValue, v3EffectSo.targetValue, GetCurveValue());

            return nextValue;
        }

        protected override void ResetEffect()
        {
            print(rectTransform.anchoredPosition);
            base.ResetEffect();

            rectTransform.anchoredPosition = v3EffectSo.startValue;
            print(rectTransform.anchoredPosition);
        }

        public override void PlayEffect()
        {
            if (!enabled)
                return;

            base.PlayEffect();
        }
    }
}
