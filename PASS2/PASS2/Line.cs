using System;
namespace PASS2
{
    public class Line : Shape
    {
        private double slope;

        public Line(string colour, Point point1, Point point2) : base(colour, "Line", point1, point2)
        {
            if (points[1].X < points[0].X)
            {
                Point tempPoint = points[0];
                points[0] = points[1];
                points[1] = tempPoint; 
            }
            else if (points[1].X == points[0].X && points[1].Y < points[0].Y)
            {
                Point tempPoint = points[0];
                points[0] = points[1];
                points[1] = tempPoint;
            }

            //Length is calculated using the euclidean distance formula
            perimeterEquiv = points[0].GetDistance(points[1]);

            slope = (points[1].Y - points[0].Y) / (points[1].X - points[0].X);
        }



        public override void PrintAttributes()
        {
            base.PrintAttributes();

            Console.WriteLine($"Length: {perimeterEquiv}");
            Console.WriteLine($"Slope: {slope}");
        }



        public override void ScaleShape(double scaleFactor)
        {
            base.ScaleShape(scaleFactor);

            perimeterEquiv *= scaleFactor;  
        }


        public override bool CheckIntersectionWithPoint(Point point)
        {
            //If the line is vertical and the x-coordinate of the point is the same as the x-coordinate of one of the endpoints, return true
            if (Math.Abs(slope) == Double.PositiveInfinity && point.X == points[0].X)
                return true;

            else
            {
                //If the point (x, y) satisfies the equation defining the line and is within the interval the line segment spans on the x-axis, return true. The equation of the line is y = y_1 + m(x - x_1)
                if (point.Y == points[0].Y + slope * (point.X - points[0].X) && point.X >= points[0].X && point.X <= points[1].X)
                    return true;
            }

            //If both of the checks above failed, the point is not on the line, so return false. 
            return false;
        }


    }
}
