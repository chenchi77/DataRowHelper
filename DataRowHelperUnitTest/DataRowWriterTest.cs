using System.Collections.Generic;
using System.IO;
using DataRowHelper;
using Newtonsoft.Json;
using NUnit.Framework;

namespace DataRowHelperUnitTest
{
	[TestFixture]
	public class DataRowWriterTest
	{
		[Test]
		public void ReaderTextConvertToGeneralClass()
		{
			var data = new List<Profile> {
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
			var exp = "01Charlie   Taiwan, Tainan      \n02Charlie   Taiwan, Tainan      \n";
			var act = new StringWriter();
			var helper = new DataRowConvert(act);
			helper.WriteRecods(data);
			Assert.AreEqual(exp, act.ToString());
		}

		[Test]
		public void ReaderTextConvertToEnumClass()
		{
			var data = new List<MemberProfile> {
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
			var exp = "01Charlie   Taiwan, Tainan      N\n02Charlie   Taiwan, Tainan      G\n";
			var act = new StringWriter();
			var helper = new DataRowConvert(act);
			helper.WriteRecods(data);
			Assert.AreEqual(exp, act.ToString());
		}
	}
}
