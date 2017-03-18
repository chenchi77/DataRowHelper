using DataRowHelper;
using NUnit.Framework;

namespace DataRowHelperUnitTest
{
	[TestFixture]
	public class DataRowHelperTest
	{
		[Test]
		public void Test()
		{
			
		}
	}

	public class Profile
	{
		[StringRange(1, 2)]
		public int Id { get; set; }
		[StringRange(3, 10)]
		public string Name { get; set; }
		[StringRange(13, 20)]
		public string Address { get; set; }
	}
}
