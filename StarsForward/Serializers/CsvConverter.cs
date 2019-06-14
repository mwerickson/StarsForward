using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace StarsForward.Serializers
{
    public sealed class CsvConverter
    {
        private static class CsvValue
        {
            public static string FromDate(DateTime value)
            {
                return value.TimeOfDay.TotalSeconds == 0 ?
                    value.ToString("yyyy-MM-dd") : value.ToString("yyyy-MM-dd HH:mm:ss");
            }

            public static string FromNullableDate(DateTime? cell)
            {
                return cell.HasValue ? FromDate(cell.Value) : string.Empty;
            }

            public static string FromString(string value)
            {
                var comma = value.Contains(",");
                var quote = value.Contains("\"");

                return (comma || quote) ?
                    QuoteString(quote ? EscapeQuotes(value) : value) : value;
            }

            public static string From<T>(T obj)
            {
                return FromString(Convert.ToString(obj, CultureInfo.InvariantCulture));
            }

            private static string QuoteString(String value)
            {
                return '"' + value + '"';
            }

            private static string EscapeQuotes(String value)
            {
                return value.Replace("\"", "\"\"");
            }
        }

        private class CsvSerializer<T>
        {
            private IEnumerable<Tuple<PropertyInfo, Func<Object, string>>> PropertyValueConverters { get; set; }

            public CsvSerializer()
            {
                this.PropertyValueConverters = typeof(T).GetProperties().Select(x =>
                {
                    if (x.PropertyType == typeof(DateTime))
                        return Tuple.Create(x, (Func<Object, string>)(o => CsvValue.FromDate((DateTime)o)));
                    else if (x.PropertyType == typeof(DateTime?))
                        return Tuple.Create(x, (Func<Object, string>)(o => CsvValue.FromNullableDate((DateTime?)o)));
                    else
                        return Tuple.Create(x, (Func<Object, string>)CsvValue.From);
                }).ToList();
            }

            private IEnumerable<string> ExtractPropertyValues(T obj)
            {
                return this.PropertyValueConverters.Select(x => x.Item2(x.Item1.GetValue(obj, null)));
            }

            private IEnumerable<string> ExtractHeaders()
            {
                return this.PropertyValueConverters.Select(x => x.Item1.Name);
            }

            private string ToCsvRow(IEnumerable<string> values)
            {
                return string.Join(",", values);
            }

            public void SerializeToFile(IEnumerable<T> entries, string fileName)
            {
                using (StreamWriter stream = new StreamWriter(fileName) { NewLine = "\n" })
                {
                    stream.WriteLine(ToCsvRow(ExtractHeaders()));
                    foreach (var entry in entries)
                        stream.WriteLine(ToCsvRow(ExtractPropertyValues(entry)));
                    stream.Flush();
                }
            }

            public string Serialize(IEnumerable<T> entries)
            {
                using (var stream = new StringWriter() { NewLine = "\n" })
                {
                    stream.WriteLine(ToCsvRow(ExtractHeaders()));
                    foreach (var entry in entries)
                        stream.WriteLine(ToCsvRow(ExtractPropertyValues(entry)));
                    return stream.ToString();
                }
            }
        }

        public static string Serialize<T>(IEnumerable<T> values)
        {
            var serializer = new CsvSerializer<T>();
            return serializer.Serialize(values);
        }

        public static string Serialize<T>(IEnumerable<T> values, string fileName)
        {
            var serializer = new CsvSerializer<T>();
            serializer.SerializeToFile(values, fileName);
            return null;
        }
    }
}