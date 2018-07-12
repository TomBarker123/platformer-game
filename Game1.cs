using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace GameNew
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont textFont;       

        //Enum for the game state
        enum GameState
        {
            MainMenu,         
            Instructions,            
            Quit,
            LevelComplete,
            GameOver,            
            Level1,
            Level2,
            Level3,
        }
        GameState CurrentGameState = GameState.MainMenu; //The game state is set to main menu when the program starts
        
        //Screen adjustments - setting the screen width and height
        int screenWidth = 800;
        int screenHeight = 600;
        
        QuitButton btnQuit;       
        LeaderboardButton btnLeaderboard;
        MainMenuButton btnMainMenu;
        InstructionButton btnInstructions;        
        Level1Button btnLevel1;   
        Level2Button btnLevel2;
        Level3Button btnLevel3;

        DateTime startTime;
        DateTime endTime;   //Timer and timespan - used to time how long it takes for the user to reach the end of each level
        TimeSpan levelTime;     
        
        Map map;
        Map map2;      
        Map map3;
        Player player;
        Camera camera;

        public Texture2D menuImage;
        Texture2D level1Background;
        Texture2D level2Background;
        Texture2D level3Background;    
        SoundEffect effect;
        SoundEffect deathSound;
        SoundEffect endSound;
        Song song;
               
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            menuImage = null;
            Window.Title = "PLATFORM GAME";
        }           

        protected override void Initialize()
        {
            map = new Map();
            map2 = new Map();  
            map3 = new Map();
            player = new Player();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Creating a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Screen adjustments
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;  //This is to set the max width and height of the screen
            graphics.ApplyChanges();
            IsMouseVisible = true;  //Mouse is set to visible when the program starts

            //Menu buttons                  
            btnLeaderboard = new LeaderboardButton(Content.Load<Texture2D>("btnLeaderboard"), graphics.GraphicsDevice);
            btnQuit = new QuitButton(Content.Load<Texture2D>("btnQuit"), graphics.GraphicsDevice);
            btnMainMenu = new MainMenuButton(Content.Load<Texture2D>("btnMainMenu"), graphics.GraphicsDevice);
            btnInstructions = new InstructionButton(Content.Load<Texture2D>("btnInstructions"), graphics.GraphicsDevice);            
            btnLevel1 = new Level1Button(Content.Load<Texture2D>("btnLevel1"), graphics.GraphicsDevice);//Loading menu buttons from the content folder
            btnLevel2 = new Level2Button(Content.Load<Texture2D>("btnLevel2"), graphics.GraphicsDevice);
            btnLevel3 = new Level3Button(Content.Load<Texture2D>("btnLevel3"), graphics.GraphicsDevice);  
            
            Tiles.Content = Content;
            camera = new Camera(GraphicsDevice.Viewport); //Camera is set to match the screen width and height set earlier

            //0 - no tile drawn, 1 - tile1.png drawn, 2 - tile2.png draw etc
            map.Generate(new int[,]
            {
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 }, 
                {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2 },
                {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2 },  
                {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2 },
                {2,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,1,1,1,1,2,2,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2 },
                {2,0,0,0,0,0,0,1,1,2,2,2,2,1,0,0,0,0,0,0,1,2,2,1,2,2,2,2,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2 },   //This could be done using streamreader to read in a text file
                {2,0,0,0,0,0,1,2,2,2,2,2,2,2,1,1,0,0,0,0,2,2,2,2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,1,0,1,1,1,0,0,0,0,0,0,2 },   //This could use run length encoding so it takes up less space
                {2,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2,1,0,0,0,2,2,2,2,2,2,2,2,0,0,0,0,1,1,1,1,1,1,2,0,2,2,2,1,1,1,1,1,1,2 },
                {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,0,0,3,2,2,2,2,2,2,0,0,0,0,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2,2,2,2 },
                {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,3,3,0,0,3,3,3,3,3,3,3,3,0,0,2,2,2,2,2,2,3,3,3,0,3,3,3,3,3,3,3,3,3,3 },
                {3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,0,0,0,3,3,3,3,3,3,3,3,3,0,0,0,0,0,0,0,0,3,3,0,3,3,3,3,3,3,3,3,3,3 },
                {3,3,3,3,3,3,3,3,4,4,4,4,4,4,4,4,0,0,0,0,3,3,3,3,3,3,3,3,3,3,0,0,0,0,0,0,0,0,3,0,3,3,3,3,3,3,3,3,3,3 },
                {4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,0,0,0,0,3,3,3,3,3,3,3,3,3,3,3,3,0,0,0,0,0,0,0,0,0,4,4,4,4,4,4,4,4,4,4 },
                {4,4,4,4,4,4,4,4,4,4,4,4,4,4,0,0,0,0,4,4,4,4,4,4,4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,4,4,4,4,4,4,4,4,4,4 },
                {4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,4,4,4,4,4,4,4,4,4,0,0,0,0,0,0,4,0,0,0,0,0,0,0,4,4,4,4,4,4,4,4,4 },
                {4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,4,4,4,4,4,4,4 },
                {4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,4,4,4,4,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,4,0,0,0,0,0,4,4,4,4,4,4,4 },
                {5,5,5,5,5,5,5,0,0,0,5,0,5,0,5,0,4,4,4,0,0,0,0,0,0,0,0,4,0,4,0,0,0,0,0,0,0,0,0,4,0,0,4,4,4,4,4,4,4,4 },
                {5,5,5,5,5,5,5,0,0,0,5,5,5,5,5,5,5,5,0,0,0,0,0,0,0,5,0,5,0,5,0,5,0,0,0,0,0,0,0,0,0,5,5,5,5,5,5,5,5,5 },
                {5,5,5,5,5,5,5,5,0,0,0,5,5,5,5,5,0,0,0,0,0,0,0,5,0,5,0,5,0,5,0,5,5,5,0,0,0,0,0,0,5,5,5,5,5,5,5,5,5,5 },
                {5,5,5,5,5,5,5,5,5,0,0,0,0,0,0,0,0,0,0,0,0,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5 },
                {5,5,5,5,5,5,5,5,5,5,0,0,0,0,0,0,0,0,0,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5 },
                {5,5,5,5,5,5,5,5,5,5,5,5,0,5,0,5,0,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5 },
                {5,5,5,5,5,5,5,5,5,5,5,5,0,5,0,5,0,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5 },
                {5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5 },
                {5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5 },
            }, 80);        //Size of each tile in pixels

            map2.Generate(new int[,]
            {
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 }, 
                {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2 },
                {2,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,2,2,2,1,0,0,0,0,0,0,1,1,1,1,2,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,2 },
                {2,2,1,0,0,0,0,0,0,0,0,0,0,0,1,2,2,2,2,2,2,1,0,0,0,0,1,2,2,2,2,2,0,0,0,0,0,0,0,0,1,2,2,1,0,0,0,0,0,2 },
                {2,2,2,1,1,0,0,0,0,0,0,0,0,1,2,2,2,2,2,2,2,2,1,0,0,1,2,2,2,2,2,2,1,0,0,0,0,0,0,1,2,2,2,2,1,0,0,0,0,2 },
                {2,2,2,2,2,1,1,1,0,1,1,1,1,2,2,2,2,2,2,2,2,2,2,1,1,2,2,2,2,2,2,2,2,1,0,1,1,1,1,2,2,2,2,2,2,1,0,0,0,2 },   
                {2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2,2,2,2,2,1,1,1,2 },   
                {2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2 },
                {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2 },
                {5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5 },
            }, 80);        

            map3.Generate(new int[,]
            {
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 }, 
                {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2 },
                {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2 },
                {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2 },
                {2,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,2,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2 },
                {2,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,1,1,1,0,0,0,1,2,2,2,2,1,0,0,0,0,0,2,1,0,0,0,0,0,0,0,0,0,0,0,0,0,2 },   
                {2,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,2,2,2,2,2,0,0,2,2,2,2,2,2,0,0,0,0,1,2,2,1,1,1,1,1,1,1,0,0,0,0,0,0,2 },   
                {2,1,1,0,0,0,0,0,0,2,0,0,0,0,0,3,3,3,3,0,0,0,0,2,2,2,2,2,0,0,0,0,1,2,2,2,2,1,2,2,2,2,2,1,1,1,1,1,1,2 },
                {2,2,2,1,0,0,0,0,0,2,0,0,0,0,3,3,0,0,0,0,0,0,0,2,2,2,2,2,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2 },  //Map for level 3 loaded
                {2,2,2,2,3,0,0,0,0,3,0,0,0,3,3,0,0,0,0,0,0,0,0,3,3,3,3,3,0,0,0,0,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3 },
                {3,3,3,3,3,3,0,0,0,3,3,0,0,0,0,0,0,0,0,0,3,0,0,3,3,3,3,3,3,0,0,0,0,0,0,0,0,3,3,3,3,0,0,0,0,0,0,0,0,3 },
                {3,3,3,3,3,3,3,0,0,3,3,0,3,0,0,0,0,0,0,0,3,0,0,3,3,3,3,3,3,3,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,4,4 },
                {4,4,4,4,4,4,4,0,0,4,4,0,4,4,0,0,0,0,0,3,3,0,0,3,3,3,3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4 },
                {4,4,4,4,4,4,4,0,0,4,4,0,4,4,4,4,0,0,0,4,4,0,0,4,4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,4,0,4,4,4 },
                {4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,4,4,4,0,0,4,4,4,4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,4,0,4,4,4,4,4 },
                {4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,4,4,4,4,4,0,0,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,0,0,4,4,4,4,4,4,4,4 },
                {4,4,4,4,4,4,4,4,4,4,4,0,4,0,4,0,4,4,4,4,4,0,0,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,0,4,4,4,4,4,4,4,4 },
                {5,5,5,5,5,5,5,5,5,5,5,0,5,0,5,0,4,4,4,5,5,0,0,5,5,5,5,4,4,4,4,4,4,4,4,4,4,4,4,4,4,0,4,4,4,4,4,4,4,4 },
                {5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5 },
                {5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5 },              
            }, 80);      
            
            player.Load(Content);
            effect = Content.Load<SoundEffect>("JumpSound");
            song = Content.Load<Song>("Song");
            deathSound = Content.Load<SoundEffect>("deathSound");
            endSound = Content.Load<SoundEffect>("endSound");
            textFont = Content.Load<SpriteFont>("TextFont");       
            menuImage = Content.Load<Texture2D>("MenuImage");                   
            level1Background = Content.Load<Texture2D>("level1Background");
            level2Background = Content.Load<Texture2D>("level2Background");
            level3Background = Content.Load<Texture2D>("level3Background");
        }             

        protected override void UnloadContent()
        {
           
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();                       

            MouseState mouse = Mouse.GetState();
               
            btnLeaderboard.Update(mouse);
            btnQuit.Update(mouse);
            btnMainMenu.Update(mouse);
            btnInstructions.Update(mouse);           
            btnLevel1.Update(mouse);       //Updating the buttons to see if the mouse has intersected with them
            btnLevel2.Update(mouse); 
            btnLevel3.Update(mouse);               

            if (btnQuit.isClicked == true)                          
                CurrentGameState = GameState.Quit;            

            if (btnMainMenu.isClicked == true)                      
                CurrentGameState = GameState.MainMenu;
            
            if (btnInstructions.isClicked == true)                         
                CurrentGameState = GameState.Instructions;        
                       
            if (btnLevel1.isClicked == true)
            {
                CurrentGameState = GameState.Level1;
                startTime = DateTime.Now;
                MediaPlayer.Play(song);
                btnLevel1.isClicked = false;
            }

            if (btnLevel2.isClicked == true)
            {
                CurrentGameState = GameState.Level2;
                startTime = DateTime.Now;
                MediaPlayer.Play(song);
                btnLevel2.isClicked = false;
            }

            if (btnLevel3.isClicked == true)
            {
                CurrentGameState = GameState.Level3;
                startTime = DateTime.Now;
                MediaPlayer.Play(song);
                btnLevel3.isClicked = false;
            }

            switch (CurrentGameState)
            {                                                                   
                case GameState.Quit:
                    Exit();
                    break;

                case GameState.LevelComplete:
                    IsMouseVisible = true;
                    MediaPlayer.Stop();                    
                    break;                      

                case GameState.GameOver:
                    IsMouseVisible = true;
                    MediaPlayer.Stop();
                    break;

                case GameState.Level1:
                    IsMouseVisible = false;                    
                    player.Update(gameTime, effect);
                    foreach (CollisionTiles tile in map.CollisionTiles)
                    {
                        player.Collision(tile.Rectangle, map.Width, map.Height);
                        camera.Update(player.Position, map.Width, map.Height);
                    }

                    if (player.Position.X >= 3720)
                    {
                        endSound.Play();
                        endTime = DateTime.Now;
                        CurrentGameState = GameState.LevelComplete; 
                        player.Reset();
                    }

                    if (player.Position.Y >= 1840)
                    {
                        deathSound.Play();
                        CurrentGameState = GameState.GameOver;
                        player.Reset();
                    }
                    break;

                case GameState.Level2:
                    IsMouseVisible = false;
                    player.Update(gameTime, effect);
                    foreach (CollisionTiles tile in map2.CollisionTiles)
                    {
                        player.Collision(tile.Rectangle, map2.Width, map2.Height);
                        camera.Update(player.Position, map2.Width, map2.Height);
                    }

                    if (player.Position.X >= 3800)
                    {
                        endSound.Play();
                        endTime = DateTime.Now;
                        CurrentGameState = GameState.LevelComplete;
                        player.Reset();
                    }

                    if (player.Position.Y >= 560)
                    {
                        deathSound.Play();
                        CurrentGameState = GameState.GameOver;
                        player.Reset();
                    }
                    break;

                case GameState.Level3:
                    IsMouseVisible = false;
                    player.Update(gameTime, effect);
                    foreach (CollisionTiles tile in map3.CollisionTiles)
                    {
                        player.Collision(tile.Rectangle, map3.Width, map3.Height);
                        camera.Update(player.Position, map3.Width, map3.Height);
                    }

                    if (player.Position.X >= 3800) 
                    {
                        endSound.Play();
                        endTime = DateTime.Now;
                        CurrentGameState = GameState.LevelComplete;
                        player.Reset();
                    }

                    if (player.Position.Y >= 1360)
                    {
                        deathSound.Play();
                        CurrentGameState = GameState.GameOver;
                        player.Reset();
                    }
                    break;
            }          
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    spriteBatch.Begin();
                    spriteBatch.Draw(menuImage, new Vector2(0, 0), Color.White);
                    spriteBatch.DrawString(textFont, "PLATFORM GAME", new Vector2(235, 50), Color.Black);
                    btnLevel1.Draw(spriteBatch);
                    btnLevel1.SetPosition(new Vector2(350, 100));
                    btnLevel2.Draw(spriteBatch);
                    btnLevel2.SetPosition(new Vector2(350, 200));
                    btnLevel3.Draw(spriteBatch);
                    btnLevel3.SetPosition(new Vector2(350, 300));
                    btnInstructions.Draw(spriteBatch);
                    btnInstructions.SetPosition(new Vector2(300, 400));
                    btnQuit.Draw(spriteBatch);
                    btnQuit.SetPosition(new Vector2(350, 500));
                    spriteBatch.End();
                    break;

                case GameState.Instructions:
                    spriteBatch.Begin();
                    spriteBatch.Draw(menuImage, new Vector2(0, 0), Color.White);
                    spriteBatch.DrawString(textFont, "REACH THE END AS FAST AS POSSIBLE", new Vector2(10, 100), Color.Black);
                    spriteBatch.DrawString(textFont, "SPACE - JUMP", new Vector2(260, 200), Color.Black);
                    spriteBatch.DrawString(textFont, "RIGHT ARROW KEY - MOVE RIGHT", new Vector2(60, 300), Color.Black);
                    spriteBatch.DrawString(textFont, "LEFT ARROW KEY - MOVE LEFT", new Vector2(100, 400), Color.Black);
                    btnMainMenu.Draw(spriteBatch);
                    btnMainMenu.SetPosition(new Vector2(300, 500));
                    btnQuit.Draw(spriteBatch);
                    btnQuit.SetPosition(new Vector2(350, 550));
                    spriteBatch.End();
                    break;

                case GameState.Level1:                    
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);

                    spriteBatch.Draw(level1Background, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(level1Background, new Vector2(1024, 0), Color.White);
                    spriteBatch.Draw(level1Background, new Vector2(2048, 0), Color.White);
                    spriteBatch.Draw(level1Background, new Vector2(3072, 0), Color.White);
                    spriteBatch.Draw(level1Background, new Vector2(0, 614), Color.White);
                    spriteBatch.Draw(level1Background, new Vector2(1024, 614), Color.White);
                    spriteBatch.Draw(level1Background, new Vector2(2048, 614), Color.White);
                    spriteBatch.Draw(level1Background, new Vector2(3072, 614), Color.White);
                    spriteBatch.Draw(level1Background, new Vector2(0, 1226), Color.White);
                    spriteBatch.Draw(level1Background, new Vector2(1024, 1228), Color.White);
                    spriteBatch.Draw(level1Background, new Vector2(2048, 1228), Color.White);
                    spriteBatch.Draw(level1Background, new Vector2(3072, 1228), Color.White);
                    spriteBatch.Draw(level1Background, new Vector2(0, 1842), Color.White);
                    spriteBatch.Draw(level1Background, new Vector2(1024, 1842), Color.White);

                    map.Draw(spriteBatch);
                    player.Draw(spriteBatch);

                    spriteBatch.End();
                    break;

                case GameState.Level2:                   
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
                    spriteBatch.Draw(level2Background, new Vector2(0,0), Color.White);
                    spriteBatch.Draw(level2Background, new Vector2(1000, 0), Color.White);
                    spriteBatch.Draw(level2Background, new Vector2(2000, 0), Color.White);
                    spriteBatch.Draw(level2Background, new Vector2(3000, 0), Color.White);
                    spriteBatch.Draw(level2Background, new Vector2(0, 500), Color.White);
                    spriteBatch.Draw(level2Background, new Vector2(1000, 500), Color.White);
                    spriteBatch.Draw(level2Background, new Vector2(2000, 500), Color.White);
                    spriteBatch.Draw(level2Background, new Vector2(3000, 500), Color.White);
                    map2.Draw(spriteBatch);
                    player.Draw(spriteBatch);
                    spriteBatch.End();
                    break;

                case GameState.Level3:
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);

                    spriteBatch.Draw(level3Background, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(level3Background, new Vector2(3000, 0), Color.White);                  
                    spriteBatch.Draw(level3Background, new Vector2(0, 1100), Color.White);
                    spriteBatch.Draw(level3Background, new Vector2(3000, 1100), Color.White);
                    spriteBatch.Draw(level3Background, new Vector2(3200, 1100), Color.White);       
                    map3.Draw(spriteBatch);
                    player.Draw(spriteBatch);
                    spriteBatch.End();
                    break;

                case GameState.LevelComplete:
                    spriteBatch.Begin();
                    spriteBatch.Draw(menuImage, new Vector2(0, 0), Color.White);
                    btnMainMenu.Draw(spriteBatch);
                    btnMainMenu.SetPosition(new Vector2(300, 500));
                    btnQuit.Draw(spriteBatch);
                    btnQuit.SetPosition(new Vector2(350, 550));
                    levelTime = endTime - startTime;
                    Convert.ToInt32(levelTime.TotalSeconds);
                    string levelTimeString = levelTime.TotalSeconds.ToString();
                    spriteBatch.DrawString(textFont, "YOU TOOK " + levelTimeString + " SECONDS", new Vector2(100, 100), Color.Black);
                    spriteBatch.End();
                    break;

                case GameState.GameOver:
                    spriteBatch.Begin();
                    spriteBatch.Draw(menuImage, new Vector2(0, 0), Color.White);
                    btnMainMenu.Draw(spriteBatch);
                    btnMainMenu.SetPosition(new Vector2(300, 500));
                    btnQuit.Draw(spriteBatch);
                    btnQuit.SetPosition(new Vector2(350, 550));
                    spriteBatch.DrawString(textFont, "GAME OVER", new Vector2(275, 100), Color.Black);
                    spriteBatch.End();
                    break;
            }
            base.Draw(gameTime);
        }
    }
}
