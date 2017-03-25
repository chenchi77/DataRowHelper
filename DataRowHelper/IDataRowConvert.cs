using System;
using System.Collections.Generic;

namespace DataRowHelper
{
	public interface IDataRowConvert : IDisposable
	{
		IEnumerable<T> GetRecords<T>();
	}
}
