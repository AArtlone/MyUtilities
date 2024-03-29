﻿using System;
using UnityEngine;

namespace MyUtilities.GUI
{
    [RequireComponent(typeof(MyButton))]
    public abstract class SelectableCell<T> : MonoBehaviour where T : SelectableCellData
    {
        public Action<SelectableCell<T>> onCellPress;

        public T data;

        [HideInInspector] public MyButton myButton;

        public void SetData(T data)
        {
            this.data = data;

            myButton = GetComponent<MyButton>();

            myButton.onClick += MyButton_OnClick;
        }

        protected virtual void OnDestroy()
        {
            myButton.onClick -= MyButton_OnClick;
        }

        public abstract void Refresh();

        public virtual void Initialize() { Refresh(); }

        public virtual void MyButton_OnClick()
        {
            onCellPress?.Invoke(this);
        }
    }

    public class SelectableCellData
    {

    }
}
