using System;
namespace DataRowHelper
{
	public class FieldEventArgs : EventArgs
	{
		public readonly object RowData;
		public FieldEventArgs(object rowData)
		{
			RowData = rowData;
		}
	}
}
