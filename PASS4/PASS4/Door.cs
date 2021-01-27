//Author: Adar Kahiri
//File Name: Main.cs
//Project Name: PASS4
//Creation Date: Jan 26, 2021
//Modified Date: Jan 27, 2021
/* Description: This is the door class. It handles all door-related logic, such as opening the door when the player touches it with a key, displaying it, etc.
 */
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PASS4
{
    public class Door
    {
        //Graphics variables
        private Texture2D sprite;
        private Rectangle destRec;
        private Rectangle srcRec;
        private Vector2 pos;

        private bool beenOpened = false;
        

        public Door(Texture2D sprite, Rectangle destRec, Rectangle srcRec)
        {
            this.sprite = sprite;
            this.destRec = destRec;
            this.srcRec = srcRec;

            pos.X = destRec.X;
            pos.Y = destRec.Y;
        }

        public void Update(Player player, ref int numKeysCollected)
        {
            if (!beenOpened)
            {
                if (destRec.Intersects(player.GetDestRec()) && numKeysCollected > 0)
                {
                    beenOpened = true;
                    numKeysCollected--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(!beenOpened)
            {
                spriteBatch.Draw(sprite, destRec, Color.White);
            }
            
        }

        public bool GetBeenOpened()
        {
            return beenOpened;
        }

        public Rectangle GetDestRec()
        {
            return destRec;
        }
    }
}
