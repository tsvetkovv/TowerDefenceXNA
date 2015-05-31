using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TowerDefenceXNA.Sprites;

namespace TowerDefenceXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
                    /*
                            Преждевременная оптимизация — корень всех зол. (с) Д.Кнут
                    */
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private SpriteManager spriteManager;
        private StartMenu startMenu;
        private EndScreen endScreen;
        private bool isExists;
        private KeyboardState prevKeyboardState;
        public static TimeSpan ElapsedTime;
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 640;
            Window.Title = "Tower Defence special for IBS-11 by IBS-11";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            spriteManager = new SpriteManager(this);
            startMenu = new StartMenu(this);
            endScreen = new EndScreen(this);
            Components.Add(startMenu);
            //Components.Add(spriteManager); //УДАЛИТЬ
            IsFixedTimeStep = true;
            ElapsedTime = TargetElapsedTime;
            base.Initialize();
        }

        public static GraphicsDevice AgagDevice;
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            // TODO: use this.Content to load your game content here
            spriteBatch = new SpriteBatch(GraphicsDevice);
            AgagDevice = GraphicsDevice;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (startMenu.isClosed && !isExists) //смена экрана
            {
                Components.RemoveAt(0);
                Components.Add(spriteManager);
                isExists = true;
            }

            if (GlobalVars.lives == 0)
            {
                GlobalVars.lives = -1;
                Components.RemoveAt(0);
                Components.Add(endScreen);
            }

            KeyboardState curentKeyState = Keyboard.GetState();

            if (curentKeyState.IsKeyDown(Keys.Escape))
                Environment.Exit(-1);
            if (curentKeyState.IsKeyDown(Keys.RightAlt) && curentKeyState.IsKeyDown(Keys.Enter) && prevKeyboardState.IsKeyUp(Keys.Enter))
                graphics.ToggleFullScreen();
            prevKeyboardState = curentKeyState;
            TargetElapsedTime = ElapsedTime;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            base.Draw(gameTime);
        }


    }
}
