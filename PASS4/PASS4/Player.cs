//Author: Adar Kahiri
//File Name: Main.cs
//Project Name: PASS4
//Creation Date: Jan 7, 2021
//Modified Date: Jan 22, 2021
/* Description: This class stores the properties and functions of the player. It inherits the GameEntity class
 */
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace PASS4
{
    public class Player : GameEntity
    {

        enum CurrentMove
        {
            None,
            MoveRight,
            MoveLeft,
            JumpRight,
            JumpLeft,
            CollectItem,
            PushLeft,
            PushRight

        }
           
        //Physics constants
        public const float WALK_SPEED = 3f;
        public const float JUMP_SPEED = -18f;

        //Graphics variables
        private Texture2D spriteToUse;
        private Dictionary<string, Texture2D> additionalSprites;
        private int animationCounter = 0;

        //This is the number of frames there are for the animation spritesheets. It starts at 11 because that's how many frames there are in the 'Idle' sprite.
        private int numFrameIntervals = 11;
        private const int DIST_BTWN_FRAMES = 78;

        //Movement variables
        CurrentMove currentMove;
        int distanceTravelledX = 0;
        CharQueue currentControlSeq;


        public Player(Texture2D sprite, Rectangle destRec, Rectangle srcRec, Dictionary<string, Texture2D> additionalSprites) : base(sprite, destRec, srcRec)
        {
            this.additionalSprites = additionalSprites;

            //Initializing currentControlSeq
            currentControlSeq = new CharQueue();
        }

        public override void Update(List<Rectangle> terrain, GameEntity[] entities)
        {
            CollisionType entityCollision;

            //Gravity
            velocity.Y += GRAVITY;

            if(currentMove == CurrentMove.None && !currentControlSeq.IsEmpty())
            {
                switch(currentControlSeq.Dequeue())
                {
                    //Move right
                    case 'd':
                        currentMove = CurrentMove.MoveRight;
                        break;
                    //Move left
                    case 'a':
                        currentMove = CurrentMove.MoveLeft;
                        break;
                    //Collect item
                    case 'c':
                        currentMove = CurrentMove.CollectItem;
                        break;
                    //Jump right
                    case 'e':
                        currentMove = CurrentMove.JumpRight;
                        break;
                    //Jump left
                    case 'q':
                        currentMove = CurrentMove.JumpLeft;
                        break;
                    //Push right
                    case '+':
                        currentMove = CurrentMove.PushRight;
                        break;
                    //Push left
                    case '-':
                        currentMove = CurrentMove.PushLeft;
                        break;
                }
            }

            if(currentMove == CurrentMove.None && velocity.X != 0)
            {
                velocity.X = 0;
                velocity.Y = 0;
            }
            else if ((currentMove == CurrentMove.MoveRight || currentMove == CurrentMove.JumpRight || currentMove == CurrentMove.PushRight) && velocity.X != WALK_SPEED)
            {
                velocity.X = WALK_SPEED;
            }
            else if ((currentMove == CurrentMove.MoveLeft || currentMove == CurrentMove.JumpLeft || currentMove == CurrentMove.PushLeft) && velocity.X != -WALK_SPEED)
            {
                velocity.X = -WALK_SPEED;
            }

            if ((currentMove == CurrentMove.JumpLeft || currentMove == CurrentMove.JumpRight) && collideBottom == true)
            {
                velocity.Y = JUMP_SPEED;
            }


            //Collision detection (with other entities)
            for (int i = 0; i < entities.Length; i++)
            {
                if (entities[i] != this)
                {
                    entityCollision = GetCollisionType(entities[i].GetDestRec());
                    //Console.WriteLine(entityCollision);
                    if (entityCollision == CollisionType.BottomCollision)
                    {
                        pos.Y = entities[i].GetDestRec().Y - destRec.Height + 1;
                        if (velocity.Y > 0)
                        {
                            velocity.Y = 0;
                        }

                        break;
                    }
                    else if (entityCollision == CollisionType.LeftCollision || entityCollision == CollisionType.RightCollision)
                    {
                        HandleTerrainCollision(entities[i].GetDestRec());
                        break;
                    }
                }
            }

            //Collision detection (with terrain)
            terrainCollision = CollisionType.NoCollision;
            for (int i = 0; i < terrain.Count; i++)
            {
                HandleTerrainCollision(terrain[i]);
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
            distanceTravelledX += (int) pos.X;

            //Resetting currentMove and distanceTravelled so that they're ready for the next command in currentMoveSeq
            if((currentMove == CurrentMove.MoveRight || currentMove == CurrentMove.JumpRight) && distanceTravelledX >= Main.TILE_SIZE)
            {
                currentMove = CurrentMove.None;
                distanceTravelledX = 0;
            }
            else if ((currentMove == CurrentMove.MoveLeft || currentMove == CurrentMove.JumpLeft) && distanceTravelledX <= -Main.TILE_SIZE)
            {
                currentMove = CurrentMove.None;
                distanceTravelledX = 0;
            }
            else if (currentMove == CurrentMove.CollectItem)
            {
                currentMove = CurrentMove.None;
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
                //This overload of the draw method gives the option to display the flipped version of the sprite (flipped horizontally)
                spriteBatch.Draw(spriteToUse, destRec, srcRec, Color.White, 0.0f, new Vector2(0,0), SpriteEffects.FlipHorizontally, 0.0f);
            }
        }

        public void SetControlSeq(string controlSequence)
        {
            bool inLoop = false;
            int numLoopIter = -1;
            string loopedSequence = "";


            if(controlSequence.Length > 68)
            {
                throw new FormatException("The control sequence must not exceed 68 characters.");
            }
            while(controlSequence.Length > 0)
            {
                if ((controlSequence[0] == 'd' || controlSequence[0] == 'a' || controlSequence[0] == 'c' || controlSequence[0] == 'e'
                    || controlSequence[0] == 'q' || controlSequence[0] == '+' || controlSequence[0] == '-'))
                {
                    if (!inLoop)
                    {
                        currentControlSeq.Enqueue(controlSequence[0]);
                    }
                    else
                    {
                        loopedSequence += controlSequence[0];
                    }

                    controlSequence.Remove(0, 1);
                }
                else if (controlSequence[0] == 's')
                {
                    if(inLoop)
                    {
                        throw new FormatException("A loop cannot be inside another loop!");
                    }

                    inLoop = true;
                    controlSequence.Remove(0, 1);

                    try
                    {
                        numLoopIter = (int)Char.GetNumericValue(controlSequence[0]);
                        controlSequence.Remove(0, 1);
                    }
                    catch(Exception)
                    {
                        throw new FormatException("Loop iteration number must be a valid positive integer!");
                    }
                }
                else if(controlSequence[0] == 'f')
                {
                    inLoop = false;
                    for(int i = 0; i < numLoopIter; i++)
                    {
                        for(int j = 0; j < loopedSequence.Length; j++)
                        {
                            currentControlSeq.Enqueue(loopedSequence[j]);
                        }
                    }

                    loopedSequence = "";
                    controlSequence.Remove(0, 1);
                }
                else
                {
                    throw new FormatException("Invalid character. Refer to the instructions to view all valid characters.");
                }
            }

            if(inLoop)
            {
                throw new FormatException("All loops must be closed!");
            }

        }







    }
}
