//Author: Adar Kahiri
//File Name: Canvas.cs
//Project Name: PASS2
//Creation Date: October 21, 2020
//Modified Date: October 30, 2020
/* Description: this class contains the code for the canvas object. It keeps track of all the shapes, and contains all the methods needed to add, delete, modify, and view the shapes. 
 */

using System;
using System.Collections.Generic;


namespace PASS2
{
    public class Canvas
    { 
        private const int MAX_NUM_SHAPES = 6;
        //These public constants are used throughout the rest of the program to ensure shapes stay inside the canvas.
        public const int SCREEN_WIDTH = 90;
        public const int SCREEN_HEIGHT = 30;
        public const int SCREEN_DEPTH = 50;

        private List<Shape> shapes;

        //Pre: None
        //Post: None
        //Description: all the constructor does is instantiate the shape list. 
        public Canvas()
        {
            shapes = new List<Shape>();
        }

        //Pre: None
        //Post: None
        //Description: This method prints all of the shapes' basic properties.
        public void ViewShapeList()
        {
            if (shapes.Count > 0)
            {
                //For each of the shapes in the list, call its GetBasicInfo method and format it nicely with its place (i+1) in the list.
                for (int i = 0; i < shapes.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {shapes[i].GetBasicInfo()}");
                    Console.WriteLine();
                }
            }
            //If there are no shapes in the list, display the following.
            else
                Console.WriteLine("There are no shapes on the canvas.");
        }

        //Pre: None
        //Post: None
        //Description: This method takes the user through the process of adding a shape. All input is error-trapped. All while loops are used to repeat the prompts until the user enters correct input.
        public void AddShape()
        {
            //If there is room for another shape, allow the user to add a shape. Otherwise, tell them they must first delete a shape.
            if (shapes.Count < MAX_NUM_SHAPES)
            {
                char userColourChoice;
                char userShapeChoice;
                string colour;
                string[] userInput;

                //Let user select desired shape
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("ADD A SHAPE \n-------------\n");
                    Console.WriteLine("Choose one of the following shapes to add: \n1. Circle \n2. Line \n3. Triangle \n4. Rectangle \n5. Sphere");
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

                //Add circle
                if (userShapeChoice == '1')
                {
                    //one of the doubles having a value of -1 indicates to the program that that variable hasn't yet been set properly
                    double xCoord = -1;
                    double yCoord = -1;
                    double rad = -1;
                    Point circleCenter;
                    Circle circle;

                    while(true)
                    {
                        //Try catch block is used to catch any exceptions thrown by either the point class, shape class, or standard library methods and handle them appropriately (getting user to try again)
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("ADD A CIRCLE \n-------------\n");

                            //These if statements ensure that a user is only prompted to enter an input if that variable has not been validly set yet.
                            if (xCoord < 0)
                            {
                                Console.Write("Enter the coordinates of the center point in the x,y format: ");
                                userInput = Console.ReadLine().Split(',');
                                
                                if (userInput.Length != 2)
                                    throw new IndexOutOfRangeException("Your input was not entered in the right format.");

                                xCoord = Convert.ToDouble(userInput[0]);
                                yCoord = Convert.ToDouble(userInput[1]);
                            }

                            //Instantiates  the center point of the circle.
                            circleCenter = new Point(xCoord, yCoord);
                            Console.WriteLine($"First point set at ({xCoord}, {yCoord})!");

                            Console.Write("\nEnter the radius of the circle: ");
                            rad = Convert.ToDouble(Console.ReadLine());

                            Console.WriteLine("Successfully set the first point!");
                            circle = new Circle(rad, colour, circleCenter);
                            shapes.Add(circle);

                            Console.WriteLine("Your circle has been added! (Press any key to continue).");
                            Console.ReadKey();
                            break;
                        }
                        //This catches the exception that might be thrown by the Convert.ToDouble() Method
                        catch(FormatException)
                        {
                            Console.WriteLine("One or both of the numbers you entered is not valid.");
                        }
                        //This catches the exception that may be thrown if the userInput array's length does not equal 2
                        catch(IndexOutOfRangeException e)
                        {
                            Console.WriteLine("The numbers weren't entered in the right format. Please try again.");
                        }
                        //This catches any exception that may be thrown by either the point or circle class
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
                        }

                        Console.ReadKey();
                    }
                    
                    
                }
                //Add Line
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
                            Console.Clear();
                            Console.WriteLine("ADD A LINE \n-------------\n");



                            
                            if (point1X < 0)
                            {
                                Console.Write("Enter the coordinates of the first point in the x,y format: ");
                                userInput = Console.ReadLine().Split(',');
                                if (userInput.Length != 2)
                                    throw new IndexOutOfRangeException("Your input was not entered in the right format.");

                                point1X = Convert.ToDouble(userInput[0]);
                                point1Y = Convert.ToDouble(userInput[1]);
                            }

                            point1 = new Point(point1X, point1Y);
                            point1Init = true;

                            Console.WriteLine($"Successfully set the first point at ({point1X}, {point1Y})");



                            if (point2X < 0)
                            {
                                Console.Write("Enter the coordinates of the second point in the x,y format: ");
                                userInput = Console.ReadLine().Split(',');
                                if (userInput.Length != 2)
                                    throw new IndexOutOfRangeException("Your input was not entered in the right format.");

                                point2X = Convert.ToDouble(userInput[0]);
                                point2Y = Convert.ToDouble(userInput[1]);
                            }

                            point2 = new Point(point2X, point2Y);

                            if (point2X < 0)
                            {
                                Console.Write("Enter the x-coordinate of the second point: ");
                                point2X = Convert.ToDouble(Console.ReadLine());
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
                        catch (IndexOutOfRangeException e)
                        {
                            Console.WriteLine("The numbers weren't entered in the right format. Please try again.");
                            Console.ReadKey();
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            if (!point1Init)
                            {
                                point1X = -1;
                                point1Y = -1;
                            }
                            else
                            {
                                point2X = -1;
                                point2Y = -1;
                            }

                            Console.WriteLine($"{e.Message} (Press any key to continue).");
                            Console.ReadKey();
                        }
                        catch(ArgumentException e)
                        {
                            point1X = -1;
                            point1Y = -1;
                            point2X = -1;
                            point2Y = -1;

                            Console.WriteLine($"{e.Message} (Press any key to continue).");
                            Console.ReadKey();
                        }
                    }
                }
                //Add Triangle
                else if (userShapeChoice == '3')
                {
                    //
                    double p1X = -1, p1Y = -1, p2X = -1, p2Y = -1, p3X, p3Y;
                    Point point1, point2, point3;
                    Triangle triangle;
                    bool p1Init = false;
                    bool p2Init = false;

                    while (true)
                    {
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("ADD A TRIANGLE \n-------------\n");

                            //First point
                            if (p1X < 0)
                            {
                                Console.Write("Enter the coordinates of the first point in the x,y format: ");
                                userInput = Console.ReadLine().Split(',');
                                if (userInput.Length != 2)
                                    throw new IndexOutOfRangeException("Your input was not entered in the right format.");

                                p1X = Convert.ToDouble(userInput[0]);
                                p1Y = Convert.ToDouble(userInput[1]);
                            }

                            point1 = new Point(p1X, p1Y);
                            p1Init = true;
                            Console.WriteLine($"Successfully set the first point! ({point1.X}, {point1.Y})");

                            //Second point
                            if (p2X < 0)
                            {
                                Console.Write("Enter the coordinates of the second point in the x,y format: ");
                                userInput = Console.ReadLine().Split(',');
                                if (userInput.Length != 2)
                                    throw new IndexOutOfRangeException("Your input was not entered in the right format.");

                                p2X = Convert.ToDouble(userInput[0]);
                                p2Y = Convert.ToDouble(userInput[1]);
                            }

                            point2 = new Point(p2X, p2Y);
                            p2Init = true;
                            Console.WriteLine($"Successfully set the second point! ({p2X}, {p2Y})");


                            //Last point
                            Console.Write("Enter the coordinates of the third point in the x,y format: ");
                            userInput = Console.ReadLine().Split(',');
                            if (userInput.Length != 2)
                                throw new IndexOutOfRangeException("Your input was not entered in the right format.");

                            p3X = Convert.ToDouble(userInput[0]);
                            p3Y = Convert.ToDouble(userInput[1]);
                            point3 = new Point(p3X, p3Y);

                            triangle = new Triangle(colour, point1, point2, point3);
                            shapes.Add(triangle);

                            Console.WriteLine("Your triangle has been added!");
                            Console.ReadKey();
                            break;
                        }
                        catch (FormatException)
                        {
                            if (!p1Init)
                            {
                                p1X = -1;
                                p1Y = -1;
                            }
                            else if (!p2Init)
                            {
                                p2X = -1;
                                p2Y = -1;
                            }

                            Console.WriteLine("That's not a valid number. Try again. (Press any key to continue).");
                            Console.ReadKey();
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            if (!p1Init)
                            {
                                p1X = -1;
                                p1Y = -1;
                            }
                            else if (!p2Init)
                            {
                                p2X = -1;
                                p2Y = -1;
                            }

                            Console.WriteLine("The numbers weren't entered in the right format. Try again. (Press any key to continue).");

                            Console.ReadKey();
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            if (!p1Init)
                            {
                                p1X = -1;
                                p1Y = -1;
                            }
                            else if (!p2Init)
                            {
                                p2X = -1;
                                p2Y = -1;
                            }

                            Console.WriteLine($"{e.Message} (Press any key to continue).");
                            Console.ReadKey();
                        }
                        catch(ArgumentException e)
                        {
                            Console.WriteLine($"{e.Message} (Press any key to continue).");
                            Console.ReadKey();
                            p1X = -1;
                            p1Y = -1;
                            p2X = -1;
                            p2Y = -1;
                            p3X = -1;
                            p3Y = -1;
                        }
                    }
                }
                //Add Rectangle
                else if (userShapeChoice == '4')
                {
                    double pointX = -1, pointY = -1, length = -1, height = -1;
                    Point anchorPoint;
                    Rectangle rectangle;

                    while(true)
                    {
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("ADD A RECTANGLE \n-------------\n");


                            if (pointX < 0)
                            {
                                Console.Write("Enter the coordinates of the anchor point in the x,y format: ");
                                userInput = Console.ReadLine().Split(',');

                                if (userInput.Length != 2)
                                    throw new IndexOutOfRangeException();

                                pointX = Convert.ToDouble(userInput[0]);
                                pointY = Convert.ToDouble(userInput[1]);
                            }
                            
                            try
                            {
                                anchorPoint = new Point(pointX, pointY);
                            }
                            catch(ArgumentOutOfRangeException e)
                            {
                                if (e.ParamName == "point x-coord")
                                    pointX = -1;
                                else if (e.ParamName == "point y-coord")
                                    pointY = -1;

                                Console.WriteLine($"{e.Message} (Press any key to continue).");
                                Console.ReadKey();
                                continue;
                            }
                            Console.WriteLine($"Successfully set the anchor point! ({anchorPoint.X}, {anchorPoint.Y})");

                            if (length < 0)
                            {
                                Console.Write("Enter the length and height in the x,y format: ");
                                userInput = Console.ReadLine().Split(',');

                                if (userInput.Length != 2)
                                    throw new IndexOutOfRangeException();

                                length = Convert.ToDouble(userInput[0]);
                                height = Convert.ToDouble(userInput[1]);
                            }

                            rectangle = new Rectangle(colour, length, height, anchorPoint);
                            shapes.Add(rectangle);
                            Console.WriteLine("Your rectangle has been added!");
                            Console.ReadKey();
                            break;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("That's not a valid number. Try again. (Press any key to continue).");
                        }
                        catch(IndexOutOfRangeException)
                        {
                            Console.WriteLine("Your input was not entered in the right format. Try again. (Press any key to continue).");
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            if (pointX + length > SCREEN_WIDTH)
                            {
                                length = -1;
                                Console.WriteLine("A rectangle with that length would go off screen. Try decreasing the length or repositioning the rectangle. (Press any key to continue).");
                            }

                            if (pointY - height < 0)
                            {
                                height = -1;
                                Console.WriteLine("A rectangle with that height would go off screen. Try decreasing the height or repositioning the rectangle. (Press any key to continue).");
                            }
                        }

                        Console.ReadKey();
                    }
                }
                //Add Sphere
                else if (userShapeChoice == '5')
                {

                }
                //Add Rectangular Prism
                else if (userShapeChoice == '6')
                {

                }

            }
            else
            {
                Console.WriteLine("\nSorry, there are too many shapes on the screen. You must delete a shape before adding another one. (Press any key to continue.)");
                Console.ReadKey();
            }
        }

        public void ViewShape()
        {
            int viewShapeIndex;

            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("VIEW A SHAPE \n---------------");

                    Console.WriteLine("Which of the following shapes would you like to view?");
                    ViewShapeList();

                    viewShapeIndex = Convert.ToInt32(Console.ReadLine()) - 1;

                    if (viewShapeIndex < 0 || viewShapeIndex > shapes.Count - 1)
                        throw new FormatException();

                    //Add spacing
                    Console.WriteLine("\nShape Attributes: ");

                    shapes[viewShapeIndex].PrintAttributes();

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
            char userResponse;

            if (shapes.Count > 0)
            {
                int delShapeIndex;

                while (true)
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("DELETE A SHAPE \n---------------");

                        Console.WriteLine("Which of the following shapes would you like to delete?\n");
                        ViewShapeList();

                        delShapeIndex = Convert.ToInt32(Console.ReadLine()) - 1;

                        if (delShapeIndex < 0 || delShapeIndex > shapes.Count - 1)
                            throw new FormatException();


                        Console.WriteLine("Are you sure you would like to delete this shape? (Press 'y' if yes, and anything else if not");
                        shapes[delShapeIndex].PrintAttributes();

                        userResponse = Console.ReadKey().KeyChar;

                        if (userResponse == 'y')
                        {
                            Console.WriteLine("Shape deleted! (Press any key to continue).");
                            shapes.RemoveAt(delShapeIndex);
                        }
                        else
                        {
                            Console.WriteLine("No shape deleted! (Press any key to continue).");
                        }
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
            else
            {
                Console.Clear();
                Console.WriteLine("There are no shapes to delete. Add a shape first! (Press any key to continue).");
                Console.ReadKey();
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

                        if (modShapeIndex < 0 || modShapeIndex > shapes.Count - 1)
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

                    Console.WriteLine("\n\nHow would you like to modify this shape?\n1. Translate it \n2. Scale it \n3.Check for intersection with point");

                    userChoice = Console.ReadKey().KeyChar;


                    if (userChoice == '1')
                    {
                        double translateX = -1;
                        double translateY = -1;
                        double translateZ;

                        while(true)
                        {
                            try
                            {
                                Console.Clear();
                                Console.WriteLine("TRANSLATE SHAPE \n---------------");

                                Console.WriteLine("Shape Attributes: ");
                                shapes[modShapeIndex].PrintAttributes();

                                if (translateX < 0)
                                {
                                    Console.WriteLine("\nHow much would you like to translate your shape along the x-axis? (negative =  left, positive = right)");
                                    translateX = Convert.ToDouble(Console.ReadLine());
                                }

                                if (translateY < 0)
                                {
                                    Console.WriteLine("\nHow much would you like to translate your shape along the y-axis? (negative =  down, positive = up)");
                                    translateY = Convert.ToDouble(Console.ReadLine());
                                }
                                
                                if (shapes[modShapeIndex].GetIs3D())
                                {
                                    Console.WriteLine("\nHow much would you like to translate your shape along the y-axis? (negative =  down, positive = up)");
                                    translateZ = Convert.ToDouble(Console.ReadLine());
                                }
                                else
                                    translateZ = 0;

                                shapes[modShapeIndex].TranslateShape(translateX, translateY, translateZ);

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

                                Console.WriteLine("Shape Attributes: ");
                                shapes[modShapeIndex].PrintAttributes();

                                Console.WriteLine("\nBy how much would you like to scale your shape?");
                                scaleFactor = Convert.ToDouble(Console.ReadLine());

                                shapes[modShapeIndex].ScaleShape(scaleFactor);

                                Console.WriteLine("Your shape has been scaled. New shape attributes: ");
                                shapes[modShapeIndex].PrintAttributes();
                                Console.WriteLine("\nPress any key to continue.");
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
                        double pointY;
                        double pointX = -1;
                        Point pointToCheck;
                        bool doesIntersect;

                        while (true)
                        {
                            try
                            {
                                Console.Clear();
                                Console.WriteLine("CHECK FOR INTERSECTION \n---------------------");
                                Console.WriteLine("Shape Attributes: ");
                                shapes[modShapeIndex].PrintAttributes();


                                if (pointX < 0)
                                {
                                    Console.WriteLine("\nEnter the x-coordinate of the point you'd like to check: ");
                                    pointX = Convert.ToDouble(Console.ReadLine());
                                }

                                Console.WriteLine("Enter the y-coordinate of the point you'd like to check: ");
                                pointY = Convert.ToDouble(Console.ReadLine());


                                pointToCheck = new Point(pointX, pointY);
                                doesIntersect = shapes[modShapeIndex].CheckIntersectionWithPoint(pointToCheck);

                                if (doesIntersect)
                                {
                                    Console.WriteLine($"The point ({pointX}, {pointY}) does intersect this shape. (Press any key to continue).");
                                }
                                else
                                {
                                    Console.WriteLine($"The point ({pointX}, {pointY}) does not intersect this shape. (Press any key to continue).");
                                }

                                Console.ReadKey();

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
                        Console.Clear();
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
