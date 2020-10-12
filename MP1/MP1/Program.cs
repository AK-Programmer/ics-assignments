﻿using System;
using System.Threading;

namespace MP1
{
    class Program
    {
        //Global Variables

        static bool exit = false;  //Used to let the user exit the program. 

        //side length of the cubic game board 
        const int BOARD_LEN = 3;

        static string[] numToLetter = { " ", "X", "O" }; //An array that determines whether to place a space, an X, or an O in each piece depending on the contents of the game board at specific locations. 


        static void Main(string[] args)
        {   
            //This is the main loop of the program. After each complete game, the user is redirected to the menu, where they can choose to quit or play again. 
            while (!exit)
            {
                Menu();
            }
        }


        //This method serves the menu to the user. It lets them quit, or play a game. 
        public static void Menu()
        {
            //Variables & Objects
            char option; //Will be used to navigate menu
            Console.Clear();

            Console.WriteLine("TIC STAC TOE \n-----------------------\n1. Play Game \n2. View Statistics \n3. Reset Statistics \n4. Quit");

            Console.WriteLine("\n\nHOW TO PLAY\n----------------\nYou will be assigned either an X or an O. \nYour goal is to get three of your letter in a row.");

            //Reads single key input and converts from ConsoleKeyInfo object to char data type
            option = Console.ReadKey().KeyChar;

            //This switch statement either sends the user(s) to the actual game, quits, or handles their faulty input and sends them to the main menu again. 
            switch (option)
            {
                case '1':
                    MainGameLoop();
                    break;
                case '2':
                    ViewStats();
                    break;
                case '3':
                    ResetStats();
                    break;
                case '4':
                    exit = true;
                    Console.WriteLine("\nThanks for playing. Bye!");
                    break;

                default:
                    Console.WriteLine("\nThat's not a valid option. Try again (Press any key to continue).");
                    Console.ReadKey();
                    Menu();
                    break;
            }
        }

        public static void MainGameLoop()
        {

            int playerTurn = 0;

            bool gameOver = false;
            int count = 0;

            Random rnd = new Random();
            int randInt = rnd.Next(0, 2);

            string[] playerNames;

            //Putting the players in an array makes it easier to access them using playerTurn
            Player[] players = new Player[2];

            if (randInt == 0)
            {
                playerNames = new string[] { "Player 1", "Player 2" };
            }
            else
            {
                playerNames = new string[] { "Player 2", "Player 1" };
            }


            GameBoard board = new GameBoard(BOARD_LEN, playerNames);

            players[0] = new Player(playerNames[0]);
            players[1] = new Player(playerNames[1]);

            while (!gameOver)
            {
                //These variables are used to handle any errors in case the user types faulty inputs. 
                string rowStr = "";
                string colStr = "";

                //Error trapping loop: This loop prevents the user from entering faulty input and alerts them if they do. It breaks when the user enters correct input. 
                bool breakErrorLoop = false;


                while(!breakErrorLoop)
                {
                    board.PrintGameBoard(playerTurn);

                    //Here the numToLetter array (globally defined) converts the integer player value into its corresponding playing piece
                    Console.WriteLine($"Enter the row and column in which you'd like to place your {numToLetter[playerTurn + 1]}.");

                    Console.Write("Row: ");
                    rowStr = Console.ReadLine(); //Converts user char input to string for easier conversion to int

                    Console.Write("\nColumn: ");
                    colStr = Console.ReadLine();

                    Console.WriteLine(); //Creates a line break.

                    breakErrorLoop = board.SetPiece(rowStr, colStr, playerTurn + 1);
                }

                count++;

                bool win = board.CheckWin(Convert.ToInt32(rowStr) - 1, Convert.ToInt32(colStr) - 1, playerTurn + 1);

                board.PrintGameBoard(playerTurn);

                if (win)
                {
                    players[playerTurn].IncrementStats(1, playerTurn);
                    players[(playerTurn + 1) % 2].IncrementStats(2, (playerTurn + 1) % 2);

                    Console.WriteLine($"{playerNames[playerTurn]} has won! (Press any key to continue.)");
                    gameOver = true;
                    Console.ReadKey(); 
                }
                else if (count == BOARD_LEN * BOARD_LEN)
                {
                    players[playerTurn].IncrementStats(0, playerTurn + 1);
                    players[(playerTurn + 1) % 2].IncrementStats(0, (playerTurn + 1) % 2 + 1);

                    Console.WriteLine("It's a draw! (Press any key to continue.)");
                    gameOver = true;
                    Console.ReadKey();
                }

                playerTurn = (playerTurn + 1) % 2;

            }
        }


        public static void ViewStats ()
        {

            Player player1 = new Player("Player 1");
            Player player2 = new Player("Player 2");

            Console.Clear();
            Console.WriteLine("VIEW STATISTICS \n-----------------------\n\n");

            player1.PrintPlayerStats();

            Console.WriteLine("\n\n");

            player2.PrintPlayerStats();

            Console.WriteLine("\n\n");

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

        }

        public static void ResetStats ()
        {
            Console.Clear();

            Player player1 = new Player("Player 1");
            Player player2 = new Player("Player 2");

            player1.ResetStats();
            player2.ResetStats();

            string message = "Your statistics have been reset. ";

            for (int i = 0; i < message.Length; i++)
            {
                Console.Write(message[i]);
                Thread.Sleep(40);
            }

            Console.WriteLine(" (Press  any key to continue.)");
            Console.ReadKey(); 
        }
    }
}
