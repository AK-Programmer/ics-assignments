//Author: Adar Kahiri
//File Name: Main.cs
//Project Name: PASS4
//Creation Date: Jan 26, 2021
//Modified Date: Jan 27, 2021
/* Description: This is the key class. It handles all key-related logic, such as incrementing the numKeysCollected variable, toggling its visibility when its collected, displaying, etc.
 */
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PASS4
{
    public class Key
    {
        //Graphics variables
        private Texture2D sprite;
        private Rectangle destRec;
        private Rectangle srcRec;
        private Vector2 pos;

        //Animation variables
        private float verticalVelocity = -0.3f;
        private const float floatingSpeed = 0.3f;
        private float floatingResistence = 0.05f;

        private bool beenCollected = false;

        public Key(Texture2D sprite, Rectangle destRec, Rectangle srcRec)
        {
            this.sprite = sprite;
            this.destRec = destRec;
            this.srcRec = srcRec;

            pos.X = destRec.X;
            pos.Y = destRec.Y;
        }

        public void Update(Player player, ref int numKeysCollected)
        {
            if (!beenCollected)
            {
                if (destRec.Intersects(player.GetDestRec()))
                {
                    beenCollected = true;
                    numKeysCollected++;
                }

                if (Math.Abs(verticalVelocity) > floatingSpeed)
                {
                    floatingResistence *= -1;
                }

                verticalVelocity += floatingResistence;
                pos.Y += verticalVelocity;
                
                destRec.Y = (int)pos.Y;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!beenCollected)
            {
                spriteBatch.Draw(sprite, destRec, srcRec, Color.White);
            }

        }
    }
}
