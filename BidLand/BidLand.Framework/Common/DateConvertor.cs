using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidLand.Framework.Common
{
    public static class DateConvertor
    {
        public static string ConvertGregorianToPersian(DateTime miladiDate)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            int year = persianCalendar.GetYear(miladiDate);
            int month = persianCalendar.GetMonth(miladiDate);
            int day = persianCalendar.GetDayOfMonth(miladiDate);
            int hour = persianCalendar.GetHour(miladiDate);
            int minute = persianCalendar.GetMinute(miladiDate);
            int second = persianCalendar.GetSecond(miladiDate);
            string amPmDesignator = persianCalendar.GetHour(miladiDate) < 12 ? "AM" : "PM";

            string persianDateTime = string.Format("{0}/{1}/{2} {3}:{4}:{5} {6}", year, month, day, hour, minute, second, amPmDesignator);

            return persianDateTime;
        }
    }
}
