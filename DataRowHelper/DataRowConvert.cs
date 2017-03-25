using System;
using System.Collections.Generic;
using System.IO;

namespace DataRowHelper
{
	public class DataRowConvert : IDataRowConvert
	{
		private IDataRowParser parser;

		public DataRowConvert(TextReader reader) : this(reader, new DataRowConfiguration()) { }

		public DataRowConvert(TextReader reader, DataRowConfiguration config) : this(new DataRowParser(reader, config)) { }

		public DataRowConvert(IDataRowParser parser) {
			this.parser = parser;
		}

		public IEnumerable<T> GetRecords<T>()
		{
			return parser.ReadLine<T>();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
