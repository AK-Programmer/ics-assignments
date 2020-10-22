using System;


namespace PASS2
{

    public class Circle : Shape
    {
        private double radius;
        private double surfaceArea;

        public Circle(double radius, string colour, Point center): base(colour, "Circle", center)
        {
            if (radius <= 0)
                throw new ArgumentOutOfRangeException("Radius", "The radius must be a positive number");

            CheckCircleInBounds(points[0], radius, "The desired circle goes outside the screen! Either reposition it, or give it a smaller radius");

            this.radius = radius;

            surfaceArea = Math.PI * radius * radius;
            perimeterEquiv = 2 * Math.PI * radius;

            

    }

        public override void ScaleShape(double scaleFactor)
        {
            double potentialRad = radius * scaleFactor;

            double x = points[0].X;
            double y = points[0].Y;

            if (scaleFactor <= 0)
                throw new ArgumentOutOfRangeException("Scale Factor", "The scale factor must be a positive number!");

            CheckCircleInBounds(points[0], potentialRad, "Scaling the circle by this factor would make it go beyond the screen. Try scaling it by a smaller amount of reposition it first");

            radius = potentialRad;
            surfaceArea *= scaleFactor * scaleFactor;
            perimeterEquiv *= scaleFactor;
        }

        public override void PrintAttributes()
        {
            base.PrintAttributes();

            Console.WriteLine($"Radius: {Math.Round(radius, 2)}");
            Console.Write($"Surface Area: {Math.Round(surfaceArea, 2)}");
        }

        public override void TranslateShape(double translateX, double translateY)
        {
            //Will store the prospective values of x and y, and will be used to check if the shape can be translated to the desired location without going out of bounds
            double potentialX = points[0].X + translateX;
            double potentialY = points[0].Y + translateY;

            //These if statements handle all possible ways the circle could go out of bounds and deliver the appropriate message.
            if(potentialX + radius > Canvas.SCREEN_WIDTH)
                throw new ArgumentOutOfRangeException("TranslateX", "You are trying to translate the shape beyond the screen. Try translating the shape a little less to the right.");
            else if (potentialX - radius < 0)
                throw new ArgumentOutOfRangeException("TranslateX", "You are trying to translate the shape beyond the screen. Try translating the shape a little less to the left.");
            else if (potentialY + radius > Canvas.SCREEN_HEIGHT)
                throw new ArgumentOutOfRangeException("TranslateY", "You are trying to translate the shape beyond the screen. Try translating the shape a little less down.");
            else if (potentialY - radius < 0)
                throw new ArgumentOutOfRangeException("TranslateY", "You are trying to translate the shape beyond the screen. Try translating the shape a little less up.");
        }

        public override bool CheckIntersectionWithPoint(Point point)
        {
            //This expression calculates the point's squared distance from the circle's center using the Euclidean distance formula
            double DistanceFromCenter = point.GetDistance(points[0]);

            //Comparing the squared distance to the radius squared is more efficient than comparing the actual distance since the square root function is costly
            if (DistanceFromCenter <= radius)
                return true;

            return false;
        }

        private void CheckCircleInBounds(Point center, double radius, string message)
        {
            if (center.X + radius > Canvas.SCREEN_WIDTH || center.X - radius < 0 || center.Y + radius > Canvas.SCREEN_HEIGHT || center.Y - radius < 0)
                throw new ArgumentOutOfRangeException("Circle", message);
        }

    }
}
