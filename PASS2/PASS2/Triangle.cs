using System;
namespace PASS2
{
    public class Triangle : Shape
    {

        private double surfaceArea;
        private double height;
        private double[] sideLens = new double[3];

        public Triangle(string colour, double height, double surfaceArea, Point point1, Point point2, Point point3) : base(colour, point1, point2, point3)
        {

            //Checking for degenerate triangles (where some points are the same or if all three points lie on the same line)
            double slope = (points[1].y - points[0].y) / (points[1].x - points[2].x);

            //Checking if any two of the three points are equivalent
            if (points[0] == points[1] || points[0] == points[2] || points[1] == points[2])
                throw new ArgumentException("Triangle Vertices", "Two or more of the points entered are equivalent, and subsequently do not form a valid triangle. Please try again.");
            //Checking if the three points are on the same line
            else if ((Math.Abs(slope) == Double.PositiveInfinity && points[0].x == points[1].x && points[0].x == points[2].x && points[1].x == points[2].x) || points[0].y == points[1].y + slope * (points[0].x - points[1].x))
                throw new ArgumentException("Triangle Vertices", "The points entered are all on the same line, and do not form a valid triangle. Please try again.");


            //Sorting the points to ensure that the anchor points points[0] is the leftmost point
            if (points[1].x == points[2].x)
            {
                if (points[2].y > points[1].y)
                {
                    Point tempPoint = points[1];
                    points[1] = points[2];
                    points[2] = tempPoint;
                }
            }
            else if (points[2].x < points[1].x)
            {
                Point tempPoint = points[1];
                points[1] = points[2];
                points[2] = tempPoint;
            }

            if (points[1].x < points[0].x)
            {
                Point tempPoint = points[0];
                points[0] = points[1];
                points[1] = tempPoint;
            }



            sideLens[0] = Math.Sqrt((point2.x - point1.x) * (point2.x - point1.x) + (point2.y - point1.y) * (point2.y - point1.y));
            sideLens[1] = Math.Sqrt((point3.x - point2.x) * (point3.x - point2.x) + (point3.y - point2.y) * (point3.y - point2.y));
            sideLens[2] =  Math.Sqrt((point3.x - point1.x) * (point3.x - point1.x) + (point3.y - point1.y) * (point3.y - point1.y));
        }



        public override void PrintAttributes()
        {
            base.PrintAttributes();
        }

        public override void ScaleShape(double scaleFactor)
        {
            base.ScaleShape(scaleFactor);
        }

        public override bool CheckIntersectionWithPoint(Point point)
        {
            throw new NotImplementedException();
        }
    }
}
