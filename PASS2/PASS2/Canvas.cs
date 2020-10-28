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
                    Console.WriteLine("Choose one of the following shapes to add: \n1. Circle \n2. Line \n3. Triangle \n4. Rectangle \n5. Sphere \n6. Rectangular Prism");
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
                    double rad;
                    Point circleCenter = new Point();
                    bool centerInit = false;
                    Circle circle;

                    while(true)
                    {
                        //Try catch block is used to catch any exceptions thrown by either the point class, shape class, or standard library methods and handle them appropriately (getting user to try again)
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("ADD A CIRCLE \n-------------\n");

                            //These if statements ensure that a user is only prompted to enter an input if that variable has not been validly set yet.
                            if (!centerInit)
                            {
                                Console.Write("Enter the coordinates of the center point in the x,y format: ");
                                userInput = Console.ReadLine().Split(',');

                                if (userInput.Length != 2)
                                    throw new IndexOutOfRangeException("Your input was not entered in the right format.");

                                circleCenter.X = Convert.ToDouble(userInput[0]);
                                circleCenter.Y = Convert.ToDouble(userInput[1]);
                                centerInit = true;
                            }
                            Console.WriteLine($"First point set at ({circleCenter.X}, {circleCenter.Y})!");

                            Console.Write("Enter the radius of the circle: ");
                            rad = Convert.ToDouble(Console.ReadLine());

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
                            Console.WriteLine($"{e.Message} (Press any key to continue).");
                        }

                        Console.ReadKey();
                    }
                    
                    
                }
                //Add Line
                else if (userShapeChoice == '2')
                {

                    Point point1 = new Point();
                    Point point2 = new Point();
                    Line line;

                    bool point1Init = false;

                    while(true)
                    {
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("ADD A LINE \n-------------\n");

                            if (!point1Init)
                            {
                                Console.Write("Enter the coordinates of the first point in the x,y,z format: ");
                                userInput = Console.ReadLine().Split(',');
                                if (userInput.Length != 3)
                                    throw new IndexOutOfRangeException("Your input was not entered in the right format.");

                                point1.X = Convert.ToDouble(userInput[0]);
                                point1.Y = Convert.ToDouble(userInput[1]);
                                point1.Z = Convert.ToDouble(userInput[2]);
                                point1Init = true;
                            }
                            Console.WriteLine($"Successfully set the first point at ({point1.X}, {point1.Y}, {point1.Z})");

                            Console.Write("Enter the coordinates of the second point in the x,y,z format: ");
                            userInput = Console.ReadLine().Split(',');

                            if (userInput.Length != 3)
                                throw new IndexOutOfRangeException("Your input was not entered in the right format.");

                            point2.X = Convert.ToDouble(userInput[0]);
                            point2.Y = Convert.ToDouble(userInput[1]);
                            point2.Z = Convert.ToDouble(userInput[2]);

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
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("The numbers weren't entered in the right format. Please try again.");
                            Console.ReadKey();
                        }
                        catch (ArgumentOutOfRangeException e)
                        { 
                            Console.WriteLine($"{e.Message} (Press any key to continue).");
                            Console.ReadKey();
                        }
                        catch(ArgumentException e)
                        {
                            point1Init = false;

                            Console.WriteLine($"{e.Message} (Press any key to continue).");
                            Console.ReadKey();
                        }
                    }
                }
                //Add Triangle
                else if (userShapeChoice == '3')
                {
                    Point point1 = new Point(), point2 = new Point(), point3 = new Point();
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
                            if (!p1Init)
                            {
                                Console.Write("Enter the coordinates of the first point in the x,y format: ");
                                userInput = Console.ReadLine().Split(',');
                                if (userInput.Length != 2)
                                    throw new IndexOutOfRangeException("Your input was not entered in the right format.");

                                point1.X = Convert.ToDouble(userInput[0]);
                                point1.Y = Convert.ToDouble(userInput[1]);
                                point1.Z = 0;
                            }

                            p1Init = true;
                            Console.WriteLine($"Successfully set the first point! ({point1.X}, {point1.Y})");

                            //Second point
                            if (!p2Init)
                            {
                                Console.Write("Enter the coordinates of the second point in the x,y format: ");
                                userInput = Console.ReadLine().Split(',');
                                if (userInput.Length != 2)
                                    throw new IndexOutOfRangeException("Your input was not entered in the right format.");

                                point2.X = Convert.ToDouble(userInput[0]);
                                point2.Y = Convert.ToDouble(userInput[1]);
                                point2.Z = 0;
                                p2Init = true;
                            }
                            
                            Console.WriteLine($"Successfully set the second point! ({point2.X}, {point2.Y})");


                            //Last point
                            Console.Write("Enter the coordinates of the third point in the x,y format: ");
                            userInput = Console.ReadLine().Split(',');

                            if (userInput.Length != 2)
                                throw new IndexOutOfRangeException("Your input was not entered in the right format.");

                            point3.X = Convert.ToDouble(userInput[0]);
                            point3.Y = Convert.ToDouble(userInput[1]);
                            point3.Z = 0;

                            triangle = new Triangle(colour, point1, point2, point3);
                            shapes.Add(triangle);

                            Console.WriteLine("Your triangle has been added!");
                            Console.ReadKey();
                            break;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("That's not a valid number. Try again. (Press any key to continue).");
                        }
                        catch (IndexOutOfRangeException e)
                        {

                            Console.WriteLine("The numbers weren't entered in the right format. Try again. (Press any key to continue).");
                        }
                        catch (ArgumentOutOfRangeException e)
                        { 
                            Console.WriteLine($"{e.Message} (Press any key to continue).");
                        }
                        catch(ArgumentException e)
                        {
                            Console.WriteLine($"{e.Message} (Press any key to continue).");
                            p1Init = false;
                            p2Init = false;
                            
                        }

                        Console.ReadKey();
                    }
                }
                //Add Rectangle
                else if (userShapeChoice == '4')
                {
                    double length = 0, height = 0;
                    Point anchorPoint = new Point();
                    bool isAnchorPointInit = false;
                    Rectangle rectangle;

                    while(true)
                    {
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("ADD A RECTANGLE \n-------------\n");


                            if (!isAnchorPointInit)
                            {
                                Console.Write("Enter the coordinates of the anchor point in the x,y format: ");
                                userInput = Console.ReadLine().Split(',');

                                if (userInput.Length != 2)
                                    throw new IndexOutOfRangeException();

                                anchorPoint.X = Convert.ToDouble(userInput[0]);
                                anchorPoint.Y = Convert.ToDouble(userInput[1]);
                                anchorPoint.Z = 0;

                                isAnchorPointInit = true;
                            }
                            
                            
                            Console.WriteLine($"Successfully set the anchor point! ({anchorPoint.X}, {anchorPoint.Y})");

                            Console.Write("Enter the length and height in the x,y format: ");
                            userInput = Console.ReadLine().Split(',');

                            if (userInput.Length != 2)
                                throw new IndexOutOfRangeException();

                            length = Convert.ToDouble(userInput[0]);
                            height = Convert.ToDouble(userInput[1]);

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
                        catch (ArgumentOutOfRangeException e)
                        {
                            if (anchorPoint.X + length > SCREEN_WIDTH)
                                Console.WriteLine("A rectangle with that length would go off screen. Try decreasing the length or repositioning the rectangle. (Press any key to continue).");
                            else if (anchorPoint.Y - height < 0)
                                Console.WriteLine("A rectangle with that height would go off screen. Try decreasing the height or repositioning the rectangle. (Press any key to continue).");
                            else
                                Console.WriteLine($"{e.Message} (Press any key to continue).");

                        }

                        Console.ReadKey();
                    }
                }
                //Add Sphere
                else if (userShapeChoice == '5')
                {
                    double rad;
                    Point sphereCenter = new Point();
                    bool centerInit = false;
                    Sphere sphere;

                    while (true)
                    {
                        //Try catch block is used to catch any exceptions thrown by either the point class, shape class, or standard library methods and handle them appropriately (getting user to try again)
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("ADD A CIRCLE \n-------------\n");

                            //These if statements ensure that a user is only prompted to enter an input if that variable has not been validly set yet.
                            if (!centerInit)
                            {
                                Console.Write("Enter the coordinates of the center point in the x,y format: ");
                                userInput = Console.ReadLine().Split(',');

                                if (userInput.Length != 2)
                                    throw new IndexOutOfRangeException("Your input was not entered in the right format.");

                                sphereCenter.X = Convert.ToDouble(userInput[0]);
                                sphereCenter.Y = Convert.ToDouble(userInput[1]);
                                sphereCenter.Z = Convert.ToDouble(userInput[2]);
                                centerInit = true;
                            }
                            Console.WriteLine($"First point set at ({sphereCenter.X}, {sphereCenter.Y}, {sphereCenter.Z})!");

                            Console.Write("Enter the radius of the circle: ");
                            rad = Convert.ToDouble(Console.ReadLine());

                            sphere = new Sphere(rad, colour, sphereCenter);
                            shapes.Add(sphere);

                            Console.WriteLine("Your circle has been added! (Press any key to continue).");
                            Console.ReadKey();

                            break;
                        }
                        //This catches the exception that might be thrown by the Convert.ToDouble() Method
                        catch (FormatException)
                        {
                            Console.WriteLine("One or both of the numbers you entered is not valid.");
                        }
                        //This catches the exception that may be thrown if the userInput array's length does not equal 2
                        catch (IndexOutOfRangeException e)
                        {
                            Console.WriteLine("The numbers weren't entered in the right format. Please try again.");
                        }
                        //This catches any exception that may be thrown by either the point or sphere class
                        catch (ArgumentOutOfRangeException e)
                        {
                            Console.WriteLine($"{e.Message} (Press any key to continue).");
                        }

                        Console.ReadKey();
                    }
                }
                //Add Rectangular Prism
                else if (userShapeChoice == '6')
                {
                    double length = 0, height = 0, depth=0;
                    Point anchorPoint = new Point();
                    bool isAnchorPointInit = false;
                    RectangularPrism prism;

                    while (true)
                    {
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("ADD A RECTANGLE \n-------------\n");


                            if (!isAnchorPointInit)
                            {
                                Console.Write("Enter the coordinates of the anchor point in the x,y,z format: ");
                                userInput = Console.ReadLine().Split(',');

                                if (userInput.Length != 3)
                                    throw new IndexOutOfRangeException();

                                anchorPoint.X = Convert.ToDouble(userInput[0]);
                                anchorPoint.Y = Convert.ToDouble(userInput[1]);
                                anchorPoint.Z = Convert.ToDouble(userInput[2]);

                                isAnchorPointInit = true;
                            }


                            Console.WriteLine($"Successfully set the anchor point! ({anchorPoint.X}, {anchorPoint.Y}, {anchorPoint.Z})");

                            Console.Write("Enter the length, height, and depth in the x,y,z format: ");
                            userInput = Console.ReadLine().Split(',');

                            if (userInput.Length != 3)
                                throw new IndexOutOfRangeException();

                            length = Convert.ToDouble(userInput[0]);
                            height = Convert.ToDouble(userInput[1]);
                            depth = Convert.ToDouble(userInput[2]);

                            prism = new RectangularPrism(colour, length, height, depth, anchorPoint);
                            shapes.Add(prism);

                            Console.WriteLine("Your rectangular prism has been added!");

                            Console.ReadKey();
                            break;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("That's not a valid number. Try again. (Press any key to continue).");
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("Your input was not entered in the right format. Try again. (Press any key to continue).");
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            if (anchorPoint.X + length > SCREEN_WIDTH)
                                Console.WriteLine("A rectangle with that length would go off screen. Try decreasing the length or repositioning the rectangle. (Press any key to continue).");
                            else if (anchorPoint.Y - height < 0)
                                Console.WriteLine("A rectangle with that height would go off screen. Try decreasing the height or repositioning the rectangle. (Press any key to continue).");
                            else
                                Console.WriteLine($"{e.Message} (Press any key to continue).");

                        }

                        Console.ReadKey();
                    }
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


                        Console.WriteLine("Are you sure you would like to delete this shape? (Press 'y' if yes, and anything else if not)");
                        userResponse = Console.ReadKey().KeyChar;

                        if (userResponse == 'y')
                        {
                            Console.WriteLine("\nShape deleted! (Press any key to continue).");
                            shapes.RemoveAt(delShapeIndex);
                        }
                        else
                        {
                            Console.WriteLine("\nNo shape deleted! (Press any key to continue).");
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
                string[] userInput;

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

                    Console.WriteLine("\n\nHow would you like to modify this shape?\n1. Translate it \n2. Scale it \n3. Check for intersection with point");

                    userChoice = Console.ReadKey().KeyChar;


                    if (userChoice == '1')
                    {
                        while(true)
                        {
                            try
                            {
                                Console.Clear();
                                Console.WriteLine("TRANSLATE SHAPE \n---------------");

                                Console.WriteLine("Shape Attributes: ");
                                shapes[modShapeIndex].PrintAttributes();

                                if(shapes[modShapeIndex].GetIs3D())
                                {
                                    Console.Write("\nHow much would you like to translate your shape along the x- and y-axes in the x,y format: ");
                                    userInput = Console.ReadLine().Split(',');

                                    if (userInput.Length != 2)
                                        throw new IndexOutOfRangeException();

                                    shapes[modShapeIndex].TranslateShape(Convert.ToDouble(userInput[0]), Convert.ToDouble(userInput[1]),0);
                                }
                                else
                                {
                                    Console.Write("\nHow much would you like to translate your shape along the x- and y- and z-axes in the x,y,z format: ");
                                    userInput = Console.ReadLine().Split(',');

                                    if (userInput.Length != 3)
                                        throw new IndexOutOfRangeException();

                                    shapes[modShapeIndex].TranslateShape(Convert.ToDouble(userInput[0]), Convert.ToDouble(userInput[1]), Convert.ToDouble(userInput[2]));
                                }

                                Console.WriteLine("Shape modified! New shape attributes:");
                                shapes[modShapeIndex].PrintAttributes();
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
                        Point pointToCheck;
                        bool doesIntersect;
                        bool is3D = shapes[modShapeIndex].GetIs3D();

                        while (true)
                        {
                            try
                            {
                                Console.Clear();
                                Console.WriteLine("CHECK FOR INTERSECTION \n---------------------");
                                Console.WriteLine("Shape Attributes: ");
                                shapes[modShapeIndex].PrintAttributes();

                                if (!is3D)
                                {
                                    Console.Write("\nEnter the coordinates of the point you'd like to check in x,y format: ");
                                    userInput = Console.ReadLine().Split(',');

                                    if (userInput.Length != 2)
                                        throw new IndexOutOfRangeException();

                                    pointToCheck = new Point(Convert.ToDouble(userInput[0]), Convert.ToDouble(userInput[1]));
                                }
                                else
                                {
                                    Console.Write("\nEnter the coordinates of the point you'd like to check in x,y,z format: ");
                                    userInput = Console.ReadLine().Split(',');

                                    if (userInput.Length != 2)
                                        throw new IndexOutOfRangeException();

                                    pointToCheck = new Point(Convert.ToDouble(userInput[0]), Convert.ToDouble(userInput[1]));
                                }

                                doesIntersect = shapes[modShapeIndex].CheckIntersectionWithPoint(pointToCheck);

                                string pointZ = is3D ? pointToCheck.Z.ToString() : "";
                                if (doesIntersect)
                                {
                                    Console.WriteLine($"The point ({pointToCheck.X}, {pointToCheck.Y}, {pointZ}) does intersect this shape. (Press any key to continue).");
                                }
                                else
                                {
                                    Console.WriteLine($"The point ({pointToCheck.X}, {pointToCheck.Y}, {pointZ}) does not intersect this shape. (Press any key to continue).");
                                }

                                Console.ReadKey();
                                break;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Please enter a valid number. (Press any key to continue).");
                                Console.ReadKey();
                            }
                            catch (ArgumentOutOfRangeException e)
                            { 
                                Console.WriteLine($"{e.Message} (Press any key to continue).");
                                Console.ReadKey();
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
