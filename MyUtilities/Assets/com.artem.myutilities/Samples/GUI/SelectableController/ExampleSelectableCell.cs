using MyUtilities.GUI;
using TMPro;
using UnityEngine;

public class ExampleSelectableCell : SelectableCell<ExampleSelectableCellData>
{
    [SerializeField] private TextMeshProUGUI textComponent = default;

    public override void Refresh()
    {
        textComponent.text = data.someText;
    }
}

public class ExampleSelectableCellData : SelectableCellData
{
    public string someText;

    public ExampleSelectableCellData(string text)
    {
        someText = text;
    }
}
