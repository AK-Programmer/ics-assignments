//Author: Adar Kahiri
//File Name: Line.cs
//Project Name: PASS2
//Creation Date: October 21, 2020
//Modified Date: October 30, 2020
/* Description: This class inherits from the shape class, defines additional attributes of a line (such as its slope), and overrides the methods defined
 * in the shape class in order to carry out calculations specific to lines. Often times, when this class overrides methods from its base class, it will still
 * call the base method its overriding and then add additional logic specific to the line. This is possible since the base methods have been designed to 
 * be as useful as possible for as many classes as possible.
 */

using System;
namespace PASS2
{
    public class Line : Shape
    {
        private double slope;
        private double length;

        //Pre: colour and shapeName are set internally by the program and don't cause any program-crashing bugs if they're not set correctly. Points must be within the bounds of the canvas, but this ensured elsewhere.
        //Post: None.
        //Desc: This constructor ensures the two points aren't actually the same point, sorts the points so that the leftmost one is first in the array, and calculates the slope and length of the line.
        public Line(string colour, Point point1, Point point2) : base(colour, "Line", true, point1, point2)
        {
            //If the two points are the same point, throw an exception
            if (point1.X == point2.X && point1.Y == point2.Y && point1.Z == point2.Z)
                throw new ArgumentException("Both of your points cannot be the same. Please try again!", "same points");

            //Sort the points so that the leftmost one is first in the points array. 
            if (points[1].X < points[0].X)
            {
                Point tempPoint = points[0];
                points[0] = points[1];
                points[1] = tempPoint; 
            }
            //If their x - coordinates are the same, make the bottom point first.
            else if (points[1].X == points[0].X && points[1].Y < points[0].Y)
            {
                Point tempPoint = points[0];
                points[0] = points[1];
                points[1] = tempPoint;
            }
            //If their x and y coordinates are the same, select the least deep point as the anchor point.
            else if (points[1].X == points[0].X && points[1].Y == points[0].Y && points[1].Z < points[0].Z)
            {
                Point tempPoint = points[0];
                points[0] = points[1];
                points[1] = tempPoint;
            }

            //Length is calculated using the euclidean distance formula
            length = points[0].GetDistance(points[1]);
            if (points[0].Z == 0 && points[1].Z == 0)
                slope = 0;
            else
                slope = (points[1].Y - points[0].Y) / Math.Sqrt((points[1].X - points[0].X)*(points[1].X - points[0].X) + (points[1].Z - points[0].Z)* (points[1].Z - points[0].Z));
            
        }

        //Pre: none.
        //Post: none.
        //Description: This method prints the line's attributes. It calls the base PrintAttributes() method and prints the unique properties of a line. 
        public override void PrintAttributes()
        {
            base.PrintAttributes();
            Console.WriteLine($"- Length: {Math.Round(length,2)}");
            Console.WriteLine($"- Slope: {Math.Round(slope,2)}");
        }


        //Pre: scale factor must not be negative.
        //Post: none
        //Description: This method calls the base ScaleShape method and multiplies the line's length by the scale factor.
        public override void ScaleShape(double scaleFactor)
        {
            base.ScaleShape(scaleFactor);

            length *= scaleFactor;  
        }

        //Pre: the point must be within the bounds of the canvas.
        //Post: returns true if the point does intersect with the line, and false otherwise. 
        //Description: This method checks if the given point intersects with the line by checking if the sum of its distances from both end points is equal to the length of the line and returning true if so (returning false otherwise).
        public override bool CheckIntersectionWithPoint(Point point)
        {
            return (points[0].GetDistance(point) + points[1].GetDistance(point) == length);
        }


        //Pre: none.
        //Post: none.
        //Description: this method displays the line's basic information and is used when displaying all of the shapes on the canvas at once. 
        public override string GetBasicInfo()
        {
            string basicInfo = base.GetBasicInfo();

            basicInfo += $"\n- Other endpoint: ({Math.Round(points[1].X,2)}, {Math.Round(points[1].Y, 2)}, {Math.Round(points[1].Z,2)})";
            return basicInfo;
        }
    }
}
