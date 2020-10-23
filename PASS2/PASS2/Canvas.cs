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
            shapes = new List<Shape>();
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

                Console.Clear();

                //Circle
                if (userShapeChoice == '1')
                {
                    double xCoord = -1;
                    double yCoord = -1;
                    double rad = -1;

                    Point circleCenter;

                    Circle circle;

                    while(true)
                    {
                        try
                        {
                            Console.WriteLine("ADD A CIRCLE \n-------------\n");

                            if (xCoord < 0)
                            {
                                Console.Write("Enter the x-coordinate of the circle's center: ");
                                xCoord = Convert.ToDouble(Console.ReadLine());
                            }

                            if (yCoord < 0)
                            {
                                Console.Write("\nEnter the y-coordinate of the circle's center: ");
                                yCoord = Convert.ToDouble(Console.ReadLine());
                            }

                            circleCenter = new Point(xCoord, yCoord);

                            if (rad < 0)
                            {
                                Console.Write("\nEnter the radius of the circle: ");
                                rad = Convert.ToDouble(Console.ReadLine());
                            }

                            circle = new Circle(rad, colour, circleCenter);

                            shapes.Add(circle);

                            Console.WriteLine("Your circle has been added! (Press any key to continue).");
                            Console.ReadKey();
                            break;

                        }
                        catch(FormatException)
                        {
                            Console.WriteLine("That's not a valid number. Try again. (Press any key to continue).");
                            Console.ReadKey();
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
                    
                    double point1X = -1;
                    double point1Y = -1;
                    double point2X = -1;
                    double point2Y = -1;


                    Point point1;
                    Point point2;
                    Line line;

                    bool point1Init = false;

                    while(true)
                    {
                        try
                        {
                            Console.WriteLine("ADD A LINE \n-------------\n");

                            if (point1X < 0)
                            {
                                Console.Write("Enter the x-coordinate of the first point: ");
                                point1X = Convert.ToDouble(Console.ReadLine());
                            }

                            if (point1Y < 0)
                            { 
                                Console.Write("Enter the y-coordinate of the first point: ");
                                point1Y = Convert.ToDouble(Console.ReadLine());
                            }

                            point1 = new Point(point1X, point1Y);
                            point1Init = true;

                            Console.WriteLine("Successfully set the first point!");


                            if (point2X < 0)
                            {
                                Console.Write("Enter the x-coordinate of the second point: ");
                                point2X = Convert.ToDouble(Console.ReadLine());
                            }

                            if (point2Y < 0)
                            {
                                Console.Write("Enter the y-coordinate of the second point: ");
                                point2Y = Convert.ToDouble(Console.ReadLine());

                                
                            }

                            point2 = new Point(point2X, point2Y);

                            line = new Line(colour, point1, point2);
                            shapes.Add(line);

                            Console.WriteLine("Your line has been added! (Press any key to continue).");
                            Console.ReadKey();
                            break;


                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("That's not a valid number. Try again. (Press any key to continue).");
                            Console.ReadKey();
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            if (!point1Init)
                            {
                                point1X = -1;
                                point1Y = -1;
                            }

                            Console.WriteLine($"{e.Message} (Press any key to continue).");
                            Console.ReadKey();
                        }
                    }

                }
                //Triangle
                else if (userShapeChoice == '3')
                {

                    double p1X = -1, p1Y = -1, p2X = -1, p2Y = -1, p3X = -1, p3Y = -1;
                    Point point1, point2, point3;
                    Triangle triangle;

                    bool p1Init = false;
                    bool p2Init = false;

                    while (true)
                    {
                        try
                        {
                            Console.WriteLine("ADD A TRIANGLE \n-------------\n");

                            if (p1X < 0)
                            {
                                Console.Write("Enter the x-coordinate of the first point: ");
                                p1X = Convert.ToDouble(Console.ReadLine());
                            }

                            if (p1Y < 0)
                            {
                                Console.Write("Enter the y-coordinate of the first point: ");
                                p1Y = Convert.ToDouble(Console.ReadLine());
                            }

                            point1 = new Point(p1X, p1Y);
                            p1Init = true;


                            Console.WriteLine("Successfully set the first point!");


                            if (p2X < 0)
                            {
                                Console.Write("Enter the x-coordinate of the second point: ");
                                p2X = Convert.ToDouble(Console.ReadLine());
                            }

                            if (p2Y < 0)
                            {
                                Console.Write("Enter the y-coordinate of the second point: ");
                                p2Y = Convert.ToDouble(Console.ReadLine());
                            }

                            point2 = new Point(p2X, p2Y);
                            p2Init = true;

                            Console.WriteLine("Successfully set the second point!");

                            if (p3X < 0)
                            {
                                Console.Write("Enter the x-coordinate of the third point: ");
                                p3X = Convert.ToDouble(Console.ReadLine());
                            }

                            if (p3Y < 0)
                            {
                                Console.Write("Enter the y-coordinate of the third point: ");
                                p3Y = Convert.ToDouble(Console.ReadLine());
                            }

                            point3 = new Point(p3X, p3Y);

                            triangle = new Triangle(colour, point1, point2, point3);

                            Console.WriteLine("Your triangle has been added!");
                            Console.ReadKey();
                            break;


                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("That's not a valid number. Try again. (Press any key to continue).");
                            Console.ReadKey();
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            Console.WriteLine($"{e.Message} (Press any key to continue).");
                            Console.ReadKey();


                            if (p1Init)
                            {
                                p1X = -1;
                                p1Y = -1;
                            }
                            else if(!p2Init)
                            {
                                p2X = -1;
                                p2Y = -1;
                            }
                       
                        }

                    }

                }
                //Rectangle
                else
                {
                    double pointX = -1, pointY = -1, length = -1, height = -1;

                    Point anchorPoint;

                    Rectangle rectangle;

                    while(true)
                    {
                        try
                        {
                            Console.WriteLine("ADD A RECTANGLE \n-------------\n");

                            if (pointX < 0)
                            {
                                Console.Write("Enter the x-coordinate of the first point: ");
                                pointX = Convert.ToDouble(Console.ReadLine());
                            }

                            if (pointY < 0)
                            {
                                Console.Write("Enter the y-coordinate of the first point: ");
                                pointY = Convert.ToDouble(Console.ReadLine());
                            }

                            anchorPoint = new Point(pointX, pointY);

                            Console.WriteLine("Successfully set the first point!");

                            if (length < 0)
                            {
                                Console.Write("Enter the desired length: ");
                                length = Convert.ToDouble(Console.ReadLine());
                            }

                            if (height < 0)
                            {
                                Console.Write("Enter the desired height: ");
                                height = Convert.ToDouble(Console.ReadLine());
                            }

                            rectangle = new Rectangle(colour, length, height, anchorPoint);

                            Console.WriteLine("Your rectangle has been added!");
                            Console.ReadKey();
                            break;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("That's not a valid number. Try again. (Press any key to continue).");
                            Console.ReadKey();
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            if (e.ParamName == "length")
                                length = -1;
                            else if (e.ParamName == "height")
                                height = -1;
                            else if (e.ParamName == "point x-coord")
                                pointX = -1;
                            else if (e.ParamName == "point y-coord")
                                pointY = -1;

                            Console.WriteLine($"{e.Message} (Press any key to continue).");
                            Console.ReadKey();
                        }
                    }
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
            int modShapeIndex;
            char userChoice;

            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("VIEW A SHAPE \n---------------");

                    Console.WriteLine("Which of the following shapes would you like to view?\n");
                    ViewShapeList();

                    modShapeIndex = Convert.ToInt32(Console.ReadLine()) - 1;

                    if (modShapeIndex < 1 || modShapeIndex > shapes.Count)
                        throw new FormatException();

                    //Add spacing
                    Console.WriteLine("\nShape Attributes: ");

                    shapes[modShapeIndex].PrintAttributes();

                    Console.WriteLine("\nPress any key to continue.");
                    Console.ReadKey();
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Please enter a number between 1 and {shapes.Count}. (Press any key to continue).");
                    Console.ReadKey();
                }
            }
        }



        public void DeleteShape()
        {
            int modShapeIndex;

            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("DELETE A SHAPE \n---------------");

                    Console.WriteLine("Which of the following shapes would you like to delete?\n");
                    ViewShapeList();

                    modShapeIndex = Convert.ToInt32(Console.ReadLine()) - 1;

                    if (modShapeIndex < 1 || modShapeIndex > shapes.Count)
                        throw new FormatException();


                    shapes.RemoveAt(modShapeIndex);

                    Console.WriteLine("Shape removed! (Press any key to continue).");
                    Console.ReadKey();

                    break;

                }
                catch (FormatException)
                {
                    Console.WriteLine($"Please enter a number between 1 and {shapes.Count}. (Press any key to continue).");
                    Console.ReadKey();
                }
            }
        }


        public void ModifyShape()
        {

            if (shapes.Count > 0)
            {
                int modShapeIndex;
                char userChoice;

                while (true)
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("MODIFY A SHAPE \n---------------");

                        Console.WriteLine("Which of the following shapes would you like to modify?\n");
                        ViewShapeList();

                        modShapeIndex = Convert.ToInt32(Console.ReadLine()) - 1;

                        if (modShapeIndex < 1 || modShapeIndex > shapes.Count)
                            throw new FormatException();

                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($"Please enter a number between 1 and {shapes.Count}. (Press any key to continue).");
                        Console.ReadKey();
                    }
                }


               
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("MODIFY A SHAPE \n---------------");
                    Console.WriteLine("Shape Attributes: ");
                    shapes[modShapeIndex].PrintAttributes();

                    Console.WriteLine("How would you like to modify this shape?\n1. Translate it \n2. Scale it 3.Check for intersection with point");

                    userChoice = Console.ReadKey().KeyChar;


                    if (userChoice == '1')
                    {
                        double translateX = -1;
                        double translateY = -1;

                        while(true)
                        {
                            try
                            {
                                Console.Clear();
                                Console.WriteLine("TRANSLATE SHAPE \n---------------");

                                if (translateX < 0)
                                {
                                    Console.WriteLine("How much would you like to translate your shape along the x-axis? (negative =  left, positive = right)");
                                    translateX = Convert.ToDouble(Console.ReadLine());
                                }

                                Console.WriteLine("How much would you like to translate your shape along the y-axis? (negative =  down, positive = up)");
                                translateY = Convert.ToDouble(Console.ReadLine());

                                shapes[modShapeIndex].TranslateShape(translateX, translateY);

                                break;
                            }
                            catch(FormatException)
                            {
                                Console.WriteLine("Please enter a valid number. (Press any key to continue).");
                                Console.ReadKey();
                                continue;
                            }
                            catch(ArgumentOutOfRangeException e)
                            {
                                translateX = -1;

                                Console.WriteLine($"{e.Message} (Press any key to continue).");
                                Console.ReadKey();
                                continue;
                            }
                        }

                        break;
                    }
                    else if (userChoice == '2')
                    {
                        double scaleFactor;

                        while(true)
                        {
                            try
                            {
                                Console.Clear();
                                Console.WriteLine("SCALE SHAPE \n---------------");


                                Console.WriteLine("By how much would you like to scale your shape?");
                                scaleFactor = Convert.ToDouble(Console.ReadLine());

                                shapes[modShapeIndex].ScaleShape(scaleFactor);

                                Console.WriteLine("Your shape has been scaled. New shape attributes: ");
                                shapes[modShapeIndex].PrintAttributes();
                                Console.WriteLine("Press any key to continue.");
                                Console.ReadKey();

                                break;
                            }
                            catch(FormatException)
                            {
                                Console.WriteLine("Please enter a valid number. (Press any key to continue).");
                                Console.ReadKey();
                            }
                            catch(ArgumentOutOfRangeException e)
                            {
                                Console.WriteLine($"{e.Message} (Press any key to continue).");
                                Console.ReadKey();
                            }
                        }
                        break;
                    }
                    else if (userChoice == '3')
                    {
                        double pointX = -1;
                        Point pointToCheck;

                        while (true)
                        {
                            try
                            {
                                Console.Clear();
                                Console.WriteLine("CHECK FOR INTERSECTION \n---------------------");
                                if (pointX < 0)
                                {
                                    Console.WriteLine("Enter the x-coordinate of the point you'd like to check: ");
                                    pointX = Convert.ToDouble(Console.ReadLine());
                                }

                                Console.WriteLine("Enter the y-coordinate of the point you'd like to check: ");
                                double pointY = Convert.ToDouble(Console.ReadLine());


                                pointToCheck = new Point(pointX, pointY);
                                shapes[modShapeIndex].CheckIntersectionWithPoint(pointToCheck);

                                break;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Please enter a valid number. (Press any key to continue).");
                                Console.ReadKey();
                                continue;
                            }
                            catch (ArgumentOutOfRangeException e)
                            {
                                pointX = -1;

                                Console.WriteLine($"{e.Message} (Press any key to continue).");
                                Console.ReadKey();
                                continue;
                            }
                        }

                        break;
                    }
                    else
                    {
                        Console.WriteLine("That's not a valid choice. Try again. (Press any key to continue).");
                        Console.ReadKey();
                    }
                }
            }
            else
            {
                Console.WriteLine("There are no shapes to modify. Add a shape first! (Press any key to continue).");
                Console.ReadKey();
            }
             

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
