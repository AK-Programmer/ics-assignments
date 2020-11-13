//Author: Adar Kahiri
//File Name: Program.cs
//Project Name: mP9
//Creation Date: Nov. 13, 2020
//Modified Date: Nov. 13, 2020
//Description: This is the driver class. It handles the general flow of the game, such as prompting the user to enter input, error-trapping input, et cetera.

using System;

namespace mP9
{
    class Program
    {
        //The grid that the game will take place on
        static Grid gameGrid;
        //Constant that determines the number of rounds
        const int NUM_ROUNDS = 3;

        static void Main(string[] args)
        {
            //Initializing the grid
            gameGrid = new Grid();

            PlayGame();
        }


        //Pre: none
        //Post: none
        //Description: this method takes the player through a full game. At the end, it asks the player if they would like to play again or quit, and acts accordingly.
        public static void PlayGame()
        {
            //This string will store the user's initial input
            string userInput;

            //This queue will store the input as a queue
            SequenceQueue seqQueue = new SequenceQueue();

            //This bool will store whether the player has won or not
            bool playerWon = false;

            //Before each game, reset the positions of the player, goal, and obstacles
            gameGrid.SetGame();


            //This for loop ensures the game goes on for NUM_ROUNDS rounds
            for (int i = 0; i < NUM_ROUNDS; i++)
            {
                //Clearing the sequence after each game before each round.
                seqQueue.Clear();

                Console.Clear();
                Console.WriteLine("REACH THE GOAL \n-----------------");
                Console.WriteLine("Enter a sequence of moves (wasd) to try to reach the goal. Press enter when you've finished making your sequence.");
                gameGrid.DisplayGrid();

                userInput = Console.ReadLine();


                //Try catch block to catch any exceptions thrown by the Enqueue function
                try
                {
                    //For each character in the user's input, enqueue it. The enqueue method throws an exception if the character is not a valid move.
                    for (int j = 0; j < userInput.Length; j++)
                    {
                        seqQueue.Enqueue(userInput[j]);
                    }
                }
                catch(ArgumentException)
                {
                    //decrementing i to ensure the player does not lose a round for entering faulty input
                    i--;
                    Console.WriteLine("One or more of the characters you entered is invalid. Press ENTER to try again.");
                    Console.ReadLine();
                    continue;
                }

                //Try catch block to catch any exceptions thrown by the CheckWin function
                try
                {
                    //CheckWin takes the player's Queue of moves
                    playerWon = gameGrid.CheckWin(seqQueue);

                    //If the CheckWin function returns true, break out of the loop. 
                    if(playerWon)
                    {
                        break;
                    }
                    //Otherwise, notify the user that they haven't yet succeeded and display the number of tries they have left. 
                    else
                    {
                        Console.WriteLine($"Close! But that sequence didn't get you to the goal. Try again!  ({NUM_ROUNDS - i - 1} tries left). Press ENTER to continue.");
                    }
                }
                catch(ArgumentException e)
                {
                    Console.WriteLine($"{e.Message} Press ENTER to continue.");
                }
                Console.ReadLine();
            }


            //Display to the user whether they have won or not, and ask them if they would like to play again. 
            if(playerWon)
            {
                Console.WriteLine("You reached the goal! Good job! Would you like to play again? Enter 'y' if yes, and anything else if not.");
            }
            else
            {
                Console.WriteLine("You didn't win this time. Would you like to play again? Enter 'y' if yes, and anything else if not.");
            }
            //If the user would like to play again, call PlayGame() again. Otherwise, display a goodbye message. 
            if(Console.ReadLine() == "y" ? true : false)
            {
                PlayGame();
            }
            else
            {
                Console.WriteLine("Thanks for playing. Bye!");
            }
        }
    }
}
