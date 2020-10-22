using System;
using System.Collections.Generic;


namespace PASS2
{
    public class Canvas
    {
        private const int MAX_NUM_SHAPES = 6;
        public const int SCREEN_WIDTH = 90;
        public const int SCREEN_HEIGHT = 30;

        List<Shape> shapes;

        public Canvas()
        {

        }


        public void ViewShapeList()
        {
            Console.WriteLine("ALL SHAPES \n-------------");

            for (int i = 0; i < MAX_NUM_SHAPES; i++)
            {
                Console.WriteLine($"{i + 1}. {shapes[i].GetBasicInfo()}");
            }
        }


        public void AddShape()
        {
            if (shapes.Count < MAX_NUM_SHAPES)
            {
                char userColourChoice;
                char userShapeChoice;

                string colour;

                Point[] shapePoints; 


                //Let user select desired shape
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("ADD A SHAPE \n-------------\n");

                    Console.WriteLine("Choose one of the following shapes to add: \n1. Circle \n2. Line \n3. Triangle \n4. Rectangle");
                    userShapeChoice = Console.ReadKey().KeyChar;


                    if (userShapeChoice == '1' || userShapeChoice == '2' || userShapeChoice == '3' || userShapeChoice == '4')
                    {
                        break;
                        
                    }
                    else
                    {
                        Console.WriteLine("\nThat's not a valid option. Please try again (Press any key to continue).");
                        Console.ReadKey();
                    }

                }

                //Let user select desired colour
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("ADD A SHAPE \n-------------\n");

                    Console.WriteLine("Which colour would you like your shape to be? \n1. Red \n2. Green \n3. Blue \n4. Orange");
                    userColourChoice = Console.ReadKey().KeyChar;


                    if (userColourChoice == '1')
                    {
                        colour = "Red";
                        break;
                    }
                    else if (userColourChoice == '2')
                    {
                        colour = "Green";
                        break;
                    }
                    else if (userColourChoice == '3')
                    {
                        colour = "Blue";
                        break;
                    }
                    else if (userColourChoice == '4')
                    {
                        colour = "Orange";
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nThat's not a valid option. Please try again (Press any key to continue).");
                        Console.ReadKey();
                    }

                }

                //Circle
                if (userShapeChoice == '1')
                {
                    string xCoordStr;
                    string yCoordStr;
                    string radStr;

                    double xCoord = -1;
                    double yCoord = -1;
                    double rad = -1;

                    Point circleCenter;

                    Circle circle;

                    while(true)
                    {
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("ADD A SHAPE \n-------------\n");

                            if (xCoord < 0)
                            {
                                Console.Write("Enter the x-coordinate of the circle's center: ");
                                xCoordStr = Console.ReadLine();
                                xCoord = Convert.ToDouble(xCoordStr);
                            }

                            if (yCoord < 0)
                            {
                                Console.Write("\nEnter the y-coordinate of the circle's center: ");
                                yCoordStr = Console.ReadLine();
                                yCoord = Convert.ToDouble(yCoordStr);
                            }

                            circleCenter = new Point(xCoord, yCoord);

                            if (rad < 0)
                            {
                                Console.Write("\nEnter the radius of the circle: ");
                                radStr = Console.ReadLine();
                                rad = Convert.ToDouble(radStr);
                            }

                            circle = new Circle(rad, colour, circleCenter);

                            shapes.Add(circle);

                            Console.WriteLine("Your circle has been added!");

                        }
                        catch(FormatException e)
                        {
                            Console.WriteLine("That's not a valid number. Try again. (Press any key to continue).");
                            Console.ReadKey();
                            continue;
                        }
                        catch(ArgumentOutOfRangeException e)
                        {
                            if (e.ParamName == "point x-coord")
                                xCoord = -1;

                            else if (e.ParamName == "point y-coord")
                                yCoord = -1;

                            else if (e.ParamName == "Radius")
                                rad = -1;

                            else
                            {
                                xCoord = -1;
                                yCoord = -1;
                                rad = -1;
                            }

                            Console.WriteLine($"{e.Message} (Press any key to continue).");
                            Console.ReadKey();
                        }
                    }
                    
                    




                }
                //Line
                else if (userShapeChoice == '2')
                {

                }
                //Triangle
                else if (userShapeChoice == '3')
                {

                }
                //Rectangle
                else
                {

                }


            }
            else
            {
                Console.WriteLine("Sorry, there are too many shapes on the screen. You must delete a shape before adding another one. (Press any key to continue.)");
                Console.ReadKey();
            }
        }

        public void ViewShape()
        {

        }

        public void DeleteShape()
        {

        }


        public void ModifyShape()
        {

        }


        public void ClearCanvas()
        {
            shapes.Clear();
            Console.Clear();

            Console.WriteLine("The canvas has been cleared. (Press any key to continue).");
            Console.ReadKey();

        }
    }
}
