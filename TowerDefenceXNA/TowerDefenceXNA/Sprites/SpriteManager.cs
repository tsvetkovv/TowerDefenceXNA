using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TowerDefenceXNA.Towers;
using TowerDefenceXNA.Zombies;

namespace TowerDefenceXNA.Sprites
{
    public partial class SpriteManager : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;

        readonly List<Sprite> spriteList = new List<Sprite>();
        public static List<Tower> towerList = new List<Tower>();
        public static List<Zombie> zombieList = new List<Zombie>();
        DrowerIcons DI = new DrowerIcons();
        DrowerIcons DI1 = new DrowerIcons();
        DrowerIcons DI2 = new DrowerIcons();
        private KeyboardState prevKeyboardState;
        private MouseState mouseState;
        private SpawnHelper spawnHelper;
        public SpriteManager(Game game)
            : base(game)
        {
            this.Initialize();
            spawnHelper = new SpawnHelper();
        }

        public override void Update(GameTime gameTime)
        {
            //Обновление всех спрайтов из списка
            for (int index = 0; index < spriteList.Count; index++)
            {
                Sprite s = spriteList[index];
                s.Update(gameTime, Game.Window.ClientBounds);
            }

            var curentKeyState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            Point mousePos = new Point(mouseState.X, mouseState.Y);
            foreach (Tower tower in towerList)
            {
                if (tower.CollisionRect.Contains(mousePos) && !GlobalVars.onMouse)
                    if (prevKeyboardState.IsKeyUp(Keys.U) && curentKeyState.IsKeyDown(Keys.U))
                        tower.Upgrade();

                tower.Update(gameTime, Game.Window.ClientBounds);
            }

            spawnHelper.GoWave();
            DI.UpdateIcons(0);
            DI1.UpdateIcons(1);
            DI2.UpdateIcons(2);


            for (int i = 0; i < zombieList.Count; i++)
            {
                zombieList[i].Update(gameTime, Game.Window.ClientBounds);
                if (zombieList[i].Remove && zombieList[i].Health <= 0)
                    zombieList.RemoveAt(i);
                else
                    if (zombieList[i].Remove && zombieList[i].Health > 0)
                    {
                        zombieList.RemoveAt(i);
                        GlobalVars.lives--;
                    }

            }

            if (prevKeyboardState.IsKeyUp(Keys.Up) && curentKeyState.IsKeyDown(Keys.Up))
            {
                if ((Game1.ElapsedTime - TimeSpan.FromMilliseconds(4)).Milliseconds > 0) Game1.ElapsedTime -= TimeSpan.FromMilliseconds(4);
            }
            if (prevKeyboardState.IsKeyUp(Keys.Down) && curentKeyState.IsKeyDown(Keys.Down))
            {
                if ((Game1.ElapsedTime + TimeSpan.FromMilliseconds(4)).Milliseconds <= 16) Game1.ElapsedTime += TimeSpan.FromMilliseconds(4);
            }
            if (prevKeyboardState.IsKeyUp(Keys.NumPad0) && curentKeyState.IsKeyDown(Keys.NumPad0))
            {
                Game1.ElapsedTime = TimeSpan.FromMilliseconds(16);
            }

            prevKeyboardState = Keyboard.GetState();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            //Отрисовываем все спрайты
            foreach (Sprite s in spriteList)
            {
                s.Draw(gameTime, spriteBatch);
            }
            foreach (var tower in towerList)
            {
                tower.Draw(gameTime, spriteBatch);
            }

            foreach (Zombie zombie in zombieList)
            {
                zombie.Draw(gameTime, spriteBatch);
            }

            #region Рисование текста


            spriteBatch.DrawString(myFont2, GlobalVars.money.ToString(), new Vector2(560, 75), Color.Black, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(myFont2, GlobalVars.lives.ToString(), new Vector2(560, 97), Color.Black, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(myFont2, (GlobalVars.wave).ToString(), new Vector2(560, 119), Color.Black, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(myFont2, GlobalVars.score.ToString(), new Vector2(560, 140), Color.Black, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);

            #endregion

            DI.DrowIcons(spriteBatch, 0);
            DI1.DrowIcons(spriteBatch, 1);
            DI2.DrowIcons(spriteBatch, 2);
            spriteBatch.End();

            base.Draw(gameTime);
        }


        public Texture2D hudTexture;
        public Texture2D easyMapTexture;
        public Texture2D targetButtonTexture;
        public Texture2D zombiePortraitsTexture;
        public static Texture2D towerPortraitsTexture;
        public static Texture2D towerSpriteSheetTexture;
        public static Texture2D zombiesTexture;
        public static Texture2D towerPodiumTexture;
        public static SpriteFont myFont;
        public static SpriteFont myFont2;
        public static Texture2D healthBarTexture;
        public static Texture2D tableBackgroundTower;
        public static Texture2D FireTexture;

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            hudTexture = Game.Content.Load<Texture2D>("Images/hud");
            easyMapTexture = Game.Content.Load<Texture2D>("Images/easy_map");
            towerPortraitsTexture = Game.Content.Load<Texture2D>("Images/tower_portraits");
            towerSpriteSheetTexture = Game.Content.Load<Texture2D>("Images/tower_spritesheet");
            zombiesTexture = Game.Content.Load<Texture2D>("Images/zombies");
            towerPodiumTexture = Game.Content.Load<Texture2D>("Images/tower_podium");
            healthBarTexture = Game.Content.Load<Texture2D>("Images/health_bar");
            tableBackgroundTower = Game.Content.Load<Texture2D>("Images/table_background_tower");
            FireTexture = Game.Content.Load<Texture2D>("Images/fire");
            myFont = Game.Content.Load<SpriteFont>("font");
            myFont2 = Game.Content.Load<SpriteFont>("font2");
            #region Добавление спрайтов башен

            //ButtonSprite sentry = new ButtonSprite(towerPortraitsTexture, new Vector2(489, 170), new Point(40, 40), 0,
            //new Point(0, 0), spriteList);
            //Bomb bomb = new Bomb(towerPortraitsTexture, new Vector2(489, 217), new Point(40, 40), 0,
            //new Point(0, 1), spriteList);
            ButtonSprite sam = new ButtonSprite(towerPortraitsTexture, new Vector2(489, 264), new Point(40, 40), 0,
                new Point(0, 2), spriteList);
            ButtonSprite sensor = new ButtonSprite(towerPortraitsTexture, new Vector2(489, 311), new Point(40, 40), 0,
                new Point(0, 3), spriteList);
            //ButtonSprite rapid = new ButtonSprite(towerPortraitsTexture, new Vector2(540, 170), new Point(40, 40), 0,
            //    new Point(0, 7), spriteList);
            ButtonSprite buzzsaw = new ButtonSprite(towerPortraitsTexture, new Vector2(540, 217), new Point(40, 40), 0,
                new Point(0, 6), spriteList);
            ButtonSprite ice = new ButtonSprite(towerPortraitsTexture, new Vector2(540, 264), new Point(40, 40), 0,
                new Point(0, 4), spriteList);
            ButtonSprite magnet = new ButtonSprite(towerPortraitsTexture, new Vector2(540, 311), new Point(40, 40), 0,
                new Point(0, 11), spriteList);
            ButtonSprite laser = new ButtonSprite(towerPortraitsTexture, new Vector2(591, 170), new Point(40, 40), 0,
                new Point(0, 8), spriteList);
            ButtonSprite fire = new ButtonSprite(towerPortraitsTexture, new Vector2(591, 217), new Point(40, 40), 0,
                new Point(0, 5), spriteList);
            ButtonSprite lightning = new ButtonSprite(towerPortraitsTexture, new Vector2(591, 264), new Point(40, 40), 0,
                new Point(0, 9), spriteList);
            ButtonSprite corpx = new ButtonSprite(towerPortraitsTexture, new Vector2(591, 311), new Point(40, 40), 0,
                new Point(0, 10), spriteList);
            //spriteList.Add(sentry);
            //spriteList.Add(bomb);
            spriteList.Add(sam);
            spriteList.Add(sensor);
            //spriteList.Add(rapid);
            spriteList.Add(buzzsaw);
            spriteList.Add(ice);
            spriteList.Add(magnet);
            spriteList.Add(laser);
            spriteList.Add(fire);
            spriteList.Add(lightning);
            spriteList.Add(corpx);

            //towerList.Add();
            #endregion

            Point hudSheetSize = new Point(3, 12);
            Point hudSizeFrame = new Point(40, 40);
            StaticSprite hud = new StaticSprite(hudTexture, new Vector2(480, 0), new Point(160, 480), 0, new Point(0, 0), new Point(0, 0), 0.1f);
            StaticSprite map = new StaticSprite(easyMapTexture, Vector2.Zero, new Point(480, 480), 0, Point.Zero, Point.Zero, 0f);
            spriteList.Add(hud);
            spriteList.Add(map);
            base.LoadContent();
        }
    }
}
