using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PixApplication.Helper
{
    public static class Helper
    {
        public static List<KeyValuePair<int, string>> GetEnumDescriptionList<T>() where T : struct, IConvertible
        {
            // Garantir que T é um Enum
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T deve ser um tipo de enumeração.");
            }

            var result = new List<KeyValuePair<int, string>>();

            foreach (var field in typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var value = (int)field.GetValue(null);
                var description = GetDescription(field);
                result.Add(new KeyValuePair<int, string>(value, description));
            }

            return result;
        }

        private static string GetDescription(FieldInfo field)
        {
            var attribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                 .Cast<DescriptionAttribute>()
                                 .FirstOrDefault();
            return attribute?.Description ?? field.Name;
        }
    }
}
