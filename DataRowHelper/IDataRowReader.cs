using System;
using System.Collections.Generic;

namespace DataRowHelper
{
	public interface IDataRowReader : IDisposable
	{
		IEnumerable<dynamic> ReadLine(Type type);
	}
}
