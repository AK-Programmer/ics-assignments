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


        public Gem(Texture2D sprite, Rectangle destRec, Rectangle srcRec)
        {
            this.sprite = sprite;
            this.destRec = destRec;
            this.srcRec = srcRec;

            pos.X = destRec.X;
            pos.Y = destRec.Y;
        }

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
