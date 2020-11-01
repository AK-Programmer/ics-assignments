//Author: Adar Kahiri
//File Name: Program.cs
//Project Name: PASS2
//Creation Date: October 20, 2020
//Modified Date: Nov 1, 2020
/* Description: this is the driver class for the program. It contains an instance of the canvas class, and has methods that allow the user to interact with the methods/subprograms of
 * the canvas class via the ManipulateCanvas() method. 
 */

using System;
using System.Text;

namespace PASS2
{
    class Program
    {
        static Canvas canvas = new Canvas();
        static bool exit = false;


        //Pre: technically none.
        //Post: none.
        //Description: the main method. It calls the menu method which either sends the user to the canvas or exits the program, can continues to loop back to it until the user decides to exit.
        static void Main(string[] args)
        {
            
            for(int i = 0; i < 90; i ++)
            {
                Console.Write("0");
            }

            for (int i = 0; i < 29; i++)
            {
                Console.WriteLine("0");
            }

            Console.ReadKey();

            ManipulateCanvas();
            /*while (!exit)
             {
                 Menu();
             }*/
        }

        public static void Menu()
        {
            //Variables & Objects
            int option; //Will be used to navigate menu
            Console.Clear();

            Console.WriteLine("SHAPE DRAWER \n-----------------------\n1. Draw! \n2. Exit");

            //Reads single key input and converts from ConsoleKeyInfo object to char data type
            option = GetInput(1,2, "That's not an option.");

            //This switch statement either sends the user(s) to the actual game, quits, or handles their faulty input and sends them to the main menu again. 
            switch (option)
            {
                case '1':
                    ManipulateCanvas();
                    break;
                case '2':
                    exit = true;
                    Console.WriteLine("\nThanks for playing. Bye!");
                    break;
                default:
                    Console.WriteLine("\nThat's not a valid option. Try again (Press any key to continue).");
                    Console.ReadKey();
                    break;
            }
        }


        //Pre: None
        //Pro: None
        //Description: This method lets the user navigate the canvas
        public static void ManipulateCanvas()
        {
            int userOption;
            
            PrintCanvas();
            Console.WriteLine("SHAPE DRAWER \n-----------------------\nWhat would  you like to do?\n1. Add a shape \n2. Delete a shape \n3. Modify a shape \n4. View one shape \n5. Clear the canvas \n6. Exit.\n");
                

            userOption = GetInput(1,6, "That's not an option.");

            //Each of these if statements directs the user to a different canvas method, and always calls the ManipulateCanvas() method again (except the last one) to let the user manipulate the canvas in another way.
            switch(userOption)
            {
                case 1:
                    canvas.AddShape();
                    ManipulateCanvas();
                    break;
                case 2:
                    canvas.DeleteShape();
                    ManipulateCanvas();
                    break;
                case 3:
                    canvas.ModifyShape();
                    ManipulateCanvas();
                    break;
                case 4:
                    canvas.ViewShape();
                    ManipulateCanvas();
                    break;
                case 5:
                    canvas.ClearCanvas();
                    ManipulateCanvas();
                    break;
                default:
                    break;
                }
      
            
        }


        //Pre: min  must be smaller than max.
        //Post: returns the user's input as a double.
        //Description: This method ensures the user enters valid input by continuing to prompt them until they do so.
        public static double GetInput(double min, double max, string errorMessage)
        {
            double input;

            while (true)
            {
                try
                {
                    input = Convert.ToDouble(Console.ReadLine());

                    //If user input is not within the specified range, throw an exception with the given error message.
                    if (input > max || input < min)
                    {
                        if (errorMessage == "")
                            throw new Exception();

                        throw new Exception(errorMessage);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + " (Press ENTER to continue). ");
                    Console.ReadLine();
                    ClearLines(3);
                    continue;
                }


                ClearLines(2);
                return input;
            }
        }

        //Pre: min  must be smaller than max.
        //Post: returns the user's input as a double.
        //Description: This method ensures the user enters valid input by continuing to prompt them until they do so.
        public static int GetInput(int min, double max, string errorMessage)
        {
            int input;

            while (true)
            {
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                    //If user input is not within the specified range, throw an exception with the given error message.
                    if (input > max || input < min)
                    {
                        if (errorMessage == "")
                        {
                            throw new Exception();
                        }
                        throw new Exception(errorMessage);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + " (Press ENTER to continue). ");
                    Console.ReadLine();
                    ClearLines(3);
                    continue;
                }

                ClearLines(2);
                return input;
            }
        }


        //Pre: numLines must be an integer greater than zero. 
        //Post: None.
        //Description: This method clears the last numLines lines of the console.
        public static void ClearLines(int numLines)
        {
            for (int i = 0; i < numLines; i++)
            {
                //Move the cursor up
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                //Replace whatever was on that line with spaces
                Console.Write(new string(' ', Console.BufferWidth));
                //Move the cursor up
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
        }


        public static void PrintCanvas()
        {
            Console.Clear();
            Console.WriteLine("CANVAS \n-------------");
            canvas.ViewShapeList();

        }
    }
}