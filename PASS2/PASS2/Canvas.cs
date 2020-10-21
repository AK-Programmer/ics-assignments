using System;
using System.Collections.Generic;


namespace PASS2
{
    public class Canvas
    {
        private const int MAX_NUM_SHAPES = 6;
        public const int SCREEN_WIDTH = 90;
        public const int SCREEN_HEIGHT = 30;


        List<Shape> shapes;

        public Canvas()
        {

        }



        public void ViewShapeList()
        {

        }

        public void AddShape()
        {

        }

        public void ViewShape()
        {

        }

        public void DeleteShape()
        {

        }


        public void ModifyShape()
        {

        }


        public void ClearCanvas()
        {
            shapes.Clear();
            Console.Clear();

            Console.WriteLine("The canvas has been cleared. (Press any key to continue).");
            Console.ReadKey();

        }
    }
}
