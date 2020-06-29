using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandsAPI.Helpers
{
    public static class FoundYearsAgo
    {
        public static int GetYearsAgo(this DateTime dateTime)
        {
            var currentDate = DateTime.Now;
            return currentDate.Year - dateTime.Year;
        }
    }
}
