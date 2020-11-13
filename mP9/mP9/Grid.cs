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
        public bool CheckWin(SequenceQueue moveSequence)
        {
            int potentialPlayerPos =  playerPos;
            char nextMove;
            int size = moveSequence.Size();

            for (int i = 0; i < size; i++)
            {
                nextMove = (char) moveSequence.Dequeue();

                switch(nextMove)
                {
                    case 'w':
                        potentialPlayerPos -= COLS;
                        break;
                    case 's':
                        potentialPlayerPos += COLS;
                        break;
                    case 'a':
                        potentialPlayerPos -= 1;
                        break;
                    case 'd':
                        potentialPlayerPos += 1;
                        break;
                    default:
                        throw new ArgumentException("This sequence is invalid.");
                }
               
                if((potentialPlayerPos < 0 && potentialPlayerPos >= ROWS * COLS) || ((potentialPlayerPos + 1) % COLS == 0 && nextMove == 'a') || (potentialPlayerPos % 5 == 0 && nextMove == 'd'))
                {
                    throw new ArgumentException("This sequence of moves would make the player go out of bounds!");
                }

                if(potentialPlayerPos == goalPos)
                {
                    return true;
                }

                for(int j = 0; j < obstacles.Length; j++)
                {
                    if(potentialPlayerPos == obstacles[j].GetPos())
                    {
                        obstacles[j].ToggleVisibility();

                        throw new ArgumentException("You ran into an obstacle!");
                    }
                }
            }


            return false;
        }


        //Pre: none
        //Post: none
        //Description: This method displays the game grid
        public void DisplayGrid()
        {
            //The string that will be displayed in each slot
            string stringToDisplay;

            Console.WriteLine("--------------------------");

            //For each row, display all the slots in that row
            for(int i = 0; i < ROWS; i++)
            {
                Console.Write("|");

                //For each column in a row, display either a number, player, goal, or obstacle (if visible)
                for(int j = 0; j < COLS; j++)
                {
                    //Setting stringToDisplay to the number at that position by default
                    stringToDisplay = (i*COLS + j).ToString();

                    //If that position is the player position or goal position, set it to "P" or "G" respectively.
                    if (i * COLS + j == playerPos)
                    {
                        stringToDisplay = "P";
                    }
                    else if (i * COLS + j == goalPos)
                    {
                        stringToDisplay = "G";
                    }
                    //Otherwise, check if the position equals a position of any of the obstacles.
                    else
                    {
                        //For each obstacle, if the obstacle position equals the current position and the obstacle is visible, set strignToDisplay to "O"
                        for (int k = 0; k < obstacles.Length; k++)
                        {
                            if (i * COLS + j == obstacles[k].GetPos() && obstacles[k].GetIsVisible())
                            {
                                stringToDisplay = "O";
                            }
                        }
                    }

                    //If stringToDisplay is only one character, pad it so that everything aligns nicely.
                    stringToDisplay = stringToDisplay.Length == 1 ? stringToDisplay + " " : stringToDisplay;
                    Console.Write($" {stringToDisplay} |");
                }

                Console.WriteLine("\n--------------------------");
            }
        }


        //Pre: none
        //Post: none
        //Description: randomly sets the positions of the player, goal, and obstacles. 
        public void SetGame()
        {
            //tracking the positions of the player and goal (2 positions), and the positioons of each obstacles
            int[] usedPos = new int[2 + obstacles.Length];
            int obstaclePos;

            //Set all elements of usedPos to -1 by default so that they don't interfere with setting the positions.
            for(int i = 0; i < usedPos.Length; i++)
            {
                usedPos[i] = -1;
            }

            //For both playerPos and goalPos, set them to random positions, and add those positions to the list of used positions.
            playerPos = FindRndNoneRptNum(0, ROWS*COLS, usedPos);
            usedPos[0] = playerPos;
            goalPos = FindRndNoneRptNum(0, ROWS*COLS, usedPos);
            usedPos[1] = goalPos;

            //For each obstacle, set it to a random position, and add that position the list of used positions.
            for(int i = 2; i < usedPos.Length; i++)
            {   
                obstaclePos = FindRndNoneRptNum(0, ROWS*COLS, usedPos);
                obstacles[i - 2] = new Obstacle(obstaclePos);
                usedPos[i] = obstaclePos;
            }

       
        }

        //Pre: minimum must be smaller than the maximum
        //Post: returns an integer within the given range that isn't contained in the given array
        //Description: this method returns an integer with the given range that isn't contained in the given array.
        private int FindRndNoneRptNum(int min, int max, params int[] nums)
        {
            //Used to indicate that a duplicate has been found
            bool isDuplicate;
            //Will store temporary random numbers that may or may not be duplicates.
            int temp;

            //While a number that isn't in nums hasn't been found...
            while(true)
            {
                //Choose a random number
                temp = rnd.Next(min, max);

                isDuplicate = false;
                //For each number in nums, check if temp is equal to that number. If it is, set isDuplicate  to true.
                for (int i = 0; i < nums.Length; i++)
                {
                    if(temp == nums[i])
                    {
                        isDuplicate = true;
                        break;
                    }
                }

                //If no duplicate has been found, return temp
                if(!isDuplicate)
                {
                    return temp;
                }
            }
        }
    }
}
