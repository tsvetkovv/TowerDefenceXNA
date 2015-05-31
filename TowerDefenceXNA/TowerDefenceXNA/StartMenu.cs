using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TowerDefenceXNA
{
    public class StartMenu : DrawableGameComponent
    {
        private Vector2 positionNewGame;
        private Vector2 positionScreen;
        private SpriteBatch spriteBatch;
        private bool isClosing;
        public bool isClosed;

        public StartMenu(Game game)
            : base(game)
        {
            Initialize();
        }

        private int i;
        public override void Update(GameTime gameTime)
        {
            if (isClosing)
            {
                positionNewGame.X -= 15;
                positionScreen.X -= 15;
                if (positionScreen.X < -backgroundTexture.Width)
                {
                    isClosed = true;
                }
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) || Keyboard.GetState().IsKeyDown(Keys.Space))
                    isClosing = true;

                if (positionNewGame.X < 24)
                {
                    positionNewGame.X = (float)Math.Sqrt(i) - currNewGame.Width;
                    i = (i < 22000 + currNewGame.Width * currNewGame.Width) ? i + 500 : i;
                }
                //else
                {
                    var mouseState = Mouse.GetState();
                    if (mouseState.X > positionNewGame.X && mouseState.X < positionNewGame.X + currNewGame.Width &&
                        mouseState.Y > positionNewGame.Y && mouseState.Y < positionNewGame.Y + currNewGame.Height)
                    {
                        currNewGame = newGameTexture2;
                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            isClosing = true;
                        }
                    }
                    else
                    {
                        currNewGame = newGameTexture1;
                    }
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(gameField, Vector2.Zero, Color.White);
            spriteBatch.Draw(backgroundTexture, positionScreen, Color.White);
            spriteBatch.Draw(currNewGame, positionNewGame, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private static Texture2D gameField;
        private static Texture2D backgroundTexture;
        private static Texture2D currNewGame;
        private static Texture2D newGameTexture1;
        private static Texture2D newGameTexture2;

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            gameField = Game.Content.Load<Texture2D>("Images/game_field");
            backgroundTexture = Game.Content.Load<Texture2D>("Images/background");
            newGameTexture1 = Game.Content.Load<Texture2D>("Images/newGame1");
            newGameTexture2 = Game.Content.Load<Texture2D>("Images/newGame2");
            currNewGame = newGameTexture1;

            positionNewGame = new Vector2(-newGameTexture1.Width, 300);
            positionScreen = Vector2.Zero;
            base.LoadContent();
        }
    }
}