using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Realms;

namespace StarsForward.Serializers
{
    public class CsvSerializer
    {
        public string Serialize<T>(List<T> items, string parentName, bool includeId = false) where T : class
        {
            var output = string.Empty;
            var delimiter = ",";
            var modelType = typeof(T);
            var props = typeof(T).GetProperties()
                .Where(n =>
                    n.PropertyType == typeof(string)
                    || n.PropertyType == typeof(bool)
                    || n.PropertyType == typeof(char)
                    || n.PropertyType == typeof(byte)
                    || n.PropertyType == typeof(decimal)
                    || n.PropertyType == typeof(int)
                    || n.PropertyType == typeof(DateTime)
                    || n.PropertyType == typeof(DateTime?));

            using (var sw = new StringWriter())
            {
                var header = props
                    .Where(x => x.Name != "Id")
                    .Select(n => n.Name)
                    .Aggregate((a, b) => a + delimiter + b);
                //header.Insert(0, parentName);
                sw.WriteLine(header);

                foreach (var item in items)
                {
                    var row = props
                        .Where(x => x.Name != "Id")
                        .Select(n => n.GetValue(item, null))
                        .Select(n => n == null ? "null" : n.ToString())
                        .Aggregate((a, b) => a + delimiter + b);
                    sw.WriteLine(row);
                }
                output = sw.ToString();
            }
            return output;

        }
    }
}