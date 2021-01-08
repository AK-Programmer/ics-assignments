//Author: Adar Kahiri
//File Name: Main.cs
//Project Name: PASS4
//Creation Date: Jan 6, 2021
//Modified Date: Jan 22, 2021
/* Description:
 */
//Assets used: https://pixelfrog-store.itch.io/kings-and-pigs

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace PASS4
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //Constants
        const int SCREEN_W = 800;
        const int SCREEN_H = 560;

        //player sprites
        private Texture2D idleSprite;
        private Texture2D jumpSprite;
        private Texture2D runSprite;

        //Other sprites
        private Texture2D crateSprite;
        private Texture2D terrainSprite;

        //Asset paths
        private const string terrainSpritePath = "Images/Sprites/Level Assets/Terrain (32x32)";
        private const string idleSpritePath = "Images/Sprites/Player/Idle";
        private const string jumpSpritePath = "Images/Sprites/Player/Jump (78x58)";
        private const string runSpritePath = "Images/Sprites/Player/Run (78x58)";


        private Dictionary<string, Rectangle> terrainCoords = new Dictionary<string, Rectangle>();
        private Dictionary<string, Texture2D> playerSprites = new Dictionary<string, Texture2D>();

        //Game Entities
        private Player player;
        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            this._graphics.PreferredBackBufferWidth = SCREEN_W;
            this._graphics.PreferredBackBufferHeight = SCREEN_H;
            _graphics.PreferMultiSampling = false;
            this._graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            terrainSprite = Content.Load<Texture2D>(terrainSpritePath);
            idleSprite = Content.Load<Texture2D>(idleSpritePath);
            jumpSprite = Content.Load<Texture2D>(jumpSpritePath);
            runSprite = Content.Load<Texture2D>(runSpritePath);

            //Adding various player sprites to playerSprite dictionary
            playerSprites.Add("jump", jumpSprite);
            playerSprites.Add("run", runSprite);

            //Adding locations of all images in terrain sprite to terrain dictionary
            terrainCoords.Add("floor", new Rectangle(41, 32, 77, 24));

            player = new Player(idleSprite, new Rectangle(50, 368, 74, 52), new Rectangle(9, 17, 37, 26), playerSprites);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.W) && player.standingOnGround)
            {
                player.speed.Y = Player.jumpSpeed;
                player.standingOnGround = false;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                player.speed.X = -Player.walkSpeed;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                player.speed.X = Player.walkSpeed;
            }
            else
            {
                player.speed.X = 0.0f;
            }

            player.Update(new Rectangle(0, 420, 231, 72), new Rectangle(231, 420, 231, 72), new Rectangle(462, 420, 231, 72), new Rectangle(693, 420, 231, 72));
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(63, 56, 81));

            // TODO: Add your drawing code here
            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, null);

            _spriteBatch.Draw(terrainSprite, new Rectangle(0, 420, 231, 72), terrainCoords["floor"], Color.White);
            _spriteBatch.Draw(terrainSprite, new Rectangle(231, 420, 231, 72), terrainCoords["floor"], Color.White);
            _spriteBatch.Draw(terrainSprite, new Rectangle(462, 420, 231, 72), terrainCoords["floor"], Color.White);
            _spriteBatch.Draw(terrainSprite, new Rectangle(462, 420, 231, 72), terrainCoords["floor"], Color.White);
            _spriteBatch.Draw(terrainSprite, new Rectangle(693, 420, 231, 72), terrainCoords["floor"], Color.White);

            player.Draw(_spriteBatch);

            _spriteBatch.End();


            base.Draw(gameTime);
        }


        public static GameEntity.CollisionType CheckCollision(Rectangle rec1, Rectangle rec2)
        {
            if ((rec1.X < rec2.X && rec1.X + rec1.Width >= rec2.X) || (rec2.X < rec1.X && rec2.X + rec2.Width >= rec1.X))
            {
                return GameEntity.CollisionType.SideCollision;
            }
            else if (rec1.Y < rec2.Y && rec1.Y + rec1.Height >= rec2.Y)
            {
                //Collision from the bottom with respect to rec 1
                return GameEntity.CollisionType.BottomCollision;
            }
            else if (rec2.Y < rec1.Y && rec2.Y + rec2.Height >= rec1.Y)
            {
                //Collision from the top with respect to rec 1
                return GameEntity.CollisionType.TopCollision;
            }


            return GameEntity.CollisionType.NoCollision;
        }
    }
}
