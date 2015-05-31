using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenceXNA.Sprites
{
    internal class ButtonSprite : Sprite
    { //ВРЕМЕННЫЙ КЛАСС
        private bool TowerUnderMouse = false;
        List<Sprite> spriteList = new List<Sprite>(); 
        /// <summary>
        /// Башня в HUD
        /// </summary>
        /// <param name="textureImage">Текстура</param>
        /// <param name="position">Положение</param>
        /// <param name="frameSize">Размер фрейма</param>
        /// <param name="collisionOffset">Смещение прямоугольника соприкосновения</param>
        /// <param name="currentFrame">Текущий фрейм</param>
        /// <param name="sheetSize">Количество фреймов в ширину/высоту</param>
        /// <param name="millisecondsPerFrame">Скорость анимации</param>
        public ButtonSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, List<Sprite> spriteList,
            int millisecondsPerFrame = defaultMillisecondsPerFrame)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
                Point.Zero, Vector2.Zero, 0.9f, millisecondsPerFrame)
        {
            this.spriteList = spriteList;
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
                currentFrame.X = 2;

            base.Update(gameTime, clientBounds);
        }

    }
}
