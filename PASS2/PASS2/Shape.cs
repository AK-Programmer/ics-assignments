//Author: Adar Kahiri
//File Name: Shape.cs
//Project Name: PASS2
//Creation Date: October 20, 2020
//Modified Date: October 30, 2020
/* Description: this class contains the code for the general shape class or 'archetype'. It contains all of the attributes and code that 
 * all of the child shape classes have in common, as well as an abstract method to indicate that all child classes must implement a method that checks for intersections with a point. 
 */

using System;

namespace PASS2
{
    public abstract class Shape
    {
        private string shapeName;
        protected string colour;
        protected Point[] points;
        //This variable refers to the 'perimeter equivalent' of each shape. For lines it would be their length, for circles their circumference, for triangles their perimeter, for spheres their boundary (surface area) and so on. 
        protected double perimeterEquiv;


        //Pre: colour and shapeName are set internally by the program and don't cause any program-crashing bugs if they're not set correctly. Points must be within the bounds of the canvas, but this ensured elsewhere.
        //Post: None.
        //Desc: There isn't any special logic here. The class attributes are set to their respective parameters.
        public Shape(string colour, string shapeName, params Point[] points)
        {
            this.shapeName = shapeName;
            this.colour = colour;
            this.points = points;
        }


        //Pre: the point must be within the bounds of the canvas.
        //Post: returns true if the point does intersect with the shape, and false otherwise. 
        //Description: This method will be defined in each of the class
        public abstract bool CheckIntersectionWithPoint(Point point);


        //Pre: none.
        //Post: none.
        //Description: This method prints the shape's attributes. It's extended in all classes to print their unique attributes.
        public virtual void PrintAttributes()
        {
            Console.Write("- Vertices: ");
            //For each vertex, print it in a nicely formatted way (rounding decimals, inside parentheses, etc.)
            foreach (Point point in points)
            {
                Console.Write($"({Math.Round(point.X, 2)}, {Math.Round(point.Y, 2)}) ");
            }

            Console.WriteLine($"\n- Colour: {colour}");
        }



        //Pre: scale factor must not be negative.
        //Post: none
        //Description: This method scales the shape's points by scaleFactor. It ensures that scaling the points by this factor does not cause any of them to go outside the canvas. This method is overridden either extended (by overriding and calling base.ScaleShape()) or completely overriden in all classes.
        public virtual void ScaleShape(double scaleFactor)
        {
            if (scaleFactor <= 0)
                throw new ArgumentOutOfRangeException("Scale Factor", "The scale factor must be positive!");

            try
            {
                //For each point
                for (int i = 1; i < points.Length; i++)
                {
                    //Here, the ArgumentOutOfRangeException that may be thrown by the Point class is caught and then thrown again. This is done to change the message shown to the user. 

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
            return $"{colour} {shapeName} \n- Anchor point: ({points[0].X}, {points[0].Y})";
        }


        
    }
}
