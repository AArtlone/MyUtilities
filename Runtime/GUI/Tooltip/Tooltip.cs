using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class Tooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title = default;

    [SerializeField] private LayoutElement layoutElement = default;

    [SerializeField] private TextMeshProUGUI contentPrefab = default;

    [SerializeField] private int characterWrapLimit = default;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Application.isEditor && Input.GetMouseButtonDown(0))
            Destroy(gameObject);

        if (Input.touchCount > 0)
            Destroy(gameObject);
    }

    public void SePosition(Vector2 position)
    {
        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }

    public void SetTitle(string title)
    {
        if (string.IsNullOrEmpty(title))
        {
            this.title.gameObject.SetActive(false);
            return;
        }

        this.title.text = title;

        int headerLength = this.title.text.Length;

        layoutElement.enabled = (headerLength > characterWrapLimit) ? true : false;
    }

    public void SetContent(List<string> contentElements)
    {
        for (int i = 0; i < contentElements.Count; i++)
        {
            var obj = Instantiate(contentPrefab, transform);
            obj.text = contentElements[i];

            int contentLength = contentElements[i].Length;
            layoutElement.enabled = (contentLength > characterWrapLimit) ? true : false;
        }
    }
}
