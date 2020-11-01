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


        public abstract bool CheckIntersectionWithPoint(Point point);


        //Pre: none.
        //Post: none.
        //Description: This method prints the shape's attributes. It's extended in all classes to print their unique attributes.
        public virtual void PrintAttributes()
        {
            Console.WriteLine($"- {colour} {shapeName}");
            Console.Write("- Vertices: ");
            //For each vertex, print it in a nicely formatted way (rounding decimals, inside parentheses, etc.)
            foreach (Point point in points)
            {
                Console.Write($"({Math.Round(point.X, 2)}, {Math.Round(point.Y, 2)}, {Math.Round(point.Z, 2)}) ");
            }

            Console.WriteLine($"\n- Colour: {colour}");
        }



        //Pre: scale factor must not be negative.
        //Post: none
        //Description: This method scales the shape's points by scaleFactor. It ensures that scaling the points by this factor does not cause any of them to go outside the canvas. This method is overridden either extended (by overriding and calling base.ScaleShape()) or completely overriden in all classes.
        public virtual void ScaleShape(double scaleFactor)
        {
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

        //Pre: none.
        //Post: none.
        //Description: this method displays the shape's basic information and is used when displaying all of the shapes on the canvas at once. 
        public virtual string GetBasicInfo()
        {
            string pointZ = is3D ? $", {points[0].Z}" : "";
            return $"{colour} {shapeName} \n- Anchor point: ({points[0].X}, {points[0].Y}{pointZ})";
        }

        public virtual void PrintBasicInfo(int col, int row, int shapeNum)
        {
            string pointZ = is3D ? $", {points[0].Z}" : "";

            Console.SetCursorPosition(col, row);
            Console.Write($"{shapeNum}. {colour} {shapeName}");
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
