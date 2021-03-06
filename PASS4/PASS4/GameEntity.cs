﻿//Author: Adar Kahiri
//File Name: GameEntity.cs
//Project Name: PASS4
//Creation Date: Jan 6, 2021
//Modified Date: Jan 22, 2021
/* Description: This class stores the basic properties of entities in the game (player and crates). 
 * This includes sprite, rectangles, position, speed, and more.
 */

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


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

        //Collision trackers
        protected bool collideLeft, collideRight, collideTop, collideBottom;
        protected bool collideTopTerrain, collideBottomTerrain, collideLeftTerrain, collideRightTerrain;

        public GameEntity(Texture2D sprite, Rectangle destRec, Rectangle srcRec)
        {
            this.sprite = sprite;
            this.destRec = destRec;
            this.srcRec = srcRec;

            pos.X = destRec.X;
            pos.Y = destRec.Y;

        }

        public abstract void Update(List<Rectangle> terrain, GameEntity[] entities, Door[] doors, Spike[] spikes);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, destRec, srcRec, Color.White);
        }

        protected CollisionType GetCollisionType(Rectangle otherDestRec)
        {
            if (destRec.Intersects(otherDestRec))
            {
                //This variable stores the minimum distance destRec would need to move along the y-axis to un-intersect its bottom side
                int minDist = Math.Min(Math.Abs(destRec.Y + destRec.Height - otherDestRec.Y), Math.Min(Math.Abs(destRec.X + destRec.Width - otherDestRec.X), Math.Min(Math.Abs(otherDestRec.X + otherDestRec.Width - destRec.X), Math.Abs(otherDestRec.Y + otherDestRec.Height - destRec.Y))));

                //Checking right collision
                if (Math.Abs(destRec.X + destRec.Width - otherDestRec.X) == minDist)
                {
                    collideRight = true;
                    return CollisionType.RightCollision;
                }
                //Checking left collision
                else if (Math.Abs(otherDestRec.X + otherDestRec.Width - destRec.X) == minDist)
                {
                    collideLeft = true;
                    return CollisionType.LeftCollision;
                }
                //Checking top collision
                if (Math.Abs(otherDestRec.Y + otherDestRec.Height - destRec.Y) == minDist && destRec.Y + destRec.Height - otherDestRec.Y - destRec.Height > 3)
                {
                    collideTop = true;
                    return CollisionType.TopCollision;
                }
                
                //Otherwise, the bottom of destRec is closest to destRec, so un-intersect the bottom
                else
                {
                    collideBottom = true;
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
                collideTopTerrain = true;
                pos.Y = terrainDestRec.Y + terrainDestRec.Height + 1;
                //'Bounce back' velocity
                velocity.Y = 0.1f;
            }
            else if (terrainCollision == CollisionType.BottomCollision)
            {
                collideBottomTerrain = true;
                pos.Y = terrainDestRec.Y - destRec.Height + 1;
                if (velocity.Y > 0)
                {
                    velocity.Y = 0;
                }
            }
            else if(terrainCollision == CollisionType.RightCollision)
            {
                collideRightTerrain = true;
                pos.X = terrainDestRec.X - destRec.Width;
                velocity.X = 0;
            }
            else if(terrainCollision == CollisionType.LeftCollision)
            {
                collideLeftTerrain = true;
                pos.X = terrainDestRec.X + terrainDestRec.Width;
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
