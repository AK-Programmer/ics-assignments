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
        //General Constants
        const int SCREEN_W = 800;
        const int SCREEN_H = 560;

        //Asset paths
        private const string TERRAIN_SPRITE_PATH = "Images/Sprites/Level Assets/Terrain (32x32)";
        private const string IDLE_SPRITE_PATH = "Images/Sprites/Player/Idle";
        private const string JUMP_SPRITE_PATH = "Images/Sprites/Player/Jump (78x58)";
        private const string RUN_SPRITE_PATH = "Images/Sprites/Player/Run (78x58)";
        private const string FALL_SPRITE_PATH = "Images/Sprites/Player/Fall (78x58)";
        private const string CRATE_SPRITE_PATH = "Images/Sprites/Level Assets/Crate";
        private const string GEM_SPRITE_PATH = "Images/Sprites/Level Assets/Big Diamond Idle (18x14)";
        private const string FONT_PATH = "Fonts/8BitFont";

        //Graphics & Display-related objects
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //player sprites
        private Texture2D idleSprite;
        private Texture2D jumpSprite;
        private Texture2D runSprite;
        private Texture2D fallSprite;

        //Other sprites (terrain, crates, collectibles, etc.)
        private Texture2D crateSprite;
        private Texture2D terrainSprite;
        private Texture2D gemSprite;

        //
        private SpriteFont gameFont;

        //Dictionaries
        private Dictionary<string, Rectangle> terrainCoords = new Dictionary<string, Rectangle>();
        private Dictionary<string, Texture2D> playerSprites = new Dictionary<string, Texture2D>();

        //Game Entities
        private Player player;
        private Crate [] crates;
        private GameEntity[] entities;

        //Collectibles
        private Gem[] gems;


        //Game Constants
        //Floor height
        private const int FLOOR_H = 420;

        //Destination rectangles for terrain, crates, and collectibles
        Rectangle destRecFloatingPlatform = new Rectangle(200, 250, 250, 30);
        Rectangle [] crateDestRecs = new Rectangle[] { new Rectangle(230, 100, 63, 48), new Rectangle(294, 100, 63, 48)};
        Rectangle[] gemDestRecs = new Rectangle[] { new Rectangle(10, 385, 36, 30) };


        //Terrain rectangle array
        private Rectangle[] terrainRecs = new Rectangle[] { new Rectangle(0, 420, 231, 72), new Rectangle(231, 420, 231, 72), new Rectangle(462, 420, 231, 72), new Rectangle(693, 370, 231, 72), new Rectangle(924, 420, 231, 72), new Rectangle(200, 250, 250, 30) };

        //Game variables
        private int numKeysCollected = 0;
        private int numGemsCollected = 0;

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

            //Loading all sprites
            terrainSprite = Content.Load<Texture2D>(TERRAIN_SPRITE_PATH);
            idleSprite = Content.Load<Texture2D>(IDLE_SPRITE_PATH);
            jumpSprite = Content.Load<Texture2D>(JUMP_SPRITE_PATH);
            runSprite = Content.Load<Texture2D>(RUN_SPRITE_PATH);
            fallSprite = Content.Load<Texture2D>(FALL_SPRITE_PATH);
            crateSprite = Content.Load<Texture2D>(CRATE_SPRITE_PATH);
            gemSprite = Content.Load<Texture2D>(GEM_SPRITE_PATH);

            //Loading the game font
            gameFont = Content.Load<SpriteFont>(FONT_PATH);

            //Adding various player sprites to playerSprite dictionary
            playerSprites.Add("jump", jumpSprite);
            playerSprites.Add("run", runSprite);
            playerSprites.Add("fall", fallSprite);
            
            //Adding locations of all images in terrain sprite to terrain dictionary
            terrainCoords.Add("floor", new Rectangle(41, 32, 77, 24));
            terrainCoords.Add("floating platform", new Rectangle(41, 32, 78, 9));

            //Initializing game entities and collectibles
            player = new Player(idleSprite, new Rectangle(50, 368, 74, 52), new Rectangle(9, 17, 37, 26), playerSprites);
            crates = new Crate[crateDestRecs.Length];
            gems = new Gem[gemDestRecs.Length];

            //Initializing a crate for every element in the array
            for(int i = 0; i < crateDestRecs.Length; i++)
            {
                crates[i] = new Crate(crateSprite, crateDestRecs[i], new Rectangle(0, 0, 21, 16));
            }

            //Initializing a gem for every gem in the array
            for(int i = 0; i < gemDestRecs.Length; i++)
            {
                gems[i] = new Gem(gemSprite, gemDestRecs[i], new Rectangle(5, 2, 12, 10));
            }

            //Crating the entities array to store all game entities
            entities = new GameEntity[crates.Length + 1];
            entities[0] = player;
            //Adding the crates to the entity array
            for(int i = 1; i < entities.Length; i++)
            {
                entities[i] = crates[i - 1];
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.W) && player.velocity.Y == 0)
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

            for(int i = 0; i < entities.Length; i++)
            {
                entities[i].Update(terrainRecs, entities);
            }

            for(int i = 0; i < gems.Length; i++)
            {
                gems[i].Update(player, ref numGemsCollected);
            }

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

            for(int i = 0; i < entities.Length; i++)
            {
                entities[i].Draw(_spriteBatch);
            }

            for(int i = 0; i < gems.Length; i++)
            {
                gems[i].Draw(_spriteBatch);
            }
            DrawHUD();
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawHUD()
        {
            _spriteBatch.DrawString(gameFont, numGemsCollected.ToString(), new Vector2(35, 10), Color.White);
            _spriteBatch.Draw(gemSprite, new Rectangle(10, 10, 20, 20), new Rectangle(5, 2, 12, 10), Color.White);
        }
    }

    
}
