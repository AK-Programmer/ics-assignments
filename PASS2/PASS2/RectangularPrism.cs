﻿//Author: Adar Kahiri
//File Name: RectangularPrism.cs
//Project Name: PASS2
//Creation Date: October 26, 2020
//Modified Date: Nov 1, 2020
/* Description: This class inherits from the shape class, defines additional attributes of a rectangular prism (such as its sidelengths), and overrides the methods defined
 * in the shape class in order to carry out calculations specific to rectangular prisms. Often times, when this class overrides methods from its base class, it will still
 * call the base method its overriding and then add additional logic specific to the rectangle. This is possible since the base methods have been designed to 
 * be as useful as possible for as many classes as possible.
 */

using System;
using System.Linq;
namespace PASS2
{
    public class RectangularPrism : Shape
    {
        //These variables represent various characteristics that the prism doesn't share with most other shapes.
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
            //This will store an array of 0s and 1s that will be used to get the coordinates of the other 7 points of the rectangle
            int[] bin;

            //setting vertices of the rectangular prism
            points[0] = anchorPoint;

            //This for loop adds all of the vertices of the rectangular prism to the points array
            for (int i = 1; i < points.Length; i++)
            {
                //Converts i to binary. Each binary number from 1 to 7 represents a different way to increment a combination of dimensions of the anchor point.
                //PadLeft(3, '0') ensures the binary number always has at least 3 digits (e.g., 001, 010, 011, etc.)
                //.Select converts each char '0' or '1' into ints 0 or 1. if b's char value is '0', then '0' - '0' = 0. If it's '1', then '1' - '0' = 1. ToArray() converts it into an int array.
                bin = Convert.ToString(i, 2).PadLeft(3, '0').Select(b =>  b - '0').ToArray(); 

                //Each coordinate is assigned a digit of bin. If that digit is 1, set it to the value of anchorPoint for that coordinate plus the user-inputted sidelength for that dimension. Otherwise, simply return set it to value of the anchorPoint.
                points[i] = new Point(anchorPoint.X + bin[0] * length, anchorPoint.Y - bin[1] * height, anchorPoint.Z + bin[2] * depth);
            }

            this.length = length;
            this.height = height;
            this.depth = depth;

            //If height, length, or depth are less than zero, throw an exception with an appropriate message.
            if (length <= 0)
            {
                throw new ArgumentOutOfRangeException("length", "Rectangle length must be a positive number!");
            }
            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException("height", "Rectangle height must be a positive number!");
            }
            if (depth <= 0)
            {
                throw new ArgumentOutOfRangeException("depth", "Rectangle depth must be a positive number!");
            }

            //Setting all other properties of the prism to their approrpiate values (according to formulas)
            surfaceArea = 2*length * height + 2*length*depth + 2*height*depth;
            volume = length*height*depth;
            //Diagonal length calculated using pythagorean theorem
            diagLen = Math.Sqrt(length * length + height * height + depth*depth);
        }

        //Pre: scale factor must not be negative.
        //Post: none
        //Description: This method scales the 7 non-anchor points of the prism, and scales the length, width, depth and calculated properties proportionally. Of course, all of this is error-trapped.
        public override void ScaleShape(double scaleFactor)
        {
            //Temporary variables to store what the dimensions would be if the rectangle were scaled. These dimensions will become permanent if all point remain in the canvas when scaled.
            double potentialLength = length * scaleFactor;
            double potentialHeight = height * scaleFactor;
            double potentialDepth = depth * scaleFactor;

            //Here, the ArgumentOutOfRangeException that may be thrown by the Point class is caught and then thrown again. This is done to change the message shown to the user. 
            try
            {
                //This will store an array of 0s and 1s that will be used to get the coordinates of the other 7 points of the rectangle
                int[] bin;

                //For each point in the array, scale each of its coordinates by either nothing or by the scale factor depending on which point it is. 
                for (int i = 1; i < points.Length; i++)
                {
                    //Converts i to binary. Each binary number from 1 to 7 represents a different way to increment a combination of dimensions of the anchor point.
                    //PadLeft(3, '0') ensures the binary number always has at least 3 digits (e.g., 001, 010, 011, etc.)
                    //.Select converts each char '0' or '1' into ints 0 or 1. if b's char value is '0', then '0' - '0' = 0. If it's '1', then '1' - '0' = 1. ToArray() converts it into an int array.
                    bin = Convert.ToString(i, 2).PadLeft(3, '0').Select(b =>  b - '0').ToArray(); 

                    //if bin[j] == 0, then the point's jth coordinate will remain the same. If bin[j] == 1, then the point's jth coordinate will be scaled by scaleFactor.
                    points[i].X = points[0].X + bin[0]*potentialLength;
                    points[i].Y = points[0].Y + bin[1]*potentialHeight;
                    points[i].Z = points[0].Z + bin[2]*potentialDepth;
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
        //Description: This method prints the prism's attributes. It calls the base PrintAttributes() method and prints the unique properties of a prism. 
        public override void PrintAttributes()
        {
            base.PrintAttributes();

            //Rounding all attributes to 2 decimals.
            Console.WriteLine($"- Length: {Math.Round(length, 2)}");
            Console.WriteLine($"- Height: {Math.Round(height, 2)}");
            Console.WriteLine($"- Volume: {Math.Round(volume, 2)}");
            Console.WriteLine($"- Surface Area: {Math.Round(surfaceArea, 2)}");
            Console.WriteLine($"- Diagonal Length: {Math.Round(diagLen, 2)}");
        }

        //Pre: the point must be within the bounds of the canvas.
        //Post: returns true if the point does intersect with the prism, and false otherwise. 
        //Description: This method checks if the given point intersects with the prism.
        public override bool CheckIntersectionWithPoint(Point point)
        {
            //This is essentially the same logic that is used to ensure that a point stays inside the canvas. It ensures that each of the point's coordinates is between the anchor point's coordinate and the coordinate of the point opposite of the anchor point in that dimension.
            return point.X >= points[0].X && point.X <= points[0].X + length && point.Y <= points[0].Y && point.Y >= points[0].Y - height && point.Z >= points[0].Z && point.Z <= points[0].X + depth;
        }

        //Pre:col and row should be within the bounds of the console window. It's assumed the window has dimensions 90x30. shapeNum is the shape's 'place' in the list. It's used to display a number beside each shape. 
        //Post: none.
        //This method prints the shape's basic information at the specified column and row. It extends the method of the same name in the shape class to display more information about the shape.
        public override void PrintBasicInfo(int col, int row, int shapeNum)
        {
            base.PrintBasicInfo(col, row, shapeNum);

            //Setting the cursor position to the given column and row (row incremented by 2)
            Console.SetCursorPosition(col, row + 2);
            Console.Write($"- Length: {Math.Round(length, 2)}");
            //Setting the cursor position to the given column and row (row incremented by 3)
            Console.SetCursorPosition(col, row + 3);
            Console.Write($"- Height: {Math.Round(height, 2)}");
            //Setting the cursor position to the given column and row (row incremented by 4)
            Console.SetCursorPosition(col, row + 4);
            Console.WriteLine($"- Depth: {Math.Round(depth, 2)}");

        }
    }
}
