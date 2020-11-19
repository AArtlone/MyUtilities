using System;
using UnityEngine;

namespace MyUtilities.GUI
{
    [CreateAssetMenu(fileName = "floatEffectSO", menuName = "FloatEffectSO")]
    public class FloatEffectSO : EffectSOBase
    {
        public float startValue;
        public float targetValue;
    }
}
