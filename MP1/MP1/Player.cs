using System;
using System.IO;

namespace MP1
{
    public class Player
    {
        static StreamWriter outFile;
        static StreamReader inFile;


        private string playerName;
        private string path;
        private int numGamesPlayed;
        private int numGamesPlayedX;
        private int numGamesPlayedO;

        private int numWins;
        private int numLosses;
        private int numTies;

        private int numWinsX;
        private int numWinsO;
        private int numLossesX;
        private int numLossesO;
        private int numTiesX;
        private int numTiesO;

        private double winPercent;
        private double winPercentX;
        private double winPercentO;


        //Read file. If empty/new file created, set everything to zero. Otherwise, set variables to file content
        public Player(string playerName)
        {
            this.playerName = playerName;
            path = $"{playerName}.stats";
            ReadFile();
            
        }


        public void UpdatePlayerFile ()
        {
           try
           {
                outFile = File.CreateText(path);

                outFile.WriteLine(numGamesPlayed);
                outFile.WriteLine(numGamesPlayedX);
                outFile.WriteLine(numGamesPlayedO);
                outFile.WriteLine(numWins);
                outFile.WriteLine(numLosses);
                outFile.WriteLine(numTies);
                outFile.WriteLine(numWinsX);
                outFile.WriteLine(numLossesX);
                outFile.WriteLine(numTiesX);
                outFile.WriteLine(numWinsO);
                outFile.WriteLine(numLossesO);
                outFile.WriteLine(numTiesO);
                outFile.WriteLine(winPercent);
                outFile.WriteLine(winPercentX);
                outFile.WriteLine(winPercentO);

                outFile.Close();

            }
           catch (NullReferenceException e)
           {
                ResetStats();
           }
           catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public void ReadFile()
        {

            try
            {
                inFile = File.OpenText(path);

                numGamesPlayed = Convert.ToInt32(inFile.ReadLine());
                numGamesPlayedX = Convert.ToInt32(inFile.ReadLine());
                numGamesPlayedO = Convert.ToInt32(inFile.ReadLine());
                numWins = Convert.ToInt32(inFile.ReadLine());
                numLosses = Convert.ToInt32(inFile.ReadLine());
                numTies = Convert.ToInt32(inFile.ReadLine());
                numWinsX = Convert.ToInt32(inFile.ReadLine());
                numLossesX = Convert.ToInt32(inFile.ReadLine());
                numTiesX = Convert.ToInt32(inFile.ReadLine());
                numWinsO = Convert.ToInt32(inFile.ReadLine());
                numLossesO = Convert.ToInt32(inFile.ReadLine());
                numTiesO = Convert.ToInt32(inFile.ReadLine());
                winPercent = Convert.ToDouble(inFile.ReadLine());
                winPercentX = Convert.ToDouble(inFile.ReadLine());
                winPercentO = Convert.ToDouble(inFile.ReadLine());


                inFile.Close();

            }
            catch (FileNotFoundException e) //File doesn't exist
            {
                UpdatePlayerFile();
                ReadFile();
            }
            catch (FormatException e) //Data in the file isn't in the expected format
            {
                Console.WriteLine($"The statistics file for {playerName} has been corrupted. The file and all statistics for {playerName} will be reset. (Press any key to continue.)");
                Console.ReadKey();

                ResetStats();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }

            
        }


        public void IncrementStats(int gameOutcome, int letterPlayed) //for gameOutcome, a 0 represents a tie, a  1 represents a win, and a 2 represents a loss. For letterPlayed, 0 represents X, and 1 represents O
        {
             
            numGamesPlayed++;
            numGamesPlayedX += (letterPlayed + 1) % 2;
            numGamesPlayedO += letterPlayed;

            numWins += gameOutcome % 2;
            numLosses += gameOutcome == 2 ? 1 : 0;
            numTies += gameOutcome == 0 ? 1 : 0;

            

            numWinsX += (gameOutcome % 2) * ((letterPlayed + 1) % 2);
            numWinsO += (gameOutcome % 2) * letterPlayed;

            numLossesX += (gameOutcome == 2 ? 1 : 0) * ((letterPlayed + 1) % 2);
            numLossesO += (gameOutcome == 2 ? 1 : 0) * letterPlayed;

            numTiesX += (gameOutcome == 0 ? 1 : 0) * ((letterPlayed + 1) % 2);
            numTiesO += (gameOutcome == 0 ? 1 : 0) * letterPlayed;

            winPercent = (double) 100 * numWins / numGamesPlayed;

            if(numGamesPlayedX != 0)
                winPercentX = (double) 100 * numWinsX / numGamesPlayedX;
            if (numGamesPlayedO != 0)
                winPercentO = (double) 100 * numWinsO / numGamesPlayedO;

            UpdatePlayerFile();
        }


        public void ResetStats()
        {
            numGamesPlayed = 0;
            numGamesPlayedX = 0;
            numGamesPlayedO = 0;
            numWins = 0;
            numLosses = 0;
            numTies = 0;
            numWinsX = 0;
            numLossesX = 0;
            numTiesX = 0;
            numWinsO = 0;
            numLossesO = 0;
            numTiesO = 0;
            winPercent = 0;
            winPercentX = 0;
            winPercentO = 0;

            UpdatePlayerFile();
        }


        public void PrintPlayerStats()
        {
            Console.WriteLine(playerName);
            Console.WriteLine($"- Games Played: {numGamesPlayed}");
            Console.WriteLine($"- Games Played as X: {numGamesPlayedX}");
            Console.WriteLine($"- Games Played as O: {numGamesPlayedO}");
            Console.WriteLine($"- Wins: {numWins}");
            Console.WriteLine($"- Losses: {numLosses}");
            Console.WriteLine($"- Ties: {numTies}");
            Console.WriteLine($"- Wins as X: {numWinsX}");
            Console.WriteLine($"- Losses as X: {numLossesX}");
            Console.WriteLine($"- Ties as X: {numTiesX}");
            Console.WriteLine($"- Wins as O: {numWinsO}");
            Console.WriteLine($"- Losses as O: {numLossesO}");
            Console.WriteLine($"- Ties as O: {numTiesO}");
            Console.WriteLine($"- Win %: {winPercent.ToString("n2")}%");
            Console.WriteLine($"- X Win %: {winPercentX.ToString("n2")}%");
            Console.WriteLine($"- O Win %: {winPercentO.ToString("n2")}%");
        }
    }
}
