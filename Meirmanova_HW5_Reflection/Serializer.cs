using System;
using System.Linq;
using System.Text;

namespace Meirmanova_HW5_Reflection
{
    class Serializer
    {
        public static string ListSeparator = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;

        public static string SerializeToCsv(object obj)
        {
            var sb = new StringBuilder();

            foreach (var field in obj.GetType().GetFields(System.Reflection.BindingFlags.Public))
            {
                sb.AppendLine($"{field.Name}{ListSeparator}{field.GetValue(obj)}");
            }

            return sb.ToString();
        }

        public static T DeserializeFromCsv<T>(string csv, T ob)
        {
            var type = ob.GetType();
            var lines = csv.Split("\r\n").Where(line => !string.IsNullOrEmpty(line)).ToArray();

            var obj = Activator.CreateInstance(type);

            for (var i = 1; i < lines.Length; i++)
            {
                LoadFields(lines[i], obj);
            }

            return (T)obj;
        }

        private static void LoadFields(string field, object obj)
        {
            var parts = field.Split(ListSeparator);
            var type = obj.GetType();
            var fieldInfo = type.GetField(parts[0]);
            fieldInfo.SetValue(field, obj);
        }
    }
}