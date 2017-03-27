using System.Collections.Generic;
using System.IO;
using DataRowHelper;
using Newtonsoft.Json;
using NUnit.Framework;

namespace DataRowHelperUnitTest
{
	[TestFixture]
	public class DataRowReaderTest
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
			var data = "01Charlie   Taiwan, Tainan      \n02Charlie   Taiwan, Tainan      \n";
			var helper = new DataRowConvert(new StringReader(data));
			var act = helper.ReadRecords<Profile>();
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
			var data = "01Charlie   Taiwan, Tainan      N\n02Charlie   Taiwan, Tainan      G\n";
			var helper = new DataRowConvert(new StringReader(data));
			var act = helper.ReadRecords<MemberProfile>();
			Assert.AreEqual(JsonConvert.SerializeObject(exp), JsonConvert.SerializeObject(act));
		}
	}
}
