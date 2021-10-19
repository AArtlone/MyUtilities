using MyUtilities;
using UnityEngine;

public class ShowIfExampleScript : MonoBehaviour
{
    [SerializeField] private bool showInt = default;

    [ShowIf(nameof(showInt), true, ComparisonType.Equals)]
    [SerializeField] private int someInt = default;

    [Header("Set this variable to 1 to see the effect of the ShowIf attribute")]
    [SerializeField] private int showBool = default;
    [ShowIf(nameof(showBool), 1, ComparisonType.Equals)]
    [SerializeField] private bool someBool = default;
}
