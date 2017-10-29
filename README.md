# List of Holidays as JSON

This code generates holidays list of various countries and types in a simple and easy to access JSON format. The codebase is easy to extend and add holidays of other countries.

One advantage of having a JSON is once fully populated, and uploaded to GitHub, it can be accessed using simple HTTP Get, adnd the information can be parsed to meet your needs easily.

The output of this project would look like this --> https://raw.githubusercontent.com/skalavala/smarthome/master/holidays.json

In my Home Assistant (Home Automation software), I have a `Rest` based sensor that queries every 12 hours to find out if the current day is a public holiday or not. If it is, it automatically does some announcements, and does various things... I simply use the JSON in my repo to query and no need to host it o any server as long as there is a way to access the URL over the web, you are in business!

The code is highly extensible, and you can add other country's holidays just as easily.  Please follow the simple steps to get started.

## Development Environment
* You need Visual Studio or VSCode that supports C# and Microsoft .Net runtime
* Make sure you can connect to GitHub and sync the code locally and publish when you are done making changes

## Design

* Extremely simple design - Just one class to implement
* The Base class `Holiday` is an abstract class with some base functionality already built-in
* The implemented class (for ex: `UKHolidays`) implements the following required three methods:
  * `GetName() returns name of the implemented class that should go into JSON`
  * `PopulateStaticHolidays() returns a Dictionary of holidays `
  * `PopulateDynamicHolidays() returns a Dictionary of holidays`

`GetName()` returns a name of the holidays list - for ex: `UK_HOLIDAYS`.
`PopulateStaticHolidays()` method simply returns a bunch of hard coded dictionary items (date and title)
`PopulateDynamicHolidays()` method has most of the logic to return appropriate holidays based on some `dynamic` logic

The base class has a method `GetHolidayList()` that takes a few parameters like `month`, `occurance`, `DayOfWeek` and `title` that gives a bunch fo holidays list for a known pattern... for ex: To get the exact date of a holiday that occurs  Every 3rd Tuesday of August, you can pass a few parameters, and it does the job for you.

The base abstract class `Holiday` has a public static variable named, `NUMBER_OF_YEARS` - that is used to generate dates for that many years. If that number is 10, each holiday for the next 10 years will be generated.

Refer to the existing holidays code for more information. For any questions, feel free to create an issue or do a PR :smile:

## The following is a sample class to create holidays list for UK...

```c#
using System;
using System.Collections;
using System.Collections.Generic;

namespace HolidayGen
{
    public class UKHolidays : Holiday
    {
        private readonly static string HOLIDAY_PACKAGE = "UK_HOLIDAYS";

        // Static names... add more hard coded names, so that you have one place to modify later
        private readonly static string HOLIDAY_QUEENS_BIRTHDAY = "Queen's Birthday!";
		private readonly static string HOLIDAY_MOTHERSDAY = "Mother's Day!";

        protected override string GetName()
        {
            return HOLIDAY_PACKAGE;
        }

        protected override Dictionary<string, string> PopulateStaticHolidays()
        {
            Dictionary<string, string> staticHolidays = new Dictionary<string, string>(16);

            staticHolidays.Add("1/1", "New Year's Day");
			// .... add more static holidays
            staticHolidays.Add("12/31", "New Year's Eve");

            return staticHolidays;
        }

        protected override Dictionary<string, string> PopulateDynamicHolidays()
        {
            Dictionary<string, string> dynamicHolidays = new Dictionary<string, string>(16);

            // Mother's Day - 2nd Monday of every May
            foreach (var item in GetHolidayList((int)Month.May, (int)Week.SecondWeek, DayOfWeek.Sunday, HOLIDAY_MOTHERSDAY))
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
```
