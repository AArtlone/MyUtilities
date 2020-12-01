using MyUtilities.DataSheets;
using UnityEngine;

public class ExampleDSModel: DSModelBase<ExampleDSRecord, ExampleDSID>
{
	protected override ExampleDSRecord CreateRecord(string[] csvFileLine)
	{
		var result = new ExampleDSRecord(csvFileLine);

		return result;
	}
}
