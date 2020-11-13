using System;

namespace mP9
{
    class Program
    {

        static Grid gameGrid;
        const int NUM_ROUNDS = 3;

        static void Main(string[] args)
        {
            gameGrid = new Grid();
            PlayGame();
        }

        public static void PlayGame()
        {
            string movesSequence;
            SequenceQueue seqQueue;
            bool success = false;

            gameGrid.SetGame();

            for (int i = 0; i < NUM_ROUNDS; i++)
            {
                
                Console.Clear();
                Console.WriteLine("REACH THE GOAL \n-----------------");
                Console.WriteLine("Enter a sequence of moves (wasd) to try to reach the goal. Press enter when you've finished making your sequence.");
                gameGrid.DisplayGrid();
                movesSequence = Console.ReadLine();

                seqQueue = new SequenceQueue();

                try
                {
                    for (int j = 0; j < movesSequence.Length; j++)
                    {
                        seqQueue.Enqueue(movesSequence[j]);
                    }
                }
                catch(ArgumentException)
                {
                    i--;
                    Console.WriteLine("One or more of the characters you entered is invalid. Press ENTER to try again.");
                    Console.ReadLine();
                    continue;
                }


                try
                {
                    success = gameGrid.CheckWin(seqQueue);

                    if(success)
                    {
                        break;
                    }
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

            if(success)
            {
                Console.WriteLine("You reached the goal! Good job! Would you like to play again? Enter 'y' if yes, and anything else if not.");
            }
            else
            {
                Console.WriteLine("You didn't win this time. Would you like to play again? Enter 'y' if yes, and anything else if not.");
            }

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
