using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TowerDefenceXNA.Sprites
{
    internal class TowerOnField : Sprite
    {
        List<Sprite> spriteList; 
        /// <summary>
        /// Спрайт башни под мышью
        /// </summary>
        /// <param name="textureImage">Текстура</param>
        /// <param name="position">Положение</param>
        /// <param name="frameSize">Размер фрейма</param>
        /// <param name="currentFrame">Текущий фрейм</param>
        /// <param name="sheetSize">Количество фреймов в ширину/высоту</param>
        /// <param name="millisecondsPerFrame">Скорость анимации</param>
        public TowerOnField(Texture2D textureImage, Vector2 position,
            Point frameSize, Point currentFrame, List<Sprite> spriteList,
            int millisecondsPerFrame = defaultMillisecondsPerFrame)
            : base(textureImage, position, frameSize, 0, currentFrame,
                Point.Zero, Vector2.Zero, 0.1f)
        {
            this.spriteList = spriteList;
        }


        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            position.X = Mouse.GetState().X - 20;
            position.Y = Mouse.GetState().Y - 20;
            currentFrame.X = CollisionRect.Contains(new Point((int)position.X, (int)position.Y)) ? 1 : 0;

            base.Update(gameTime, clientBounds);
        }
    }
}
