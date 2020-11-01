﻿//Author: Adar Kahiri
//File Name: Canvas.cs
//Project Name: PASS2
//Creation Date: October 21, 2020
//Modified Date: Nov 1, 2020
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
                int currentCursorRow = Console.CursorTop;
                int colIncrement = 30;
                int rowIncrement = 6;

                int numShapes = shapes.Count;
                int numInRow;

                Console.WriteLine(shapes.Count);
                for (int i = 0; i < (shapes.Count > 3 ? 2 : 1); i++)
                {
                    numInRow = numShapes <= 3 ? numShapes : i == 0 ? 3 : numShapes - 3;

                    for (int j = 0; j < numInRow; j++)
                    {
                        shapes[j + (3 * i)].PrintBasicInfo(colIncrement * j, currentCursorRow + i * rowIncrement, j + 3*i + 1);
                        //Console.WriteLine($"{i}, {j}, {j + 3 * i}");
                    }
                }
                //For each of the shapes in the list, call its GetBasicInfo method and format it nicely with its place (i+1) in the list.
                /*for (int i = 0; i < shapes.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {shapes[i].GetBasicInfo()}");
                    Console.WriteLine("\n\n")

                    Console.SetCursorPosition(5,)
                }*/

                Console.WriteLine("\n");
            }

            //If there are no shapes in the list, display the following.
            else
            {
                Console.WriteLine("There are no shapes on the canvas.\n\n");
            }
        }

        //Pre: None
        //Post: None
        //Description: This method takes the user through the process of adding a shape. All input is error-trapped. All while loops are used to repeat the prompts until the user enters correct input.
        public void AddShape()
        {
            //If there is room for another shape, allow the user to add a shape. Otherwise, tell them they must first delete a shape.
            if (shapes.Count < MAX_NUM_SHAPES)
            {
                int userColourChoice;
                int userShapeChoice;

                //Let user select desired shape
                Program.PrintCanvas();
                Console.WriteLine("ADD A SHAPE \n-------------\n");

                Console.WriteLine("Choose one of the following shapes to add: \n1. Circle \n2. Line \n3. Triangle \n4. Rectangle \n5. Sphere \n6. Rectangular Prism");
                userShapeChoice = Program.GetInput(1, 6, "That's not a valid shape option.");


                //Let user select desired colour
                Program.PrintCanvas();
                Console.WriteLine("CHOOSE A COLOUR \n-------------\n");

                //
                Console.WriteLine("Which colour would you like your shape to be? \n1. Red \n2. Green \n3. Blue \n4. Orange");
                userColourChoice = Program.GetInput(1, 4, "That's not a valid colour option.");
                Program.ClearLines(1);

                string colour = userColourChoice switch
                {
                    1 => "Red",
                    2 => "Green",
                    3 => "Blue",
                    4 => "Orange",
                    _ => "",
                };

                //Add circle
                if (userShapeChoice == 1)
                {
                    double rad;
                    double minDistanceFromEdge;
                    Point circleCenter;
                    Circle circle;

                    Program.PrintCanvas();
                    Console.WriteLine("ADD A CIRCLE \n-------------");

                    Console.WriteLine("Enter the coordinates of the center point. ");
                    circleCenter = GetPoint2D(true);

                    Program.ClearLines(1);

                    minDistanceFromEdge = Math.Min(circleCenter.X, Math.Min(circleCenter.Y, Math.Min(SCREEN_WIDTH - circleCenter.X, SCREEN_HEIGHT - circleCenter.Y)));

                    
                    Console.WriteLine($"Enter the radius (maximum radius is {minDistanceFromEdge}): ");
                    rad = Program.GetInput(1 / Double.MaxValue, minDistanceFromEdge, "The radius is too large!");
                    circle = new Circle(rad, colour, circleCenter);
                    shapes.Add(circle);

                    Console.WriteLine("Circle added! (Press any key to continue).");
                    Console.ReadKey();
                }
                //Add Line
                else if (userShapeChoice == 2)
                {
                    Point point1, point2;
                    Line line;

                    while (true)
                    {
                        try
                        {
                            Program.PrintCanvas();
                            Console.WriteLine("ADD A LINE \n-------------\n");

                            Console.WriteLine("Enter the coordidnates of the first point: ");
                            point1 = GetPoint3D(true);

                            Program.ClearLines(1);

                            Console.WriteLine("Enter the coordinates of the second point: ");
                            point2 = GetPoint3D(true);

                            Program.ClearLines(1);

                            line = new Line(colour, point1, point2);
                            shapes.Add(line);
                            Console.WriteLine("Line added! (Press any key to continue).");
                            Console.ReadKey();
                            break;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message + " (Press any key to continue).");
                            Console.ReadKey();
                        }
                    }

                }
                //Add Triangle
                else if (userShapeChoice == 3)
                {
                    Point point1, point2, point3;
                    Triangle triangle;

                    while (true)
                    {
                        try
                        {
                            Program.PrintCanvas();
                            Console.WriteLine("ADD A TRIANGLE \n-------------\n");

                            Console.WriteLine("Enter the coordidnates of the first point: ");
                            point1 = GetPoint2D(false);

                            Program.ClearLines(1);

                            Console.WriteLine("Enter the coordidnates of the second point: ");
                            point2 = GetPoint2D(false);

                            Program.ClearLines(1);

                            Console.WriteLine("Enter the coordidnates of the third point: ");
                            point3 = GetPoint2D(false);

                            Program.ClearLines(1);

                            triangle = new Triangle(colour, point1, point2, point3);
                            shapes.Add(triangle);

                            Console.WriteLine("Triangle added! (Press any key to continue).");
                            Console.ReadKey();

                            break;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message + " (Press any key to continue)");
                            Console.ReadKey();
                        }
                    }
                }
                //Add Rectangle
                else if (userShapeChoice == 4)
                {
                    double length, height;
                    Point anchorPoint;
                    Rectangle rectangle;

                    while (true)
                    {
                        try
                        {
                            Program.PrintCanvas();
                            Console.WriteLine("ADD A RECTANGLE \n-------------\n");

                            Console.WriteLine("Enter the coordidnates of the anchor point: ");
                            anchorPoint = GetPoint2D(true);

                            Program.ClearLines(1);

                            Console.WriteLine("Enter the length ");
                            length = Program.GetInput(1 / Double.MaxValue, SCREEN_WIDTH - anchorPoint.X, "The length is too large!");

                            Program.ClearLines(1);

                            Console.WriteLine("Enter the height ");
                            height = Program.GetInput(1 / Double.MaxValue, anchorPoint.Y, "The height is too large!");

                            Program.ClearLines(1);

                            rectangle = new Rectangle(colour, length, height, anchorPoint);
                            shapes.Add(rectangle);

                            Console.WriteLine("Rectangle added! (Press any key to continue).");
                            Console.ReadKey();

                            break;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message + " (Press any key to continue)");
                            Console.ReadKey();
                        }
                    }
                }
                //Add Sphere
                else if (userShapeChoice == 5)
                {
                    double rad;
                    double minDistanceFromEdge;
                    Point sphereCenter;
                    Sphere sphere;

                    Program.PrintCanvas();
                    Console.WriteLine("ADD A SPHERE \n-------------");

                    Console.WriteLine("Enter the coordinates of the center point. ");
                    //hasMargin is set to true because if any of the center point's coordinates are zero, than a radius cannot be defined.
                    sphereCenter = GetPoint3D(true);

                    minDistanceFromEdge = Math.Min(sphereCenter.X, Math.Min(sphereCenter.Y, Math.Min(SCREEN_WIDTH - sphereCenter.X,
                        Math.Min(SCREEN_HEIGHT - sphereCenter.Y, Math.Min(sphereCenter.Z, SCREEN_DEPTH - sphereCenter.Z)))));

                    Console.WriteLine($"Enter the radius (maximum radius is {Math.Round(minDistanceFromEdge,2)}): ");
                    rad = Program.GetInput(1 / Double.MaxValue, minDistanceFromEdge, "That radius isn't valid. Radii must be larger than zero and less than or equal the maximum!");
                    sphere = new Sphere(rad, colour, sphereCenter);
                    shapes.Add(sphere);

                    Console.WriteLine("Sphere added! (Press any key to continue).");
                    Console.ReadKey();
                }
                //Add Rectangular Prism
                else if (userShapeChoice == 6)
                {
                    double length, height, depth;
                    Point anchorPoint;
                    RectangularPrism prism;

                    while (true)
                    {
                        try
                        {
                            Program.PrintCanvas();
                            Console.WriteLine("ADD A RECTANGULAR PRISM \n-----------------\n");

                            Console.WriteLine("Enter the coordidnates of the anchor point: ");
                            anchorPoint = GetPoint3D(true);

                            Program.ClearLines(1);

                            Console.WriteLine($"Enter the length (maximum of {SCREEN_WIDTH - anchorPoint.X})");
                            length = Program.GetInput(1 / Double.MaxValue, SCREEN_WIDTH - anchorPoint.X, "That length isn't valid. Lengths must be larger than zero and less than or equal the maximum!");

                            Console.WriteLine($"Enter the height  (maximum of {anchorPoint.Y})");
                            height = Program.GetInput(1 / Double.MaxValue, anchorPoint.Y, "That height isn't valid. Heights must be larger than zero and less than or equal the maximum!");
    
                            Console.WriteLine($"Enter the depth  (maximum of {SCREEN_DEPTH - anchorPoint.Z})");
                            depth = Program.GetInput(1 / Double.MaxValue, SCREEN_DEPTH - anchorPoint.Z, "That depth isn't valid. Depths must be larger than zero and less than or equal the maximum!");

                            prism = new RectangularPrism(colour, length, height, depth, anchorPoint);
                            shapes.Add(prism);

                            Console.WriteLine("Rectangular prism added! (Press any key to continue).");
                            Console.ReadKey();

                            break;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message + " (Press any key to continue)");
                            Console.ReadKey();
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("\nSorry, there are too many shapes on the screen. You must delete a shape before adding another one. (Press any key to continue.)");
                Console.ReadKey();
            }
            
        }

        //Pre: none
        //Post: none
        //Description: This method lets the user view all the properties (including calculated properties) of one shape
        public void ViewShape()
        {
            int viewShapeIndex;

            Program.PrintCanvas();
            Console.WriteLine("VIEW A SHAPE \n---------------");

            Console.WriteLine("Which of the following shapes would you like to view?");
            ViewShapeList();

            viewShapeIndex = Program.GetInput(1,6, "That's not a valid option.")  - 1;

            Console.WriteLine("\nShape Attributes: ");

            shapes[viewShapeIndex].PrintAttributes();

            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }

        //Pre: none.
        //Post: none.
        //Description: This method lets the user select a shape to delete. 
        public void DeleteShape()
        {
            char userResponse;

            //If there are shapes to delete, take the user through the process. Otherwise, inform them that there are no shapes to delete.
            if (shapes.Count > 0)
            {
                int delShapeIndex;

                Program.PrintCanvas();
                Console.WriteLine("DELETE A SHAPE \n---------------");
                Console.WriteLine("Which of these shapes would you like to delete?");

                //User selects the index of the shape they'd like to delete
                delShapeIndex = Program.GetInput(1,6, "That's not a valid option.") - 1;

                Console.WriteLine("Shape: ");
                shapes[delShapeIndex].PrintAttributes();

                //Double  checking that the user really wants to delete this shape.
                Console.WriteLine("Are you sure you would like to delete this shape? (Press 'y' if yes, and anything else if not)");
                userResponse = Console.ReadKey().KeyChar;

                if (userResponse == 'y')
                {
                    shapes.RemoveAt(delShapeIndex);
                    Console.WriteLine("\nShape deleted! (Press any key to continue).");
                }
                else
                {
                    Console.WriteLine("\nNo shape deleted! (Press any key to continue).");
                }
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("There are no shapes to delete. Add a shape first! (Press any key to continue).");
                Console.ReadKey();
            }
        }

        //Pre: None
        //Post: None
        //Description: This method lets the user interact with a shape in one of three ways: translating, scaling, or checking for intersection with a point.
        public void ModifyShape()
        {

            if (shapes.Count > 0)
            {
                int modShapeIndex;
                int userChoice;

                Program.PrintCanvas();
                Console.WriteLine("MODIFY A SHAPE \n---------------");
                Console.WriteLine("Which of these following shapes would you like to modify?");

                modShapeIndex = Program.GetInput(1, shapes.Count, "That's not an option.") - 1;

                Console.WriteLine("Shape Attributes: ");
                shapes[modShapeIndex].PrintAttributes();

                Console.WriteLine("\n\nHow would you like to modify this shape?\n1. Translate it \n2. Scale it \n3. Check for intersection with point");

                userChoice = Program.GetInput(1,3, "That's not a valid option");

                //Translating
                if (userChoice == 1)
                {
                    double transX, transY, transZ;
                    //This is used to determine whether the user should be asked for translation along the z-axis.
                    bool is3D = shapes[modShapeIndex].GetIs3D();

                    while(true)
                    {
                        try
                        {
                            Program.PrintCanvas();
                            Console.WriteLine("TRANSLATE SHAPE \n---------------");

                            Console.WriteLine("Shape Attributes: ");
                            shapes[modShapeIndex].PrintAttributes();

                            Console.WriteLine("How much would you like to translate your shape along the x-axis?");
                            transX = Program.GetInput(-SCREEN_WIDTH, SCREEN_WIDTH, "Translating by this much would cause the shape to go out of bounds.");
                            Program.ClearLines(1);

                            Console.WriteLine("How much would you like to translate your shape along the y-axis?");
                            transY = Program.GetInput(-SCREEN_HEIGHT, SCREEN_HEIGHT, "Translating by this much would cause the shape to go out of bounds.");
                            Program.ClearLines(1);

                            if (is3D)
                            {
                                Console.WriteLine("How much would you like to translate your shape along the z-axis?");
                                transZ = Program.GetInput(-SCREEN_WIDTH, SCREEN_WIDTH, "Translating by this much would cause the shape to go out of bounds.");
                                Program.ClearLines(1);
                            }
                            else
                            {
                                transZ = 0;
                            }

                            //Translating the shape with by the given amounts
                            shapes[modShapeIndex].TranslateShape(transX, transY, transZ);
                            Console.WriteLine("\nShape translated! New shape attributes");
                            shapes[modShapeIndex].PrintAttributes();
                            Console.Write("(Press ENTER to continue).");
                            Console.ReadLine();
                            break;
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message + " (Press ENTER to continue).");
                            Console.ReadLine();
                        } 
                    }
                }
                //Scaling
                else if (userChoice == 2)
                {
                    double scaleFactor;

                    while (true)
                    {
                        try
                        {
                            Program.PrintCanvas();
                            Console.WriteLine("SCALE SHAPE \n---------------");

                            Console.WriteLine("Shape Attributes: ");
                            shapes[modShapeIndex].PrintAttributes();

                            Console.WriteLine("\nBy how much would you like to scale your shape?");
                            scaleFactor = Program.GetInput(1 / Double.MaxValue, Double.MaxValue, "You cannot scale the shape by that number.");
                            Program.ClearLines(1);

                            //Error handling (for things like negative input) is done inside the ScaleShape method
                            shapes[modShapeIndex].ScaleShape(scaleFactor);

                            Console.WriteLine("\nYour shape has been scaled. New shape attributes: ");
                            shapes[modShapeIndex].PrintAttributes();
                            Console.WriteLine("\nPress any key to continue.");
                            Console.ReadKey();

                            break;
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            Console.WriteLine($"{e.Message} (Press any key to continue).");
                            Console.ReadKey();
                        }
                    }
                }
                else if (userChoice == 3)
                {
                    Point pointToCheck;
                    bool doesIntersect;
                    //Used to prompt the user for and display the correct number of coordinates
                    bool is3D = shapes[modShapeIndex].GetIs3D();
                    //If the shape is 3D, this will contain the value of the z-coordinate. Otherwise it will be empty.
                    string pointZ;

                    Program.PrintCanvas();
                    Console.WriteLine("CHECK FOR INTERSECTION \n---------------------");
                    Console.WriteLine("Shape Attributes: ");
                    shapes[modShapeIndex].PrintAttributes();

                    Console.Write("\nEnter the coordinates of the point you'd like to check. ");
                    if (is3D)
                    {
                        pointToCheck = GetPoint3D(false);
                    }
                    else
                    {
                        pointToCheck = GetPoint2D(false);
                    }


                    doesIntersect = shapes[modShapeIndex].CheckIntersectionWithPoint(pointToCheck);

                    //If the shape is 3D, this will contain the z-value of the point. Otherwise, it will be empty.
                    pointZ = is3D ? $", {pointToCheck.Z}" : "";

                    //Indicate to the user whether the point intersects the shape or not.
                    if (doesIntersect)
                    {
                        Console.WriteLine($"The point ({pointToCheck.X}, {pointToCheck.Y}{pointZ}) does intersect this shape. (Press any key to continue).");
                    }
                    else
                    {
                        Console.WriteLine($"The point ({pointToCheck.X}, {pointToCheck.Y}{pointZ}) does not intersect this shape. (Press any key to continue).");
                    }

                    Console.ReadKey();

                }
                
            }
            else
            {
                Console.WriteLine("There are no shapes to modify. Addd some first! (Press ENTER to continue).");
                Console.ReadKey();    
            }
        }

        //Pre: none
        //Post: none
        //Description: this method clears the shape list and alerts the user that the canvas was cleared.
        public void ClearCanvas()
        {
            shapes.Clear();
            Console.Clear();

            Console.WriteLine("The canvas has been cleared. (Press any key to continue).");
            Console.ReadKey();
        }

        //Pre: haveMargin is used to determine whether to let a point lie exactly on the zero of any axis (e.g., (0, 1, 1)).
        //Post: returns a point that lies inside the canvas.
        //Description: this method uses the Program.GetInput method to get the coordinates of a point from a user.
        public static Point GetPoint2D(bool haveMargin)
        {
            Point point = new Point();
            double margin = haveMargin ? (1 / Double.MaxValue) : 0;

            Console.WriteLine("Enter the x coordinate of the point: ");
            point.X = Program.GetInput(margin, SCREEN_WIDTH - margin, "This coordinate is not sufficiently within canvas width.");
            Console.WriteLine("Enter the y coordinate of the point: ");
            point.Y = Program.GetInput(margin, SCREEN_HEIGHT - margin, "This coordinate is not sufficiently within canvas height.");
            point.Z = 0;

            return point;
        }

        //Pre: haveMargin is used to determine whether to let a point lie exactly on the zero of any axis (e.g., (0, 1, 1)).
        //Post: returns a point that lies inside the canvas.
        //Description: this method uses the Program.GetInput method to get the coordinates of a point from a user.
        public static Point GetPoint3D(bool haveMargin)
        {
            Point point = new Point();
            double margin = haveMargin ? (1 / Double.MaxValue) : 0.0;

            Console.WriteLine("Enter the x coordinate of the point: ");
            point.X = Program.GetInput(margin, SCREEN_WIDTH - margin, "This coordinate is not sufficiently within canvas width.");
            Console.WriteLine("Enter the y coordinate of the point: ");
            point.Y = Program.GetInput(margin, SCREEN_HEIGHT - margin, "This coordinate is not sufficiently within canvas height.");
            Console.WriteLine("Enter the z coordinate of the point: ");
            point.Z = Program.GetInput(margin, SCREEN_DEPTH - margin, "This coordinate is not sufficiently within canvas depth.");

            return point;
        }
    }
}