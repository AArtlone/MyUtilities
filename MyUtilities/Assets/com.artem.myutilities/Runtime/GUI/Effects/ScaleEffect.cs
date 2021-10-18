using UnityEngine;

namespace MyUtilities.GUI
{
    public class ScaleEffect : EffectBase
    {
        private Vector3EffectSO v3EffectSo;

        protected override void Awake()
        {
            if (!(effectSO is Vector3EffectSO))
            {
                Debug.LogWarning("The reference EffectSO is of wrong type. This component requires EffectSO to be of type Vector3EffectSO", this);
                enabled = false;
                return;
            }

            v3EffectSo = (Vector3EffectSO)effectSO;

            base.Awake();
        }

        protected override void ApplyEffect()
        {
            transform.localScale = GetNextValue();
        }

        private Vector3 GetNextValue()
        {
            Vector3 nextValue = Vector3.Lerp(v3EffectSo.startValue, v3EffectSo.targetValue, GetCurveValue());

            return nextValue;
        }

        protected override void ResetEffect()
        {
            if (!enabled)
                return;

            base.ResetEffect();

            transform.localScale = v3EffectSo.startValue;
        }
    }
}
