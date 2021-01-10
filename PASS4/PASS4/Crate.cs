using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PASS4
{
    public class Crate : GameEntity
    {
        public Crate(Texture2D sprite, Rectangle destRec, Rectangle srcRec) : base(sprite, destRec, srcRec)
        {
        }

        public override void Update(Rectangle[] terrain, GameEntity [] entities)
        {
            CollisionType entityCollision;

            base.Update(terrain, entities);

            //Collision detection (other entities)
            for (int i = 0; i < entities.Length; i++)
            {
                if (entities[i] != this)
                {
                    entityCollision = GetCollisionType(entities[i].GetDestRec());

                    if (entityCollision == CollisionType.BottomCollision)
                    {
                        pos.Y = entities[i].GetDestRec().Y - entities[i].GetDestRec().Height + 1;
                        if (velocity.Y > 0)
                        {
                            velocity.Y = 0;
                            if(entities[i].GetVelocity().X != 0)
                            {
                                velocity.X = entities[i].GetVelocity().X;
                            }
                            break;

                        }
                    }
                    else if ((entityCollision == CollisionType.LeftCollision && entities[i].GetVelocity().X > 0)|| (entityCollision == CollisionType.RightCollision && entities[i].GetVelocity().X < 0))
                    {
                        velocity.X = entities[i].GetVelocity().X;
                        break;
                    }
                    else
                    {
                        velocity.X = 0;
                    }

                }
            }

            //Updating crate's position based on its velocity
            pos.Y += velocity.Y;
            pos.X += velocity.X;
            destRec.X = (int)pos.X;
            destRec.Y = (int)pos.Y;
        }
    }
}
