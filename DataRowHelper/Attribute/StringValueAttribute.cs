using System;
namespace DataRowHelper
{
	public class StringValueAttribute : Attribute
	{
		public string Value { get; set; }
		public StringValueAttribute(string Value)
		{
			this.Value = Value;
		}
	}
}
