using System;
namespace PASS2
{
    public class Line : Shape
    {
        private double slope;
        private double length;


        public Line(string colour, Point point1, Point point2) : base(colour, point1, point2)
        {
            if (points[1].x < points[0].x)
            {
                Point tempPoint = points[0];
                points[0] = points[1];
                points[1] = tempPoint; 
            }
            else if (points[1].x == points[0].x && points[1].y < points[0].y)
            {
                Point tempPoint = points[0];
                points[0] = points[1];
                points[1] = tempPoint;
            }


            //Setting slope and length attributes
            SetLength();
            SetSlope();
        }


        public override void PrintAttributes()
        {
            base.PrintAttributes();

            Console.WriteLine($"length: {length}");
            Console.WriteLine($"Slope: {slope}");
        }


        public void SetLength()
        {
            length = Math.Sqrt((points[0].x - points[1].x) * (points[0].x - points[1].x) + (points[0].y - points[1].y) * (points[0].y - points[1].y));
        }


        public void SetSlope()
        {
            slope = (points[1].y - points[0].y) / (points[1].x - points[0].x);
        }



        public override bool CheckIntersectionWithPoint(double x, double y)
        {
            //If the line is vertical and the x-coordinate of the point is the same as the x-coordinate of one of the endpoints, return true
            if (Math.Abs(slope) == Double.PositiveInfinity && x == points[0].x)
                return true;
            else
            {
                //If the point (x, y) satisfies the equation defining the line and is within the interval the line segment spans on the x-axis, return true. The equation of the line is y = y_1 + m(x - x_1)
                if (y == points[0].y + slope * (x - points[0].x) && x >= points[0].x && x <= points[1].x)
                    return true;
            }

            //If both of the checks above failed, the point is not on the line, so return false. 
            return false;
        }
    }
}
