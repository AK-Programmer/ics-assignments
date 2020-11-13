using System;
namespace mP9
{
    public class Grid
    {
        const int ROWS = 5;
        const int COLS = 5;
        const int NUM_OBSTACLES = 2;

        int playerPos, goalPos;
        Obstacle[] obstacles = new Obstacle[NUM_OBSTACLES];

        static Random rnd = new Random();

        public Grid()
        {
            SetGame();
        }

        //Pre: accepts a character sequence of the user's moves.
        //Post: none.
        //Description: this function carries out the user's sequences of moves, and 
        public bool CheckWin(char[] moveSequence)
        {
            for (int i = 0; i < moveSequence.Length; i++)
            {
                if (moveSequence[i] != 'w' && moveSequence[i] != 'a' && moveSequence[i] != 's' && moveSequence[i] != 'd')
                {
                    throw new ArgumentException("This sequence is invalid");
                }
            }


            return false;
        }


        public void DisplayGrid()
        {
            string stringToDisplay;

            Console.WriteLine($"{playerPos}, {goalPos}, {obstacles.Length}");

            Console.WriteLine("--------------------------");

            for(int i = 0; i < ROWS; i++)
            {
                Console.Write("|");

                for(int j = 0; j < COLS; j++)
                {
                    stringToDisplay = (i*5 + j).ToString();
                    
                    if (i * 5 + j == playerPos)
                    {
                        stringToDisplay = "P";
                    }
                    else if (i * 5 + j == goalPos)
                    {
                        stringToDisplay = "G";
                    }
                    else
                    {
                    
                        for (int k = 0; k < obstacles.Length; k++)
                        {
                            if (i * 5 + j == obstacles[k].GetPos())
                            {
                                stringToDisplay = obstacles[k].GetIsVisible() ? "O" : stringToDisplay;
                            }
                        }
                    }


                    stringToDisplay = stringToDisplay.Length == 1 ? stringToDisplay + " " : stringToDisplay;
                    Console.Write($" {stringToDisplay} |");
                }

                Console.WriteLine("\n--------------------------");
            }

            Console.ReadKey();
        }

        public void SetGame()
        {
            //tracking the positions of the player and goal (2 positions), and the positioons of each obstacles
            int[] usedPos = new int[2 + obstacles.Length];
            int obstaclePos;

            for(int i = 0; i < usedPos.Length; i++)
            {
                usedPos[i] = -1;
            }

            playerPos = FindRndNoneRptNum(0, 25, usedPos);
            usedPos[0] = playerPos;
            goalPos = FindRndNoneRptNum(0, 25, usedPos);
            usedPos[1] = goalPos;


            for(int i = 2; i < usedPos.Length; i++)
            {
                obstaclePos = FindRndNoneRptNum(0, 25, usedPos);
                obstacles[i - 2] = new Obstacle(obstaclePos);
                usedPos[i] = obstaclePos;
            }
        }


        private int FindRndNoneRptNum(int min, int max, params int[] nums)
        {
            bool isDuplicate = false;
            int temp;

            while(true)
            {
                temp = rnd.Next(min, max);

                for(int i = 0; i < nums.Length; i++)
                {
                    if(temp == nums[i])
                    {
                        isDuplicate = true;
                        break;
                    }
                }

                if(!isDuplicate)
                {
                    return temp;
                }
            }
        }
    }
}
