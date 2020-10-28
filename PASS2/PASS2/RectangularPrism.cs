//Author: Adar Kahiri
//File Name: RectangularPrism.cs
//Project Name: PASS2
//Creation Date: October 26, 2020
//Modified Date: October 30, 2020
/* Description: This class inherits from the shape class, defines additional attributes of a rectangular prism (such as its sidelengths), and overrides the methods defined
 * in the shape class in order to carry out calculations specific to rectangular prisms. Often times, when this class overrides methods from its base class, it will still
 * call the base method its overriding and then add additional logic specific to the rectangle. This is possible since the base methods have been designed to 
 * be as useful as possible for as many classes as possible.
 */

using System;
namespace PASS2
{
    public class RectangularPrism : Shape
    {

        private double length;
        private double height;
        private double depth;
        private double volume;
        private double surfaceArea;
        private double diagLen;

        //Pre: colour and shapeName are set internally by the program. Points must be within the bounds of the canvas, but this ensured elsewhere.
        //Post: None.
        //Desc: This constructor calculates the positions of the other  7 vertices of the rectangular prism and calculates properties like volume and diagonal length.
        public RectangularPrism(string colour, double length, double height, double depth, Point anchorPoint) : base(colour, "Rectangular Prism", true, new Point[8])
        {
            string bin;

            //setting vertices of the rectangular prism
            points[0] = anchorPoint;

            //This for loop adds all of the vertices of the rectangular prism to the points array
            for (int i = 1; i < points.Length; i++)
            {
                //Converts i to binary. Each binary number from 1 to 7 represents a different way to increment a combination of dimensions of the anchor point.
                bin = Convert.ToString(i, 2);
                points[i] = new Point(points[0].X + bin[0] == 1 ? length : 0, points[0].Y + bin[1] == 1 ? height : 0, points[0].Z + bin[2] == 1 ? depth : 0);
            }

            this.length = length;
            this.height = height;
            this.depth = depth;

            //If height or length are less than zero, throw an exception with an appropriate message.
            if (length <= 0)
                throw new ArgumentOutOfRangeException("length", "Rectangle length must be a positive number!");

            if (height <= 0)
                throw new ArgumentOutOfRangeException("height", "Rectangle height must be a positive number!");

            if(depth <= 0)
                throw new ArgumentOutOfRangeException("height", "Rectangle depth must be a positive number!");


            surfaceArea = 2*length * height + 2*length*depth + 2*height*depth;
            volume = length*height*depth;
            diagLen = Math.Sqrt(length * length + height * height + depth*depth);
        }

        //Pre: scale factor must not be negative.
        //Post: none
        //Description: This method scales the 3 non-anchor points of the rectangle, and scales the length, width, and calculated properties proportionally. Of course, all of this is error-trapped.
        public override void ScaleShape(double scaleFactor)
        {
            double potentialLength = length * scaleFactor;
            double potentialHeight = height * scaleFactor;
            double potentialDepth = depth * scaleFactor;

            //Here, the ArgumentOutOfRangeException that may be thrown by the Point class is caught and then thrown again. This is done to change the message shown to the user. 
            try
            {
                string bin;

                for (int i = 1; i < points.Length; i++)
                {
                    //Converts i to binary. Each binary number from 1 to 7 represents a different way to increment a combination of dimensions of the anchor point.
                    bin = Convert.ToString(i, 2);

                    points[i].X = points[0].X + bin[0] == 1 ? potentialLength : 0;
                    points[i].Y = points[0].Y + bin[1] == 1 ? potentialHeight : 0;
                    points[i].Z = points[0].Z + bin[2] == 1 ? potentialDepth : 0;
                }

                //Perimeter and diagonal length scale linearly, surface area scales by the square of the scale factor, volume scales by the cube of the scale factor.
                length = potentialHeight;
                height = potentialHeight;
                depth = potentialDepth;
                surfaceArea *= scaleFactor * scaleFactor;
                volume *= scaleFactor * scaleFactor * scaleFactor;
                diagLen *= scaleFactor;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Scale Factor", "Scaling this rectangular prism by this much would cause parts of it go beyond the canvas. Try scaling by a smaller factor or repositioning first.");
            }
        }

        //Pre: none.
        //Post: none.
        //Description: This method prints the rectangle's attributes. It calls the base PrintAttributes() method and prints the unique properties of a rectangle. 
        public override void PrintAttributes()
        {
            base.PrintAttributes();

            Console.WriteLine($"- Length: {Math.Round(length, 2)}");
            Console.WriteLine($"- Height: {Math.Round(height, 2)}");
            Console.WriteLine($"- Volume: {Math.Round(volume, 2)}");
            Console.WriteLine($"- Surface Area: {Math.Round(surfaceArea, 2)}");
            Console.WriteLine($"- Diagonal Length: {Math.Round(diagLen, 2)}");
        }

        //Pre: the point must be within the bounds of the canvas.
        //Post: returns true if the point does intersect with the rectangle, and false otherwise. 
        //Description: This method checks if the given point intersects with the line by checking if the sum of its distances from both end points is equal to the length of the line and returning true if so (returning false otherwise).
        public override bool CheckIntersectionWithPoint(Point point)
        {
            return point.X >= points[0].X && point.X <= points[0].X + length && point.Y <= points[0].Y && point.Y >= points[0].Y - height && point.Z >= points[0].Z && point.Z <= points[0].X + depth;
        }

        //Pre: none.
        //Post: none.
        //Description: this method displays the rectangle's basic information and is used when displaying all of the shapes on the canvas at once. It calls the base GetBasicInfo method to display the general shape information.
        public override string GetBasicInfo()
        {
            string basicInfo = base.GetBasicInfo();
            basicInfo += $"\n- Length: {Math.Round(length, 2)} \n- Height: {Math.Round(height, 2)}";
            return basicInfo;
        }
    }
}
