using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DataRowHelper
{
	public class RowWriter : IDisposable
	{
		public TextWriter Writer => _writer;

		bool _disposed;
		TextWriter _writer;
		DataRowConfiguration _config;

		public RowWriter(TextWriter writer, DataRowConfiguration config)
		{
			_writer = writer;
			_config = config;
		}

		public void WriteLine(string line)
		{
			_writer.Write(line);
			_writer.WriteLine();
		}

		public string SetField(Type type, object obj)
		{

			var str = new Dictionary<int, string>();
			PropertyInfo[] props = type.GetProperties();
			foreach (PropertyInfo prop in props)
			{
				var att =
					prop.GetCustomAttribute(typeof(StringRangeAttribute)) as StringRangeAttribute;
				if (att == null) continue;

				string value = prop.PropertyType.IsEnum ?
					GetEnumValue(prop.GetValue(obj)) : prop.GetValue(obj) == null ?
					string.Empty : prop.GetValue(obj).ToString();

				if (prop.PropertyType == typeof(string) || prop.PropertyType.IsEnum)
					value = value.PadRight(att.Length, _config.PadString);
				else
					value = value.Replace(".", string.Empty).PadLeft(att.Length, _config.PadNumber);
				str.Add(att.StartIndex, value);
			}
			return string.Concat(str.OrderBy(x => x.Key).Select(x => x.Value).ToArray());
		}

		string GetEnumValue(object value)
		{
			string str = value.ToString();
			FieldInfo fieldInfo = value.GetType().GetField(str);
			var attribute = fieldInfo.GetCustomAttribute(
				typeof(StringValueAttribute), false) as StringValueAttribute;

			if (attribute != null && attribute.Value.Length > 0)
				str = attribute.Value;
			return str;
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
				Writer?.Dispose();
			}
			_disposed = true;
		}
	}
}
