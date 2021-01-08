//Author: Adar Kahiri
//File Name: Main.cs
//Project Name: PASS4
//Creation Date: Jan 6, 2021
//Modified Date: Jan 22, 2021
/* Description:
 */

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

        //Sprites and assets
        private Texture2D playerSprite;
        private Texture2D crateSprite;
        private Texture2D terrainSprite;

        //Asset paths
        private const string terrainPath = "Images/Sprites/Level Assets/Terrain (32x32)";
        private const string playerPath = "Images/Sprites/Player/Idle";


        private Dictionary<string, Rectangle> terrainCoords = new Dictionary<string, Rectangle>();

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

            terrainCoords.Add("floor", new Rectangle(41, 32, 77, 24));
            //terrainCoords.Add

            terrainSprite = Content.Load<Texture2D>(terrainPath);
            playerSprite = Content.Load<Texture2D>(playerPath);

            player = new Player(playerSprite, new Rectangle(50, 368, 74, 52), new Rectangle(9, 17, 37, 26));

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.W) && player.standingOnGround)
            {
                player.speed.Y = -15.0f;
                player.standingOnGround = false;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                player.speed.X = -3f;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                player.speed.X = 3f;
            }
            else
            {
                player.speed.X = 0.0f;
            }

            player.Update();
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
    }
}
