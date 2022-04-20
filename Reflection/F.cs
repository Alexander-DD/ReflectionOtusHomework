using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public class F
    {
        [JsonProperty]
        int i1, i2, i3, i4, i5;
        public static F Get() => new F()
        {
            i1 = 1,
            i2 = 2,
            i3 = 3,
            i4 = 4,
            i5 = 5
        };

        public override string ToString()
        {
            FieldInfo[] fieldInfos = typeof(F).GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            StringBuilder sb = new StringBuilder();

            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                sb.Append(fieldInfo.Name);
                sb.Append(" = ");
                sb.Append(fieldInfo.GetValue(this).ToString());
                sb.Append(", ");
            }

            return sb.ToString();
        }
    }
}
