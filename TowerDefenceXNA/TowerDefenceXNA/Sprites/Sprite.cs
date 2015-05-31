using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenceXNA.Sprites
{
    public abstract class Sprite
    {
        protected Texture2D textureImage;
        protected Point frameSize;
        protected Point currentFrame;
        protected Point sheetSize;
        private int collisionOffset;
        /// <summary>
        /// счётчик для ожидания отрисовки фрейма
        /// </summary>
        protected int timeSinceLastFrame = 0;
        /// <summary>
        /// Скорость анимации (раз в N мс)
        /// </summary>
        protected int millisecondsPerFrame;
        /// <summary>
        /// Скорость анимации по-умолчанию (раз в n мс)
        /// </summary>
        protected const int defaultMillisecondsPerFrame = 16;

        protected float angle = 0;
        /// <summary>
        /// Центр поворота
        /// </summary>
        protected Vector2 origin = Vector2.Zero;

        protected Vector2 speed;
        /// <summary>
        /// Позиция спрайта
        /// </summary>
        protected Vector2 position;

        protected float layerDepth;

        /// <summary>
        /// Базовый класс спрайта
        /// </summary>
        /// <param name="textureImage">Текстура</param>
        /// <param name="position">Положение</param>
        /// <param name="frameSize">Размер фрейма</param>
        /// <param name="collisionOffset">Смещение прямоугольника соприкосновения</param>
        /// <param name="currentFrame">Текущий фрейм</param>
        /// <param name="sheetSize">Количество фреймов в ширину/высоту</param>
        /// <param name="speed">Скорость изменения положения</param>
        /// <param name="millisecondsPerFrame">Скорость анимации</param>
        protected Sprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, float layerDepth = 0f,
            int millisecondsPerFrame = 16)
        {
            this.textureImage = textureImage;
            this.position = position;
            this.frameSize = frameSize;
            this.collisionOffset = collisionOffset;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.speed = speed;
            this.layerDepth = layerDepth;
            this.millisecondsPerFrame = millisecondsPerFrame;
        }


        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {
            if (sheetSize != Point.Zero)
            {
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > millisecondsPerFrame)
                {
                    timeSinceLastFrame = 0;
                    ++currentFrame.X;
                    if (currentFrame.X >= sheetSize.X)
                    {
                        currentFrame.X = 0;
                    }
                }
            }
            if (speed != Vector2.Zero)
                position += (speed / 60);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureImage,
                position + origin,
                new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y,
                    frameSize.X, frameSize.Y),
                    Color.White, angle, origin,
                    1f, SpriteEffects.None, layerDepth);
        }

        public Rectangle CollisionRect
        {
            get
            {
                return new Rectangle(
                    (int)position.X + collisionOffset,
                    (int)position.Y + collisionOffset,
                    frameSize.X - (collisionOffset * 2),
                    frameSize.Y - (collisionOffset * 2));
            }
        }

    }
}
