//Author: Adar Kahiri
//File Name: Shape.cs
//Project Name: PASS2
//Creation Date: October 20, 2020
//Modified Date: Nov 1, 2020
/* Description: this class contains the code for the general shape class or 'archetype'. It contains all of the attributes and code that 
 * all of the child shape classes have in common, as well as an abstract method to indicate that all child classes must implement a method that checks for intersections with a point. 
 */

using System;

namespace PASS2
{
    public abstract class Shape
    {
        //shapeName is used to make it easy to display the shape's name (e.g., "Line", "Circle") in the canvas class while iterating through the shape list.
        private string shapeName;
        //Used to indicate whether a shape is 3 dimensional or not to the rest of the program.
        private bool is3D;
        protected string colour;
        protected Point[] points;


        //Pre: colour and shapeName are set internally by the program and don't cause any program-crashing bugs if they're not set correctly. Points must be within the bounds of the canvas, but this ensured elsewhere.
        //Post: None.
        //Desc: There isn't any special logic here. The class attributes are set to their respective parameters.
        public Shape(string colour, string shapeName, bool is3D, params Point[] points)
        {
            this.colour = colour;
            this.shapeName = shapeName;
            this.is3D = is3D;
            this.points = points;
        }


        //Pre: point must be inside the canvas.
        //Post: returns true if the given point does intersect the shape, and false otherwise.
        //Description: This method must be defined by each shape individually, as each shape has a unique intersection method (with the exception of the circle and sphere).
        public abstract bool CheckIntersectionWithPoint(Point point);


        //Pre: none.
        //Post: none.
        //Description: This method prints the shape's attributes. It's extended in all classes to print their unique attributes.
        public virtual void PrintAttributes()
        {
            //Will store the string value of the z-coordinate of the point if the shape is 3D. Otherwise it will be empty.
            string pointZ;

            Console.WriteLine($"- {colour} {shapeName}");
            Console.Write("- Vertices: ");

            //For each vertex, print it in a nicely formatted way (rounding decimals, inside parentheses, etc.)
            for (int i = 0; i < points.Length; i ++)
            {
                //If the shape is 3D, display it's z-coordinate. Otherwise, pointZ is empty.
                pointZ = is3D ? $", {Math.Round(points[i].Z,2)}" : "";

                //Displaying the coordinates of the point                                            i ==
                Console.Write($"({Math.Round(points[i].X, 2)}, {Math.Round(points[i].Y, 2)}{pointZ}) {(i == 4 ? "\n" : "")}");
            }

            Console.WriteLine($"\n- Colour: {colour}");
        }



        //Pre: scale factor must not be negative.
        //Post: none
        //Description: This method scales the shape's points by scaleFactor. It ensures that scaling the points by this factor does not cause any of them to go outside the canvas. This method is overridden either extended (by overriding and calling base.ScaleShape()) or completely overriden in all classes.
        public virtual void ScaleShape(double scaleFactor)
        {
            //Scale factor cannot less than or equal to zero. 
            if (scaleFactor <= 0)
            {
                throw new ArgumentOutOfRangeException("Scale Factor", "The scale factor must be positive!");
            }
               
            try
            {
                //Here, the ArgumentOutOfRangeException that may be thrown by the Point class is caught and then thrown again. This is done to change the message shown to the user. 
                //For each point, translate it so that the anchor point is the origin, scale it, and translate it back.
                for (int i = 1; i < points.Length; i++)
                {
                    //These expressions scale each point's coordinates by first treating the anchor point point[0] as the origin (by subtracting it). 
                    points[i].X = points[0].X + scaleFactor * (points[i].X - points[0].X);
                    points[i].Y = points[0].Y + scaleFactor * (points[i].Y - points[0].Y);
                    points[i].Z = points[0].Z + scaleFactor * (points[i].Z - points[0].Z);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Scale Factor", "Scaling this shape by this much would parts of it go beyond the canvas. Try scaling by a smaller factor or repositioning first.");
            }
        }

        //Pre: translateX, translateY, translateZ, must not cause any of the shape's points to go out of bound.
        //Post: none.
        //Description: This method translates each of the shape's points by the desired amounts andd ensures none of them go outside the canvas.
        public virtual void TranslateShape(double translateX, double translateY, double translateZ)
        {
            //Here, the ArgumentOutOfRangeException that may be thrown by the Point class is caught and then thrown again. This is done to change the message shown to the user. 
            try
            {
                //For each point, add translateX to  its X coordinate, translateY to its Y coordinate, and translateZ to its Z coordinate.
                foreach(Point point in points)
                {
                    point.X += translateX;
                    point.Y += translateY;
                    point.Z += translateZ;
                }
            }
            //This catches any exceptions thrown by the point class's set methods and then re-thrown in order to change the message. 
            catch(ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Translate", "Translating the shape by this much in these directions would cause all or parts of it to go off screen. Try translating it by a smaller amount.");
            }
        }

        //Pre:col and row should be within the bounds of the console window. It's assumed the window has dimensions 90x30. shapeNum is the shape's 'place' in the list. It's used to display a number beside each shape. 
        //Post: none.
        //This method prints the shape's basic information at the specified column and row.
        public virtual void PrintBasicInfo(int col, int row, int shapeNum)
        {
            //If the shape is 3D, display the Z coordinate. Otherwise, don't.
            string pointZ = is3D ? $", {points[0].Z}" : "";

            //Setting the cursor position to the given column and row.
            Console.SetCursorPosition(col, row);
            Console.Write($"{shapeNum}. {colour} {shapeName}");
            //Incrementing the row
            Console.SetCursorPosition(col, row + 1);
            Console.Write($"- Anchor point: ({points[0].X}, {points[0].Y}{pointZ})");
        }

        //Pre: none
        //Post: returns the value of the is3D attribute of the shape
        //Description: this is a get method for the is3D variable.
        public bool GetIs3D()
        {
            return is3D;
        }
    }
}
