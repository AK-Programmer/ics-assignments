//Author: Adar Kahiri
//File Name: Sphere.cs
//Project Name: PASS2
//Creation Date: October 26, 2020
//Modified Date: Nov 1, 2020
/* Description: This class inherits from the shape class, defines additional attributes of a sphere (such as its radius and volume), and overrides the methods defined
 * in the shape class in order to carry out calculations specific to circles. Often times, when this class overrides methods from its base class, it will still
 * the base method its overriding and then add additional logic specific to the sphere. This is possible since the base methods have been designed to 
 * be as useful as possible for as many classes as possible.
 */

using System;

namespace PASS2
{
    public class Sphere : Shape
    {
        private double radius;
        private double surfaceArea;
        private double volume;

        //Pre: The center point must be inside the canvas. No point of the sphere (calculated using the center point and the radius) should be outside the canvas.
        //Post: None.
        //Description: This constructor ensures that the radius and center point are valid (e.g., that no point of the sphere goes out of bounds), and calculates the surface area and volume of the sphere.
        public Sphere(double radius, string colour, Point center) : base(colour, "Sphere", true, center)
        {
            if (radius <= 0)
            {
                throw new ArgumentOutOfRangeException("Radius", "The radius must be a positive number");
            }
                

            CheckSphereInBounds(points[0], radius, "The desired sphere goes outside the screen! Either reposition it, or give it a smaller radius");

            //Setting the rest of the values appropriately. 
            this.radius = radius;
            surfaceArea = 4* Math.PI * radius * radius;
            volume = (4 * Math.PI * radius * radius * radius)/3;
        }

        //Pre: scaleFactor must be a positive number.
        //Post: None.
        //Description: This method scales the radius, volume, and surface area by the scale factor (proportionally).  
        public override void ScaleShape(double scaleFactor)
        {
            double potentialRad = radius * scaleFactor;

            if (scaleFactor <= 0)
            {
                throw new ArgumentOutOfRangeException("Scale Factor", "The scale factor must be a positive number!");
            }
                

            //Logic for checking if a sphere has gone out of bounds is different than other shapes, so this CheckSphereInBounds method is called to ensure that it's within bounds.
            CheckSphereInBounds(points[0], potentialRad, "Scaling the sphere by this factor would make it go beyond the screen. Try scaling it by a smaller amount or reposition it first");

            //Scaling the rest of the values appropriately (radius and perimeter increase linearly, area increases quadratically)
            radius = potentialRad;
            surfaceArea *= scaleFactor * scaleFactor;
            volume *= scaleFactor * scaleFactor * scaleFactor;
        }

        //Pre: none.
        //Post: none.
        //Description: This method prints the circle's attributes. It calls the base PrintAttributes() method and prints the unique properties of a circle. 
        public override void PrintAttributes()
        {
            base.PrintAttributes();

            Console.WriteLine($"- Radius: {Math.Round(radius, 2)}");
            Console.WriteLine($"- Volume: {Math.Round(volume, 2)}");
            Console.WriteLine($"- Surface Area: {Math.Round(surfaceArea, 2)}");
 
        }

        //Pre: translateX, translateY, translateZ, must not cause any of the shape's points to go out of bound.
        //Post: none.
        //Description: This method translates the circle by the desired amount. It ensures that the circle can be validly translated by the desired amount before doing so.
        public override void TranslateShape(double translateX, double translateY, double translateZ)
        {
            //Will store the prospective values of x and y, and will be used to check if the shape can be translated to the desired location without going out of bounds
            double potentialX = points[0].X + translateX;
            double potentialY = points[0].Y + translateY;
            double potentialZ = points[0].Z + translateZ;

            //These if statements handle all possible ways the circle could go out of bounds and deliver the appropriate message.
             
            if (potentialX + radius > Canvas.SCREEN_WIDTH)
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
            else if (potentialZ + radius > Canvas.SCREEN_DEPTH)
            {
                throw new ArgumentOutOfRangeException("TranslateZ", "You are trying to translate the shape beyond the screen. Try translating the shape a little less out.");
            }
            else if (potentialZ - radius < 0)
            {
                throw new ArgumentOutOfRangeException("TranslateZ", "You are trying to translate the shape beyond the screen. Try translating the shape a little less in.");
            }

            points[0].X = potentialX;
            points[0].Y = potentialY;
            points[0].Z = potentialZ;
        }

        //Pre: the point must be within the bounds of the canvas.
        //Post: returns true if the point does intersect with the circle, and false otherwise. 
        //Description: This method checks if the given point is inside the circle. 
        public override bool CheckIntersectionWithPoint(Point point)
        {
            //return true if the point's distance from the center is smaller than the radius (meaning it's inside the sphere) and false otherwise.
            return point.GetDistance(points[0]) <= radius;
        }

        //Pre: the point must be within the bounds of the canvas, and the radius must be positive. Both of these conditions are ensured elsewhere.
        //Post: none.
        //Description: This method checks if the entirety of the circle is inside the canvas, and throws an exception otherwise.
        private void CheckSphereInBounds(Point center, double radius, string message)
        {
            //If any part of the circle goes out of bounds, throw an exception
            if (center.X + radius > Canvas.SCREEN_WIDTH || center.X - radius < 0 || center.Y + radius > Canvas.SCREEN_HEIGHT ||
                center.Y - radius < 0 || center.Z + radius > Canvas.SCREEN_DEPTH || center.Z - radius < 0)
            {
                throw new ArgumentOutOfRangeException("Sphere", message);
            }
        }


        //Pre: none.
        //Post: none.
        //Description: this method displays the circle's basic information and is used when displaying all of the shapes on the canvas at once. It extends the base GetBasicInfo method by overriding and calling it.
        public override string GetBasicInfo()
        {
            string basicInfo = base.GetBasicInfo();
            basicInfo += $"\n- Radius: {Math.Round(radius, 2)}";

            return basicInfo;
        }

    }
}
