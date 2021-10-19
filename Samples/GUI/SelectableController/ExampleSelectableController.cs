using MyUtilities.GUI;
using System.Collections.Generic;

public class ExampleSelectableController : SelectableController<ExampleSelectableCell, ExampleSelectableCellData>
{
    private void Awake()
    {
        List<ExampleSelectableCellData> dataSet = new List<ExampleSelectableCellData>();

        for (int i = 0; i < 3; i++)
        {
            dataSet.Add(new ExampleSelectableCellData(i.ToString()));
        }

        SetDataSet(dataSet);
    }

    protected override void Cell_OnCellPress(SelectableCell<ExampleSelectableCellData> cell)
    {
        base.Cell_OnCellPress(cell);

        print(cell.data.someText);
    }
}
