using System;
namespace PASS2
{
    public class Triangle : Shape
    {

        private double surfaceArea;
        private double height;
        private double[] sideLens = new double[3];

        public Triangle(string colour, Point point1, Point point2, Point point3) : base(colour, "Triangle", point1, point2, point3)
        {


            //Checking if the triangle is aligned horizontally  
            if (points[0].Y != points[1].Y && points[0].Y != points[2].Y && points[2].Y != points[1].Y)
                throw new ArgumentException("Triangle Vertices", "The given triangle is on an angle. Two of your points must form a horizontal side length.");

            //Checking if any two of the three points are equivalent
            else if (points[0] == points[1] || points[0] == points[2] || points[1] == points[2])
                throw new ArgumentException("Triangle Vertices", "Two or more of the points entered are equivalent, and subsequently do not form a valid triangle. Please try again.");

            //Checking if the three points are on the same line
            else if ((points[0].Y == points[1].Y && points[0].Y == points[2].Y && points[2].Y == points[1].Y) || (points[0].X == points[1].X && points[0].X == points[2].X && points[2].X == points[1].X))
                throw new ArgumentException("Triangle Vertices", "The points entered are all on the same line, and do not form a valid triangle. Please try again.");


            //Bubble sort and tie breaker to ensure the first point in points array is the leftmost one
            Point temp;
            for (int i = points.Length - 1; i > 0; i--)
            {
                if (points[i].X < points[i-1].X)
                {
                    temp = points[i];
                    points[i] = points[i - 1];
                    points[i - 1] = temp;
                }
            }

            if (points[0].X == points[1].X && points[1].Y < points[0].Y)
            {
                temp = points[1];
                points[1] = points[0];
                points[0] = temp;
            }



            int shift = 0;

            for (int i = points.Length - 1; i >= 0; i--)
            {
                //If points[i] and points[(i+1)%3] form the base, set the first element in sideLens as the base length, and calculate the height
                if (points[i].Y == points[(i + 1) % points.Length].Y)
                {
                    sideLens[0] = points[i].GetDistance(points[(i + 1) % points.Length]);

                    //This ensures that the rest of the sidelengths are assigned to the correct indices and that the values of no indices are overwritten.
                    shift = 1;

                    //The chunky expression involving modulo operators gets the index of the point that is NOT i or (i+1)%3
                    height = Math.Abs(points[i].Y - points[((points.Length - 1) * (i + (i+1) % points.Length)) % points.Length].Y);
                }
                else
                {
                    sideLens[i+shift] = points[i].GetDistance(points[(i + 1) % 3]);
                }
            }


            perimeterEquiv = 0;

            for (int i = 0; i < sideLens.Length; i ++)
            {
                perimeterEquiv += sideLens[i];
            }

            //Formula for area of a triangle. It's ensured that sideLens[0] is the base of the triangle.
            surfaceArea = sideLens[0] * height / 2;
        }



        public override void PrintAttributes()
        {
            base.PrintAttributes();

            Console.WriteLine($"- Side lengths: {Math.Round(sideLens[0], 2)}, {Math.Round(sideLens[1], 2)}, {Math.Round(sideLens[2], 2)}");
            Console.WriteLine($"- Height:  {Math.Round(height, 2)}");
            Console.WriteLine($"- Surface Area: {Math.Round(surfaceArea, 2)}");
        }


        public override void ScaleShape(double scaleFactor)
        {
            base.ScaleShape(scaleFactor);

            //Scaling the rest of the variables
            height *= scaleFactor;
            surfaceArea *= scaleFactor * scaleFactor;
            perimeterEquiv *= scaleFactor;

            for (int i = 0; i < sideLens.Length; i ++)
            {
                sideLens[i] *= scaleFactor;
            }
        }

        public override bool CheckIntersectionWithPoint(Point point)
        {
            double px = point.X;
            double py = point.Y;
            double x1 = points[0].X;
            double y1 = points[0].Y;
            double x2 = points[1].X;
            double y2 = points[1].Y;
            double x3 = points[2].X;
            double y3 = points[2].Y;


            double area1 = Math.Abs((x1 - px) * (y2 - py) - (x2 - px) * (y1 - py));
            double area2 = Math.Abs((x2 - px) * (y3 - py) - (x3 - px) * (y2 - py));
            double area3 = Math.Abs((x3 - px) * (y1 - py) - (x1 - px) * (y3 - py));

            if (surfaceArea == (area1 + area2 + area3)/2)
                return true;

            return false;
        }
    }
}
