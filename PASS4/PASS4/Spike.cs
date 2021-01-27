//Author: Adar Kahiri
//File Name: Spike.cs
//Project Name: PASS4
//Creation Date: Jan 27, 2021
//Modified Date: Jan 27, 2021
/* Description: This is the spike class. It will store the position and Texture of each spike.
 */
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PASS4
{
    public class Spike
    {
        //Graphics variables
        private Texture2D sprite;
        Rectangle srcRec;
        Rectangle destRec;

        public Spike(Texture2D sprite, Rectangle destRec, Rectangle srcRec)
        {
            this.sprite = sprite;
            this.srcRec = srcRec;
            this.destRec = destRec;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, destRec, srcRec, Color.White);
        }

        public Rectangle GetDestRec()
        {
            return destRec;
        }
    }
}
