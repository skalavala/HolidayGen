/*****************************************************************************
@ Author         :   Suresh Kalavala
@ Date           :   10/29/2017
@ Class          :   HolidayGenerator
@ Description    :   Generates JSON output from all Holiday Packages
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace HolidayGen
{
    public class HolidayGenerator
    {
        /// <summary>
        /// Public static method that iterates through all the implementations of Holiday abstract class
        /// and creates a JSON output for consumption
        /// </summary>
        /// <returns>JSON representation of ALL Holidays</returns>
        public static string Generate()
        {
            IEnumerable<Holiday> holidayPackages = typeof(Holiday).GetTypeInfo().Assembly.GetTypes()
                .Where(t => t.GetTypeInfo().IsSubclassOf(typeof(Holiday)) && !t.GetTypeInfo().IsAbstract)
                .Select(t => (Holiday)Activator.CreateInstance(t));

            StringBuilder sb = ConvertToJson(holidayPackages);
            return sb.ToString();
        }

        private static StringBuilder ConvertToJson(IEnumerable<Holiday> holidayPackages)
        {
            StringBuilder sb = new StringBuilder();
            JsonWriter jw = new JsonTextWriter(new StringWriter(sb));
            jw.Formatting = Formatting.Indented;

            jw.WriteStartObject();
            foreach (var holidayPackage in holidayPackages)
            {
                StaticHolidays(jw, holidayPackage);
                DynamicHolidays(jw, holidayPackage);

                jw.WriteEndObject();
            }
            jw.WriteEndObject();
            return sb;
        }

        private static void DynamicHolidays(JsonWriter jw, Holiday holidayPackage)
        {
            foreach (var h in holidayPackage.DynamicHolidays)
            {
                jw.WritePropertyName(h.Key);
                jw.WriteStartObject();
                foreach (var x in h.Value)
                {
                    jw.WritePropertyName(x.Key);
                    jw.WriteValue(x.Value);
                }
                jw.WriteEndObject();
            }
        }

        private static void StaticHolidays(JsonWriter jw, Holiday holidayPackage)
        {
            jw.WritePropertyName(holidayPackage.Name);
            jw.WriteStartObject();
            foreach (var h in holidayPackage.StaticHolidays)
            {
                jw.WritePropertyName(h.Key);
                jw.WriteStartObject();
                foreach (var x in h.Value)
                {
                    jw.WritePropertyName(x.Key);
                    jw.WriteValue(x.Value);
                }
                jw.WriteEndObject();
            }
        }
    }
}