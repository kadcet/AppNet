using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNET.Infrastructure.Controls
{
    public static class MyExtensions
    {
        /// <summary>
        /// String değerlerin ilk harfleri büyük,diğelerini küçük yazar
        /// </summary>
        /// <param name="thisString"></param>
        /// <returns></returns>
        public static string FirstLetterUppercase(this string thisString)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(thisString);
        }

        //public static decimal ConverDecimal(this decimal price)
        //{
        //    return Convert.ToDecimal(price);
        //}
    }
}
