//Author: Adar Kahiri
//File Name: Main.cs
//Project Name: PASS4
//Creation Date: Jan 6, 2021
//Modified Date: Jan 27, 2021
/* Description: This is the main class. It handles all top-level logic such as the game loop, calling the update and draw methods of game objects, 
 * as well as handling gameflow (such as main menu, level transitions, gameplay, etc.)
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
        enum GameState
        {
            MainMenu,
            Play,
            ViewInstructions,
            ViewHighScores,
            Exit
        }

        //Asset paths
        //Sprite file paths
        private const string IDLE_SPRITE_PATH = "Images/Sprites/Player/Idle";
        private const string JUMP_SPRITE_PATH = "Images/Sprites/Player/Jump (78x58)";
        private const string RUN_SPRITE_PATH = "Images/Sprites/Player/Run (78x58)";
        private const string FALL_SPRITE_PATH = "Images/Sprites/Player/Fall (78x58)";
        private const string TERRAIN_SPRITE_PATH = "Images/Sprites/Level Assets/brick wall";
        private const string CRATE_SPRITE_PATH = "Images/Sprites/Level Assets/Crate";
        private const string GEM_SPRITE_PATH = "Images/Sprites/Level Assets/Big Diamond Idle (18x14)";
        private const string DOOR_SPRITE_PATH = "Images/Sprites/Level Assets/Door";
        private const string KEY_SPRITE_PATH = "Images/Sprites/Level Assets/goldkey";
        private const string SPIKE_SPRITE_PATH = "Images/Sprites/Level Assets/spikes";
        private const string FLAGPOLE_SPRITE_PATH = "Images/Sprites/Level Assets/flagpole";

        //Font file path
        private const string FONT_PATH = "Fonts/8BitFont";
        
        //Graphics constants
        public const int TILE_SIZE = 50;
        private const int NUM_TILES_W = 20;
        private const int NUM_TILES_H = 12;
        private const int GEM_SIZE = 30;
        private const int CRATE_SIZE = 74;
        private const int KEY_SIZE = 30;


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
        private Texture2D keySprite;
        private Texture2D doorSprite;

        //Fonts
        private SpriteFont gameFont;

        //Dictionaries
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

        //Collectibles, spikes, and doors
        private Gem[] gems;
        private Key[] keys;
        private Door[] doors;
        private Spike[] spikes;

        //Source rectangles
        private Rectangle crateSrcRec = new Rectangle(0, 0, 21, 16);
        private Rectangle playerSrcRec = new Rectangle(9, 17, 37, 26);
        private Rectangle gemSrcRec = new Rectangle(5, 2, 12, 10);
        private Rectangle keySrcRec = new Rectangle(0, 0, 992, 1000);
        private Rectangle doorSrcRec = new Rectangle(0, 0, 46, 56);

        //Game variables
        private int numKeysCollected = 0;
        private int numGemsCollected = 0;
        private int totalNumGems = 0;
        private string commandSequence = "";
        private char[] validCommandKeys = {'a', 'd', 'c', 'e', 'q', '+', '-', 's', 'f', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

        //Game-state variables
        GameState gameState = GameState.MainMenu;
        int currentLevel = 0;
        bool viewLegend = false;

        //Player movement variables
        public static bool isPlayerPushingCrate = false;

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
            keySprite = Content.Load<Texture2D>(KEY_SPRITE_PATH);
            doorSprite = Content.Load<Texture2D>(DOOR_SPRITE_PATH);

            //Loading the game font
            gameFont = Content.Load<SpriteFont>(FONT_PATH);

            Helper.graphics = _graphics;
            //Adding various player sprites to playerSprite dictionary
            playerSprites.Add("jump", jumpSprite);
            playerSprites.Add("run", runSprite);
            playerSprites.Add("fall", fallSprite);

            LoadLevel("level 1.txt");

            //player.SetControlSeq("qqqqdddddee");
        }

        //Pre: GameTime object
        //Post: none
        //Description: This is the 'global' update method for the entire game. It updates all entities and collectibles and handles gameflow logic.
        protected override void Update(GameTime gameTime)
        {
            UpdateGame();
            base.Update(gameTime);
        }

        //Pre: GameTime object
        //Post: none
        //Description: this is the 'global draw method for the entire game. It draws all entities, collectibles, and terrain.
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(63, 56, 81));

            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, null);

            DrawGame();
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawUI()
        {
            Rectangle controlCenterRec = new Rectangle(0, TILE_SIZE * 9, NUM_TILES_W * TILE_SIZE, TILE_SIZE * 3);
            Texture2D controlCenterTexture = Helper.GetColouredRec(controlCenterRec, Color.Black);


            //Gems and Keys
            _spriteBatch.Draw(gemSprite, new Rectangle(10, 10, 20, 20), new Rectangle(5, 2, 12, 10), Color.White);
            _spriteBatch.DrawString(gameFont, $"{numGemsCollected}/{totalNumGems}", new Vector2(35, 10), Color.White);

            _spriteBatch.Draw(keySprite, new Rectangle(70, 10, 20, 20), Color.White);
            _spriteBatch.DrawString(gameFont, numKeysCollected.ToString(), new Vector2(95, 10), Color.White);

            //Command center
            _spriteBatch.Draw(controlCenterTexture, controlCenterRec, Color.White);
            _spriteBatch.DrawString(gameFont, $"Command Sequence: {commandSequence}", new Vector2(TILE_SIZE/2, TILE_SIZE * 9 + 10), Color.White);
        }


        private void UpdateMainMenu()
        {

        }

        private void DrawMainMenu()
        {

        }

        private void UpdateGame()
        {
            //Update all entities
            for (int i = 0; i < entities.Length; i++)
            {
                entities[i].Update(terrainRecs, entities, doors);
            }

            //Update all gems
            for (int i = 0; i < gems.Length; i++)
            {
                gems[i].Update(player, ref numGemsCollected);
            }


            //Update all keys
            for (int i = 0; i < keys.Length; i++)
            {
                keys[i].Update(player, ref numKeysCollected);
            }

            //Update all doors
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].Update(player, ref numKeysCollected);
            }

            //Get user command input
            for(int i = 0; i < validCommandKeys.Length; i++)
            {
                if(Input.HasBeenPressed((Keys) char.ToUpper(validCommandKeys[i]), false))
                {
                    commandSequence += validCommandKeys[i];
                }
            }

            if(Input.HasBeenPressed(Keys.Enter, true))
            {
                //Console.WriteLine("Hello");
                player.SetControlSeq(commandSequence);
                commandSequence = "";
            }
        }
        private void DrawGame()
        {
            for (int i = 0; i < terrainRecs.Count; i++)
            {
                _spriteBatch.Draw(terrainSprite, terrainRecs[i], Color.White);
            }

            for (int i = 0; i < entities.Length; i++)
            {
                entities[i].Draw(_spriteBatch);
            }

            for (int i = 0; i < gems.Length; i++)
            {
                gems[i].Draw(_spriteBatch);
            }

            for (int i = 0; i < keys.Length; i++)
            {
                keys[i].Draw(_spriteBatch);
            }

            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].Draw(_spriteBatch);
            }

            DrawUI();
        }

        private void UpdateInstructions()
        {

        }

        private void DrawInstructions()
        {

        }

        private void UpdateHighScores()
        {

        }

        private void DrawHighScores()
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

            //Resetting counters
            numGemsCollected = 0;
            numKeysCollected = 0;

            //Adding all level borders
            //The floor of the level
            terrainRecs.Add(new Rectangle(0, NUM_TILES_H * TILE_SIZE, NUM_TILES_W * TILE_SIZE, TILE_SIZE));
            //The left wall of the level
            terrainRecs.Add(new Rectangle(-TILE_SIZE, 0, TILE_SIZE, NUM_TILES_H * TILE_SIZE));
            //The Right wall of the level
            terrainRecs.Add(new Rectangle(NUM_TILES_W * TILE_SIZE, 0, TILE_SIZE, NUM_TILES_H * TILE_SIZE));
            //The ceiling of the level
            terrainRecs.Add(new Rectangle(0, -TILE_SIZE, NUM_TILES_W * TILE_SIZE, TILE_SIZE));

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

            //Initializing all game object arrays
            crates = new Crate[crateDestRecs.Count];
            gems = new Gem[gemDestRecs.Count];
            keys = new Key[keyDestRecs.Count];
            doors = new Door[doorDestRecs.Count];
            spikes = new Spike[spikeDestRecs.Count];

            //Setting totalNumGems variable
            totalNumGems = gems.Length;
            //Initializing all crates
            for (int i = 0; i < crates.Length; i++)
            {
                crates[i] = new Crate(crateSprite, crateDestRecs[i], crateSrcRec);
            }

            //Initializing all gems
            for (int i = 0; i < gems.Length; i++)
            {
                gems[i] = new Gem(gemSprite, gemDestRecs[i], gemSrcRec);
            }

            //Initializing all keys
            for(int i = 0; i < keys.Length; i++)
            {
                keys[i] = new Key(keySprite, keyDestRecs[i], keySrcRec);
            }

            //Initializing all doors
            for(int i = 0; i < doors.Length; i++)
            {
                doors[i] = new Door(doorSprite, doorDestRecs[i], doorSrcRec);
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
