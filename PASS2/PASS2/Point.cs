using System;


namespace PASS2
{

    public class Point
    {
        private double x;
        private double y;

        public double X { get => x; set => SetX(value); }
        public double Y { get => y; set => SetY(value); }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }


        private void SetX(double x)
        {
            if (x <= Canvas.SCREEN_WIDTH && x >= 0)
                this.x = x;
            else
            {
                throw new ArgumentOutOfRangeException("point x-coord", "The given x-coordinate for the point is outside the screen!");
            }
        }

        private void SetY(double y)
        {
            if (y <= Canvas.SCREEN_HEIGHT && y >= 0)
                this.y = y;
            else
                throw new ArgumentOutOfRangeException("point y-coord", "The given y-coordinate for the point is outside the screen!");
        }

        public double GetDistance(Point point)
        {
            return Math.Sqrt((x - point.X) * (x - point.X) + (y - point.Y) * (y - point.Y));
        }
    }
}
