using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataRowHelper
{
	public class DataRowConvert : IDataRowConvert
	{
		bool _disposed;
		IDataRowReader _reader;
		IDataRowWriter _writer;

		public DataRowConvert(TextReader reader) : this(reader, new DataRowConfiguration()) { }

		public DataRowConvert(TextReader reader, DataRowConfiguration config) : this(new DataRowReader(reader, config)) { }

		public DataRowConvert(IDataRowReader reader)
		{
			_reader = reader;
		}

		public DataRowConvert(TextWriter writer) : this(writer, new DataRowConfiguration()) { }

		public DataRowConvert(TextWriter writer, DataRowConfiguration config) : this(new DataRowWriter(writer, config)){ }

		public DataRowConvert(IDataRowWriter writer)
		{
			_writer = writer;
		}

		public IEnumerable<T> ReadRecords<T>()
		{
			return _reader.ReadLine(typeof(T)).Cast<T>().ToList();
		}

		public void WriteRecods(IEnumerable objs)
		{
			_writer.WriteLine(objs);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed) return;

			if (disposing)
			{
				_reader?.Dispose();
				_writer?.Dispose();
			}
			_disposed = true;
		}
	}
}
