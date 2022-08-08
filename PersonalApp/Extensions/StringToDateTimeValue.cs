using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalApp.Extensions
{
    public static class StringToDateTimeValue
    {
        public static DateTime? StringToDatetime(this string strLine)
        {
            DateTime result;
            if (DateTime.TryParseExact(strLine, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
