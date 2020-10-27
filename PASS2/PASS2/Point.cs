//Author: Adar Kahiri
//File Name: Point.cs
//Project Name: PASS2
//Creation Date: October 21, 2020
//Modified Date: October 30, 2020
/* Description: This class contains the attributes of a 2D point (just x and y coordinates). It exists to prevent repetition and make the rest of the program much cleaner. 
 * In particular, the set methods for the x and y coordinates ensure that those coordinates remain within the bounds of the canvas. If it weren't done this way, 
 * logic for throwing exceptions would have to be manually written every single time a point is created or modified, which is not DRY and very tedious. 
 */

using System;

namespace PASS2
{

    public class Point
    {
        //These are the coordinates of the point
        private double x;
        private double y;
        private double z;

        //Auto-implemented properties for each of the coordinates
        public double X { get => x; set => SetX(value); }
        public double Y { get => y; set => SetY(value); }
        public double Z { get => z; set => SetZ(value); }


        //Pre: The parameters must be doubles within the bounds of the canvas.
        //Post: None.
        //Description: This constructor sets the private variables 'x' and 'y' to what the user inputted via the auto-implemented properties (doing it this way calls the set methods). It sets z to zero. 
        public Point(double x, double y)
        {
            X = x;
            Y = y;
            Z = 0;
        }

        //Pre: The parameters must be doubles within the bounds of the canvas.
        //Post: None.
        //Description: This constructor sets the private variables 'x', 'y' and 'z' to what the user inputted via the auto-implemented properties (doing it this way calls the set methods).
        public Point(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        //Pre: The double inputted  must be greater than zero andd smaller than the width of the canvas
        //Post: None.
        //Description: If the x coordinate is within the bounds of the canvas, this method updates the value of the private double 'x'. Otherwise, it throws an exception.
        private void SetX(double x)
        {
            if (x <= Canvas.SCREEN_WIDTH && x >= 0)
                this.x = x;
            else
            {
                throw new ArgumentOutOfRangeException("point x-coord", "The given x-coordinate for the point is outside the screen!");
            }
        }

        //Pre: The double inputted must be greater than zero andd smaller than the height of the canvas
        //Post: None.
        //Description: If the y coordinate is within the bounds of the canvas, this method updates the value of the private double 'y'. Otherwise, it throws an exception.
        private void SetY(double y)
        {
            if (y <= Canvas.SCREEN_HEIGHT && y >= 0)
                this.y = y;
            else
                throw new ArgumentOutOfRangeException("point y-coord", "The given y-coordinate for the point is outside the screen!");
        }

        private void SetZ(double z)
        {
            if (z <= Canvas.SCREEN_HEIGHT && z >= 0)
                this.z = z;
            else
                throw new ArgumentOutOfRangeException("point z-coord", "The given z-coordinate for the point is outside the screen!");
        }

        //Pre: The point inputted must be within the bounds of the canvas; the logic for ensuring that this is the case is computed outside this method.
        //Post: None.
        //Description: This method calculates the Euclidean between the point 'this' and the point inputted using Pythagorean theorem.
        public double GetDistance(Point point)
        {
            return Math.Sqrt((x - point.X) * (x - point.X) + (y - point.Y) * (y - point.Y) + (z - point.Z)*(z - point.Z));
        }
    }
}
