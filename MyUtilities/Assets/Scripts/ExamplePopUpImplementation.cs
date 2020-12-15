using MyUtilities.GUI;
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
            System.Action callback1 = new System.Action(() => { });
            PopUpManager.CreateSingleButtonTitleTextPopUp(title, text, b1Text, callback1, PopUpPriority.Regular);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            string text = "High B";
            string b1Text = "ok";
            System.Action callback1 = new System.Action(() => { });
            PopUpManager.CreateSingleButtonTextPopUp(text, b1Text, callback1, PopUpPriority.High);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            string text = "Regular N";

            string b1Text = "Yes";
            System.Action callback1 = new System.Action(() => { });

            string b2Text = "No";
            System.Action callback2 = new System.Action(() => { });

            PopUpManager.CreateDoubleButtonTextPopUp(text, b1Text, callback1, b2Text, callback2, PopUpPriority.Regular);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            string title = "AHAHAHAH";
            string text = "High M";

            string b1Text = "ok";
            System.Action callback1 = new System.Action(() => { });

            string b2Text = "No";
            System.Action callback2 = new System.Action(() => { });

            PopUpManager.CreateDoubleButtonTitleTextPopUp(title, text, b1Text, callback1, b2Text, callback2, PopUpPriority.High);
        }
    }
}
