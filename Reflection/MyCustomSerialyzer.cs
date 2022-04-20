using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    /// <summary> Serializer </summary>
    public static class MyCustomSerialyzer
    {
        /// <summary> Serialize from object to CSV </summary>
        /// <param name="obj">any object</param>
        /// <returns>CSV</returns>
        public static string SerializeFromObjectToCSV(object input)
        {
            Type inputType = input.GetType();

            FieldInfo[] fieldInfos = inputType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            StringBuilder sb = new StringBuilder();

            sb.Append(inputType.FullName);

            foreach (var field in fieldInfos)
            {
                sb.AppendLine();
                sb.Append(field.FieldType);
                sb.Append('\u002C');
                sb.Append(field.Name);
                sb.Append('\u002C');
                sb.Append(field.GetValue(input).ToString());
            }

            return sb.ToString();
        }

        /// <summary> Deserialize from CSV to object</summary>
        /// <param name="csv">string in CSV format</param>
        /// <returns>object</returns>
        public static object DeserializeFromCSVToObject(string csv)
        {
            string[] lines = csv.Split("\r\n");

            Type outputType = Type.GetType(typeName: lines[0]);
            object outputObj = Activator.CreateInstance(outputType);

            foreach (var line in lines.Skip(1))
            {
                string[] parts = line.Split('\u002C');

                Type fieldType = Type.GetType(typeName: parts[0]);

                FieldInfo fieldInfo = outputType.GetField(parts[1], BindingFlags.Instance | BindingFlags.NonPublic);

                fieldInfo.SetValue(obj: outputObj, value: Convert.ChangeType(parts[2], fieldType));
            }

            return outputObj;
        }
    }
}
