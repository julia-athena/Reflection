using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public class CsvContent<T>: Content<T> where T: class, new()
    {
        public readonly string Csv;
        public readonly CsvFormat Format;

        private readonly BindingFlags BindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;


        public CsvContent(string csv): this(csv, new CsvFormat()) { }
        public CsvContent(string csv, CsvFormat csvFormat)
        {
            Csv = csv;
            Format = csvFormat;
        }
        public IList<T> Data()
        {
            var lines = Lines();
            return DeserializedData(lines);
        }

        private string[] Lines()
        {
            var lines = Csv.Split(Format.RowSeparator);
            return lines;
        }

        private IList<T> DeserializedData(string[] lines)
        {
            string[] columns = lines[0].Split(Format.ValueSeparator);
            var ListData = new List<T>();
            for(int i = 1; i < lines.Length; i++)
            {
                ListData.Add(DeserialisedT(columns, lines[i]));
            }

            return ListData;
        }

        private T DeserialisedT(string[] columns, string line)
        {
            var values = line.Split(Format.ValueSeparator);
            if (columns.Length != values.Length) throw new ApplicationException("Csv has invalid format. Columns != Values");
            var item = new T();
            var type = item.GetType();
            for(int i = 0; i < columns.Length; i++)
            {
                var member = type.GetMember(columns[i], BindingFlags)?[0];
                if (member.MemberType == MemberTypes.Field) type.GetField(columns[i], BindingFlags).SetValue(item,values[i]); //todo сконвертировать значение к типу поля
                else if (member.MemberType == MemberTypes.Property) type.GetProperty(columns[i], BindingFlags).SetValue(item, values[i]); //todo сконвертировать значение к типу свойства
            }
            return item;
        }

    }
}
