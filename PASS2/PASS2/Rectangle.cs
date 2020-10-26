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


        public Rectangle(string colour, double length, double height, Point anchorPoint) : base(colour, "Rectangle", anchorPoint, new Point(anchorPoint.X + length, anchorPoint.Y), new Point(anchorPoint.X, anchorPoint.Y - height), new Point(anchorPoint.X + length, anchorPoint.Y - height))
        {
            this.length = length;
            this.height = height;

            if (length <= 0)
                throw new ArgumentOutOfRangeException("length", "Rectangle length must be a positive number!");

            if (height <= 0)
                throw new ArgumentOutOfRangeException("height", "Rectangle height must be a positive number!");


            surfaceArea = length * height;
            perimeterEquiv = 2 * length + 2 * height;
            diagLen = Math.Sqrt(length * length + height * height);
        }


        public override void ScaleShape(double scaleFactor)
        {
            double potentialLength = length * scaleFactor; 
            double potentialHeight = height * scaleFactor;

            //Here, the ArgumentOutOfRangeException that may be thrown by the Point class is caught and then thrown again. This is done to change the message shown to the user. 
            try
            {
                points[1].X = points[0].X + potentialLength;

                points[2].Y = points[0].Y - potentialHeight;

                points[3].X = points[0].X + potentialLength;
                points[3].Y = points[0].Y - potentialHeight;

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

        public override void PrintAttributes()
        {
            base.PrintAttributes();

            Console.WriteLine($"- Length: {Math.Round(length, 2)}");
            Console.WriteLine($"- Height: {Math.Round(height, 2)}");
            Console.WriteLine($"- Surface Area: {Math.Round(surfaceArea, 2)}");
            Console.WriteLine($"- Diagonal Length: {Math.Round(diagLen, 2)}");
        }
       
        public override bool CheckIntersectionWithPoint(Point point)
        {
            if (point.X >= points[0].X && point.X <= points[1].X && point.Y <= points[0].Y && point.Y >= points[2].Y)
                return true;

            return false;
        }

        public override string GetBasicInfo()
        {
            string basicInfo = base.GetBasicInfo();

            basicInfo += $"\n- Length: {Math.Round(length, 2)} \n- Height: {Math.Round(height, 2)}";
            return basicInfo;
        }
    }
}
