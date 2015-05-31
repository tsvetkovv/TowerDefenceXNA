using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TowerDefenceXNA.Sprites;

namespace TowerDefenceXNA.Towers
{
    public class DrowerIcons
    {

        private Vector2 iconPosition;
        private Point iconCurrentFrame;
        static Point iconFrameSize = new Point(40, 40);

        private MouseState prevMouseState;
        private MouseState mouseState;
        private Point currentIconFrame;
        private Point currentFrame;
        private Point portraitIndex;
        private int podiumIndex;
        private static Texture2D circle;
        int range;
        /// <summary>
        /// Рисование иконок в правом меню
        /// </summary>
        /// <param name="towerIndex">1 - bomb</param>
        public void DrowIcons(SpriteBatch spriteBatch, int towerIndex)
        {
            int range = 0;
            switch (towerIndex)
            {
                case 0:
                    {
                        iconPosition = new Vector2(489, 170);
                        iconCurrentFrame.Y = 0;
                        portraitIndex = new Point(0, 0);
                        podiumIndex = 0;
                        currentFrame = new Point(0, 0);
                        range = 75;
                        circle = Tower.CreateCircle(range);
                        if (iconCurrentFrame.X == 1)
                            Sentry.DrowStaticTable(spriteBatch);
                    }
                    break;
                case 1:
                    {
                        iconPosition = new Vector2(489, 217);
                        iconCurrentFrame.Y = 1;
                        portraitIndex = new Point(0, 1);
                        podiumIndex = 1;
                        currentFrame = new Point(0, 1);
                        range = 50;
                        circle = Tower.CreateCircle(range);
                        if (iconCurrentFrame.X == 1)
                            Bomb.DrowStaticTable(spriteBatch);
                    }
                    break;
                case 2: //Minigun
                    {
                        iconPosition = new Vector2(540, 170);
                        iconCurrentFrame.Y = 7;
                        portraitIndex = new Point(0, 7);
                        podiumIndex = 3;
                        currentFrame = new Point(0, 7);
                        range = 75;
                        circle = Tower.CreateCircle(range);
                        if (iconCurrentFrame.X == 1)
                            Minigun.DrowStaticTable(spriteBatch);
                    }
                    break;
            }

            if (iconCurrentFrame.X == 1)
                Sentry.DrowStaticTable(spriteBatch);
            spriteBatch.Draw(SpriteManager.towerPortraitsTexture, iconPosition,
                new Rectangle(iconCurrentFrame.X * iconFrameSize.X,
            iconCurrentFrame.Y * iconFrameSize.Y,
            iconFrameSize.X, iconFrameSize.Y),
                    Color.White, 0, Vector2.Zero,
                    1f, SpriteEffects.None, Tower.layerDepth);

            if (GlobalVars.onMouse && GlobalVars.towerIndexUnderMouse == towerIndex)
            {
                TowerUnderMouseSprite.Draw(spriteBatch, SpriteManager.towerSpriteSheetTexture, currentFrame, new Point(36, 36), Tower.layerDepth + 0.2f);
                TowerUnderMouseSprite.Draw(spriteBatch, SpriteManager.towerPodiumTexture, new Point(0, podiumIndex), new Point(30, 30), Tower.layerDepth + 0.15f);

                spriteBatch.Draw(circle, new Vector2(mouseState.X - range, mouseState.Y - range), null, Color.Red, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
            }
        }


        public void UpdateIcons(int towerIndex)
        {

            switch (towerIndex)
            {
                case 0:
                    {
                        iconPosition = new Vector2(489, 170);
                        portraitIndex = new Point(0, 0);
                        iconCurrentFrame.Y = 0;
                        podiumIndex = 0;
                        range = 75;
                        currentFrame = new Point(0, 0);

                        if (GlobalVars.money < Sentry.cost)
                        {
                            iconCurrentFrame.X = 2;
                            return;
                        }

                    }
                    break;
                case 1:
                    {
                        iconPosition = new Vector2(489, 217);
                        iconCurrentFrame.Y = 1;
                        portraitIndex = new Point(0, 1);
                        podiumIndex = 1;
                        currentFrame = new Point(0, 1);
                        range = 50;
                        if (GlobalVars.money < Bomb.cost)
                        {
                            iconCurrentFrame.X = 2;
                            return;
                        }
                    }
                    break;
                case 2:
                    {
                        iconPosition = new Vector2(540, 170);
                        iconCurrentFrame.Y = 7;
                        portraitIndex = new Point(0, 7);
                        podiumIndex = 3;
                        currentFrame = new Point(0, 7);
                        range = 75;

                        if (GlobalVars.money < Minigun.cost)
                        {
                            iconCurrentFrame.X = 2;
                            return;
                        }
                    }
                    break;

            }
            circle = Tower.CreateCircle(range);
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();

            if (new Rectangle(
                    (int)iconPosition.X,
                    (int)iconPosition.Y,
                    iconFrameSize.X,
                    iconFrameSize.Y).Contains(new Point(mouseState.X, mouseState.Y)))
            {
                iconCurrentFrame.X = 1;
                if (prevMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
                {
                    GlobalVars.onMouse = true;
                    GlobalVars.towerIndexUnderMouse = towerIndex;
                }
            }
            else
            {
                iconCurrentFrame.X = 0;
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    if (GlobalVars.onMouse && GlobalVars.towerIndexUnderMouse == towerIndex)
                        try
                        {
                            {
                                Vector2 pos = new Vector2(mouseState.X / 30 * 30 + 15, mouseState.Y / 30 * 30 + 15);
                                switch (towerIndex)
                                {
                                    case 0:
                                        if (FieldsTable.EasyMapSet(mouseState.X, mouseState.Y, 2))
                                        {
                                            GlobalVars.money -= Sentry.cost;
                                            SpriteManager.towerList.Add(new Sentry(pos));
                                        }
                                        break;
                                    case 1: if (FieldsTable.EasyMapSet(mouseState.X, mouseState.Y, 2))
                                        {
                                            GlobalVars.money -= Bomb.cost;
                                            SpriteManager.towerList.Add(new Bomb(pos));
                                        }
                                        break;
                                    case 2: if (FieldsTable.EasyMapSet(mouseState.X, mouseState.Y, 2))
                                        {
                                            GlobalVars.money -= Minigun.cost;
                                            SpriteManager.towerList.Add(new Minigun(pos));
                                        }
                                        break;
                                }
                                GlobalVars.onMouse = false;

                            }
                        }
                        catch (Exception)
                        {

                        }
                }
            }

            if (GlobalVars.onMouse)
            {
                TowerUnderMouseSprite.Update(portraitIndex);
            }
        }
    }
}