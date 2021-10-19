using UnityEngine;

namespace MyUtilities.GUI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class AlphaEffect : EffectBase
    {
        private CanvasGroup canvasGroup;

        private FloatEffectSO floatEffectSO;

        protected override void Awake()
        {
            if (effectSO == null)
            {
                Debug.LogWarning("Missing reference for the EffectSO", this);
                enabled = false;
                return;
            }

            if (!(effectSO is FloatEffectSO))
            {
                Debug.LogWarning("The reference EffectSO is of wrong type. This component requires EffectSO to be of type FloatEffectSO", this);
                enabled = false;
                return;
            }

            canvasGroup = GetComponent<CanvasGroup>();

            floatEffectSO = (FloatEffectSO)effectSO;

            base.Awake();
        }

        protected override void ApplyEffect()
        {
            canvasGroup.alpha = GetNextValue();
        }

        private float GetNextValue()
        {
            float nextValue = Mathf.Lerp(floatEffectSO.startValue, floatEffectSO.targetValue, GetCurveValue());

            return nextValue;
        }

        protected override void ResetEffect()
        {
            if (!enabled)
                return;

            base.ResetEffect();

            if (canvasGroup == null)
            {
                Debug.LogWarning("CanvasGroup is null, returning");
                return;
            }

            canvasGroup.alpha = floatEffectSO.startValue;
        }
    }
}
