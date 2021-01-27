//Author: Adar Kahiri
//File Name: Main.cs
//Project Name: PASS4
//Creation Date: Jan 6, 2021
//Modified Date: Jan 27, 2021
/* Description: This is the crate class. It handles all crate-related logic such as collisions with terrain and the player, gravity, etc.
 */
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PASS4
{
    public static class Helper
    {
        public static GraphicsDeviceManager graphics;

        public static Texture2D GetColouredRec(Rectangle rectangle, Color color)
        {
            Texture2D recTexture = new Texture2D(graphics.GraphicsDevice, rectangle.Width, rectangle.Height);

            Color[] data = new Color[rectangle.Width * rectangle.Height];

            for(int i = 0; i < data.Length; i++)
            {
                data[i] = color;
            }

            recTexture.SetData(data);

            return recTexture;
        }
    }
}
