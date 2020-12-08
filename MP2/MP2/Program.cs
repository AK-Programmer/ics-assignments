//Author: Adar Kahiri
//Project name: MP2
//File name: Program.cs
//Date created: Nov 22, 2020
//Date modified: Nov 29, 2020
//Description: This program determines whether a given set of words (saved in a file) are nerd words. It outputs the results to an output text file, and displays in console the time it took to determine whether each of the words is a nerd or code word.

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace MP2
{
    class Program
    {

        static StreamReader inFile;
        static StreamWriter outFile;
        static Stopwatch timer = new Stopwatch();
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            List<string> words = new List<string>();
            List<bool> results = new List<bool>();

            string userInput;

            try
            {
                Console.WriteLine("(NERD/CODE)WORD CHECKER\n-------------------\nWould you like to print the nerd/code words to console? (Enter 'y' for yes, and anything else for no)");
                userInput = Console.ReadLine();

                timer.Reset();
                timer.Start();

                inFile = new StreamReader("input.txt");
                string currentLine;

                while (!inFile.EndOfStream)
                {
                    currentLine = inFile.ReadLine();
                    words.Add(currentLine);
                    results.Add(CheckNerdWord(currentLine));
                }

                inFile.Close();

                outFile = new StreamWriter("Kahiri_A.txt");

                if (userInput == "y")
                {
                    Console.WriteLine("Writing to file...");
                    for (int i = 0; i < words.Count; i++)
                    {
                        outFile.WriteLine($"{words[i]}:{(results[i] ? "YES" : "NO")}");
                        Console.WriteLine($"{ words[i]}:{ (results[i] ? "YES" : "NO")}");
                    }
                }
                else
                {
                    for (int i = 0; i < words.Count; i++)
                    {
                        outFile.WriteLine($"{words[i]}:{(results[i] ? "YES" : "NO")}");
                    }
                }

                outFile.Close();
                timer.Stop();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
            }

            Console.WriteLine($"Execution time: {timer.Elapsed.Days} Days, {timer.Elapsed.Hours} Hours, {timer.Elapsed.Minutes} Minutes, {timer.Elapsed.Seconds} Seconds, {timer.Elapsed.Milliseconds} Milliseconds");
        }

        public static bool CheckNerdWord(string word)
        {
            if (word == "X")
            {
                return true;
            }

            int currentALoc = -1;

            for (int i = 0; i < word.Length; i++)
            {
                switch (word[i])
                {
                    case 'A':
                        currentALoc = i;
                        break;
                    case 'B' when currentALoc >= 0:
                        return CheckNerdWord(word.Substring(currentALoc + 1, i - currentALoc - 1)) && CheckNerdWord(word.Substring(0, currentALoc) + "X" + word.Substring(i + 1));
                    case 'Y' when currentALoc == -1:
                        return CheckNerdWord(word.Substring(0, i)) && CheckNerdWord(word.Substring(i + 1));
                }
            }
            return false;
        }
    }
}
