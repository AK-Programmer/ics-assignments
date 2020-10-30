//Author: Adar Kahiri
//File Name: Circle.cs
//Project Name: PASS2
//Creation Date: October 21, 2020
//Modified Date: Nov 1, 2020
/* Description: This class inherits from the shape class, defines additional attributes of a circle (such as its radius), and overrides the methods defined
 * in the shape class in order to carry out calculations specific to circles. Often times, when this class overrides methods from its base class, it will still
 * call the base method its overriding and then add additional logic specific to the circle. This is possible since the base methods have been designed to 
 * be as useful as possible for as many classes as possible.
 */

using System;


namespace PASS2
{

    public class Circle : Shape
    {
        private double radius;
        private double surfaceArea;
        private double circumference;


        //Pre: The center point must be inside the canvas. No point of the circle (calculated using the center point and the radius) should be outside the canvas.
        //Post: None.
        //Description: This constructor ensures that the radius and center point are valid (e.g., that no point of the circle goes out of bounds), and calculates the surface area and circumference of the circle.
        public Circle(double radius, string colour, Point center): base(colour, "Circle", false, center)
        {
            if (radius <= 0)
            {
                throw new ArgumentOutOfRangeException("radius", "The radius must be a positive number");
            }

            CheckCircleInBounds(points[0], radius, "The desired circle goes outside the screen! Either reposition it, or give it a smaller radius");

            //Setting the rest of the values appropriately. 
            this.radius = radius;
            surfaceArea = Math.PI * radius * radius;
            circumference = 2 * Math.PI * radius;
        }

        //Pre: scaleFactor must be a positive number.
        //Post: None.
        //Description: This method scales the radius, perimeter, and area by the scale factor (proportionally).  
        public override void ScaleShape(double scaleFactor)
        {
            double potentialRad = radius * scaleFactor;

            if (scaleFactor <= 0)
            {
                throw new ArgumentOutOfRangeException("Scale Factor", "The scale factor must be a positive number!");
            }


            //Logic for checking if a circle has gone out of bounds is different than other shapes, so this CheckCircleInBounds method is called to ensure that it's within bounds.
            CheckCircleInBounds(points[0], potentialRad, "Scaling the circle by this factor would make it go beyond the screen. Try scaling it by a smaller amount or reposition it first");

            //Scaling the rest of the values appropriately (radius and perimeter increase linearly, area increases quadratically)
            radius = potentialRad;
            surfaceArea *= scaleFactor * scaleFactor;
            circumference *= scaleFactor;
        }

        //Pre: none.
        //Post: none.
        //Description: This method prints the circle's attributes. It calls the base PrintAttributes() method and prints the unique properties of a circle. 
        public override void PrintAttributes()
        {
            base.PrintAttributes();

            Console.WriteLine($"- Radius: {Math.Round(radius, 2)}");
            Console.WriteLine($"- Surface Area: {Math.Round(surfaceArea, 2)}");
            Console.WriteLine($"- Circumference: {Math.Round(circumference, 2)}");
        }

        //Pre: translateX, translateY, translateZ, must not cause any of the shape's points to go out of bound.
        //Post: none.
        //Description: This method translates the circle by the desired amount. It ensures that the circle can be validly translated by the desired amount before doing so.
        public override void TranslateShape(double translateX, double translateY, double translateZ = 0)
        {
            //Will store the prospective values of x and y, and will be used to check if the shape can be translated to the desired location without going out of bounds
            double potentialX = points[0].X + translateX;
            double potentialY = points[0].Y + translateY;

            //These if statements handle all possible ways the circle could go out of bounds and deliver the appropriate message.
            if(potentialX + radius > Canvas.SCREEN_WIDTH)
            {
                throw new ArgumentOutOfRangeException("TranslateX", "You are trying to translate the shape beyond the screen. Try translating the shape a little less to the right.");
            }
            else if (potentialX - radius < 0)
            {
                throw new ArgumentOutOfRangeException("TranslateX", "You are trying to translate the shape beyond the screen. Try translating the shape a little less to the left.");
            }
            else if (potentialY + radius > Canvas.SCREEN_HEIGHT)
            {
                throw new ArgumentOutOfRangeException("TranslateY", "You are trying to translate the shape beyond the screen. Try translating the shape a little less up.");
            }
            else if (potentialY - radius < 0)
            {
                throw new ArgumentOutOfRangeException("TranslateY", "You are trying to translate the shape beyond the screen. Try translating the shape a little less down.");
            }

            points[0].X = potentialX;
            points[0].Y = potentialY;
        }


        //Pre: the point must be within the bounds of the canvas.
        //Post: returns true if the point does intersect with the circle, and false otherwise. 
        //Description: This method checks if the given point is inside the circle. 
        public override bool CheckIntersectionWithPoint(Point point)
        {
            //Return true if the points distance from the center is smaller than the radius (meaning its inside the circle), and false otherwise.
            return point.GetDistance(points[0]) <= radius;
        }

        //Pre: the point must be within the bounds of the canvas, and the radius must be positive. Both of these conditions are ensured elsewhere.
        //Post: none.
        //Description: This method checks if the entirety of the circle is inside the canvas, and throws an exception otherwise.
        private void CheckCircleInBounds(Point center, double radius, string message)
        {
            //If any part of the circle goes out of bounds, throw an exception
            if (center.X + radius > Canvas.SCREEN_WIDTH || center.X - radius < 0 || center.Y + radius > Canvas.SCREEN_HEIGHT || center.Y - radius < 0)
            {
                throw new ArgumentOutOfRangeException("Circle", message);
            }
        }


        //Pre: none.
        //Post: none.
        //Description: this method displays the circle's basic information and is used when displaying all of the shapes on the canvas at once. It extends the base GetBasicInfo method by overriding and calling it.
        public override string GetBasicInfo()
        {
            string basicInfo =  base.GetBasicInfo();
            basicInfo += $"\n- Radius: {Math.Round(radius, 2)}";

            return basicInfo;
        }

    }
}
