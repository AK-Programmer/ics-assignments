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
            //The index of the desired fibonacci number
            int fibNumIndex;
            //UInt64 in order to store very large positive integers
            UInt64 fibNum;
            string userInput;

            //This loop keeps asking the users for indices until they decide to quit
            while(true)
            {
                Console.Clear();
                Console.WriteLine("FIBONACCI NUMBER CALCULATOR\n------------------------------------\nEnter a positive integer to calculate the fibonacci number at that index, or 'q' to quit.");

                userInput = Console.ReadLine();

                //Break the loop if the user decides to quit
                if(userInput == "q")
                {
                    break;
                }
                //Otherwise, check if their input is valid, and if so, calculate the fibonacci number at that index
                else
                {
                    try
                    {
                        fibNumIndex = Convert.ToInt32(userInput);

                        if(fibNumIndex < 0)
                        {
                            Console.WriteLine("Only positive integers are allowed. Press ENTER to try again.");
                        }
                        else
                        {
                            //Resetting and starting the timer
                            stopWatch.Reset();
                            stopWatch.Start();

                            fibNum = CalcFib(fibNumIndex);
                            //Stopping the timer as soon as the Fibonacci number is calculated
                            stopWatch.Stop();
                            Console.WriteLine(fibNum);

                            //Printing how long it took to calculate the fibonacci number
                            Console.WriteLine(GetTimeOutput(stopWatch));

                        }

                    }
                    catch(FormatException)
                    {
                        Console.WriteLine("That's not a valid input. Press ENTER to try again.");
                    }

                    Console.ReadLine();
                }
            }
            

        }

        //Pre: must be a positive integer
        //Post: returns the sum of the previous two Fibonacci numbers.
        //Description: this method recursively calculates the value of the nth fibonacci number.
        public static UInt64 CalcFib(int n)
        {
            //The 0th term of the sequence is zero
            if(n == 0)
            {
                return 0;
            }
            //The 1st term is one
            else if (n == 1)
            {
                return 1;
            }
            //The rest of the terms are the sum of the previous two terms
            else
            {
                return CalcFib(n - 1) + CalcFib(n - 2);
            }
        }

        //Pre: must be a valid Stopwatch object
        //Post: returns how much time has elapsed in a nicely formatted fashion
        //Descrption: This method returnss the days, hours, minutes, seconds, and milliseconds that the given timer was running for
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
    }
}
