namespace DataRowHelper
{
	public class DataRowConfiguration
	{
		public bool HasHeaderRecord { get; set; } = false;
		public string[] Separator { get; set; } = new string[] { "\r\n" };
		public char PadNumber { get; set; } = '0';
		public char PadString { get; set; } = ' ';
	}
}
