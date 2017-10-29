/*****************************************************************************
@ Author         :   Suresh Kalavala
@ Date           :   10/29/2017
@ Class          :   IndianHolidays
@ Description    :   All Indian Holidays and respective code goes in here
******************************************************************************/
using System.Collections.Generic;

namespace HolidayGen
{
    public class IndianHolidays : Holiday
    {
        private readonly static string HOLIDAY_PACKAGE = "INDIAN_HOLIDAYS";
        protected override string GetName()
        {
            return HOLIDAY_PACKAGE;
        }

        protected override Dictionary<string, string> PopulateStaticHolidays()
        {
            Dictionary<string, string> indianHolidays = new Dictionary<string, string>(16);
            indianHolidays.Add("1/1", "New Year");
            indianHolidays.Add("1/14", "Sankranthi");
            indianHolidays.Add("1/26", "Republic Day");
            indianHolidays.Add("3/1", "Holi");
            indianHolidays.Add("3/18", "Ugadi");
            indianHolidays.Add("3/25", "Srirama Navami");
            indianHolidays.Add("8/15", "Independence Day");
            indianHolidays.Add("9/3", "Janmastami");
            indianHolidays.Add("10/02", "Gandhi's Birthday");
            indianHolidays.Add("10/17", "Durga Astami");
            indianHolidays.Add("10/19", "Dussehra");
            indianHolidays.Add("11/07", "Diwali");
            return indianHolidays;
        }

        protected override Dictionary<string, string> PopulateDynamicHolidays()
        {
            Dictionary<string, string> indianHolidays = new Dictionary<string, string>(16);
            return indianHolidays;
        }
    }
}