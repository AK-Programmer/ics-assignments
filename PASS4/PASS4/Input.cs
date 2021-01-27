using System;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace PASS4
{
    public static class Input
    {

        private static KeyboardState prevKeyboardState = new KeyboardState();
        private static KeyboardState currentKeyboardState;
        private  static bool beenPressed;
        public static bool HasBeenPressed(Keys key, bool prevToCurrent)
        {
            

            if(key == Keys.Execute)
            {
                key = Keys.OemPlus;
            }
            else if (key == Keys.Insert)
            {
                key = Keys.OemMinus;
            }
            //Console.Clear();

            currentKeyboardState = Keyboard.GetState();

            beenPressed = prevKeyboardState.IsKeyDown(key) && !currentKeyboardState.IsKeyDown(key);

            if(prevToCurrent)
            {
                prevKeyboardState = currentKeyboardState;
            }
            

            return beenPressed;
        }

    }
}
