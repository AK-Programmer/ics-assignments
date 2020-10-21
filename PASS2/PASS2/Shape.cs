using System;

namespace PASS2
{
    public abstract class Shape
    {
        protected string colour;
        protected Point[] points;
        protected double perimeterEquiv;


        public Shape(string colour, params Point[] points)
        {
            this.colour = colour;
            this.points = points;

        }

        public abstract bool CheckIntersectionWithPoint(double x, double y);


        public virtual void PrintAttributes()
        {
            Console.Write("Vertices: ");
            foreach(Point point in points)
            {
                Console.Write($"({Math.Round(point.x, 2)}, {Math.Round(point.y, 2)})\t"); 
            }

            Console.WriteLine($"\nColour: {colour}");
        }



        public virtual void ScaleShape(double scaleFactor)
        {
            if (scaleFactor <= 0)
                throw new ArgumentOutOfRangeException("Scale Factor", "The scale factor must be positive!");


            for (int i = 1; i < points.Length; i++)
            {
                //These expressions scale each point's coordinates by first treating the anchor point point[0] as the origin (by subtracting it). 
                points[i].x = points[0].x + scaleFactor * (points[i].x - points[0].x);
                points[i].y = points[0].y + scaleFactor * (points[i].y - points[0].y);
            }
        }


        public virtual void TranslateShape(double translateX, double translateY)
        {
            foreach(Point point in points)
            {
                point.x = point.x + translateX;
                point.y = point.y - translateY;
            }
        }


        
    }
}
