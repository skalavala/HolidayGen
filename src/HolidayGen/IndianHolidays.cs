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
            indianHolidays.Add("11/2", "Diwali");
            indianHolidays.Add("9/10", "Dussehra");
            return indianHolidays;
        }

        protected override Dictionary<string, string> PopulateDynamicHolidays()
        {
            Dictionary<string, string> indianHolidays = new Dictionary<string, string>(16);
            indianHolidays.Add("1/1", "New Year");
            indianHolidays.Add("11/2", "Diwali");
            indianHolidays.Add("9/10", "Dussehra");
            return indianHolidays;
        }
    }
}