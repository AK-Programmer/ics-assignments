//Author: Adar Kahiri
//File Name: Line.cs
//Project Name: PASS2
//Creation Date: October 21, 2020
//Modified Date: October 30, 2020
/* Description: This class inherits from the shape class, defines additional attributes of a line (such as its slope), and overrides the methods defined
 * in the shape class in order to carry out calculations specific to lines. Often times, when this class overrides methods from its base class, it will still
 * the base method its overriding and then add additional logic specific to the line. This is possible since the base methods have been designed to 
 * be as useful as possible for as many classes as possible.
 */

using System;
namespace PASS2
{
    public class Line : Shape
    {
        private double slope;

        public Line(string colour, Point point1, Point point2) : base(colour, "Line", point1, point2)
        {
            if (point1.X == point2.X && point1.Y == point2.Y)
                throw new ArgumentException("Both of your points cannot be the same. Please try again!", "same points");

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

            Console.WriteLine($"- Length: {perimeterEquiv}");
            Console.WriteLine($"- Slope: {slope}");
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

        public override string GetBasicInfo()
        {
            string basicinfo = base.GetBasicInfo();


            basicinfo += $"\n- Other endpoint: ({points[1].X}, {points[1].Y})";
            return basicinfo;
        }
    }
}
