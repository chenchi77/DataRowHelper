using System.Collections.Generic;
using System.IO;
using DataRowHelper;
using Newtonsoft.Json;
using NUnit.Framework;

namespace DataRowHelperUnitTest
{
	[TestFixture]
	public class DataRowHelperTest
	{
		[Test]
		public void ReaderTextConvertToGeneralClass()
		{
			var exp = new List<Profile> {
				new Profile {
					Id = 1,
					Name = "Charlie",
					Address = "Taiwan, Tainan"
				},
				new Profile {
					Id = 2,
					Name = "Charlie",
					Address = "Taiwan, Tainan"
				}
			};
			var data = "01Charlie   Taiwan, Tainan      \r\n02Charlie   Taiwan, Tainan      ";
			var helper = new DataRowConvert(new StringReader(data));
			var act = helper.GetRecords<Profile>();
			Assert.AreEqual(JsonConvert.SerializeObject(exp), JsonConvert.SerializeObject(act));
		}

		[Test]
		public void ReaderTextConvertToEnumClass()
		{
			var exp = new List<MemberProfile> {
				new MemberProfile {
					Id = 1,
					Name = "Charlie",
					Address = "Taiwan, Tainan",
					MemberType = MemberType.General
				},
				new MemberProfile {
					Id = 2,
					Name = "Charlie",
					Address = "Taiwan, Tainan",
					MemberType = MemberType.Gold
				}
			};
			var data = "01Charlie   Taiwan, Tainan      N\r\n02Charlie   Taiwan, Tainan      G";
			var helper = new DataRowConvert(new StringReader(data));
			var act = helper.GetRecords<MemberProfile>();
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

	public class MemberProfile
	{
		[StringRange(1, 2)]
		public int Id { get; set; }
		[StringRange(3, 10)]
		public string Name { get; set; }
		[StringRange(13, 20)]
		public string Address { get; set; }
		[StringRange(33, 1)]
		public MemberType MemberType { get; set; }
	}

	public enum MemberType
	{
		[StringValue("N")]
		General = 0,
		[StringValue("G")]
		Gold = 1
	}
}
