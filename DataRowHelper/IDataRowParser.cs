using System;
using System.Collections.Generic;

namespace DataRowHelper
{
	public interface IDataRowParser : IDisposable
	{
		IEnumerable<T> ReadLine<T>();
	}
}
