/*****************************************************************************
@ Author         :   Suresh Kalavala
@ Date           :   10/29/2017
@ Class          :   USHolidays
@ Description    :   All USA Holidays and respective code goes in here
******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;

namespace HolidayGen
{
    public class CanadianHolidays : Holiday
    {
        private readonly static string HOLIDAY_PACKAGE = "MAJOR_CA";

        // Static names
        private readonly static string HOLIDAY_THANKSGIVING = "Thanks Giving";
        private readonly static string HOLIDAY_MOTHERSDAY = "Mother's Day";
        private readonly static string HOLIDAY_FATHERSDAY = "Father's Day";
        private readonly static string HOLIDAY_LABORDAY = "Labor Day";
        private readonly static string HOLIDAY_VICTORIADAY = "Victoria Day";

        protected override string GetName()
        {
            return HOLIDAY_PACKAGE;
        }

        /// <summary>
        /// Static or Fixed Holidays
        /// </summary>
        /// <returns>List of holidays that always fall on same date, every year</returns>
        protected override Dictionary<string, string> PopulateStaticHolidays()
        {
            Dictionary<string, string> staticHolidays = new Dictionary<string, string>(16);

            staticHolidays.Add("1/1", "New Year's Day");
            staticHolidays.Add("2/2", "Groundhog Day");
            staticHolidays.Add("2/14", "Valentine's Day");
            staticHolidays.Add("7/1", "Canada Day");
            staticHolidays.Add("11/11", "Remembrance Day");
            staticHolidays.Add("12/25", "Christmas Day");
            staticHolidays.Add("12/26", "Boxing Day");
            staticHolidays.Add("12/31", "New Year's Eve");

            return staticHolidays;
        }

        protected override Dictionary<string, string> PopulateDynamicHolidays()
        {
            Dictionary<string, string> dynamicHolidays = new Dictionary<string, string>(16);

            // Thanksgiving - 4th Thursday of every November
            foreach (var item in GetHolidayList((int) Month.November, (int) Week.FourthWeek, DayOfWeek.Thursday, HOLIDAY_THANKSGIVING))
                dynamicHolidays.Add(item.Key, item.Value);

            // Mother's Day - 2nd Monday of every May
            foreach (var item in GetHolidayList((int)Month.May, (int)Week.SecondWeek, DayOfWeek.Sunday, HOLIDAY_MOTHERSDAY))
                dynamicHolidays.Add(item.Key, item.Value);

            // Father's Day:	3rd Sunday in June
            foreach (var item in GetHolidayList((int)Month.June, (int)Week.ThirdWeek, DayOfWeek.Sunday, HOLIDAY_FATHERSDAY))
                dynamicHolidays.Add(item.Key, item.Value);

            // Labor Day:	1st Monday in September
            foreach (var item in GetHolidayList((int)Month.September, (int)Week.FirstWeek, DayOfWeek.Monday, HOLIDAY_LABORDAY))
                dynamicHolidays.Add(item.Key, item.Value);

            // Victoria Day - Last Monday preceding May 25. If it falls on 25, go back to previous Monday
            Dictionary<string, string> victoriaDayDict = PolulateVictoriaDay();
            foreach (var item in victoriaDayDict)
                dynamicHolidays.Add(item.Key, item.Value);

            return dynamicHolidays;
        }

        /// <summary>
        /// Populate Victoria Day
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> PolulateVictoriaDay()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            for (int i = DateTime.Now.Year; i < DateTime.Now.Year + NUMBER_OF_YEARS; i++)
            {
                DateTime date = new DateTime(i, (int)Month.May, 25);
                int offset = date.DayOfWeek - DayOfWeek.Monday;
                if (offset == 0)
                {
                    // if the offset is zero, that means 25th is Monday. We need to go back 7 days
                    offset = 7;
                }

                DateTime lastMonday = date.AddDays(-offset);

                dict.Add(lastMonday.Month + "/" + lastMonday.Day + "/" + lastMonday.Year, HOLIDAY_VICTORIADAY);
            }
            return dict;
        }
    }
}