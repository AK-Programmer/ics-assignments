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

            Console.WriteLine(CheckNerdWord("XYXY"));
            /*for (int i = 0; i < 10; i++)
            {
                words.Add(GenerateNerdWord());
                results.Add(CheckNerdWord(words[i]));
            }

            for(int i = 0; i < words.Count)
            {

            }*/
            
            


            /*
            

            timer.Reset();
            timer.Start();


            try
            {
                inFile = new StreamReader("input.txt");
                string currentLine;

                while (true)
                {
                    currentLine = inFile.ReadLine();

                    if (currentLine == "")
                    {
                        break;
                    }

                    words.Add(currentLine);
                    results.Add(CheckNerdWord(currentLine));
                }
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine("File not found");
            }

            inFile.Close();

            outFile = new StreamWriter("Kahiri_A.txt");

            for (int i = 0; i < words.Count; i++)
            {
                outFile.WriteLine($"{words[i]}:{(results[i] ? "YES" : "NO")}");
            }

            outFile.Close();
            timer.Stop();
            Console.WriteLine($"Execution time: {timer.Elapsed.Days} Days, {timer.Elapsed.Hours} Hours, {timer.Elapsed.Minutes} Minutes, {timer.Elapsed.Seconds} Seconds, {timer.Elapsed.Milliseconds}");
            */
        }
        
        public static bool CheckNerdWord(string word)
        {

            if(word == "X")
            {
                return true;
            }
            else
            {
                int numAs = 0;
                int numBs = 0;
                int firstALoc = 0;

                for (int i = 0; i < word.Length; i++)
                {
                    switch (word[i])
                    {
                        case 'A':
                            if (numAs == 0)
                            {
                                firstALoc = i;
                            }
                            numAs++;
                            break;
                        case 'B':
                            numBs++;
                            break;
                        default:
                            break;
                    }

                    if (numAs > 0 && numAs == numBs)
                    {
                        return CheckNerdWord(word[0..firstALoc] + word[(firstALoc + 1)..i] + word[(i + 1)..]);
                    }
                    if (numAs == 0 && word[i] == 'Y')
                    {
                        return CheckNerdWord(word[0..i]) && CheckNerdWord(word[(i + 1)..]);
                    }
                }
            }
            
            return false;
        }



        public static string GenerateNerdWord()
        {
            int choice = rnd.Next(0, 3);

            switch(choice)
            {
                case 0:
                    return GenerateNerdWord() + "Y" + GenerateNerdWord();
                case 1:
                    return "A" + GenerateNerdWord() + "B";
                default:
                    return "X";

            }
        }
    }
}
