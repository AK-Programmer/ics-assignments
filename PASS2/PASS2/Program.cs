//Author: Adar Kahiri
//File Name: Program.cs
//Project Name: PASS2
//Creation Date: October 20, 2020
//Modified Date: October 30, 2020
/* Description: this is the driver class for the program. It contains an instance of the canvas class, and has methods that allow the user to interact with the methods/subprograms of
 * the canvas class via the ManipulateCanvas() method. 
 */

using System;

namespace PASS2
{
    class Program
    {
        static Canvas canvas = new Canvas();
        static bool exit = false;


        static void Main(string[] args)
        {

            

            while(!exit)
            {
                Menu();
            }

        }


        public static void Menu()
        {
            //Variables & Objects
            char option; //Will be used to navigate menu
            Console.Clear();

            Console.WriteLine("SHAPE DRAWER \n-----------------------\n1. Draw! \n2. Exit");

            //Reads single key input and converts from ConsoleKeyInfo object to char data type
            option = Console.ReadKey().KeyChar;

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

        public static void ManipulateCanvas()
        {
            char userOption;
            while (true)
            {

                Console.Clear();
                Console.WriteLine("SHAPE DRAWER \n-----------------------\nWhat would  you like to do?\n1. Add a shape \n2. Delete a shape \n3. Modify a shape \n4. View one shape \n5. Clear the canvas \n6. Go back.\n");
                Console.WriteLine("CANVAS \n-------------");
                canvas.ViewShapeList();

                userOption = Console.ReadKey().KeyChar;

               
                if (userOption == '1')
                {
                    canvas.AddShape();
                    ManipulateCanvas();
                    break;
                }
                else if (userOption == '2')
                {
                    canvas.DeleteShape();
                    ManipulateCanvas();
                    break;

                }
                else if (userOption == '3')
                {
                    canvas.ModifyShape();
                    ManipulateCanvas();
                    break;
                }
                else if (userOption == '4')
                {
                    canvas.ViewShape();
                    ManipulateCanvas();
                    break;
                }
                else if (userOption == '5')
                {
                    canvas.ClearCanvas();
                    ManipulateCanvas();
                    break;
                }
                else if (userOption == '6')
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\nThat's not a valid option. Try again (Press any key to continue).");
                    Console.ReadKey();
                }
            }
        }
    }
}
