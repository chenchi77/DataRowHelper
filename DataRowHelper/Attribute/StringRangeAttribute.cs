using System;
namespace DataRowHelper
{
	public class StringRangeAttribute : Attribute
	{
		public int StartIndex { get; set; }
		public int Length { get; set; }

		public StringRangeAttribute(int StartIndex, int Length)
		{
			this.StartIndex = StartIndex - 1;
			this.Length = Length;
		}
	}
}
