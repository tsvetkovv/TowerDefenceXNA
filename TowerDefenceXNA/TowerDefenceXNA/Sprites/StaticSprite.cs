using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenceXNA.Sprites
{
    internal class StaticSprite : Sprite
    {
        /// <summary>
        /// Управляемый компьюетром спрайт
        /// </summary>
        /// <param name="textureImage">Текстура</param>
        /// <param name="position">Положение</param>
        /// <param name="frameSize">Размер фрейма</param>
        /// <param name="collisionOffset">Смещение прямоугольника соприкосновения</param>
        /// <param name="currentFrame">Текущий фрейм</param>
        /// <param name="sheetSize">Количество фреймов в ширину/высоту</param>
        /// <param name="layerDepth">Глубина слоя</param>
        /// <param name="millisecondsPerFrame">Скорость анимации</param>
        public StaticSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize, float layerDepth,
            int millisecondsPerFrame = defaultMillisecondsPerFrame)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
                sheetSize, Vector2.Zero, layerDepth, millisecondsPerFrame)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            base.Update(gameTime, clientBounds);
        }
    }
}
