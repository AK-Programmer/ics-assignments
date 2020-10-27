//Author: Adar Kahiri
//File Name: Rectangle.cs
//Project Name: PASS2
//Creation Date: October 21, 2020
//Modified Date: October 30, 2020
/* Description: This class inherits from the shape class, defines additional attributes of a rectangle (such as its sidelengths), and overrides the methods defined
 * in the shape class in order to carry out calculations specific to rectangles. Often times, when this class overrides methods from its base class, it will still
 * the base method its overriding and then add additional logic specific to the rectangle. This is possible since the base methods have been designed to 
 * be as useful as possible for as many classes as possible.
 */

using System;
namespace PASS2
{
    public class Rectangle : Shape
    {

        private double length;
        private double height;
        private double surfaceArea;
        private double diagLen;

        //Pre: colour and shapeName are set internally by the program and don't cause any program-crashing bugs if they're not set correctly. Points must be within the bounds of the canvas, but this ensured elsewhere.
        //Post: None.
        //Desc: This constructor ensures the two points aren't actually the same point, sorts the points so that the leftmost one is first in the array, and calculates the slope and length of the line.
        public Rectangle(string colour, double length, double height, Point anchorPoint) : base(colour, "Rectangle", false, anchorPoint, new Point(anchorPoint.X + length, anchorPoint.Y), new Point(anchorPoint.X, anchorPoint.Y - height), new Point(anchorPoint.X + length, anchorPoint.Y - height))
        {
            this.length = length;
            this.height = height;

            //If height or length are less than zero, throw an exception with an appropriate message.
            if (length <= 0)
                throw new ArgumentOutOfRangeException("length", "Rectangle length must be a positive number!");

            if (height <= 0)
                throw new ArgumentOutOfRangeException("height", "Rectangle height must be a positive number!");

            surfaceArea = length * height;
            perimeterEquiv = 2 * length + 2 * height;
            diagLen = Math.Sqrt(length * length + height * height);
        }

        //Pre: scale factor must not be negative.
        //Post: none
        //Description: This method scales the 3 non-anchor points of the rectangle, and scales the length, width, and calculated properties proportionally. Of course, all of this is error-trapped.
        public override void ScaleShape(double scaleFactor)
        {
            double potentialLength = length * scaleFactor; 
            double potentialHeight = height * scaleFactor;

            //Here, the ArgumentOutOfRangeException that may be thrown by the Point class is caught and then thrown again. This is done to change the message shown to the user. 
            try
            {
                //Try to update all the points. The point class's set method will throw an exception if any of the updated points go out of bounds.
                points[1].X = points[0].X + potentialLength;
                points[2].Y = points[0].Y - potentialHeight;
                points[3].X = points[0].X + potentialLength;
                points[3].Y = points[0].Y - potentialHeight;

                //Perimeter and diagonal length scale linearly, surface area scales quadratically.
                length = potentialHeight;
                height = potentialHeight;
                surfaceArea *= scaleFactor * scaleFactor;
                perimeterEquiv *= scaleFactor;
                diagLen *= scaleFactor;
            }
            catch(ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Scale Factor", "Scaling this rectangle by this much would parts of it go beyond the canvas. Try scaling by a smaller factor or repositioning first.");
            }
        }

        //Pre: none.
        //Post: none.
        //Description: This method prints the rectangle's attributes. It calls the base PrintAttributes() method and prints the unique properties of a rectangle. 
        public override void PrintAttributes()
        {
            base.PrintAttributes();

            Console.WriteLine($"- Length: {Math.Round(length, 2)}");
            Console.WriteLine($"- Height: {Math.Round(height, 2)}");
            Console.WriteLine($"- Perimeter: {Math.Round(perimeterEquiv, 2)}");
            Console.WriteLine($"- Surface Area: {Math.Round(surfaceArea, 2)}");
            Console.WriteLine($"- Diagonal Length: {Math.Round(diagLen, 2)}");
        }

        //Pre: the point must be within the bounds of the canvas.
        //Post: returns true if the point does intersect with the rectangle, and false otherwise. 
        //Description: This method checks if the given point intersects with the line by checking if the sum of its distances from both end points is equal to the length of the line and returning true if so (returning false otherwise).
        public override bool CheckIntersectionWithPoint(Point point)
        {
            if (point.X >= points[0].X && point.X <= points[1].X && point.Y <= points[0].Y && point.Y >= points[2].Y)
                return true;

            return false;
        }

        //Pre: none.
        //Post: none.
        //Description: this method displays the rectangle's basic information and is used when displaying all of the shapes on the canvas at once. It calls the base GetBasicInfo method to display the general shape information.
        public override string GetBasicInfo()
        {
            string basicInfo = base.GetBasicInfo();
            basicInfo += $"\n- Length: {Math.Round(length, 2)} \n- Height: {Math.Round(height, 2)}";
            return basicInfo;
        }
    }
}
