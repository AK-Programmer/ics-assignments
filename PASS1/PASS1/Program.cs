//Author: Adar Kahiri
//File Name: program.cs
//Project Name: PASS1
//Creation Date: September 22, 2020
//Modified Date: September 29, 2020
//Description: This program lets the user play 3D Tic Tac Toe. It consists of the (rudimentary) user interface, as well as all of the logic necessary for the game to work properly (properly placing the users' letters, checking for wins, etc.)

using System;

namespace PASS1
{
    class Program
    {


        //Global Variables

        static bool exit = false;  //Used to let the user exit the program. 

        //side length of the cubic game board 
        const int BOARD_LEN = 3;

        static string[] numToLetter = { " ", "X", "O" }; //An array that determines whether to place a space, an X, or an O in each piece depending on the contents of the game board at specific locations. 





        //Main Method
        public static void Main(string[] args)
        {
            //This is the main loop of the program. After each complete game, the user is redirected to the menu, where they can choose to quit or play again. 
            while (!exit)
            {
                Console.Clear();
                Menu();
            }
        }



        //This method serves the menu to the user. It lets them quit, or play a game. 
        public static void Menu()
        {
            //Variables & Objects
            char option; //Will be used to navigate menu
            Console.Clear();

            Console.WriteLine("TIC STAC TOE \n-----------------------\n1. Play Game \n2. Quit");
            Console.WriteLine("\n\nHOW TO PLAY\n----------------\nYou will be assigned either an X or an O. \nYour goal is to get three of your letter in a row. \nWhenever you place a piece, it will fall to the bottom of the board, or on  top of the other piece(s) placed in that location. Good luck and have fun!");


            //Reads single key input and converts from ConsoleKeyInfo object to char data type
            option = Console.ReadKey().KeyChar;

            //This switch statement either sends the user(s) to the actual game, quits, or handles their faulty input and sends them to the main menu again. 
            switch (option)
            {
                case '1':
                    MainGameLoop();
                    break;


                case '2':
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


        //This is the method where the 3D array is stored, and where the game will take place. 
        public static void MainGameLoop()
        {
            bool gameOver = false; //Used to exit the game loop when the game is over
            bool lastResult = false; //This int stores the result of each round (a player won, there is a draw, no one has won yet).
            int count = 0; //This variable keeps track of how many pieces have been placed.


            //This randomizes which player goes first. If randInt == 1, player 1 goes first, otherwise player 2 goes first.
            Random rnd = new Random();
            int randInt = rnd.Next(0, 2);
            string[] playerNames = new string[2];


            if (randInt == 0)
            {
                playerNames[0] = "Player 1";
                playerNames[1] = "Player 2";

            }
            else
            {
                playerNames[0] = "Player 2";
                playerNames[1] = "Player 1";
            }


            /*Below is the game board. 
              - 0 represents an empty cell
              - 1 represents an X 
              - 2 represents an O

            The first dimension represents the rows of each board looking from top down, the second dimension represents the columns looking from top down, and the third dimension represents the 'depth' or which 'board'is being referred to. 
            */
            int[,,] gameBoard = new int[BOARD_LEN, BOARD_LEN, BOARD_LEN];


            //This variable dictates whose turn it is. A zero means it's X's turn, a 1 means it's player O's turn. As may be apparent later in the documentation, this can be pretty cumbersome, since Xs and Os are represented by ones and twos in the array, respectively. The reason I designed it this way is to avoid confusion with the 'Players 1 & 2' which are displayed to the user and randomized. 
            int playerTurn = 0;


            //This loop continues until either a draw or a win occurs
            while (!gameOver)
            {
                //These variables are used to handle any errors in case the user types faulty inputs. 
                String rowStr;
                String colStr;

                //These variables store the user's choice for row and column so that their letter can be placed there. They are set to -1 by default so that they don't break the error-handling logic below. It isn't actually expected that they would break the logic if they are undefined, but this is just an extra precaution. 
                int row = -1;
                int col = -1;


                //Error trapping loop: This loop prevents the user from entering faulty input and alerts them if they do. It breaks when the user enters correct input. 
                bool breakErrorLoop = false;

                while (!breakErrorLoop)
                {
                    Console.Clear();


                    //Prints the game board
                    PrintGameBoard(gameBoard, playerTurn, playerNames);


                    //Here the numToLetter array (globally defined) converts the integer player value into its corresponding playing piece
                    Console.WriteLine($"Enter the row and column in which you'd like to place your {numToLetter[playerTurn + 1]}.");

                    Console.Write("Row: ");
                    rowStr = Console.ReadKey().KeyChar.ToString(); //Converts user char input to string for easier conversion to int

                    Console.Write("\nColumn: ");

                    colStr = Console.ReadKey().KeyChar.ToString();

                    Console.WriteLine(); //Creates a line break. 


                    //These conditionals first check if the user entered a valid integer, then they check if that integer is within the correct range. 
                    if (Int32.TryParse(rowStr, out row) && Int32.TryParse(colStr, out col))
                    {

                        //This if statement checks if the user has entered numbers within the correct range. If the numbers are within the correct range, then it updates the gameBoard array.
                        if (row >= 1 && row <= BOARD_LEN && col >= 1 && col <= BOARD_LEN) //gameBoard.GetLength(n) is the length of the n-th dimension of the area. 
                        {
                            //This for loop iterates through the different boards or "depths", and checks if there is already a letter there. It places the user's letter in the lowest empty square. 
                            for (int i = BOARD_LEN - 1; i >= 0; i--)
                            {
                                if (gameBoard[row - 1, col - 1, i] == 0) //If the depth i at the user-given row and column is empty, place the user's letter there. 
                                {
                                    gameBoard[row - 1, col - 1, i] = playerTurn + 1; //Update array with user's letter

                                    count++; //Updates the count variable to keep track of how  many pieces have been placed.

                                    //Checks if a player has won or if there's a draw (i.e., the game board is full)
                                    lastResult = CheckWin(gameBoard, row - 1, col - 1, i);

                                    breakErrorLoop = true; //Tells the error trapping loop to break
                                    break;
                                }

                                //If all three slots at the given index are filled, then alert the user and let them try again. 
                                if (gameBoard[row - 1, col - 1, i] != 0 && i == 0)
                                {
                                    Console.WriteLine("The slots here are already full. Please try again. (Press any key to continue.)");
                                    Console.ReadKey();
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("One of the numbers entered above is outside the valid range. Please enter a number between 1 and 3 (inclusive). (Press any key to continue.)");

                            Console.ReadKey(); //This prevents the loop from resetting until the user actually reads the error message.
                        }
                    }
                    else
                    {
                        Console.WriteLine("One of the characters entered above is not a valid number. Please try again. (Press any key to continue.)");

                        Console.ReadKey();  //See above. 
                    }


                }

                if (lastResult)
                {
                    PrintGameBoard(gameBoard, playerTurn, playerNames);
                    Console.WriteLine($"{playerNames[playerTurn]} has won! (Press any key to continue.)");
                    gameOver = true;
                    Console.ReadKey();
                    break;
                }

                //If this count equals 27 (more generally, the length of the board cubed), it means all spaces have been filled, and if no win is found, it means there is a draw. 
                if (count == BOARD_LEN * BOARD_LEN * BOARD_LEN)
                {
                    PrintGameBoard(gameBoard, playerTurn, playerNames);
                    Console.WriteLine("It's a draw! (Press any key to continue.)");
                    gameOver = true;
                    Console.ReadKey();
                }



                playerTurn = (playerTurn + 1) % 2; //Mod 2 so that the only possible values are 0 and 1. 
            }
        }


        //This method prints the gameboard in a nicely formatted way that resembles 3 tic tac toe boards stacked on top of each other. 
        public static void PrintGameBoard(int[,,] gameBoard, int playerTurn, string[] playerNames)
        {

            string board = "";

            for (int i = 0; i < BOARD_LEN; i++) //This loop iterates through the 'depth' dimension.
            {
                string padding = " "; //Will be used to add spaces at the start of each line so the boards look like they're on an angle (to get that 3D effect)

                board += "+-----+-----+-----+\n";

                //The two loops below iterate in descending order so that the game is displayed the 'right way' (the left column is first, the top row is first). 
                for (int j = 0; j < BOARD_LEN; j++) //This loops iterates through the 'rows' dimension
                {

                    board += padding + "\\";

                    padding += " ";

                    for (int k = 0; k < BOARD_LEN; k++) //This loop iterates through the 'columns' dimension
                    {

                        // If gameBoard[j,k,i] == 0, it will print a space, if it equals 1 it will print an X, and if it equals two it will print an O (see the way the numToLetter array is defined above). 
                        //The new square or 'cell' is then added to the board.
                        board += $"  {numToLetter[gameBoard[j, k, i]]}  \\";
                    }

                    board += "\n" + padding + "+-----+-----+-----+\n";
                    padding += " ";

                }

                board += "\n"; //Creates spacing between boards
            }

            // The ternary expression here returns an 'X' if playerTurn is equal to zero, and an 'O' otherwise. This makes sense since 'X' always goes first.
            Console.Clear();
            Console.WriteLine($"{playerNames[playerTurn]}'s Turn ({numToLetter[playerTurn+1]}) \n-----------------------\n");

            Console.WriteLine(board);
        }

        /* 
         * This method checks if a player has won and returns true or false.
         *It takes as input the game board and the location of the last letter placed.
         */
        public static bool CheckWin(int[,,] gameBoard, int lastRow, int lastColumn, int lastDepth)
        {

            int lastPiece = gameBoard[lastRow, lastColumn, lastDepth];

            //This 'magic' number 8 is purely a result of the number of dimensions of the board (3 dimensions). There are 2^3 ways to choose to increment dimensions; 7 ways excluding doing nothing (000)
            for (int i = 1; i < 8; i++)
            {

                //Converts i into a binary number with 3 digits. Each binary number represents a different way to increment the indices from the location of the last piece placed (e.g., 001 means only the z axis will be incremented, 101 means both the z and the x axes will be incremented). Together, the seven numbers represent most of the ways in which the indices can be incremented, and as a result most of the straight lines. The rest of the ways in which the indices can be incremented can be obtained by tacking on a minus sign to one of the indices being incremented. 
                String binary = Convert.ToString(i, 2).PadLeft(3, '0'); //Again, this 'magic' number 3 is purely a result of the dimensions of the board. 

                int rowIncrement = Convert.ToInt32(Char.GetNumericValue(binary[0]));
                int colIncrement = Convert.ToInt32(Char.GetNumericValue(binary[1]));
                int depthIncrement = Convert.ToInt32(Char.GetNumericValue(binary[2]));


                //Below, the realMod (real modulo) method (it's defined further down in the code) is used to prevent the incremented indices from going beyond the size of the array. But more importantly, using the modulo function has a very useful geometric meaning. Suppose we would like to check for all the straight lines around some piece. If that piece is not the center piece (gameBoard[1,1,1]), then incrementing its indices in all possible ways will inevitably lead to going outside of the index range of the array. However, what happens when the modulo function is used, is that this incremented index is then 'wrapped' around to the opposite side of the cube, just like in pacman (when the player goes through one side, they come back from the opposite side). Doing this, along with incrementing the dimensions in different ways (e.g., [x+1, y, z],[x+1, y+1, z], [x+1, y-1, z].[x+1, y+1, z-1], etc.)

                //One of the pieces that could be in a straight line along with the last piece.
                int nextPiece1 = gameBoard[realMod(lastRow + rowIncrement, BOARD_LEN), realMod(lastColumn + colIncrement, BOARD_LEN), realMod(lastDepth + depthIncrement, BOARD_LEN)];

                //The other piece that could be in a straight line along with the last piece by reversing all the indices. 
                int nextPiece2 = gameBoard[realMod(lastRow - rowIncrement, BOARD_LEN), realMod(lastColumn - colIncrement, BOARD_LEN), realMod(lastDepth - depthIncrement, BOARD_LEN)];




                /*
                 * The problem with using the modulo function to 'wrap around' is that this method 
                 * sometimes detects non-straight lines as straight lines. Consider the following arrangement:
                    +-----+-----+-----+
                     \     \     \  X  \
                      +-----+-----+-----+
                       \  X  \  O  \     \
                        +-----+-----+-----+
                         \     \  X  \     \
                          +-----+-----+-----+
             
                 * Since modulo 'wraps' the board around at the edges, the board 'actually' looks like this: 
                    +-----+-----+-----+-----+
                     \  X  \     \     \  X  \
                      +-----+-----+-----+-----+
                       \     \  X  \  O  \     \
                        +-----+------+-----+----+
                         \     \     \  X  \     \
                          +-----+-----+-----+-----+

                 * Notice how a diagonal line is formed. This diagonal line would be detected by the algorithm, even though it shouldn't be,
                 * and so we need to find a way to get around this. 
                 * 
                 * So, what separates 'real' straight lines from 'fake' ones like the one above? Well, for 
                 * every dimension that is being incremented, the line must pass through the center of those dimensions. 
                 * 
                 * For example, if only rows and columns are being incremented to check for lines going through the piece [0,0,0],
                 * then any 'real' line going through it must pass through [1,1,0]. 
                 * 
                 * If all three dimensions are being incremented, any 'real' line must pass through [1,1,1], and so on. 
                 * 
                 * The code below ensures that at least one of lastPiece, nextPiece1, and nextPiece2, goes through the center 
                 * of the dimensions being incremented. 
                 * 
                 * It does this in the following way: 
                 * 1. If rows are being incremented, check if the location of the last row equals 1. 
                 * 2. If columns are being incremented, check if the location of the last column equals 1.
                 * 3. If depth is being incremented, check if the location of the last depth equals 1. 
                 * 4. Do the above for all of lastPiece, nextPiece1, nextPiece2 (replacing lastX with lastX +/- 1 as appropriate).
                 * 5. If the location of at least one of lastPiece, nextPiece1, nextPiece2 goes through the center of the dimensions 
                 *    being incremented and the three pieces all have  the same letter, return true. 
                 */

                bool isLastPceMid;
                bool isNextPce1Mid;
                bool isNextPce2Mid;


                //If there is a straight line, check if any of its pieces are the center of the dimensions being incremented. 
                if (lastPiece == nextPiece1 && lastPiece == nextPiece2)
                {
                    //These ternary expressions assign 'true' to their respective variables if the dimensions being incremented are equal to 1 (the center). If a dimension isn't being incremented, that part of the expression simply returns true so as to not interfere. 
                    isLastPceMid = (binary[0] == '1' ? lastRow == 1 : true) && (binary[1] == '1' ? lastColumn == 1 : true) && (binary[2] == '1' ? lastDepth == 1 : true);

                    isNextPce1Mid = (binary[0] == '1' ? lastRow + rowIncrement == 1 : true) && (binary[1] == '1' ? lastColumn + colIncrement == 1 : true) && (binary[2] == '1' ? lastDepth + depthIncrement == 1 : true);

                    isNextPce2Mid = (binary[0] == '1' ? lastRow - rowIncrement == 1 : true) && (binary[1] == '1' ? lastColumn - colIncrement == 1 : true) && (binary[2] == '1' ? lastDepth - depthIncrement == 1 : true);

                    //If one of lastPiece, nextPiece1, nextPiece2 goes through the center of the dimensions being incremented
                    if (isLastPceMid || isNextPce1Mid || isNextPce2Mid)
                    {
                        return true;
                    }

                }

                /*After checking increments of the same kind (either adding 1 to all dimensions selected by the 
                 * binary number or subtracting 1 from all of them), we now check other lines that can be formed by 
                 * incrementing those dimensions in different ways (adding 1 to one dimension while subtracting 1 from another, vice versa, and so on). 
                */

                if (!IsPowerOfTwo(i)) //This makes the algorithm more efficient by avoiding computation for the binary numbers 001, 010, 100, which are non-diagonal lines and do not need to be reversed.
                {
                    for (int j = 0; j < 3; j++) //This 'magic' number 3 is purely a result of the number of dimensions of the board
                    {
                        if (binary[j] == '1')
                        {
                            int reverseRowIncrement = j == 0 ? -rowIncrement : rowIncrement;
                            int reverseColIncrement = j == 1 ? -colIncrement : colIncrement;
                            int reverseDepthIncrement = j == 2 ? -depthIncrement : depthIncrement;


                            //One of the pieces that could be in a straight line along with the last piece placed.
                            int revNextPiece1 = gameBoard[realMod(lastRow + reverseRowIncrement, BOARD_LEN), realMod(lastColumn + reverseColIncrement, BOARD_LEN), realMod(lastDepth + reverseDepthIncrement, BOARD_LEN)];

                            //The other corresponding piece that could be in a straight line along with the last piece placed. 
                            int revNextPiece2 = gameBoard[realMod(lastRow - reverseRowIncrement, BOARD_LEN), realMod(lastColumn - reverseColIncrement, BOARD_LEN), realMod(lastDepth - reverseDepthIncrement, BOARD_LEN)];

                            if (lastPiece == revNextPiece1 && lastPiece == revNextPiece2)
                            {
                                isLastPceMid = (binary[0] == '1' ? lastRow == 1 : true) && (binary[1] == '1' ? lastColumn == 1 : true) && (binary[2] == '1' ? lastDepth == 1 : true);
                                isNextPce1Mid = (binary[0] == '1' ? lastRow + reverseRowIncrement == 1 : true) && (binary[1] == '1' ? lastColumn + reverseColIncrement == 1 : true) && (binary[2] == '1' ? lastDepth + reverseDepthIncrement == 1 : true);
                                isNextPce2Mid = (binary[0] == '1' ? lastRow - reverseRowIncrement == 1 : true) && (binary[1] == '1' ? lastColumn - reverseColIncrement == 1 : true) && (binary[2] == '1' ? lastDepth - reverseDepthIncrement == 1 : true);


                                if (isLastPceMid || isNextPce1Mid || isNextPce2Mid)
                                {             
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false; //If no wins are found in the loop above, return false
        }




        //**************** AUXILIARY METHODS ****************


        /*This function is the 'real' modulo operator and is used instead of the one native to C#. This is because, 
         * for negative numbers -n, for n<m, -n % m = -n, whereas this isn't the actual definition of the modulo operator in mathematics. 
         * For example, in C#, -1 % 3 = -1, whereas the real mod function would give -1 % 3 = 2. We want the 'real modulo' so that when 
         * an index goes below zero when checking for wins, it 'wraps around' to the opposite side (index value of 2) instead of remaining negative.
        */
        public static int realMod(int num, int divisor)
        {
            return (num % divisor + divisor) % divisor;
        }


        /*This function determines whether a number is a power of two (for x>0) using the AND bitwise operator. To see how it works, 
         * suppose x = 7 and x-1 = 6. Then 7 & 6 compares each of the bits in the binary representation of each number. 
         * If they are both 1, it returns a 1, and if one or both are a zero, it returns a zero. So we have

                7 = 111
                     &
                6 = 110
                -------
                    110

        But now look what happens when we do this with a power of 2: 

                8 = 1000
                     &
                7 = 0111
                --------
                    0000

        We get zero. So if the expression below is equal to zero, meaning it's a power of 2, the function returns true. 
        */
        public static bool IsPowerOfTwo(int x)
        {
            return (x & (x - 1)) == 0;
        }
    }
}
