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
    public class USHolidays : Holiday
    {
        private readonly static string HOLIDAY_PACKAGE = "MAJOR_US";

        // Static names
        private readonly static string HOLIDAY_THANKSGIVING = "Thanks Giving";
        private readonly static string HOLIDAY_MOTHERSDAY = "Mother's Day";
        private readonly static string HOLIDAY_MLKJR = "Martin Luther King Jr. Day";
        private readonly static string HOLIDAY_FATHERSDAY = "Father's Day";
        private readonly static string HOLIDAY_MEMORIAL_DAY = "Memorial Day";
        private readonly static string HOLIDAY_COLUMBUSDAY = "Columbus Day";
        private readonly static string HOLIDAY_LABORDAY = "Labor Day";
        private readonly static string HOLIDAY_PRESIDENTSDAY = "Presidents Day";

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
            staticHolidays.Add("3/17", "St. Patrick's Day");
            staticHolidays.Add("4/22", "Earth Day");
            staticHolidays.Add("7/4", "Independence Day");
            staticHolidays.Add("9/11", "Patriot Day");
            staticHolidays.Add("10/31", "Halloween");
            staticHolidays.Add("11/11", "Veterans' Day");
            staticHolidays.Add("12/7", "Pearl Harbor Day");
            staticHolidays.Add("12/25", "Christmas Day");
            staticHolidays.Add("12/31", "New Year's Eve");

            return staticHolidays;
        }

        protected override Dictionary<string, string> PopulateDynamicHolidays()
        {
            Dictionary<string, string> dynamicHolidays = new Dictionary<string, string>(16);

            // MLK Jr - 3rd Monday of every January
            foreach (var item in GetHolidayList((int)Month.January, (int)Week.ThirdWeek, DayOfWeek.Monday, HOLIDAY_MLKJR))
                dynamicHolidays.Add(item.Key, item.Value);

            // Thanksgiving - 4th Thursday of every November
            foreach (var item in GetHolidayList((int) Month.November, (int) Week.FourthWeek, DayOfWeek.Thursday, HOLIDAY_THANKSGIVING))
                dynamicHolidays.Add(item.Key, item.Value);

            // Mother's Day - 2nd Monday of every May
            foreach (var item in GetHolidayList((int)Month.May, (int)Week.SecondWeek, DayOfWeek.Sunday, HOLIDAY_MOTHERSDAY))
                dynamicHolidays.Add(item.Key, item.Value);

            // Father's Day:	3rd Sunday in June
            foreach (var item in GetHolidayList((int)Month.June, (int)Week.ThirdWeek, DayOfWeek.Sunday, HOLIDAY_FATHERSDAY))
                dynamicHolidays.Add(item.Key, item.Value);

            // Last Monday in May - Memorial day weekend
            foreach (var item in GetHolidayList((int)Month.May, (int)Week.FourthWeek, DayOfWeek.Monday, HOLIDAY_MEMORIAL_DAY))
                dynamicHolidays.Add(item.Key, item.Value);

            // Columbus Day:	2nd Monday in October
            foreach (var item in GetHolidayList((int)Month.October, (int)Week.SecondWeek, DayOfWeek.Monday, HOLIDAY_COLUMBUSDAY))
                dynamicHolidays.Add(item.Key, item.Value);

            // Labor Day:	1st Monday in September
            foreach (var item in GetHolidayList((int)Month.September, (int)Week.FirstWeek, DayOfWeek.Monday, HOLIDAY_LABORDAY))
                dynamicHolidays.Add(item.Key, item.Value);

            // Presidents Day:	3rd Monday in Feb
            foreach (var item in GetHolidayList((int)Month.February, (int)Week.ThirdWeek, DayOfWeek.Monday, HOLIDAY_PRESIDENTSDAY))
                dynamicHolidays.Add(item.Key, item.Value);

            return dynamicHolidays;
        }
    }
}