using System;
namespace MP1
{
    public class GameBoard
    {

        private int boardLen; 
        private int[,] gameBoard;

        private string[] playerNames; 

        static string[] numToLetter = { " ", "X", "O" };


        private int[,] rowsArray;
        private int[,] colsArray;
        private int[,] diagArray;


        public GameBoard()
        {
            boardLen = 3;
            gameBoard = new int[boardLen, boardLen];
            playerNames = new string[] {"Player 1", "Player2"};

            rowsArray = new int[boardLen, 2];
            colsArray = new int[boardLen, 2];
            diagArray = new int[2, 2]; //Always 2 diagonals no matter what on a 2D board
        }

        
        public GameBoard(int boardLen, string[] playerNames)
        {
            this.boardLen = boardLen;
            gameBoard = new int[boardLen, boardLen];
            this.playerNames = playerNames;

            rowsArray = new int[boardLen, 2];
            colsArray = new int[boardLen, 2];
            diagArray = new int[2, 2]; //Always 2 diagonals no matter what on a 2D board

        }   



        public bool SetPiece(string rowStr, string colStr, int letter)
        {
            int row;
            int col;

            if (Int32.TryParse(rowStr, out row) && Int32.TryParse(colStr, out col))
            {
                if (row >= 1 && row <= boardLen && col >= 1 && col <= boardLen)
                {

                    if (gameBoard[row - 1, col - 1] == 0)
                    {
                        gameBoard[row - 1, col - 1] = letter;
                        return true; 
                    }
                    else
                    {
                        Console.WriteLine("The slots here are already full. Please try again. (Press any key to continue.)");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine($"One of the numbers entered above is outside the valid range. Please enter a number between 1 and {boardLen} (inclusive). (Press any key to continue.)");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("One of the inputs entered above is not a valid integer. Please try again. (Press any key to continue.)");
                Console.ReadKey();
            }

            return false;
        }


        public void PrintGameBoard(int playerTurn, int pieceBeingPlaced)
        {

            string board = "";
            string padding = " "; //Will be used to add spaces at the start of each line so the boards look like they're on an angle (to get that 3D effect)
            string rowSeparator = "+";



            for (int i = 0; i < boardLen; i++)
            {
                rowSeparator += "-----+";
            }



            board += $"{rowSeparator}\n";



            //The two loops below iterate in descending order so that the game is displayed the 'right way' (the left column is first, the top row is first). 
            for (int i = 0; i < this.boardLen; i++) //This loops iterates through the 'rows' dimension
            {

                board += padding + "\\";
                padding += " ";


                for (int j = 0; j < this.boardLen; j++) //This loop iterates through the 'columns' dimension
                {

                    // If gameBoard[j,k,i] == 0, it will print a space, if it equals 1 it will print an X, and if it equals two it will print an O (see the way the numToLetter array is defined above). 
                    //The new square or 'cell' is then added to the board.
                    board += $"  {numToLetter[this.gameBoard[i,j]]}  \\";
                }

                board += $"\n{padding}{rowSeparator}\n";
                padding += " ";
            }

            board += "\n"; //Creates spacing between boards

            // The ternary expression here returns an 'X' if playerTurn is equal to zero, and an 'O' otherwise. This makes sense since 'X' always goes first.
            Console.Clear();
            Console.WriteLine($"{playerNames[playerTurn]}'s Turn ({numToLetter[pieceBeingPlaced + 1]}) \n-----------------------\n");

            Console.WriteLine(board);
        }



        public bool CheckWin(int lastRow, int lastCol, int letter)
        {

            if (rowsArray[lastRow, 1] == 0 || rowsArray[lastRow, 1] == letter + 1)
            {
                rowsArray[lastRow, 1] = letter + 1;
                rowsArray[lastRow, 0] += 1;

                if (rowsArray[lastRow, 0] == boardLen)
                {
                    return true; 
                }
            }

            if (colsArray[lastCol, 1] == 0 || colsArray[lastCol, 1] == letter + 1)
            {
                colsArray[lastCol, 1] = letter + 1;
                colsArray[lastCol, 0] += 1;

                if (colsArray[lastCol, 0] == boardLen)
                {
                    return true;
                }
            }

            if (lastRow == lastCol && (diagArray[0, 1] == 0 || diagArray[0, 1] == letter+1))
            {
                diagArray[0, 1] = letter + 1;
                diagArray[0, 0] += 1;

                if (diagArray[0, 0] == boardLen)
                {
                    return true;
                }
            }

            if (lastRow + lastCol + 1 == boardLen && (diagArray[1, 1] == 0 || diagArray[1, 1] == letter + 1))
            {
                diagArray[1, 1] = letter + 1;
                diagArray[1, 0] += 1;

                if (diagArray[1,0] == boardLen)
                {
                    return true;
                }
            }

            return false; 
        }
    }
}
