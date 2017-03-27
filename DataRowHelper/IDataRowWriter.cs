using System;
using System.Collections;

namespace DataRowHelper
{
	public interface IDataRowWriter : IDisposable
	{
		void WriteLine(IEnumerable objs);
	}
}
