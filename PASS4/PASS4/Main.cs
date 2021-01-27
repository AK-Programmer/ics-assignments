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
using System.IO;

namespace PASS4
{
    public class Main : Game
    {

        //Asset paths
        //Sprite file paths
        private const string IDLE_SPRITE_PATH = "Images/Sprites/Player/Idle";
        private const string JUMP_SPRITE_PATH = "Images/Sprites/Player/Jump (78x58)";
        private const string RUN_SPRITE_PATH = "Images/Sprites/Player/Run (78x58)";
        private const string FALL_SPRITE_PATH = "Images/Sprites/Player/Fall (78x58)";
        private const string TERRAIN_SPRITE_PATH = "Images/Sprites/Level Assets/brick wall";
        private const string CRATE_SPRITE_PATH = "Images/Sprites/Level Assets/Crate";
        private const string GEM_SPRITE_PATH = "Images/Sprites/Level Assets/Big Diamond Idle (18x14)";
        private const string FONT_PATH = "Fonts/8BitFont";


        //Graphics constants
        public const int TILE_SIZE = 50;
        private const int NUM_TILES_W = 20;
        private const int NUM_TILES_H = 9;
        private const int GEM_SIZE = 30;
        private const int CRATE_SIZE = 74;


        //Graphics & Display-related objects
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //File IO
        private static StreamReader readFile;
        private static StreamWriter writeFile;

        //player sprites
        private Texture2D idleSprite;
        private Texture2D jumpSprite;
        private Texture2D runSprite;
        private Texture2D fallSprite;

        //Other sprites (terrain, crates, collectibles, etc.)
        private Texture2D crateSprite;
        private Texture2D terrainSprite;
        private Texture2D gemSprite;

        //Fonts
        private SpriteFont gameFont;

        //Dictionaries
        private Dictionary<string, Rectangle> terrainCoords = new Dictionary<string, Rectangle>();
        private Dictionary<string, Texture2D> playerSprites = new Dictionary<string, Texture2D>();

        //Destination rectangles for terrain, crates, collectibles, etc.
        private List<Rectangle> terrainRecs = new List<Rectangle>();
        private List<Rectangle> crateDestRecs = new List<Rectangle>();
        private List<Rectangle> gemDestRecs = new List<Rectangle>();
        private List<Rectangle> keyDestRecs = new List<Rectangle>();
        private List<Rectangle> spikeDestRecs = new List<Rectangle>();
        private List<Rectangle> doorDestRecs = new List<Rectangle>();
        private Rectangle playerDestRec;
        private Rectangle flagPoleDestRec;

        //Game Entities
        private Player player;
        private Crate [] crates;
        //Stores both the player and crates for easy iteration
        private GameEntity[] entities;

        //Collectibles and spikes
        private Gem[] gems;
        private Spike[] spikes;

        //Source rectangles
        private Rectangle crateSrcRec = new Rectangle(0, 0, 21, 16);
        private Rectangle playerSrcRec = new Rectangle(9, 17, 37, 26);
        private Rectangle gemSrcRec = new Rectangle(5, 2, 12, 10);

        //Game variables
        private int numKeysCollected = 0;
        private int numGemsCollected = 0;

        //Player movement variables
        public static bool isPlayerPushingCrate = true;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            this._graphics.PreferredBackBufferWidth = TILE_SIZE * NUM_TILES_W;
            this._graphics.PreferredBackBufferHeight = TILE_SIZE * NUM_TILES_H;
            _graphics.PreferMultiSampling = false;
            this._graphics.ApplyChanges();

            base.Initialize();
        }

        //Pre: none
        //Post: none
        //Description: this is the load method for the entire game. It loads all the assets.
        protected override void LoadContent()
        {
            Console.WriteLine(System.IO.Path.GetPathRoot("Main.cs"));
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

            LoadLevel("level 1.txt");

            player.SetControlSeq("dee+ee");
        }

        //Pre: GameTime object
        //Post: none
        //Description: This is the 'global' update method for the entire game. It updates all entities and collectibles and handles gameflow logic.
        protected override void Update(GameTime gameTime)
        {
            for (int i = 0; i < entities.Length; i++)
            {
                entities[i].Update(terrainRecs, entities);
            }

            for (int i = 0; i < gems.Length; i++)
            {
                gems[i].Update(player, ref numGemsCollected);
            }
            base.Update(gameTime);
        }

        //Pre: GameTime object
        //Post: none
        //Description: this is the 'global draw method for the entire game. It draws all entities, collectibles, and terrain.
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(63, 56, 81));

            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, null);

            for(int i = 0; i < terrainRecs.Count; i++)
            {
                _spriteBatch.Draw(terrainSprite, terrainRecs[i], Color.White);
            }

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

        private void DrawGame()
        {

        }


        private void LoadLevel(string levelFileName)
        {
            readFile = new StreamReader(levelFileName);
            int numLines = 0;

            string line;

            /*
             * 0 = Player
             * 1 = Wall
             * 2 = Crate
             * 3 = Goal
             * 4 = Door
             * 5 = Spike
             * 6 = Gem
             * 7 = Key
             */

            //Clearing all rectangle lists in case a level was previously loaded
            terrainRecs.Clear();
            crateDestRecs.Clear();
            gemDestRecs.Clear();
            spikeDestRecs.Clear();

            while ((line = readFile.ReadLine()) != null)
            {
                numLines++;
                if(numLines > NUM_TILES_H)
                {
                    throw new FormatException($"There are too many tiles. The level must be no larger than {NUM_TILES_H} tiles in height.");
                }

                if (line.Length > NUM_TILES_W)
                {
                    throw new FormatException($"There are too many tiles. The level must be no larger than {NUM_TILES_W} tiles in width.");
                }

                for(int i = 0; i < line.Length; i++)
                {
                    switch(line[i])
                    {
                        //Player
                        case '0':
                            playerDestRec = new Rectangle(i * TILE_SIZE, numLines * TILE_SIZE, TILE_SIZE, TILE_SIZE - 10);
                            break;
                        //Terrain
                        case '1':
                            terrainRecs.Add(new Rectangle(i * TILE_SIZE, numLines * TILE_SIZE, TILE_SIZE, TILE_SIZE));
                            break;
                        //Crates
                        case '2':
                            crateDestRecs.Add(new Rectangle(i * TILE_SIZE, numLines * TILE_SIZE, TILE_SIZE, TILE_SIZE));
                            break;
                        //Flag
                        case '3':
                            flagPoleDestRec = new Rectangle(i * TILE_SIZE, numLines * TILE_SIZE, TILE_SIZE, TILE_SIZE);
                            break;
                        //Doors
                        case '4':
                            doorDestRecs.Add(new Rectangle(i * TILE_SIZE, numLines * TILE_SIZE, TILE_SIZE, TILE_SIZE));
                            break;
                        //Spikes
                        case '5':
                            spikeDestRecs.Add(new Rectangle(i * TILE_SIZE, numLines * TILE_SIZE, TILE_SIZE, TILE_SIZE));
                            break;
                        //Gems
                        case '6':
                            gemDestRecs.Add(new Rectangle(i * TILE_SIZE, numLines * TILE_SIZE, TILE_SIZE, TILE_SIZE));
                            break;
                        //Keys
                        case '7':
                            keyDestRecs.Add(new Rectangle(i * TILE_SIZE, numLines * TILE_SIZE, TILE_SIZE, TILE_SIZE));
                            break;
                    }
                }
            }
           
            crates = new Crate[crateDestRecs.Count];
            gems = new Gem[gemDestRecs.Count];
            spikes = new Spike[spikeDestRecs.Count];

            for (int i = 0; i < crates.Length; i++)
            {
                crates[i] = new Crate(crateSprite, crateDestRecs[i], crateSrcRec);
            }

            for (int i = 0; i < gems.Length; i++)
            {
                gems[i] = new Gem(gemSprite, gemDestRecs[i], gemSrcRec);
            }

            player = new Player(idleSprite, playerDestRec, playerSrcRec, playerSprites);

            //Crating the entities array to store all game entities
            entities = new GameEntity[crates.Length + 1];
            entities[0] = player;

            //Adding the crates to the entity array
            for (int i = 1; i < entities.Length; i++)
            {
                entities[i] = crates[i - 1];
            }

            Console.WriteLine(numLines);

            try
            {

            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(FormatException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    
}
