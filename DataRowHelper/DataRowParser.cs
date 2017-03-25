using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataRowHelper
{
	public class DataRowParser : IDataRowParser
	{
		private bool _disposed;
		private RowReader _reader;
		private List<dynamic> _currentData = new List<dynamic>();
		//private DataRowConfiguration _config = new DataRowConfiguration();
		public virtual TextReader TextReader => _reader.Reader;

		public DataRowParser(TextReader reader, DataRowConfiguration config)
		{
			_reader = new RowReader(reader);
			_reader.FieldEvent += OnGetField;
			//_config = config;
		}

		public void OnGetField(object sender, FieldEventArgs args)
		{
			_currentData.Add(args.RowData);
		}

		public IEnumerable<T> ReadLine<T>()
		{
			StringBuilder currentLine = new StringBuilder();
			bool lastCharIsEnd = false;
			int c;

			while ((c = _reader.GetChar()) != -1)
			{
				if (lastCharIsEnd || c == '\n')
				{
					_reader.GetField(typeof(T), currentLine.ToString());
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
				_reader.GetField(typeof(T), currentLine.ToString());
			}

			return (IEnumerable<T>)_currentData;
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
