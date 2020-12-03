using MyUtilities.GUI;
using System.Collections.Generic;
using UnityEngine;

public class ExampleTooltipImplementation : MonoBehaviour
{
    private MyButton myButton;

    private void Awake()
    {
        myButton = GetComponent<MyButton>();
        myButton.onClick += Btn_OnClick;
    }

    private void Btn_OnClick()
    {
        var contentElemenets = new List<string> { "aa", "bbb" };
        MyTooltip.CreateTooltip(transform.position, contentElemenets);
    }
}
