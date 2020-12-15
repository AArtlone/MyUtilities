using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace MyUtilities.GUI
{
    public static class PopUpManager
    {
        private static List<PopUpQueueElement> queueElements;
        private static PopUp popOnScreen;

        public static void CreateSingleButtonTitleTextPopUp(string title, string text, string buttonText, Action callback = default(Action), PopUpPriority priority = PopUpPriority.Regular)
        {
            if (callback == null)
                callback = new Action(() => { });

            var popUp = new PopUpQueueElement(title, text, buttonText, callback, priority);

            CreatePopUp(popUp);
        }

        public static void CreateSingleButtonTextPopUp(string text, string buttonText, Action callback = default(Action), PopUpPriority priority = PopUpPriority.Regular)
        {
            if (callback == null)
                callback = new Action(() => { } );

            var popUp = new PopUpQueueElement(text, buttonText, callback, priority);

            CreatePopUp(popUp);
        }

        public static void CreateDoubleButtonTitleTextPopUp(string title, string text, string firstButtonText, string secondButtonText, Action firstButtonCallback = default(Action), Action secondButtonCallback = default(Action), PopUpPriority priority = PopUpPriority.Regular)
        {
            if (firstButtonCallback == null)
                firstButtonCallback = new Action(() => { });

            if (secondButtonCallback == null)
                secondButtonCallback = new Action(() => { });

            var popUp = new PopUpQueueElement(title, text, firstButtonText, firstButtonCallback, secondButtonText, secondButtonCallback, priority);

            CreatePopUp(popUp);
        }

        public static void CreateDoubleButtonTextPopUp(string text, string firstButtonText, string secondButtonText, Action firstButtonCallback = default(Action), Action secondButtonCallback = default(Action), PopUpPriority priority = PopUpPriority.Regular)
        {
            if (firstButtonCallback == null)
                firstButtonCallback = new Action(() => { });

            if (secondButtonCallback == null)
                secondButtonCallback = new Action(() => { });

            var popUp = new PopUpQueueElement(text, firstButtonText, firstButtonCallback, secondButtonText, secondButtonCallback, priority);

            CreatePopUp(popUp);
        }

        private static void CreatePopUp(PopUpQueueElement popUp)
        {
            if (popOnScreen != null)
            {
                AddPopUpToQueue(popUp);
                return;
            }

            InstantiatePopUpObject();

            if (popOnScreen == null)
                return;

            popOnScreen.ReceiveData(popUp);
        }

        private static void AddPopUpToQueue(PopUpQueueElement popUpQueueElement)
        {
            if (queueElements == null)
                queueElements = new List<PopUpQueueElement>();

            queueElements.Add(popUpQueueElement);

            queueElements = queueElements.OrderBy(e => e.priority).ToList();
        }

        private static void InstantiatePopUpObject()
        {
            var popUpContainer = GameObject.FindGameObjectWithTag("PopUpContainer");

            if (popUpContainer == null)
            {
                Debug.LogError("PopUpContainer could not been found in the scene. Assing the PopUpContainer tag the container object in the scene");
                return;
            }

            popOnScreen = UnityEngine.Object.Instantiate(Resources.Load<PopUp>("Prefabs/PopUpPrefab"), popUpContainer.transform);
        }

        public static void PopUpWasClosed()
        {
            ShowNextPopUp();
        }

        private static void ShowNextPopUp()
        {
            if (queueElements == null || queueElements.Count == 0)
                return;
          
            InstantiatePopUpObject();

            if (popOnScreen == null)
                return;

            var nextPopUp = queueElements[0];

            popOnScreen.ReceiveData(nextPopUp);

            queueElements.Remove(nextPopUp);
        }
    }

    public enum PopUpPriority 
    {
        High,
        Regular
    }
}
