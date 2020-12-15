using System;
using TMPro;
using UnityEngine;

namespace MyUtilities.GUI
{
    public class PopUp : MonoBehaviour
    {
        public TextMeshProUGUI title = default;
        public TextMeshProUGUI description = default;

        public MyButton firstButton = default;
        public MyButton secondButton = default;

        public void ReceiveData(PopUpQueueElement popUp)
        {
            if (!string.IsNullOrEmpty(popUp.title))
                AddTitle(popUp.title);

            AddDescription(popUp.text);

            if (string.IsNullOrEmpty(popUp.b2))
                AddOneButton(popUp.b1, popUp.b1Callback);
            else
                AddTwoButtons(popUp.b1, popUp.b1Callback, popUp.b2, popUp.b2Callback);
        }

        private void AddTitle(string titleText)
        {
            title.gameObject.SetActive(true);
            title.text = titleText;
        }

        private void AddDescription(string text)
        {
            description.gameObject.SetActive(true);
            description.text = text;
        }

        private void AddOneButton(string buttonText, Action callback)
        {
            firstButton.gameObject.SetActive(true);
            firstButton.SetButtonText(buttonText);

            var onClick = new Action(() =>
            {
                callback();

                PopUpManager.PopUpWasClosed();

                Destroy(gameObject);
            });

            firstButton.onClick += onClick;
        }

        private void AddTwoButtons(string firstButtonText, Action firstCallback, string secondButtonText, Action secondCallback)
        {
            firstButton.gameObject.SetActive(true);
            firstButton.SetButtonText(firstButtonText);

            var onClickOne = new Action(() =>
            {
                firstCallback();

                PopUpManager.PopUpWasClosed();

                Destroy(gameObject);
            });

            firstButton.onClick += onClickOne;

            secondButton.gameObject.SetActive(true);
            secondButton.SetButtonText(secondButtonText);

            var onClickTwo = new Action(() =>
            {
                secondCallback();

                PopUpManager.PopUpWasClosed();

                Destroy(gameObject);
            });

            secondButton.onClick += onClickTwo;
        }
    }

}