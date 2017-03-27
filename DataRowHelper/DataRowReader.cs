using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataRowHelper
{
	public class DataRowReader : IDataRowReader
	{
		public virtual TextReader TextReader => _reader.Reader;

		bool _disposed;
		RowReader _reader;
		List<dynamic> _currentData = new List<dynamic>();

		public DataRowReader(TextReader reader, DataRowConfiguration config)
		{
			_reader = new RowReader(reader, config);
			_reader.FieldEvent += OnGetField;
		}

		public void OnGetField(object sender, FieldEventArgs args)
		{
			_currentData.Add(args.RowData);
		}

		public IEnumerable<dynamic> ReadLine(Type type)
		{
			StringBuilder currentLine = new StringBuilder();
			bool lastCharIsEnd = false;
			int c;

			while ((c = _reader.GetChar()) != -1)
			{
				if (lastCharIsEnd || c == '\n')
				{
					_reader.GetField(type, currentLine.ToString());
					currentLine.Length = 0;
					lastCharIsEnd = false;
					if (c == '\n') continue;
				}

				if (c == '\r')
				{
					lastCharIsEnd = true;
					continue;
				}

				currentLine.Append((char)c);
			}

			if (currentLine.Length > 0)
			{
				_reader.GetField(type, currentLine.ToString());
			}

			return _currentData;
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
				_currentData = null;
			}
			_disposed = true;
		}
	}
}
