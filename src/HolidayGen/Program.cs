﻿/*****************************************************************************
@ Author         :   Suresh Kalavala
@ Date           :   10/29/2017
@ Class          :   Program
@ Description    :   Entry to the application
******************************************************************************/
using System;
using System.Collections.Generic;

namespace HolidayGen
{
    public class Program
    {
        public void Main(string[] args)
        {
            Console.WriteLine(HolidayGenerator.Generate());
        }
    }
}