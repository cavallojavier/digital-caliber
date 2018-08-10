using System;
using System.Collections.Generic;
using System.Text;

namespace digital.caliber.services.Extensions
{
    public static class DecimalExtensions
    {
        public static string ToStringDecimal(this decimal? number)
        {
            return number.HasValue ? number.ToString() : "-";
        }
    }
}
