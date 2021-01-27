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
