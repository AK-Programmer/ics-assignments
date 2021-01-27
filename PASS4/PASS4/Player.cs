//Author: Adar Kahiri
//File Name: Main.cs
//Project Name: PASS4
//Creation Date: Jan 7, 2021
//Modified Date: Jan 27, 2021
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
        public const float WALK_SPEED = 2f;
        public const float JUMP_SPEED = -10f;

        //Graphics variables
        private Texture2D spriteToUse;
        private Dictionary<string, Texture2D> additionalSprites;
        private int animationCounter = 0;

        //This is the number of frames there are for the animation spritesheets. It starts at 11 because that's how many frames there are in the 'Idle' sprite.
        private int numFrameIntervals = 11;
        private const int DIST_BTWN_FRAMES = 78;

        //Movement variables
        CurrentMove currentMove;
        int targetPosX;
        CharQueue currentControlSeq;
        CollisionType entityCollision;
        bool anyMovement = false;
        bool alreadyJumped = false;


        public Player(Texture2D sprite, Rectangle destRec, Rectangle srcRec, Dictionary<string, Texture2D> additionalSprites) : base(sprite, destRec, srcRec)
        {
            this.additionalSprites = additionalSprites;

            //Initializing currentControlSeq
            currentControlSeq = new CharQueue();
            targetPosX = destRec.X;
        }

        public override void Update(List<Rectangle> terrain, GameEntity[] entities, Door[] doors)
        {
            collideBottom = false;
            collideTop = false;
            collideLeft = false;
            collideRight = false;

            anyMovement = false;

            //Gravity
            velocity.Y += GRAVITY;

            //Collision handling (with other entities)
            for (int i = 0; i < entities.Length; i++)
            {
                if (entities[i] != this)
                {
                    entityCollision = GetCollisionType(entities[i].GetDestRec());

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

            
            //Collision handling (with terrain)
            terrainCollision = CollisionType.NoCollision;
            for (int i = 0; i < terrain.Count; i++)
            {
                HandleTerrainCollision(terrain[i]);
            }


            //Collision handling with doors
            for (int i = 0; i < doors.Length; i++)
            {
                if (destRec.Intersects(doors[i].GetDestRec()) && !doors[i].GetBeenOpened())
                {
                    HandleTerrainCollision(doors[i].GetDestRec());
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

            //Checking if any entities are currently moving
            if (velocity.X != 0 || velocity.Y != 0)
            {
                anyMovement = true;
            }

            for(int i = 0; i < entities.Length; i++)
            {
                if (entities[i].GetVelocity().X !=0 || entities[i].GetVelocity().Y != 0)
                {
                    anyMovement = true;
                }
            }


            //If there is no movement happening and  the control sequence isn't empty, call the next command in the queue.
            if(currentMove == CurrentMove.None && !anyMovement && !currentControlSeq.IsEmpty())
            {
                switch(currentControlSeq.Dequeue())
                {
                    //Move right
                    case 'd':
                        //Setting target position
                        targetPosX = destRec.X + Main.TILE_SIZE;
                        currentMove = CurrentMove.MoveRight;
                        break;
                    //Move left
                    case 'a':
                        //Setting target position
                        targetPosX = destRec.X - Main.TILE_SIZE;
                        currentMove = CurrentMove.MoveLeft;
                        break;
                    //Collect item
                    case 'c':
                        currentMove = CurrentMove.CollectItem;
                        break;
                    //Jump right
                    case 'e':
                        targetPosX = destRec.X + Main.TILE_SIZE;
                        alreadyJumped = false;
                        currentMove = CurrentMove.JumpRight;
                        break;
                    //Jump left
                    case 'q':
                        targetPosX = destRec.X - Main.TILE_SIZE;
                        alreadyJumped = false;
                        currentMove = CurrentMove.JumpLeft;
                        break;
                    //Push right
                    case '+':
                        targetPosX = destRec.X + Main.TILE_SIZE;
                        currentMove = CurrentMove.PushRight;
                        Main.isPlayerPushingCrate = true;
                        break;
                    //Push left
                    case '-':
                        targetPosX = destRec.X - Main.TILE_SIZE;
                        currentMove = CurrentMove.PushLeft;
                        Main.isPlayerPushingCrate = true;
                        break;
                    //Default is no movement
                    default:
                        currentMove = CurrentMove.None;
                        break;
                }
            }

            //If there is no movement, then the horizontal velocity should be zero
            if(currentMove == CurrentMove.None)
            {
                velocity.X = 0;
            }
            //If the player is moving to the right (moving, pushing, jumping) set horizontal velocity to WALK_SPEED
            else if ((currentMove == CurrentMove.MoveRight || currentMove == CurrentMove.JumpRight || currentMove == CurrentMove.PushRight) && velocity.X != WALK_SPEED)
            {
                velocity.X = WALK_SPEED;
            }
            //If the player is moving to the left (moving, pushing, jumping) set horizontal velocity to -WALK_SPEED
            else if ((currentMove == CurrentMove.MoveLeft || currentMove == CurrentMove.JumpLeft || currentMove == CurrentMove.PushLeft) && velocity.X != -WALK_SPEED)
            {
                velocity.X = -WALK_SPEED;
            }

            if ((currentMove == CurrentMove.JumpLeft || currentMove == CurrentMove.JumpRight) && !alreadyJumped)
            {
                alreadyJumped = true;
                velocity.Y = JUMP_SPEED;
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

            //Resetting currentMove and distanceTravelled so that they're ready for the next command in currentMoveSeq
            if (velocity.X == 0 && targetPosX == destRec.X + Main.TILE_SIZE)
            {
                currentMove = CurrentMove.None;
                Main.isPlayerPushingCrate = false;
                targetPosX = destRec.X;
            }

            if((currentMove == CurrentMove.MoveRight || currentMove == CurrentMove.JumpRight || currentMove == CurrentMove.PushRight) && destRec.X == targetPosX)
            {
                currentMove = CurrentMove.None;
                Main.isPlayerPushingCrate = false;
            }
            else if ((currentMove == CurrentMove.MoveLeft || currentMove == CurrentMove.JumpLeft || currentMove == CurrentMove.PushLeft) && destRec.X == targetPosX)
            {
                currentMove = CurrentMove.None;
                Main.isPlayerPushingCrate = false;
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

        //Pre: a string containing the movement commands
        //Post: none
        //Description: this method parses the control sequence the user entered, and adds the expanded sequence into the currentMoveSeq queue.
        public void SetControlSeq(string controlSequence)
        {
            bool inLoop = false;
            int numLoopIter = -1;
            string loopedSequence = "";
            int whileCount = 0;

            if(controlSequence.Length > 68)
            {
                throw new FormatException("The control sequence must not exceed 68 characters.");
            }

            while(controlSequence.Length > 0)
            {
                whileCount++;
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

                    controlSequence = controlSequence.Remove(0, 1);
                }
                else if (controlSequence[0] == 's')
                {
                    if(inLoop)
                    {
                        throw new FormatException($"A loop cannot be inside another loop!");
                    }

                    inLoop = true;
                    controlSequence = controlSequence.Remove(0, 1);

                    try
                    {
                        numLoopIter = (int)Char.GetNumericValue(controlSequence[0]);
                        controlSequence = controlSequence.Remove(0, 1);
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
                    controlSequence = controlSequence.Remove(0, 1);
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
