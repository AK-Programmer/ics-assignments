//Author: Adar Kahiri
//File Name: Main.cs
//Project Name: PASS4
//Creation Date: Jan 6, 2021
//Modified Date: Jan 22, 2021
/* Description:
 */
//Assets used: https://pixelfrog-store.itch.io/kings-and-pigs

using System;
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

        //General Constants
        const int SCREEN_W = 800;
        const int SCREEN_H = 560;

        //player sprites
        private Texture2D idleSprite;
        private Texture2D jumpSprite;
        private Texture2D runSprite;
        private Texture2D fallSprite;

        //Other sprites
        private Texture2D crateSprite;
        private Texture2D terrainSprite;

        //Asset paths
        private const string terrainSpritePath = "Images/Sprites/Level Assets/Terrain (32x32)";
        private const string idleSpritePath = "Images/Sprites/Player/Idle";
        private const string jumpSpritePath = "Images/Sprites/Player/Jump (78x58)";
        private const string runSpritePath = "Images/Sprites/Player/Run (78x58)";
        private const string fallSpritePath = "Images/Sprites/Player/Fall (78x58)";
        private const string crateSpritePath = "Images/Sprites/Level Assets/Crate";

        //Dictionaries
        private Dictionary<string, Rectangle> terrainCoords = new Dictionary<string, Rectangle>();
        private Dictionary<string, Texture2D> playerSprites = new Dictionary<string, Texture2D>();

        //Game Entities
        private Player player;
        private Crate crate;
        private GameEntity[] entities;

        //Game Constants
        //Floor height
        private const int FLOOR_H = 420;

        //Destination rectangles for Terrain
        Rectangle destRecFloatingPlatform = new Rectangle(200, 250, 250, 30);
        Rectangle crateDestRec = new Rectangle(250, 100, 63, 48);
        //Terrain rectangle array
        private Rectangle[] terrainRecs = new Rectangle[] { new Rectangle(0, 420, 231, 72), new Rectangle(231, 370, 231, 72), new Rectangle(462, 420, 231, 72), new Rectangle(693, 420, 231, 72), new Rectangle(924, 420, 231, 72), new Rectangle(200, 250, 250, 30) };
        

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
            fallSprite = Content.Load<Texture2D>(fallSpritePath);
            crateSprite = Content.Load<Texture2D>(crateSpritePath);

            //Adding various player sprites to playerSprite dictionary
            playerSprites.Add("jump", jumpSprite);
            playerSprites.Add("run", runSprite);
            playerSprites.Add("fall", fallSprite);

            //Adding locations of all images in terrain sprite to terrain dictionary
            terrainCoords.Add("floor", new Rectangle(41, 32, 77, 24));
            terrainCoords.Add("floating platform", new Rectangle(41, 32, 78, 9));

            player = new Player(idleSprite, new Rectangle(50, 368, 74, 52), new Rectangle(9, 17, 37, 26), playerSprites);
            crate = new Crate(crateSprite, new Rectangle(250, 100, 63, 48), new Rectangle(0,0, 21, 16));

            entities = new GameEntity[] { player, crate };

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.W) && player.velocity.Y == 0 )
            {
                player.velocity.Y = Player.JUMP_SPEED;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                player.velocity.X = -Player.WALK_SPEED;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                player.velocity.X = Player.WALK_SPEED;
            }
            else
            {
                player.velocity.X = 0.0f;
            }

            player.Update(terrainRecs, entities);
            crate.Update(terrainRecs, entities);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(63, 56, 81));

            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, null);


            for(int i = 0; i < terrainRecs.Length - 1; i++)
            {
                _spriteBatch.Draw(terrainSprite, terrainRecs[i], terrainCoords["floor"], Color.White);
            }

            _spriteBatch.Draw(terrainSprite, destRecFloatingPlatform, terrainCoords["floating platform"], Color.White);

            player.Draw(_spriteBatch);
            crate.Draw(_spriteBatch);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
