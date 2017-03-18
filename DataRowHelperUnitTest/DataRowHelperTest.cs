using System.Collections.Generic;
using DataRowHelper;
using Newtonsoft.Json;
using NUnit.Framework;

namespace DataRowHelperUnitTest
{
	[TestFixture]
	public class DataRowHelperTest
	{
		private DataRowConfiguration config = new DataRowConfiguration
		{
			HasHeaderRecord = false
		};

		[Test]
		public void ReadOneRowNotWithHeader()
		{
			var exp = new List<Profile> {
				new Profile
				{
					Id = 1,
					Name = "Charlie",
					Address = "Taiwan, Tainan"
				}
			};
			var data = "01Charlie   Taiwan, Tainan      ";
			var helper = new DataRowParser(config);
			var act = helper.Reader<Profile>(data);
			Assert.AreEqual(JsonConvert.SerializeObject(exp), JsonConvert.SerializeObject(act));
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
