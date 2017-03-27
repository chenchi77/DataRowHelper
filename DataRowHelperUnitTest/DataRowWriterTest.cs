using System.Collections.Generic;
using System.IO;
using System.Text;
using DataRowHelper;
using Newtonsoft.Json;
using NUnit.Framework;

namespace DataRowHelperUnitTest
{
	[TestFixture]
	public class DataRowWriterTest
	{
		[Test]
		public void WriterTextConvertToGeneralClass()
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
			var exp = new StringWriter();
			exp.WriteLine("01Charlie   Taiwan, Tainan      ");
			exp.WriteLine("02Charlie   Taiwan, Tainan      ");

			var act = new StringWriter();
			var helper = new DataRowConvert(act);
			helper.WriteRecods(data);
			Assert.AreEqual(exp.ToString(), act.ToString());
		}

		[Test]
		public void WriterTextConvertToEnumClass()
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
			var exp = new StringWriter();
			exp.WriteLine("01Charlie   Taiwan, Tainan      N");
			exp.WriteLine("02Charlie   Taiwan, Tainan      G");

			var act = new StringWriter();
			var helper = new DataRowConvert(act);
			helper.WriteRecods(data);
			Assert.AreEqual(exp.ToString(), act.ToString());
		}
	}
}
