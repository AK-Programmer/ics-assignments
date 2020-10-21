﻿using System;
namespace PASS2
{
    public class Rectangle : Shape
    {

        private double length;
        private double height;
        private double surfaceArea;
        private double diagLen;


        public Rectangle(double length, double height, string colour, Point anchorPoint) : base(colour, anchorPoint, new Point(anchorPoint.X + length, anchorPoint.Y), new Point(anchorPoint.X, anchorPoint.Y - height), new Point(anchorPoint.X + length, anchorPoint.Y - height))
        {
            this.length = length;
            this.height = height;

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

            Console.WriteLine($"Length: {length}");
            Console.WriteLine($"Height: {height}");
            Console.WriteLine($"Surface Area: {surfaceArea}");
            Console.WriteLine($"Diagonal Length: {diagLen}");
        }

       
        public override bool CheckIntersectionWithPoint(Point point)
        {
            if (point.X >= points[0].X && point.X <= points[1].X && point.Y <= points[0].Y && point.Y >= points[2].Y)
                return true;

            return false;
        }
    }
}
