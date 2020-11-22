//Author: Adar Kahiri
//Project name: mP11
//File name: Program.cs
//Date created: Nov 20, 2020
//Date modified: Nov 20, 2020
//Description: This program defines a method for calculating fibonacci numbers and keeps track of its execution speed.

using System;
using System.Diagnostics;


namespace mP11
{
    class Program
    {
        static Stopwatch stopWatch = new Stopwatch();

        static void Main(string[] args)
        {
            int fibNum;

            //Resetting and starting the timer
            stopWatch.Reset();
            stopWatch.Start();

            fibNum = CalcFib(46);
            //Stopping the timer as soon as the Fibonacci number is calculated
            stopWatch.Stop();

            Console.WriteLine(fibNum);

            //Printing how long it took to calculate the fibonacci number
            Console.WriteLine(GetTimeOutput(stopWatch));

            Console.WriteLine(MysterySub(6, 2, 5));
        }

        //Pre: must be a positive integer
        //Post: returns the sum of the previous two Fibonacci numbers.
        //Description: this method recursively calculates the value of the nth fibonacci number.
        public static int CalcFib(int n)
        {
            //If n = 1 or n = 2, then the nth fibonacci number is 1 (the fibonacci sequence starts with 1 as its first 2 numbers)
            if (n <= 2)
            {
                return 1;
            }
            //Otherwise, recursively return the previous two fibonacci numbers. Those function calls will also recursively return the previous two, until n = 2 or n = 1.
            else
            {
                return CalcFib(n - 1) + CalcFib(n - 2);
            }
        }

        //Pre: must be a valid Stopwatch object
        //Post: returns how much time has elapsed in a nicely formatted fashion
        //Descrption: This method returns the days, hours, minutes, seconds, and milliseconds that the given timer was running for
        public static string GetTimeOutput(Stopwatch timer)
        {
            TimeSpan ts = timer.Elapsed;
            int millis = ts.Milliseconds;
            int seconds = ts.Seconds;
            int minutes = ts.Minutes;
            int hours = ts.Hours;
            int days = ts.Days;

            return "Time- Days:Hours:Minutes:Seconds.Milliseconds:" + days + ":" +
                                                                      hours + ":" +
                                                                      minutes + ":" +
                                                                      seconds + "." +
                                                                      millis;
        }

        public static int MysterySub(int a, int b, int c)
        {
            if (a <= 3)
            {
                return b;
            }
            else
            {
                return c + MysterySub(a - 2, b, c);
            }
        }


    }

}
