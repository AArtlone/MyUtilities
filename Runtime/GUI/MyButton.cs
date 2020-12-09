using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MyUtilities.GUI
{
    public class MyButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        public Action onClick;

        [SerializeField] private UpdateMethod updateMethod = default;

        [SerializeField] private bool otherBackgroundImage = default;
        [ShowIf(nameof(otherBackgroundImage), true, ComparisonType.Equals)]
        [SerializeField] private Image backgroundImage = default;

        [Space(5f)]
        [SerializeField] private TextMeshProUGUI buttonText = default;

        [Space(5f)]
        [ShowIf(nameof(updateMethod), nameof(UpdateMethod.Sprite), ComparisonType.Equals)]
        [SerializeField] private Sprite idleSprite = default;
        [ShowIf(nameof(updateMethod), nameof(UpdateMethod.Sprite), ComparisonType.Equals)]
        [SerializeField] private Sprite hoverSprite = default;
        [ShowIf(nameof(updateMethod), nameof(UpdateMethod.Sprite), ComparisonType.Equals)]
        [SerializeField] private Sprite activeSprite = default;
        [Space(5f)]
        [ShowIf(nameof(updateMethod), nameof(UpdateMethod.Color), ComparisonType.Equals)]
        [SerializeField] private Color idleColor = default;
        [ShowIf(nameof(updateMethod), nameof(UpdateMethod.Color), ComparisonType.Equals)]
        [SerializeField] private Color hoverColor = default;
        [ShowIf(nameof(updateMethod), nameof(UpdateMethod.Color), ComparisonType.Equals)]
        [SerializeField] private Color activeColor = default;

        [Space(5f)]
        [SerializeField] private UnityEvent onClickEvent = default;

        public bool Interactable { get; private set; } = true;

        private Image background;

        private void Awake()
        {
            if (!otherBackgroundImage)
                background = GetComponent<Image>();
            else
                background = backgroundImage;

            if (background == null)
            {
                Debug.LogWarning($"BackgroundImage is not set in the editor on { gameObject.name }");
                enabled = false;
                return;
            }

            if (!Interactable)
                return;


            UpdateVisualMain(ButtonState.Idle);
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (!Interactable)
                return;

            UpdateVisualMain(ButtonState.Hover);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (!Interactable)
                return;

            UpdateVisualMain(ButtonState.Active);

            if (onClickEvent != null)
                onClickEvent.Invoke();

            if (onClick != null)
                onClick.Invoke();
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (!Interactable)
                return;

            UpdateVisualMain(ButtonState.Idle);
        }

        private void UpdateVisualMain(ButtonState buttonState)
        {
            if (!otherBackgroundImage && background == null)
                background = GetComponent<Image>();

            if (updateMethod == UpdateMethod.Color)
                UpdateBackgroundColor(buttonState);
            else
                UpdateBackgroundSprite(buttonState);

            UpdateButtonTextVisual();
        }

        private void UpdateBackgroundSprite(ButtonState buttonState)
        {
            switch (buttonState)
            {
                case ButtonState.Idle:
                    background.sprite = idleSprite;
                    break;

                case ButtonState.Hover:
                    background.sprite = hoverSprite;
                    break;

                case ButtonState.Active:
                    background.sprite = activeSprite;
                    break;
            }
        }

        private void UpdateBackgroundColor(ButtonState buttonState)
        {
            Color color;
            switch (buttonState)
            {
                case ButtonState.Idle:
                    color = new Color(idleColor.r, idleColor.g, idleColor.b, 1f);
                    break;

                case ButtonState.Hover:
                    color = new Color(hoverColor.r, hoverColor.g, hoverColor.b, 1f);
                    break;

                case ButtonState.Active:
                    color = new Color(activeColor.r, activeColor.g, activeColor.b, 1f);
                    break;

                default:
                    color = new Color(idleColor.r, idleColor.g, idleColor.b, 1f);
                    break;
            }

            background.color = color;
        }

        private void UpdateButtonTextVisual()
        {
            if (buttonText == null)
                return;

            var textColor = buttonText.color;
            Color newColor;

            if (Interactable)
                newColor = new Color(textColor.r, textColor.g, textColor.b, 1f);
            else
                newColor = new Color(textColor.r, textColor.g, textColor.b, .5f);

            buttonText.color = newColor;
        }

        private void DimButton()
        {
            if (background == null)
                return;

            var colorToUpdate = background.color;
            Color color = new Color(colorToUpdate.r, colorToUpdate.g, colorToUpdate.b, .5f);
            background.color = color;
        }

        public void SetInteractable(bool value)
        {
            Interactable = value;

            if (Interactable)
            {
                UpdateVisualMain(ButtonState.Idle);
            }
            else
            {
                DimButton();
                UpdateButtonTextVisual();
            }
        }

        public void SetButtonText(string text)
        {
            if (buttonText == null)
            {
                Debug.LogWarning($"ButtonText object is not assigned in the editor or null on { gameObject.name }");
                return;
            }

            buttonText.text = text;
        }
    }

    public enum UpdateMethod
    {
        Sprite, Color
    }

    public enum ButtonState
    {
        Idle, Hover, Active
    }
}