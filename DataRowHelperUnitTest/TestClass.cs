using System;
using DataRowHelper;

namespace DataRowHelperUnitTest
{
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
