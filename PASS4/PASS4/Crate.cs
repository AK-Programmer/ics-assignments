﻿//Author: Adar Kahiri
//File Name: Main.cs
//Project Name: PASS4
//Creation Date: Jan 6, 2021
//Modified Date: Jan 27, 2021
/* Description: This is the crate class. It handles all crate-related logic such as collisions with terrain and the player, gravity, etc.
 */
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace PASS4
{
    public class Crate : GameEntity
    {

        CollisionType entityCollision;

        public Crate(Texture2D sprite, Rectangle destRec, Rectangle srcRec) : base(sprite, destRec, srcRec)
        {
            
            collideTopTerrain = false;
            collideBottomTerrain = false;
            collideLeftTerrain = false;
            collideRightTerrain = false;
        }

        public override void Update(List<Rectangle> terrain, GameEntity [] entities, Door[] doors)
        {
            collideLeft = false;
            collideRight = false;
            collideTop = false;
            collideBottom = false;
            collideTopTerrain = false;
            collideBottomTerrain = false;
            collideLeftTerrain = false;
            collideRightTerrain = false;

            //Gravity
            velocity.Y += GRAVITY;

            //By default horizontal velocity should be zero. It will be set to something else if a collision with a player/crate is detected
            velocity.X = 0;

            
            //Collision detection (other entities)
            if (Main.isPlayerPushingCrate)
            {
                for(int i = 0; i < entities.Length; i++)
                {
                    if(entities[i] != this)
                    {
                        entityCollision = GetCollisionType(entities[i].GetDestRec());

                        if (entityCollision == CollisionType.BottomCollision)
                        {
                            pos.Y = entities[i].GetDestRec().Y - destRec.Height + 1;
                            if (velocity.Y > 0)
                            {
                                velocity.Y = 0;
                                if (entities[i].GetVelocity().X != 0)
                                {
                                    velocity.X = entities[i].GetVelocity().X;
                                }
                            }
                        }

                        if (((entityCollision == CollisionType.LeftCollision && !collideRightTerrain) || (entityCollision == CollisionType.RightCollision && !collideLeftTerrain)) && velocity.X == 0)
                        {
                            velocity.X = entities[i].GetVelocity().X;
                        }

                        if (collideTop)
                        {
                            velocity.X = 0;
                        }
                    }
                    
                }

            }

            //Collision detection (terrain)
            for (int i = 0; i < terrain.Count; i++)
            {
                HandleTerrainCollision(terrain[i]);
            }

            //Updating crate's position based on its velocity
            pos.Y += velocity.Y;
            pos.X += velocity.X;
            destRec.X = (int)pos.X;
            destRec.Y = (int)pos.Y;


        }
    }
}
