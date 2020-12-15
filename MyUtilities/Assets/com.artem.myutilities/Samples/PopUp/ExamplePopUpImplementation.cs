using MyUtilities.GUI;
using System;
using UnityEngine;

public class ExamplePopUpImplementation : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            string title = "AHAHAHAH";
            string text = "Regular V";
            string b1Text = "ok";

            PopUpManager.CreateSingleButtonTitleTextPopUp(title, text, b1Text);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            string text = "High B";
            string b1Text = "ok";

            PopUpManager.CreateSingleButtonTextPopUp(text, b1Text, default(Action), PopUpPriority.High);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            string text = "Regular N";
            string b1Text = "Yes";
            string b2Text = "No";

            PopUpManager.CreateDoubleButtonTextPopUp(text, b1Text, b2Text);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            string title = "AHAHAHAH";
            string text = "High M";

            string b1Text = "ok";
            Action callback1 = new Action(() => { });

            string b2Text = "No";
            Action callback2 = new Action(() => { });

            PopUpManager.CreateDoubleButtonTitleTextPopUp(title, text, b1Text, b2Text, default(Action), default(Action), PopUpPriority.High);
        }
    }
}
