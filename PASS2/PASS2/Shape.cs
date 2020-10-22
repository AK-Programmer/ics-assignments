using System;

namespace PASS2
{
    public abstract class Shape
    {
        private string shapeName; 
        protected string colour;
        protected Point[] points;
        protected double perimeterEquiv;


        public Shape(string colour, string shapeName, params Point[] points)
        {
            this.shapeName = shapeName;
            this.colour = colour;
            this.points = points;

        }

        public abstract bool CheckIntersectionWithPoint(Point point);


        public virtual void PrintAttributes()
        {
            Console.Write("Vertices: ");
            foreach(Point point in points)
            {
                Console.Write($"({Math.Round(point.X, 2)}, {Math.Round(point.Y, 2)}) "); 
            }

            Console.WriteLine($"\nColour: {colour}");
        }



        public virtual void ScaleShape(double scaleFactor)
        {
            if (scaleFactor <= 0)
                throw new ArgumentOutOfRangeException("Scale Factor", "The scale factor must be positive!");


            for (int i = 1; i < points.Length; i++)
            {
                //Here, the ArgumentOutOfRangeException that may be thrown by the Point class is caught and then thrown again. This is done to change the message shown to the user. 
                try
                {
                    //These expressions scale each point's coordinates by first treating the anchor point point[0] as the origin (by subtracting it). 
                    points[i].X = points[0].X + scaleFactor * (points[i].X - points[0].X);
                    points[i].Y = points[0].Y + scaleFactor * (points[i].Y - points[0].Y);
                }
                catch(ArgumentOutOfRangeException)
                {
                    throw new ArgumentOutOfRangeException("Scale Factor", "Scaling this shape by this much would parts of it go beyond the canvas. Try scaling by a smaller factor or repositioning first.");
                }
                
            }
        }


        public virtual void TranslateShape(double translateX, double translateY)
        {
            //Here, the ArgumentOutOfRangeException that may be thrown by the Point class is caught and then thrown again. This is done to change the message shown to the user. 
            try
            {
                foreach(Point point in points)
                {
                    point.X = point.X + translateX;
                    point.Y = point.Y + translateY;
                }
            }
            catch(ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Translate", "Translating the shape by this much in these directions would cause all or parts of it to go off screen. Try translating it by a smaller amount.");
            }
        }


        public string GetBasicInfo()
        {
            return $"{colour} {shapeName} - Anchor point: {points[0]}";
        }


        
    }
}
