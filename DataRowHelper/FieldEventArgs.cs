using System;
namespace DataRowHelper
{
	public class FieldEventArgs : EventArgs
	{
		public readonly Type Type;
		public readonly object RowData;
		public FieldEventArgs(Type type, object rowData)
		{
			Type = type;
			RowData = rowData;
		}
	}
}
