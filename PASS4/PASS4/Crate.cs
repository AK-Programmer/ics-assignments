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
        }

        public override void Update(List<Rectangle> terrain, GameEntity [] entities)
        {

            collideLeft = false;
            collideRight = false;
            collideTop = false;
            collideBottom = false;

            //Gravity
            velocity.Y += GRAVITY;
            //Collision detection (other entities)
            for (int i = 0; i < entities.Length; i++)
            {
                if (entities[i] != this)
                {
                    entityCollision = GetCollisionType(entities[i].GetDestRec());

                    if (entityCollision == CollisionType.BottomCollision)
                    {
                        collideBottom = true;

                        pos.Y = entities[i].GetDestRec().Y - destRec.Height + 1;
                        if (velocity.Y > 0)
                        {
                            velocity.Y = 0;
                            if(entities[i].GetVelocity().X != 0)
                            {
                                velocity.X = entities[i].GetVelocity().X;
                            }
                        }
                    }
                    else if (entityCollision == CollisionType.LeftCollision || entityCollision == CollisionType.RightCollision)
                    {
                        velocity.X = entities[i].GetVelocity().X;
                        HandleTerrainCollision(entities[i].GetDestRec());
                    }
                    else if (entityCollision == CollisionType.TopCollision)
                    {
                        collideTop = true;
                        velocity.X = 0;
                    }
                    else
                    {
                        velocity.X = 0;
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
