using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace DataRowHelper
{
	public class RowReader : IDisposable
	{
		public delegate void FieldHandler(object sender, FieldEventArgs args);
		public event FieldHandler FieldEvent;

		bool _disposed;
		readonly char[] _buffer;
		TextReader _reader;
		int _charCount;
		int _currentPosition;

		public TextReader Reader => _reader;

		public RowReader(TextReader reader)
		{
			_reader = reader;
			_buffer = new char[2048];
		}

		public int GetChar()
		{
			if (_currentPosition < _charCount)
			{
				return _buffer[_currentPosition++];
			}

			_charCount = Reader.Read(_buffer, 0, _buffer.Length);
			if (_charCount == 0) return -1;

			return _buffer[_currentPosition++];
		}

		public void GetField(Type t, string line)
		{
			var obj = Activator.CreateInstance(t);
			PropertyInfo[] props = t.GetProperties();
			foreach (PropertyInfo prop in props)
			{
				var att =
					prop.GetCustomAttribute(typeof(StringRangeAttribute)) as StringRangeAttribute;
				if (att == null || (att.StartIndex + att.Length) > line.Length)
					continue;

				string valueStr = line.Substring(att.StartIndex, att.Length).Trim();
				if (string.IsNullOrEmpty(valueStr))
				{
					prop.SetValue(t, null);
					continue;
				}
				if (prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(float))
					valueStr = valueStr.Insert(valueStr.Length - 2, ".");

				if (prop.PropertyType.IsEnum)
					valueStr = GetEnum(prop.PropertyType, valueStr).ToString();

				TypeConverter typeConverter = TypeDescriptor.GetConverter(prop.PropertyType);
				var value = typeConverter.ConvertFromString(valueStr);
				prop.SetValue(obj, value);
			}

			FieldEvent?.Invoke(this, new FieldEventArgs(obj));
		}

		private object GetEnum(Type type, string value)
		{
			foreach (var field in type.GetFields())
			{
				var attribute = Attribute.GetCustomAttribute(field,
					typeof(StringValueAttribute)) as StringValueAttribute;
				if ((attribute != null && attribute.Value == value) || field.Name == value)
					return field.GetValue(null);
			}
			return value;
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
				Reader?.Dispose();
			}
			_disposed = true;
		}

	}
}
