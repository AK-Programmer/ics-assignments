using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace PASS4
{
    public class Player : GameEntity
    {

        public const float walkSpeed = 3f;
        public const float jumpSpeed = -15;
        private Texture2D spriteToUse;
        private Dictionary<string, Texture2D> additionalSprites;
        private int animationCounter = 0;
        private int numFrameIntervals = 11;

        public Player(Texture2D sprite, Rectangle destRec, Rectangle srcRec, Dictionary<string, Texture2D> additionalSprites) : base(sprite, destRec, srcRec)
        {
            this.additionalSprites = additionalSprites;
        }

        public override void Update(params Rectangle[] terrain)
        {
            base.Update();

            if (speed.Y != 0)
            {
                spriteToUse = additionalSprites["jump"];
            }
            else if (speed.X != 0)
            {
                spriteToUse = additionalSprites["run"];
                numFrameIntervals = 8;
            }
            else
            {
                spriteToUse = sprite;
                numFrameIntervals = 11;
            }

            //Updating player's animation
            if (spriteToUse == sprite || spriteToUse == additionalSprites["run"])
            {
                if (animationCounter == 0)
                {
                    srcRec.X += 78;
                    
                    srcRec.X %= 78*numFrameIntervals;
                }

                animationCounter++;
                animationCounter %= 7;
            }
            else
            {
                srcRec.X = 9;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(facingRight)
            {
                spriteBatch.Draw(spriteToUse, destRec, srcRec, Color.White);
            }
            else
            {
                spriteBatch.Draw(spriteToUse, destRec, srcRec, Color.White, 0.0f, new Vector2(0,0), SpriteEffects.FlipHorizontally, 0.0f);
            }

        }

    }
}
