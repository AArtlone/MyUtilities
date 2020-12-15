using System;

namespace MyUtilities.GUI
{
    public class PopUpQueueElement
    {
        public string title;
        public string text;
        public string b1;
        public string b2;
        public Action b1Callback;
        public Action b2Callback;
        public PopUpPriority priority;

        public PopUpQueueElement(string title, string text, string b1, Action b1Callback, PopUpPriority priority)
        {
            this.title = title;
            this.text = text;

            this.b1 = b1;
            this.b1Callback = b1Callback;

            this.priority = priority;
        }

        public PopUpQueueElement(string text, string b1, Action b1Callback, PopUpPriority priority)
        {
            this.text = text;

            this.b1 = b1;
            this.b1Callback = b1Callback;

            this.priority = priority;
        }

        public PopUpQueueElement(string title, string text, string b1, Action b1Callback, string b2, Action b2Callback, PopUpPriority priority)
        {
            this.title = title;
            this.text = text;

            this.b1 = b1;
            this.b1Callback = b1Callback;

            this.b2 = b2;
            this.b2Callback = b2Callback;

            this.priority = priority;
        }

        public PopUpQueueElement(string text, string b1, Action b1Callback, string b2, Action b2Callback, PopUpPriority priority)
        {
            this.text = text;

            this.b1 = b1;
            this.b1Callback = b1Callback;

            this.b2 = b2;
            this.b2Callback = b2Callback;

            this.priority = priority;
        }
    }
}