﻿//Author: Adar Kahiri
//File Name: Rectangle.cs
//Project Name: PASS2
//Creation Date: October 21, 2020
//Modified Date: Nov 1, 2020
/* Description: This class inherits from the shape class, defines additional attributes of a rectangle (such as its sidelengths), and overrides the methods defined
 * in the shape class in order to carry out calculations specific to rectangles. Often times, when this class overrides methods from its base class, it will still
 * call the base method its overriding and then add additional logic specific to the rectangle. This is possible since the base methods have been designed to 
 * be as useful as possible for as many classes as possible.
 */

using System;
namespace PASS2
{
    public class Rectangle : Shape
    {
        //These variables represent various characteristics that the rectangle doesn't share with most other shapes.
        private double length;
        private double height;
        private double surfaceArea;
        private double diagLen;
        private double perimeter;

        //Pre: colour and shapeName are set internally by the program and don't cause any program-crashing bugs if they're not set correctly. Points must be within the bounds of the canvas, but this ensured elsewhere.
        //Post: None.
        //Desc: This constructor ensures that height and length aren't zero and calculates properties like area and diagonal length.
        public Rectangle(string colour, double length, double height, Point anchorPoint) : base(colour, "Rectangle", false, anchorPoint, new Point(anchorPoint.X + length, anchorPoint.Y),
            new Point(anchorPoint.X, anchorPoint.Y - height), new Point(anchorPoint.X + length, anchorPoint.Y - height))
        {
            this.length = length;
            this.height = height;

            //If height or length are less than zero, throw an exception with an appropriate message.
            if (length <= 0)
            {
                throw new ArgumentOutOfRangeException("length", "Rectangle length must be a positive number!");
            }
                
            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException("height", "Rectangle height must be a positive number!");
            }
                
            //Scaling all properties proportionally (scaling area by the square, scaling perimeter linearly, etc.)
            surfaceArea = length * height;
            perimeter = 2 * length + 2 * height;
            diagLen = Math.Sqrt(length * length + height * height);
        }

        //Pre: scale factor must not be negative.
        //Post: none
        //Description: This method scales the 3 non-anchor points of the rectangle, and scales the length, width, and calculated properties proportionally. Of course, all of this is error-trapped.
        public override void ScaleShape(double scaleFactor)
        {
            //Scaling the height and width by the scale factor.
            double potentialLength = length * scaleFactor; 
            double potentialHeight = height * scaleFactor;

            //Here, the ArgumentOutOfRangeException that may be thrown by the Point class is caught and then thrown again. This is done to change the message shown to the user. 
            try
            {
                //Try to update all the points. The point class's set method will throw an exception if any of the updated points go out of bounds.
                points[1].X = points[0].X + potentialLength;
                points[2].Y = points[0].Y - potentialHeight;
                points[3].X = points[0].X + potentialLength;
                points[3].Y = points[0].Y - potentialHeight;

                //Perimeter and diagonal length scale linearly, surface area scales quadratically.
                length = potentialHeight;
                height = potentialHeight;
                surfaceArea *= scaleFactor * scaleFactor;
                perimeter *= scaleFactor;
                diagLen *= scaleFactor;
            }
            catch(ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Scale Factor", "Scaling this rectangle by this much would cause parts of it go beyond the canvas. Try scaling by a smaller factor or repositioning first.");
            }
        }

        //Pre: none.
        //Post: none.
        //Description: This method prints the rectangle's attributes. It calls the base PrintAttributes() method and prints the unique properties of a rectangle. 
        public override void PrintAttributes()
        {
            base.PrintAttributes();

            //Printing all attributes, rounded to 2 decimals.
            Console.WriteLine($"- Length: {Math.Round(length, 2)}");
            Console.WriteLine($"- Height: {Math.Round(height, 2)}");
            Console.WriteLine($"- Perimeter: {Math.Round(perimeter, 2)}");
            Console.WriteLine($"- Surface Area: {Math.Round(surfaceArea, 2)}");
            Console.WriteLine($"- Diagonal Length: {Math.Round(diagLen, 2)}");
        }

        //Pre: the point must be within the bounds of the canvas.
        //Post: returns true if the point does intersect with the rectangle, and false otherwise. 
        //Description: This method checks if the given point intersects with the prism.
        public override bool CheckIntersectionWithPoint(Point point)
        {
            //This is essentially the same logic that is used to ensure that a point stays inside the canvas. It ensures that each of the point's coordinates is between the anchor point's coordinate and the coordinate of the point opposite of the anchor point in that dimension.
            return point.X >= points[0].X && point.X <= points[1].X && point.Y <= points[0].Y && point.Y >= points[2].Y;
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
            Console.WriteLine($"- Height: {Math.Round(height, 2)}");
        }
    }
}
