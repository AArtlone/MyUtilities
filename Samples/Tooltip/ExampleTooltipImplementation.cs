using MyUtilities.GUI;
using System.Collections.Generic;
using UnityEngine;

public class ExampleTooltipImplementation : MonoBehaviour
{
    [SerializeField] private List<string> textToShow = default;

    private MyButton myButton;

    private void Awake()
    {
        myButton = GetComponent<MyButton>();

        myButton.onPointerEnter += Btn_OnPointerEnter;
        myButton.onPointerExit += Btn_OnPointerExit;
    }

    private void OnDestroy()
    {
        myButton.onPointerEnter -= Btn_OnPointerEnter;
        myButton.onPointerExit -= Btn_OnPointerExit;
    }

    private void Btn_OnPointerEnter()
    {
        MyTooltip.CreateTooltip(transform.position, textToShow);
    }

    private void Btn_OnPointerExit()
    {
        MyTooltip.DestroyTooltip();
    }
}