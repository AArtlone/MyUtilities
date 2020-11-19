﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace MyUtilities.GUI
{
    [RequireComponent(typeof(Image))]
    public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        public Action onTabSelected;

        [SerializeField] private Image backgroundImage = default;

        public Image BackgroundImage { get { return backgroundImage; } }

        private TabGroup tabGroup = default;

        public void SetTabGroup(TabGroup tabGroup)
        {
            this.tabGroup = tabGroup;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            tabGroup.EnterTab(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            tabGroup.SelectTab(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tabGroup.ExitTab();
        }

        public void Select(Color color)
        {
            UpdateVisual(color);

            if (onTabSelected != null)
                onTabSelected.Invoke();
        }

        public void Select(Sprite sprite)
        {
            UpdateVisual(sprite);

            if (onTabSelected != null)
                onTabSelected.Invoke();
        }

        public void Deselect()
        {

        }

        public void UpdateVisual(Color color, float alphaValue = 1)
        {
            var newColor = new Color(color.r, color.g, color.b, alphaValue);

            BackgroundImage.color = newColor;
        }

        public void UpdateVisual(Sprite sprite)
        {
            BackgroundImage.sprite = sprite;
        }
    }
}