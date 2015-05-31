using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TowerDefenceXNA.Sprites;
using TowerDefenceXNA.Zombies;

namespace TowerDefenceXNA.Towers
{
    public abstract class Tower : Sprite
    {
        protected static bool onMouse = false;
        protected Point PortraitIndex { get; set; }
        protected int PodiumIndex { get; set; }
        protected string Name { get; set; }
        protected string SpeedText { get; set; }
        protected int Range { get; set; }
        protected int Damage { get; set; }
        protected int SplashDamage { get; set; }
        protected int SplashRange { get; set; }
        protected double TowerSpeed { get; set; }
        protected int BulletSpeed { get; set; }
        protected int Level { get; set; }
        public int Cost { get; set; }
        protected string UpgradeCostText { get; set; }
        protected int UpgradeCost { get; set; }

        protected bool Ground { get; set; }
        protected bool Air { get; set; }

        private bool showFire;
        private bool showTowerTable;
        private TimeSpan timeSpan;
        protected int timeLastUpdate;
        protected Zombie currentTarget;
        protected static Texture2D towerTexture = SpriteManager.towerSpriteSheetTexture;
        protected static Texture2D podiumTexture = SpriteManager.towerPodiumTexture;
        protected static Texture2D tableBackgroundTower = SpriteManager.tableBackgroundTower;

        protected static Point frameSize = new Point(36, 36); //Размер спрайта башни
        protected static Point podiumFrameSize = new Point(30, 30);
        protected Point currentFrame;
        private int _timeSinceLastFrame;
        private int _fireCounter;
        private static int collisionOffset = 0;
        public static float layerDepth = 0.7f; //ГЛУБИНА СЛОЯ ВСЕХ БАШЕН
        private int podiumIndex;
        protected Texture2D circle;
        private float angle; //угол поворота орудия

        private MouseState mouseState;
        private Point mousePos;
        private KeyboardState keyState;
        private KeyboardState prevKeyState = Keyboard.GetState();

        protected Tower(Vector2 position, Point portraitIndex, int podiumIndex, Point currentFrame) :
            base(podiumTexture, new Vector2(position.X - podiumFrameSize.X / 2, position.Y - podiumFrameSize.Y / 2), podiumFrameSize, collisionOffset, new Point(0, podiumIndex), Point.Zero, Vector2.Zero, layerDepth)
        //в базовый класс передаётся рисование подиума
        {
            PortraitIndex = portraitIndex;
            this.currentFrame = currentFrame;
            GlobalVars.money -= Cost;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //рисование позции мыши в углу
            //spriteBatch.DrawString(SpriteManager.myFont, string.Format("{0}, {1}", mouseState.X, mouseState.Y), new Vector2(490, 460), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.96f);
            //рисование башни
            spriteBatch.Draw(towerTexture,
                new Vector2(position.X + podiumFrameSize.X / 2, position.Y + podiumFrameSize.Y / 2),
                new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y,
                    frameSize.X, frameSize.Y),
                    Color.White, angle, new Vector2(frameSize.X / 2, frameSize.Y / 2),
                    1f, SpriteEffects.None, layerDepth + 0.1f);

            if (showFire)
                spriteBatch.Draw(SpriteManager.FireTexture,
                    new Vector2(position.X + podiumFrameSize.X / 2, position.Y + podiumFrameSize.Y / 2), null, Color.White, angle, new Vector2(SpriteManager.FireTexture.Width / 2, SpriteManager.FireTexture.Height / 2), new Vector2(1 + Level / 5.0), SpriteEffects.None, 0.961f);

            //рисование таблички характеристик
            if (showTowerTable)
            {
                spriteBatch.Draw(tableBackgroundTower,
                    new Vector2(480, 10), null, Color.White, 0, Vector2.Zero, 0.8f, SpriteEffects.None, 0.95f);
                spriteBatch.DrawString(SpriteManager.myFont, string.Format("Name: {0}\nRange: {1}\nDamage: {2}\nSpeed: {3}\n{4}",
                    Name, Range, Damage, SpeedText, UpgradeCostText), new Vector2(490, 20), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.96f);

                spriteBatch.Draw(circle, new Vector2(position.X + podiumFrameSize.X / 2 - Range, position.Y + podiumFrameSize.Y / 2 - Range), null, Color.Red, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
            }


            base.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            #region Нанесение урона
            //позиция центра башни
            Vector2 centerPos = new Vector2(position.X + podiumFrameSize.X / 2, position.Y + podiumFrameSize.Y / 2);

            //общее количество миллисекунд с начала суток
            _timeSinceLastFrame += 16;

            #region Если цель не захвачена
            if (currentTarget == null)
            {
                for (int i = 0; i < SpriteManager.zombieList.Count; i++)
                {
                    var distance = Vector2.Distance(centerPos,
                        new Vector2(SpriteManager.zombieList[i].CollisionRect.Center.X,
                     SpriteManager.zombieList[i].CollisionRect.Center.Y));

                    if (distance < Range && SpriteManager.zombieList[i].Health >= 0)
                    {
                        currentTarget = SpriteManager.zombieList[i];
                        break;
                    }
                }
            }

            #endregion

            #region Если цель захвачена
            if (currentTarget != null)
            {
                var direction =
                    (new Vector2(currentTarget.CollisionRect.Center.X, currentTarget.CollisionRect.Center.Y) - centerPos);
                angle = (float)Math.Atan2(direction.Y, direction.X) + MathHelper.Pi;

                float distance = Vector2.Distance(centerPos,
                    new Vector2(currentTarget.CollisionRect.Center.X, currentTarget.CollisionRect.Center.Y));

                if (distance < Range && _timeSinceLastFrame >= TowerSpeed &&
                    currentTarget.Health >= 0)
                {
                    _timeSinceLastFrame = 0;
                    if (currentTarget.Health >= 0)
                    {
                        if (_fireCounter == 0)
                            showFire = true;
                        currentTarget.Health -= Damage;

                        #region Splash

                        if (SplashDamage != 0)
                        {
                            for (int i = 0; i < SpriteManager.zombieList.Count; i++)
                            {
                                var distanceBetweenZombies =
                                    Vector2.Distance(
                                        new Vector2(currentTarget.CollisionRect.Center.X,
                                            currentTarget.CollisionRect.Center.Y),
                                        new Vector2(SpriteManager.zombieList[i].CollisionRect.Center.X,
                                            SpriteManager.zombieList[i].CollisionRect.Center.Y));

                                if (distanceBetweenZombies < SplashRange)
                                {
                                    if (currentTarget == SpriteManager.zombieList[i])
                                        continue;
                                    SpriteManager.zombieList[i].Health -= SplashDamage;
                                }
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        currentTarget = null;
                    }
                }
                else
                {
                    currentTarget = null;
                }
            }
            #endregion
            #endregion

            #region Огонь

            if (showFire)
            {
                if (_fireCounter == 5)
                {
                    showFire = false;
                    _fireCounter = 0;
                }
                else
                {
                    _fireCounter++;
                }
            }

            #endregion

            #region Табличка характеристик башни

            mouseState = Mouse.GetState();
            mousePos = new Point(mouseState.X, mouseState.Y);

            if (CollisionRect.Contains(mousePos) && !onMouse)
            {
                showTowerTable = true;
            }
            else
                showTowerTable = false;


            #endregion


            base.Update(gameTime, clientBounds);
        }

        public abstract void Upgrade();


        //создание текстуры круга
        public static Texture2D CreateCircle(int radius)
        {
            int outerRadius = radius * 2 + 2; // So circle doesn't go out of bounds
            Texture2D texture = new Texture2D(Game1.AgagDevice, outerRadius, outerRadius);

            Color[] data = new Color[outerRadius * outerRadius];

            // Colour the entire texture transparent first.
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.Transparent;

            // Work out the minimum step necessary using trigonometry + sine approximation.
            double angleStep = 2f / radius;

            for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
            {
                // Use the parametric definition of a circle: http://en.wikipedia.org/wiki/Circle#Cartesian_coordinates
                int x = (int)Math.Round(radius + radius * Math.Cos(angle));
                int y = (int)Math.Round(radius + radius * Math.Sin(angle));

                data[y * outerRadius + x] = Color.Red;
            }

            texture.SetData(data);
            return texture;
        }
    }
}