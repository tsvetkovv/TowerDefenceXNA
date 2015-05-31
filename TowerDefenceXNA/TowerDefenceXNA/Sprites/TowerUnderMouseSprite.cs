using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TowerDefenceXNA.Sprites
{
    public class TowerUnderMouseSprite : Sprite
    {
        public static int range = 0;
        List<Sprite> spriteList = new List<Sprite>();
        /// <summary>
        /// Спрайт башни под мышью
        /// </summary>
        /// <param name="textureImage">Текстура</param>
        /// <param name="position">Положение</param>
        /// <param name="frameSize">Размер фрейма</param>
        /// <param name="currentFrame">Текущий фрейм</param>
        /// <param name="sheetSize">Количество фреймов в ширину/высоту</param>
        /// <param name="millisecondsPerFrame">Скорость анимации</param>
        public TowerUnderMouseSprite(Texture2D textureImage,
            Point frameSize, Point currentFrame, List<Sprite> spriteList,
            int millisecondsPerFrame = defaultMillisecondsPerFrame)
            : base(textureImage, position, frameSize, 0, currentFrame,
                Point.Zero, Vector2.Zero, 0.05f)
        {
            this.spriteList = spriteList;
        }

        private static Vector2 position = Vector2.Zero;
        public static void Update(Point currentFrame)
        {
            var StateMouse = Mouse.GetState();
            position.X = StateMouse.X;
            position.Y = StateMouse.Y;
        }

        private static float layerDepth = 0.8f;
        public static void Draw(SpriteBatch spriteBatch, Texture2D textureImage, Point currentFrame, Point frameSize, float layerDepth)
        {
            position.X -= frameSize.X / 2;
            position.Y -= frameSize.Y / 2;
            spriteBatch.Draw(textureImage,
    position,
    new Rectangle(currentFrame.X * frameSize.X,
        currentFrame.Y * frameSize.Y,
        frameSize.X, frameSize.Y),
        Color.White, 0, Vector2.Zero,
        1f, SpriteEffects.None, layerDepth);


            position.X += frameSize.X / 2;
            position.Y += frameSize.Y / 2;
        }
    }
}
