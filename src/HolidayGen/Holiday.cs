/*****************************************************************************
@ Author         :   Suresh Kalavala
@ Date           :   10/29/2017
@ Class          :   Holiday
@ Description    :   Abstract class that defines Holiday
                    All the classes that implements this abstract class must
                    implement the following three methods at the least:
                    - GetName - returns name of the holiday package
                    - PopulateStaticHolidays returns a dict - date and title
                    - PopulateDynamicHolidays returns a dict - date and title
******************************************************************************/

using System;
using System.Collections.Generic;

namespace HolidayGen
{
    public abstract class Holiday
    {
        // static information, no need to hard code in each class
        public readonly static string STATIC = "static";
        public readonly static string DYNAMIC = "dynamic";

        public readonly static int NUMBER_OF_YEARS = 5;

        protected enum Month
        {
            January = 1,
            February,
            March,
            April,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December
        }

        protected enum Week
        {
            FirstWeek = 1,
            SecondWeek,
            ThirdWeek,
            FourthWeek,
            FifthWeek,
        }

        /// <summary>
        /// Public Properties
        /// </summary>
        public string Name { get; }
        public Dictionary<string, Dictionary<string, string>> StaticHolidays { get; }
        public Dictionary<string, Dictionary<string, string>> DynamicHolidays { get; }
        
        /// <summary>
        /// Abstract methods, that require implementation
        /// </summary>

        // need the name of the implementer
        abstract protected string GetName();

        // method to populate static holidays - the ones that doesn't change
        abstract protected Dictionary<string, string> PopulateStaticHolidays();

        // method to populate dynamic holidays - the ones that change dynamically
        // for ex: Thanksgiving is always on comes on 4th Thursday in November, 
        // but the date can change every year
        abstract protected Dictionary<string, string> PopulateDynamicHolidays();

        /// <summary>
        /// Constructor of the Holiday Class
        /// </summary>
        public Holiday()
        {
            // initialize dictionary objects
            StaticHolidays = new Dictionary<string, Dictionary<string, string>>(16);
            DynamicHolidays = new Dictionary<string, Dictionary<string, string>>(16);

            // get the name and holidays populated
            Name = GetName();

            StaticHolidays.Add(STATIC, PopulateStaticHolidays());
            DynamicHolidays.Add(DYNAMIC, PopulateDynamicHolidays());
        }

        /// <summary>
        /// Helper method
        /// </summary>
        /// <param name="month"></param>
        /// <param name="occurance"></param>
        /// <param name="dayOfWeek"></param>
        /// <param name=""></param>
        /// <returns></returns>
        protected Dictionary<string, string> GetHolidayList(int month, int occurance, DayOfWeek dayOfWeek, string holidayTitle)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            for (int i = DateTime.Now.Year; i < DateTime.Now.Year + NUMBER_OF_YEARS; i++)
            {
                DateTime dt = NthOf(new DateTime(i, month, 1), occurance, dayOfWeek);
                dict.Add(dt.Month + "/" + dt.Day + "/" + dt.Year, holidayTitle);
            }
            return dict;
        }

        /// <summary>
        /// Utility method that returns specific occurance of a day in a given date
        /// </summary>
        /// <param name="CurDate"></param>
        /// <param name="Occurrence"></param>
        /// <param name="Day"></param>
        /// <returns></returns>
        protected static DateTime NthOf(DateTime CurDate, int Occurrence, DayOfWeek Day)
        {
            var fday = new DateTime(CurDate.Year, CurDate.Month, 1);

            var fOc = fday.DayOfWeek == Day ? fday : fday.AddDays(Day - fday.DayOfWeek);

            // CurDate = 2011.10.1 Occurance = 1, Day = Friday >> 2011.09.30 FIX. 
            if (fOc.Month < CurDate.Month)
                Occurrence = Occurrence + 1;

            return fOc.AddDays(7 * (Occurrence - 1));
        }
    }
}
