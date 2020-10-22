using System;

namespace PASS2
{
    class Program
    {
        static void Main(string[] args)
        {
            Point point1 = new Point(1, 0);
            Point point2 = new Point(0, 0);
            Point point3 = new Point(0, 1);

            Triangle tri = new Triangle("Blue", point1, point2, point3);

            Console.WriteLine(tri.CheckIntersectionWithPoint(new Point(0.5, 0.5)));

            tri.TranslateShape(10, 5);

            tri.TranslateShape(-10, -5);
            tri.ScaleShape(3);
            tri.PrintAttributes();

            for (int i = 0; i < 40; i++)
            {
                Console.Write("hello");
            }

            Console.ReadKey();

        }
    }
}
