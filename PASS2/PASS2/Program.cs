using System;

namespace PASS2
{
    class Program
    {
        static bool exit = false;


        static void Main(string[] args)
        {

            Canvas canvas = new Canvas();

            canvas.AddShape();

        }


        public void Menu()
        {
            //Variables & Objects
            char option; //Will be used to navigate menu
            Console.Clear();

            Console.WriteLine("SHAPE DRAWER \n-----------------------\n1. Draw! \n2. Exit");

            Console.WriteLine("\n\nHOW TO PLAY\n----------------\nYou will be assigned either an X or an O. \nYour goal is to get three of your letter in a row.");

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
                    Menu();
                    break;
            }
        }

        public void ManipulateCanvas()
        {

        }
    }
}
