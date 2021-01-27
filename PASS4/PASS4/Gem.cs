//Author: Adar Kahiri
//File Name: Gem.cs
//Project Name: PASS4
//Creation Date: Jan 8, 2021
//Modified Date: Jan 27, 2021
/* Description: This class stores the properties and functions of the gem. It inherits the GameEntity class.
*/
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PASS4
{
    public class Gem
    {
        //Graphics variables
        private Texture2D sprite;
        private Rectangle destRec;
        private Rectangle srcRec;
        private Vector2 pos;

        //Animation variables
        private int animationCounter = 0;
        private const int DIST_BTWN_FRAMES = 18;
        private const int NUM_FRAMES = 10;
        private float verticalVelocity = -0.3f;
        private const float floatingSpeed = 0.3f;
        private float floatingResistence = 0.05f;


        private bool beenCollected = false;

        //Pre: the sprite, destination rectangle, and source rectangle to be used for gems
        //Post: none
        //Description: This method initializes the graphics variables to the given arguments
        public Gem(Texture2D sprite, Rectangle destRec, Rectangle srcRec)
        {
            this.sprite = sprite;
            this.destRec = destRec;
            this.srcRec = srcRec;

            pos.X = destRec.X;
            pos.Y = destRec.Y;
        }

        //Pre: 
        public void Update(Player player, ref int numGemsCollected)
        {
            if(!beenCollected)
            {
                if (destRec.Intersects(player.GetDestRec()))
                {
                    beenCollected = true;
                    numGemsCollected++;
                }
                if (animationCounter == 0)
                {
                    srcRec.X += DIST_BTWN_FRAMES;

                    srcRec.X %= DIST_BTWN_FRAMES * NUM_FRAMES;
                }

                animationCounter++;
                animationCounter %= 7;

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
            if(!beenCollected)
            {
                spriteBatch.Draw(sprite, destRec, srcRec, Color.White);
            }
            
        }
    }
}
