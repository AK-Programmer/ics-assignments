//Author: Adar Kahiri
//File Name: Triangle.cs
//Project Name: PASS2
//Creation Date: October 21, 2020
//Modified Date: October 30, 2020
/* Description: This class inherits from the shape class, defines additional attributes of a triangle (such as its height), and overrides the methods defined
 * in the shape class in order to carry out calculations specific to triangles. Often times, when this class overrides methods from its base class, it will still
 * the base method its overriding and then add additional logic specific to triangles. This is possible since the base methods have been designed to 
 * be as useful as possible for as many classes as possible.
 */

using System;
namespace PASS2
{

    public class Triangle : Shape
    {

        //These are the attributes a triangle has in addition to the ones defined in the shape class.
        private double surfaceArea;
        private double height;
        private double[] sideLens = new double[3];
        private double perimeter;


        //Pre: The points must be within the bounds of the canvas. Technically, the colour can be set to anything, but since the colour is only being set by the canvas class,
        //the user does not have a chance to set any colours that aren't allowed.
        //Post: None.
        /*Description: This constructor
         *  1. Ensures the triangle is horizontally aligned and that the points given form an actual triangle (e.g., the three points don't all lie on one line).
         *  2. Sorts the points so that the first point in the points array is the leftmost one (or bottom-left if there's a tie).
         *  3. Calculates the sidelengths of the triangle and ensures the first sidelength in the array is the base of the triangle (the horizontal side length)
         *  4. Calculates the perimeter, height, and area of the triangle. 
         */
        public Triangle(string colour, Point point1, Point point2, Point point3) : base(colour, "Triangle", false, point1, point2, point3)
        {
            //This variable is used as part of an algorithm that ensures the base length appears first in the sidelength array.
            int shift = 0;
            int otherPointIndex;
            //This variable is used to temporarily store the value of points when sorting the array. 
            Point temp;

            //Sets perimeter to zero so that it can be incremented later on
            perimeter = 0;

            //Checking if the triangle is aligned horizontally  
            if (points[0].Y != points[1].Y && points[0].Y != points[2].Y && points[2].Y != points[1].Y)
                throw new ArgumentException("The given triangle is on an angle. Two of your points must form a horizontal side length.", "Triangle Vertices");

            //Checking if any two of the three points are equivalent
            else if (points[0].GetAllCoords() == points[1].GetAllCoords() || points[0].GetAllCoords() == points[2].GetAllCoords() || points[1].GetAllCoords() == points[2].GetAllCoords())
                throw new ArgumentException("Two or more of the points entered are equivalent, and subsequently do not form a valid triangle. Please try again.", "Triangle Vertices");

            //Checking if the three points are on the same line
            else if ((points[0].Y == points[1].Y && points[0].Y == points[2].Y && points[2].Y == points[1].Y) || (points[0].X == points[1].X && points[0].X == points[2].X && points[2].X == points[1].X))
                throw new ArgumentException("The points entered are all on the same line, and do not form a valid triangle. Please try again.", "Triangle Vertices");



            //Sorting to ensure the first point in points array is the leftmost one.
            for (int i = points.Length - 1; i > 0; i--)
            {
                //If points[i] is more to the left than points[i-1], swap the two.
                if (points[i].X < points[i-1].X)
                {
                    temp = points[i];
                    points[i] = points[i - 1];
                    points[i - 1] = temp;
                }
            }

            //Tie breaker. If there are two leftmost points, ensure that the bottommost point is the first one. 
            if (points[0].X == points[1].X && points[1].Y < points[0].Y)
            {
                temp = points[1];
                points[1] = points[0];
                points[0] = temp;
            }

            //This loop calculates the 3 sidelengths in a way that ensures that the base of the triangle is the first one in the array.
            for (int i = points.Length - 1; i >= 0; i--)
            {
                //If points[i] and points[(i+1)%3] form the base, set the first element in sideLens as the base length, and calculate the height
                if (points[i].Y == points[(i + 1) % points.Length].Y)
                {
                    //The chunky expression involving modulo operators simplifies to (2*(i + (i+1)%3)) % 3. It gets the index of the point that is NOT i or (i+1)%3.
                    //This formula does not generalize to more than three points, but it can be easily verified that it does in fact work for three points. (try setting i to different values).
                    otherPointIndex = ((points.Length - 1) * (i + (i + 1) % points.Length)) % points.Length;
                    sideLens[0] = points[i].GetDistance(points[(i + 1) % points.Length]);
                    perimeter += sideLens[0];

                    //This ensures that the rest of the sidelengths are assigned to the correct indices and that the values of no indices are overwritten.
                    shift = 1;

                    //If the height is negative it means the triangle is rotated and an exception is thrown.
                    if (points[otherPointIndex].Y - points[i].Y < 0)
                        throw new ArgumentException("This triangle is rotated. Please ensure the base of the triangle is below the ", "Triangle Vertices");
                    else
                    {
                        //The height is calculated by taking the difference between the y-coordinates of one of the points of the base and the point that does not form the base.
                        height = points[otherPointIndex].Y - points[i].Y;
                    }
                }
                else
                {
                    //If the base length hasn't yet been found, then shift equals 0, and if it has been found, then shift equals 1. This is necessary 
                    sideLens[i+shift] = points[i].GetDistance(points[(i+1) % 3]);
                    perimeter += sideLens[i + shift];
                }
            }


            
            //This loop adds up all the sidelengths to calculate the perimeter.
            for (int i = 0; i < sideLens.Length; i ++)
            {
                perimeter += sideLens[i];
            }

            //Formula for area of a triangle. It's ensured that sideLens[0] is the base of the triangle.
            surfaceArea = sideLens[0] * height / 2;
        }


        //Pre: none.
        //Post: none.
        //Description: This method prints the triangle's attributes. It calls the base PrintAttributes() method and prints the unique properties of a triangle. 
        public override void PrintAttributes()
        {
            base.PrintAttributes();

            Console.WriteLine($"- Side lengths: {Math.Round(sideLens[0], 2)}, {Math.Round(sideLens[1], 2)}, {Math.Round(sideLens[2], 2)}");
            Console.WriteLine($"- Height:  {Math.Round(height, 2)}");
            Console.WriteLine($"- Surface Area: {Math.Round(surfaceArea, 2)}");
        }


        //Pre: scale factor must not be negative.
        //Post: none
        //Description: This method scales the 2 non-anchor points of the triangle, and scales all the calculated properties proportionally (linearly for all but the area). Of course, all of this is error-trapped.
        public override void ScaleShape(double scaleFactor)
        {
            base.ScaleShape(scaleFactor);

            //Scaling the rest of the variables
            height *= scaleFactor;
            surfaceArea *= scaleFactor * scaleFactor;
            perimeter *= scaleFactor;

            for (int i = 0; i < sideLens.Length; i ++)
            {
                sideLens[i] *= scaleFactor;
            }
        }

        //Pre: the point must be within the bounds of the canvas.
        //Post: returns true if the point does intersect with the triangle, and false otherwise. 
        //Description: This method checks if the given point intersects triangle.
        public override bool CheckIntersectionWithPoint(Point point)
        {
            //Defining all the necessary values as variables for ease of reading.
            double px = point.X;
            double py = point.Y;
            double x1 = points[0].X;
            double y1 = points[0].Y;
            double x2 = points[1].X;
            double y2 = points[1].Y;
            double x3 = points[2].X;
            double y3 = points[2].Y;

            //Areas are calculated using Heron's formula
            double area1 = Math.Abs((x1 - px) * (y2 - py) - (x2 - px) * (y1 - py));
            double area2 = Math.Abs((x2 - px) * (y3 - py) - (x3 - px) * (y2 - py));
            double area3 = Math.Abs((x3 - px) * (y1 - py) - (x1 - px) * (y3 - py));

            //If the area of the triangle equals the sum of the areas of the 3 triangles the point forms with combinations of vertices of the triangle, return true.
            return surfaceArea == (area1 + area2 + area3) / 2;
            

        }

        //Pre: none.
        //Post: none.
        //Description: this method displays the triangle's basic information and is used when displaying all of the shapes on the canvas at once. It calls the base GetBasicInfo method to display the general shape information.
        public override string GetBasicInfo()
        {
            string basicInfo = base.GetBasicInfo();

            basicInfo += $"\n- Other points: ({points[1].X}, {points[1].Y}), ({points[2].X}, {points[2].Y})";
            return basicInfo;
        }
    }
}
