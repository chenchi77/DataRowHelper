using System;
using System.Collections;
using System.IO;

namespace DataRowHelper
{
	public class DataRowWriter : IDataRowWriter
	{
		bool _disposed;
		RowWriter _writer;

		public virtual TextWriter TextWriter => _writer.Writer;
		public DataRowWriter(TextWriter writer, DataRowConfiguration config)
		{
			_writer = new RowWriter(writer, config);
		}

		public void WriteLine(IEnumerable objs)
		{
			foreach (var obj in objs)
			{
				var str = _writer.SetField(obj.GetType(), obj);
				_writer.WriteLine(str);
			}
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
				_writer?.Dispose();
			}
			_disposed = true;
		}
	}
}
