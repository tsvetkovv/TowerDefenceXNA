using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenceXNA
{
    public class EndScreen : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private bool isClosing;
        public bool isClosed;

        public EndScreen(Game game)
            : base(game)
        {
            Initialize();

        }


        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(gameoverTexture, Vector2.Zero, Color.White);
            spriteBatch.DrawString(myFont, string.Format("Your score: {0}" +
                                                         "\nWaves: {1}", GlobalVars.score, GlobalVars.wave),
                                                         new Vector2(230, 320), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private static Texture2D gameoverTexture;
        private static SpriteFont myFont;

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            gameoverTexture = Game.Content.Load<Texture2D>("Images/gameover");
            myFont = Game.Content.Load<SpriteFont>("font3");

            base.LoadContent();
        }
    }
}