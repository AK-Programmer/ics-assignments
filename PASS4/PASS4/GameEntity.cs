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

        private const float GRAVITY = 0.8f;
        private Texture2D sprite;
        private Rectangle destRec;
        private Rectangle srcRec;
        Vector2 pos;
        public Vector2 speed = new Vector2(0, 0);
        private string assetDirPath;
        public bool standingOnGround = true;

        public GameEntity(Texture2D sprite, Rectangle destRec, Rectangle srcRec)
        {
            this.sprite = sprite;
            this.destRec = destRec;
            this.srcRec = srcRec;
            this.assetDirPath = assetDirPath;

            pos.X = destRec.X;
            pos.Y = destRec.Y;
            //Loading graphics data
        }


        public void Update()
        {
            if (speed.Y != 0 || !standingOnGround)
            {
                speed.Y += GRAVITY;
            }


            //Collision detection here
            if (pos.Y + destRec.Height >= 420)
            {
                speed.Y = 0;
                pos.Y--;
                standingOnGround = true;
            }

            //Updating entity's position (updating its position vector, then setting the destination rectangle's location to that vector (casted as int)
            pos.X += speed.X;
            destRec.X = (int) pos.X;
            pos.Y += speed.Y;
            destRec.Y = (int) pos.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, destRec, srcRec, Color.White);
        }


    }
}
