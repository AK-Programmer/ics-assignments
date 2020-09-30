//Author: Adar Kahiri
//File Name: main.cs
//Project Name: PASS1
//Creation Date: September 22, 2020
//Modified Date: 
//Description:
using System;



class MainClass {


  static bool exit = false;  //Used to let the user exit the program. 


  //side length of the cubic game board 
  static int boardLen = 3;


  public static void Main (string[] args) 
  {

  //This is the main loop of the program. After each complete game, the user is redirected to the menu, where they can choose to quit or play again. 
    while(!exit) 
    {
      Console.Clear();
      Menu(); 

    }

  }

  //This method serves the menu to the user. It lets them quit, or play a game. 
  public static void Menu () 
  {
    //Variables & Objects
    char option; //Will be used to navigate menu
    Console.Clear(); 

    Console.WriteLine("TIC STAC TOE \n-----------------------\n1. Play Game \n2. Quit");


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


  //This is the method where the 3d array is stored, and where the game will take place. 

  public static void MainGameLoop() 
  {
    bool gameOver = false; //Used to exit the game loop when the game is over
    int lastResult = -1; //This int stores the result of each round (a player won, there is a draw, no one has won yet). 


    /*Below is the game board. 
      - 0 represents an empty cell
      - 1 represents an X 
      - 2 represents an O
    
    The first dimension represents the rows of each board looking from top down, the second dimension represents the columns looking from top down, and the third dimension represents the 'depth' or which 'board'is being referred to. 
    */
    int [,,] gameBoard = new int[boardLen,boardLen,boardLen]; 


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

      while(!breakErrorLoop) 
      {
        

        Console.Clear();


        //Prints the game board
        PrintGameBoard(gameBoard, playerTurn); 


        //See above for info on the ternaary expression. 
        Console.WriteLine($"Enter the row and column in which you'd like to place your {playerTurn == 0 ? "X" : "O"}.");

        Console.Write("Row: ");
        rowStr = Console.ReadKey().KeyChar.ToString(); //Converts user char input to string for easier conversion to int

        Console.Write("\nColumn: ");

        colStr = Console.ReadKey().KeyChar.ToString(); 

        Console.WriteLine(); //Creates a line break. 

        //These conditionals first check if the user entered a valid integer, then they check if that integer is within the correct range. These conditionals are intended to be as generalizable as possible, which is why the array length function is used as opposed to simply putting an integer. 
        if (Int32.TryParse(rowStr, out row) && Int32.TryParse(colStr, out col))
        {

          //This if statement checks if the user has entered numbers within the correct range. If the numbers are within the correct range, then it updates the gameBoard array.
          if (row >= 1 && row <= boardLen && col >= 1 && col <= boardLen) //gameBoard.GetLength(n) is the length of the n-th dimension of the area. 
          {

            //This for loop iterates through the different boards or "depths", and checks if there is already a letter there. It places the user's letter in the lowest empty square. 
            for (int i = boardLen - 1; i >= 0; i--)
            {
              if (gameBoard[row - 1, col -1, i] == 0) //If the depth i at the user-given row and column is empty, place the user's letter there. 
              {
                gameBoard[row - 1, col - 1, i] = playerTurn + 1; //Update array with user's letter

                //Checks if a player has won or if there's a draw (i.e., the game board is full)
                lastResult = CheckGameEnd(gameBoard, row - 1, col - 1, i, playerTurn); 

                breakErrorLoop = true; //Tells the error trapping loop to break
                break;
              }

              //If all three slots at the given index are filled, then alert the user and let them try again. 
              if (gameBoard[row-1, col-1, i] != 0 && i == 0)
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

      switch (lastResult)
      {
        case 0: 
          PrintGameBoard(gameBoard, playerTurn);
          Console.WriteLine("It's a draw! (Press any key to continue.)"); 
          gameOver = true; 
          Console.ReadKey(); 
          break;
        case 1: 
          PrintGameBoard(gameBoard, playerTurn); 
          Console.WriteLine($"Player {playerTurn + 1} has won! (Press any key to continue.)");
          gameOver = true; 
          Console.ReadKey();
          break;
      }
      
      playerTurn = (playerTurn + 1) % 2; //Mod 2 so that the only possible values are 0 and 1. 
    }
  }



  //This method prints the gameboard in a nicely formatted way that resembles 3 tic tac toe boards stacked on top of each other. 
  public static void PrintGameBoard(int [,,] gameBoard, int playerTurn)
  {
    
    String board = ""; 
    String [] numToLetter = {" ", "X", "O"}; //An array that determines whether to place a space, an X, or an O in each piece depending on the contents of the game board at specific locations. 

    for (int i = 0; i < boardLen; i++) //This loop iterates through the 'depth' dimension.
    {
      String padding = " "; //Will be used to add spaces at the start of each line so the boards look like they're on an angle (to get that 3D effect)

      board += "+-----+-----+-----+\n";

      //The two loops below iterate in descending order so that the game is displayed the 'right way' (the left column is first, the top row is first). 
      for (int j = 0; j < boardLen; j++) //This loops iterates through the 'rows' dimension
      {

        board += padding + "\\";

        padding += " "; 

        for (int k = 0; k < boardLen; k++) //This loop iterates through the 'columns' dimension
        {

          // If gameBoard[j,k,i] == 0, it will print a space, if it equals 1 it will print an X, and if it equals two it will print an O (see the way the numToLetter array is defined above). 

          //The new square or 'cell' is then added to the board.
          board += $"  {numToLetter[gameBoard[j, k, i]]}  \\"; 
          
        }

        board += "\n" + padding + "+-----+-----+-----+\n";

        padding += " ";

      }


      board +="\n"; //Creates spacing between boards
    }

    // The ternary expression here returns an 'X' if playerTurn is equal to zero, and an 'O' otherwise. This makes sense since 'X' always goes first.
    Console.Clear();
    Console.WriteLine ($"PLAYER {playerTurn + 1}'S TURN ({playerTurn == 0 ? "X" : "O"}) \n-----------------------\n");

    Console.WriteLine(board); 
  }

  /* This method checks if the game has ended and returns an int. A -1 means it hasn't ended, a 1 means a player has won (there's no need to specify which player has won as that information will be known), and a 0 means the game ended in a tie.

  It takes as input the game board, the location of the last letter placed, and the 'ID' of the player who placed it. 
  */
  public static int CheckGameEnd(int [,,] gameBoard, int lastRow, int lastColumn, int lastDepth, int lastPlayer) {


    //Checking all non-diagonal lines that go through the player's last entry is in. This for loop only runs three times since there are always only three orthogonal non-diagonal lines going through a point on a 3D gameboard. 
    bool winFound = true; //This is set to true by default for efficiency. It is only set to false if one of the letters in the line isn't the same as the last letter placed by the player. 


    for (int i = 0; i < 3; i++)
    {
      //Checks for non-diagonal lines going through the last letter placed. 
      for (int j = 0; j < boardLen; j++)
      {
        
        winFound = true; //Resets the value of winFound

        /*The ternary expressions here ensure that the dimension being incremented is the same each time. Here's an example of how it would check if there are 3 X's in a row for i = 0:
        - Check if [0, y, z] is an X
        - Check if [1, y, z] is an X
        - Check if [2, y, z] is an X
        
        where [x, y, z] is the location of the last X the player put. Doing this for all 3 dimensions checks all 3 possible non-diagonal lines going through [x, y, z].  

        for = 1 it would be: 
        - Check if [x, 0, z] is an X
        - Check if [x, 1, z] is an X
        - Check if [x, 2, z] is an X

        And so on. 

        The ternary expressions work by checking if the expression (i + n) % 3 (for n = 0, 2, 1) is equal to zero. If it is equal to zero, then that is the row/column/depth that will be incremented during this loop, and otherwise it is set to the coordinates of the last piece placed. The (i+n) % 3 expression ensures that only 1 dimension is incremented at a time (only one of them is equal to zero for any given i), so that only straight (non-diagonal) lines are checked.*/
        int rowToCheck = i % 3 == 0 ? j : lastRow; 
        int colToCheck = (i + 2) % 3 == 0 ? j : lastColumn; 
        int depthToCheck = (i + 1) % 3 == 0 ? j : lastDepth;

        int blockValue = gameBoard[rowToCheck, colToCheck, depthToCheck]; 

        //If the letter in the block isn't equal to the letter the last player put down, set winFound to false and break the loop. 
        if (blockValue != lastPlayer + 1) //A 1 is added because the piece each player presses corresponds to their ID + 1
        {
          winFound = false; 
          break;
        }
      }



      //If a straight line is found, return true (this also breaks the loop)
      if (winFound)
      {
        return 1; 
      }
    }

    return -1; //If game endings are found in the loop above, return -1
  }
}

