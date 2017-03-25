using System;
using System.Collections.Generic;

namespace DataRowHelper
{
	public interface IDataRowParser : IDisposable
	{
		IEnumerable<dynamic> ReadLine(Type type);
	}
}
