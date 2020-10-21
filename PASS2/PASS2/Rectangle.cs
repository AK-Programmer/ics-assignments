using System;
namespace PASS2
{
    public class Rectangle : Shape
    {

        private double length;
        private double height;
        private double surfaceArea;
        private double diagLen;


        public Rectangle(double length, double height, string colour, Point anchorPoint) : base(colour, anchorPoint, new Point(anchorPoint.x + length, anchorPoint.y), new Point(anchorPoint.x, anchorPoint.y - height), new Point(anchorPoint.x + length, anchorPoint.y - height))
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
                points[1].x = points[0].x + potentialLength;

                points[2].y = points[0].y - potentialHeight;

                points[3].x = points[0].x + potentialLength;
                points[3].y = points[0].y - potentialHeight;

                length = potentialHeight;
                height = potentialHeight;

                surfaceArea *= scaleFactor * scaleFactor;
                perimeterEquiv *= scaleFactor;
                diagLen *= scaleFactor;
            }
            catch(ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Scale Factor", "Scaling this shape by this much would parts of it go beyond the canvas. Try scaling by a smaller factor or repositioning first.");
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
            if (point.x >= points[0].x && point.x <= points[1].x && point.y <= points[0].y && point.y >= points[2].y)
                return true;

            return false;
        }
    }
}
