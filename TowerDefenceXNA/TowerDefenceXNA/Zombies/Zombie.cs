using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefenceXNA.Sprites;

namespace TowerDefenceXNA.Zombies
{
    public abstract class Zombie : Sprite
    {
        protected string Name { get; set; }
        public int Health { get; set; }
        protected int MaxHealth;
        protected double Armor { get; set; }
        protected int Score { get; set; }
        protected int Bounty { get; set; }
        protected int Modifer { get; set; }
        protected bool isDead;
        public bool Remove;
        protected static Point frameSize = new Point(36, 36); //Размер спрайта зомби
        private readonly static Vector2 startPosition = gridToPixel(new Vector2(-1, 1));
        protected direction CurrentDirection = direction.Right;
        /// <summary>
        /// Пикселей в секунду
        /// </summary>
        protected int speed;

        protected Vector2 Speed;

        protected enum direction
        {
            Left,
            Right,
            Up,
            Down
        }

        //Конверторы пикселей в квадраты и наоборот
        static Point pixelToGrid(Point p)
        {
            return new Point(frameSize.X * p.X / frameSize.X + frameSize.X / 2, frameSize.Y * p.Y / frameSize.Y + frameSize.Y / 2);
        }
        static Point gridToPixel(Point p)
        {
            return new Point(frameSize.X * p.X, frameSize.Y * p.Y);
        }
        static Vector2 gridToPixel(Vector2 p)
        {
            return new Vector2(frameSize.X * p.X, frameSize.Y * p.Y);
        }

        private static Point currentFrame;
        private static Point sheetSize = new Point(4, 0);
        private static int collisionOffset;
        protected static float layerDepth = 0.3f; //глубина слоя всех зомби
        private static Texture2D textureZombie = SpriteManager.zombiesTexture;
        private static Texture2D textureHealthBar = SpriteManager.healthBarTexture;



        protected Zombie(Vector2 position) :
            base(textureZombie, position, frameSize, collisionOffset, currentFrame, sheetSize, new Vector2(1, 0), layerDepth, 400)
        {
            origin = new Vector2(currentFrame.X * frameSize.X + (frameSize.X / 2),
                currentFrame.Y * frameSize.Y + (frameSize.Y / 2));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!isDead)
            {
                spriteBatch.Draw(textureHealthBar, new Vector2(position.X + 5, position.Y),
                    sourceRectangle:
                        new Rectangle(0, 0, textureHealthBar.Width * Health / MaxHealth, textureHealthBar.Width),
                    layerDepth: layerDepth, color: Color.White, rotation: 0f, origin: Vector2.Zero, scale: Vector2.One,
                    effects: SpriteEffects.None);
            }
            base.Draw(gameTime, spriteBatch);
        }

        private Stopwatch sw = new Stopwatch();
        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            #region Направление
            switch (CurrentDirection)
            {
                case direction.Right:
                    {
                        if (FieldsTable.EasyMapGet((int)position.X + frameSize.X / 2, (int)position.Y) == 1)
                        {
                            base.speed.X = speed;
                            angle = 0;
                        }
                        else
                        {
                            base.speed.X = 0;
                            if (FieldsTable.EasyMapGet((int)position.X, (int)position.Y + frameSize.Y / 2) == 1)
                            {
                                CurrentDirection = direction.Down;
                            }
                            else
                            {
                                CurrentDirection = direction.Up;
                            }
                        }
                    }
                    break;

                case direction.Left:
                    {
                        if (FieldsTable.EasyMapGet((int) position.X - frameSize.X/2, (int) position.Y) == -1)
                        { //финиширует только по этому направлению, т.к. уровень один
                            Remove = true;
                            return;
                        }
                        if (FieldsTable.EasyMapGet((int)position.X - frameSize.X / 2, (int)position.Y) == 1)
                        {
                            base.speed.X = -speed;
                            angle = (float)Math.PI;
                        }
                        else
                        {
                            base.speed.X = 0;
                            if (FieldsTable.EasyMapGet((int)position.X, (int)position.Y + frameSize.Y / 2) == 1)
                            {
                                CurrentDirection = direction.Down;
                            }
                            else
                            {
                                CurrentDirection = direction.Up;
                            }
                        }
                    }
                    break;

                case direction.Down:
                    {
                        if (FieldsTable.EasyMapGet((int)position.X, (int)position.Y + frameSize.Y / 2) == 1)
                        {
                            base.speed.Y = speed;
                            angle = (float)Math.PI / 2;
                        }
                        else
                        {
                            base.speed.Y = 0;
                            if (FieldsTable.EasyMapGet((int)position.X + frameSize.X, (int)position.Y) == 1)
                            {
                                CurrentDirection = direction.Right;
                            }
                            else
                            {
                                CurrentDirection = direction.Left;
                            }
                        }
                    }
                    break;

                case direction.Up:
                    {
                        if (FieldsTable.EasyMapGet((int)position.X, (int)position.Y - frameSize.Y / 2) == 1)
                        {
                            base.speed.Y = -speed;
                            angle = -(float)Math.PI / 2;

                        }
                        else
                        {
                            base.speed.Y = 0;
                            if (
                                FieldsTable.EasyMapGet((int)position.X + frameSize.X,
                                    (int)position.Y - FieldsTable.cellTable.Y / 4) == 1)
                            {
                                CurrentDirection = direction.Right;
                            }
                            else
                            {
                                CurrentDirection = direction.Left;
                            }
                        }
                    }
                    break;
            }
            #endregion

            if (Health <= 0)
            {
                if (!isDead)
                {
                    GlobalVars.money += Bounty;
                    GlobalVars.score += Score;
                }
                isDead = true;
                Speed = Vector2.Zero;
                base.layerDepth = 0.01f;
                if (base.currentFrame.X < 4)
                    base.currentFrame.X = 4;

                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > 500)
                {
                    timeSinceLastFrame = 0;
                    base.currentFrame.X++;
                    if (base.currentFrame.X > 5)
                        Remove = true;
                }
            }
            else
                base.Update(gameTime, clientBounds);
        }
    }
}