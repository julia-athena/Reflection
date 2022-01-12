using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public class CsvMedia<T>: Media<T> where T: class, new()
    {
        public readonly IList<T> Data;
        public readonly CsvFormat Format;
        private readonly BindingFlags BindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;

        public CsvMedia(): this(new List<T>(), new CsvFormat()) { }

        public CsvMedia(List<T> data, CsvFormat format)
        {
            Data = data;
            Format = format;
        }

        public Media<T> MediaWith(T input)
        {
            var DataWith = new List<T>(Data);
            DataWith.Add(input);
            return new CsvMedia<T>(DataWith, Format);
        }

        public String AsString()
        {
            List<string> columns = Columns();
            string header = Header(columns);
            string body = Body(columns);
            return $"{header}{Format.RowSeparator}{body}";
        }

        private List<string> Columns()
        {
            var type = typeof(T);
            var members = type.GetMembers(BindingFlags);
            var columns = new List<string>();
            foreach (var m in members)
            {
                if (m.MemberType == MemberTypes.Field || m.MemberType == MemberTypes.Property) columns.Add(m.Name);
            }
            columns.Sort();
            return columns;
        }

        private string Header(List<string> sortedColumns)
        {
            var builder = new StringBuilder("");
            if (Format.AutoHeader == true)
            {
                foreach (var c in sortedColumns)
                {
                    builder.Append($"{c}{Format.ValueSeparator}");
                }
            } 
            return builder.ToString();
        }

        private string Body(List<string> sortedColumns)
        {
            var builder = new StringBuilder();
            foreach (var item in Data)
            {
                builder.Append($"{OneLineForBody(item, sortedColumns)}{Format.RowSeparator}");
            }
            return builder.ToString();
        }

        private string OneLineForBody(T item, List<string> sortedColumns)
        {
            var builder = new StringBuilder();
            var type = typeof(T);
            foreach (var column in sortedColumns)
            {
                builder.Append($"{ColumnValue(item,column)}{Format.ValueSeparator}");
            }
            return builder.ToString();
        }

        private string ColumnValue(T item, string column)
        {
            var member = item.GetType().GetMember(column, BindingFlags);
            string res = "";
            if (member is not null && member.Length > 0)
            {
                if (member[0].MemberType == MemberTypes.Property) res = item.GetType().GetProperty(column,BindingFlags).GetValue(item).ToString();
                else if (member[0].MemberType == MemberTypes.Field) res = item.GetType().GetField(column, BindingFlags).GetValue(item).ToString();
            }
            return res;
        }
    }

    public class CsvFormat
    {
        public string ValueSeparator = ";";
        public string RowSeparator = "\n";
        public bool AutoHeader = true;
    }
}
