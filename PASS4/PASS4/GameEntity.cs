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
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace PASS4
{
    public class GameEntity
    {
        public enum CollisionType
        {
            SideCollision,
            BottomCollision,
            TopCollision,
            NoCollision = -1
        };

        protected const float GRAVITY = 0.8f;
        protected Texture2D sprite;
        protected Rectangle destRec;
        protected Rectangle srcRec;
        Vector2 pos;
        public Vector2 speed = new Vector2(0, 0);
        private string assetDirPath;
        public bool standingOnGround = true;
        protected bool facingRight = true;
        private CollisionType collisionType;

        public GameEntity(Texture2D sprite, Rectangle destRec, Rectangle srcRec)
        {
            this.sprite = sprite;
            this.destRec = destRec;
            this.srcRec = srcRec;

            pos.X = destRec.X;
            pos.Y = destRec.Y;
            //Loading graphics data
        }


        public virtual void Update(params Rectangle[] terrain)
        {
            if(speed.X < 0)
            {
                facingRight = false;
            }
            else if (speed.X > 0)
            {
                facingRight = true;
            }

            if (speed.Y != 0 || !standingOnGround)
            {
                speed.Y += GRAVITY;
            }


            //Collision detection here
            for(int i = 0; i < terrain.Length; i++)
            {
                collisionType = Main.CheckCollision(destRec, terrain[i]);

                if (collisionType == CollisionType.BottomCollision)
                {
                    speed.Y = 0;
                    pos.Y--;
                    standingOnGround = true;
                }
            }
            

            //Updating entity's position (updating its position vector, then setting the destination rectangle's location to that vector (casted as int)
            pos.X += speed.X;
            destRec.X = (int) pos.X;
            pos.Y += speed.Y;
            destRec.Y = (int) pos.Y;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, destRec, srcRec, Color.White);
        }


    }
}
