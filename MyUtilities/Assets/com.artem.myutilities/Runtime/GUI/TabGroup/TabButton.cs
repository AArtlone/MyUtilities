using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace MyUtilities.GUI
{
    [RequireComponent(typeof(Image))]
    public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        public Action onTabSelected;

        public Image BackgroundImage { get; private set; }

        private TabGroup tabGroup = default;

        public void SetTabGroup(TabGroup tabGroup)
        {
            this.tabGroup = tabGroup;

            if (BackgroundImage == null)
                BackgroundImage = GetComponent<Image>();
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

        public virtual void Select(Sprite sprite)
        {
            UpdateVisual(sprite);

            if (onTabSelected != null)
                onTabSelected.Invoke();
        }

        public virtual void Deselect()
        {

        }

        public void UpdateVisual(Color color, float alphaValue = 1)
        {
            if (BackgroundImage == null)
                return;

            var newColor = new Color(color.r, color.g, color.b, alphaValue);

            BackgroundImage.color = newColor;
        }

        public void UpdateVisual(Sprite sprite)
        {
            if (BackgroundImage == null)
                return;

            BackgroundImage.sprite = sprite;
        }
    }
}