using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace PASS4
{
    public class Player : GameEntity
    {

        //Physics variables
        public const float WALK_SPEED = 3f;
        public const float JUMP_SPEED = -18f;

        //Graphics variables
        private Texture2D spriteToUse;
        private Dictionary<string, Texture2D> additionalSprites;
        private int animationCounter = 0;

        //This is the number of frames there are for the animation spritesheets. It starts at 11 because that's how many frames there are in the 'Idle' sprite.
        private int numFrameIntervals = 11;
        private const int DIST_BTWN_FRAMES = 78;

        public Player(Texture2D sprite, Rectangle destRec, Rectangle srcRec, Dictionary<string, Texture2D> additionalSprites) : base(sprite, destRec, srcRec)
        {
            this.additionalSprites = additionalSprites;
        }

        public override void Update(Rectangle[] terrain, GameEntity [] entities)
        {
            CollisionType entityCollision;

            base.Update(terrain, entities);

            //Collision detection (with other entities)
            for (int i = 0; i < entities.Length; i++)
            {
                if (entities[i] != this)
                {
                    entityCollision = GetCollisionType(entities[i].GetDestRec());
                    //Console.WriteLine(entityCollision);
                    if (entityCollision == CollisionType.BottomCollision)
                    {
                        pos.Y = entities[i].GetDestRec().Y - entities[i].GetDestRec().Height + 1;
                        if (velocity.Y > 0)
                        {
                            velocity.Y = 0;
                        }
                    }

                }
            }

            if (velocity.X < 0)
            {
                facingRight = false;
            }
            else if (velocity.X > 0)
            {
                facingRight = true;
            }

            if (velocity.Y < 0)
            {
                spriteToUse = additionalSprites["jump"];
            }
            else if (velocity.Y > 0)
            {
                spriteToUse = additionalSprites["fall"];
            }
            else if (velocity.X != 0)
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
                    srcRec.X += DIST_BTWN_FRAMES;
                    
                    srcRec.X %= DIST_BTWN_FRAMES * numFrameIntervals;
                }

                animationCounter++;
                animationCounter %= 7;
            }
            else
            {
                srcRec.X = 9;
            }

            //Updating player position based on its velocity
            pos.Y += velocity.Y;
            pos.X += velocity.X;
            destRec.X = (int)pos.X;
            destRec.Y = (int)pos.Y;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(facingRight)
            {
                spriteBatch.Draw(spriteToUse, destRec, srcRec, Color.White);
            }
            else
            {
                //This overload of the draw method gives the option to display the flipped version of the sprite (flipped horizontally)
                spriteBatch.Draw(spriteToUse, destRec, srcRec, Color.White, 0.0f, new Vector2(0,0), SpriteEffects.FlipHorizontally, 0.0f);
            }

        }

    }
}
