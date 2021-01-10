//Author: Adar Kahiri
//File Name: Main.cs
//Project Name: PASS4
//Creation Date: Jan 6, 2021
//Modified Date: Jan 22, 2021
/* Description: This class stores the basic properties of entities in the game (player and crates). 
 * This includes sprite, rectangles, position, speed, and more.
 */

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace PASS4
{
    public abstract class GameEntity
    {
        public enum CollisionType
        {
            BottomCollision,
            TopCollision,
            LeftCollision,
            RightCollision,
            NoCollision = -1
        };

        
        //Graphics-related variables
        protected Texture2D sprite;
        protected Rectangle destRec;
        protected Rectangle srcRec;
        protected Vector2 pos;
        protected bool facingRight = true;

        //Physics-related variables
        protected const float GRAVITY = 0.8f;
        public Vector2 velocity = new Vector2(0, 0);
        public CollisionType terrainCollision;

        public GameEntity(Texture2D sprite, Rectangle destRec, Rectangle srcRec)
        {
            this.sprite = sprite;
            this.destRec = destRec;
            this.srcRec = srcRec;

            pos.X = destRec.X;
            pos.Y = destRec.Y;

        }


        public virtual void Update(Rectangle[] terrain, GameEntity[] entities)
        {
            //Gravity
            velocity.Y += GRAVITY;


            //Collision detection (terrain)
            terrainCollision = CollisionType.NoCollision;

            for (int i = 0; i < terrain.Length; i++)
            {
                HandleTerrainCollision(terrain[i]);
            }

        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, destRec, srcRec, Color.White);
        }

        protected CollisionType GetCollisionType(Rectangle otherDestRec)
        {
            if (destRec.Intersects(otherDestRec))
            {
                //This variable stores the minimum distance destRec would need to move along the y-axis to un-intersect its bottom side
                int minDist = Math.Abs(destRec.Y + destRec.Height - otherDestRec.Y);

                //Checking top collision
                if (Math.Abs(otherDestRec.Y + otherDestRec.Height - destRec.Y) < minDist && destRec.Y + destRec.Height - otherDestRec.Y - destRec.Height > 4)
                {
                    return CollisionType.TopCollision;
                }
                //Checking right collision
                else if (Math.Abs(destRec.X + destRec.Width - otherDestRec.X) < minDist)
                {
                    return CollisionType.RightCollision;
                }
                //Checking left collision
                else if (Math.Abs(otherDestRec.X + otherDestRec.Width - destRec.X) < minDist)
                {
                    return CollisionType.LeftCollision;
                }
                //Otherwise, the bottom of destRec is closest to destRec, so un-intersect the bottom
                else
                {
                    return CollisionType.BottomCollision;
                }
            }
            return CollisionType.NoCollision;
        }

        protected void HandleTerrainCollision(Rectangle terrainDestRec)
        {
            terrainCollision = GetCollisionType(terrainDestRec);

            if (terrainCollision == CollisionType.TopCollision)
            {
                pos.Y = terrainDestRec.Y + terrainDestRec.Height + 1;
                velocity.Y = 0.1f;
            }
            else if (terrainCollision == CollisionType.BottomCollision)
            {
                pos.Y = terrainDestRec.Y - destRec.Height + 1;
                if (velocity.Y > 0)
                {
                    velocity.Y = 0;
                }
            }
            else if(terrainCollision == CollisionType.RightCollision)
            {
                pos.X = terrainDestRec.X - destRec.Width - 1;
                velocity.X = 0;
            }
            else if(terrainCollision == CollisionType.LeftCollision)
            {
                pos.X = terrainDestRec.X + terrainDestRec.Width + 1;
                velocity.X = 0;
            }
        }

        public Rectangle GetDestRec()
        {
            return destRec;
        }

        public Vector2 GetVelocity()
        {
            return velocity;
        }




    }
}
