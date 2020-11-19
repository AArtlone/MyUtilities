﻿using UnityEngine;

namespace MyUtilities.GUI
{
    public class PositionEffect : EffectBase
    {
        private Vector3EffectSO v3EffectSo;

        protected override void Awake()
        {
            v3EffectSo = (Vector3EffectSO)effectSO;

            base.Awake();
        }

        protected override void ApplyEffect()
        {
            transform.localPosition = GetNextValue();
        }

        private Vector3 GetNextValue()
        {
            Vector3 nextValue = Vector3.Lerp(v3EffectSo.startValue, v3EffectSo.targetValue, GetCurveValue());

            return nextValue;
        }

        protected override void ResetEffect()
        {
            base.ResetEffect();

            transform.localPosition = v3EffectSo.startValue;
        }

        public override void PlayEffect()
        {
            base.PlayEffect();
        }
    }
}
