using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace DataRowHelper
{
	public class DataRowParser
	{
		private DataRowConfiguration config = new DataRowConfiguration();

		public DataRowParser() { }

		public DataRowParser(DataRowConfiguration config) : this()
        {
			this.config = config;
		}

		public IEnumerable<T> Reader<T>(string text) where T : new()
		{
			List<T> t = new List<T>();
			if (string.IsNullOrEmpty(text)) return t;
			List<string> rows = text.Split(config.Separator, StringSplitOptions.None).ToList();

			if (config.HasHeaderRecord)
			{
			}

			foreach (string row in rows)
			{
				t.Add(ReaderProperties<T>(row));
			}
			return t;
		}

		public IEnumerable<string> Writer<T>(IEnumerable<T> objs)
		{
			var t = new List<string>();
			foreach (T obj in objs)
			{
				t.Add(WriterProperties(obj));
			}
			return t;
		}

		private T ReaderProperties<T>(string data) where T : new()
		{
			T t = new T();
			PropertyInfo[] props = typeof(T).GetProperties();
			foreach (PropertyInfo prop in props)
			{
				var att =
					prop.GetCustomAttribute(typeof(StringRangeAttribute)) as StringRangeAttribute;
				if (att == null || (att.StartIndex + att.Length) > data.Length)
					continue;

				string valueStr = data.Substring(att.StartIndex, att.Length).Trim();
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
				prop.SetValue(t, value);
			}

			return t;
		}
		private string WriterProperties<T>(T obj)
		{
			ConcurrentDictionary<int, string> dic = new ConcurrentDictionary<int, string>();
			PropertyInfo[] props = typeof(T).GetProperties();
			foreach (PropertyInfo prop in props)
			{
				var att =
					prop.GetCustomAttribute(typeof(StringRangeAttribute)) as StringRangeAttribute;
				if (att == null) continue;
				string value = prop.PropertyType.IsEnum ?
					GetEnumValue(prop.GetValue(obj)) : prop.GetValue(obj) == null ?
					string.Empty : prop.GetValue(obj).ToString();

				if (prop.PropertyType == typeof(string) || prop.PropertyType.IsEnum)
					value = value.PadRight(att.Length, config.PadString);
				else
					value = value.Replace(".", string.Empty).PadLeft(att.Length, config.PadNumber);
				dic.TryAdd(att.StartIndex, value);
			}
			return string.Concat(dic.OrderBy(x => x.Key).Select(x => x.Value).ToArray());
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

		private string GetEnumValue(object value)
		{
			string str = value.ToString();
			FieldInfo fieldInfo = value.GetType().GetField(str);
			var attribute = fieldInfo.GetCustomAttribute(
				typeof(StringValueAttribute), false) as StringValueAttribute;

			if (attribute != null && attribute.Value.Length > 0)
				str = attribute.Value;
			return str;
		}
	}
}
