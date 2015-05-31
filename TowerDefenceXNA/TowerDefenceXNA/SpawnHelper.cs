using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TowerDefenceXNA.Sprites;
using TowerDefenceXNA.Zombies;

namespace TowerDefenceXNA
{
    public class SpawnHelper
    {
        private readonly List<Zombie> zombieList;
        private const double Koef = 2;
        private const int DelayBetweenWaves = 20000;
        private int _countCurrZombie;
        private static int typeOfZombie;
        private int _timeSinceLastFrame;

        public SpawnHelper()
        {
            zombieList = SpriteManager.zombieList;
            _countCurrZombie = 10;
        }


        public void GoWave()
        {
            //double modifer = Math.Truncate(GlobalVars.wave / 6D) * Koef + 1;
            double modifer = Math.Pow(Math.Truncate(GlobalVars.wave / 6D) + 1, 2);
            switch (typeOfZombie)
            {
                case 0:
                    {
                        if (GoNormalZombie(ref _countCurrZombie, modifer))
                        {
                            _countCurrZombie--;
                        }
                    }
                    break;
                case 1:
                    {
                        if (GoTorsoZombie(ref _countCurrZombie, modifer))
                        {
                            _countCurrZombie--;
                        }
                    }
                    break;
                case 2:
                    {
                        if (GoHellHound(ref _countCurrZombie, modifer))
                        {
                            _countCurrZombie--;
                        }
                    }
                    break;
                case 3:
                    {
                        if (GoFemaleZombie(ref _countCurrZombie, modifer))
                        {
                            _countCurrZombie--;
                        }
                    }
                    break;
                case 4:
                    {
                        if (GoStrongZombie(ref _countCurrZombie, modifer))
                        {
                            _countCurrZombie--;
                        }
                    }
                    break;
                case 5:
                    {
                        if (GoAbominationZombie(ref _countCurrZombie, modifer))
                        {
                            _countCurrZombie--;
                        }
                    }
                    break;
            }

            if (_countCurrZombie == 0) //смена снаряда
            {
                _timeSinceLastFrame += 16;

                if (_timeSinceLastFrame < DelayBetweenWaves && Keyboard.GetState().IsKeyUp(Keys.Space))
                    return;

                typeOfZombie++;
                switch (typeOfZombie)
                { //количество зомби в каждой волне
                    case 0: _countCurrZombie = 10; break;
                    case 1: _countCurrZombie = 10; break;
                    case 2: _countCurrZombie = 10; break;
                    case 3: _countCurrZombie = 6; break;
                    case 4: _countCurrZombie = 6; break;
                    case 5:
                        {
                            _countCurrZombie = 5;
                            typeOfZombie = 0; break;
                        }
                }
                _timeSinceLastFrame = 0;
                GlobalVars.wave++;
            }
        }

        private bool GoNormalZombie(ref int i, double modifer)
        {
            if (i > 0 && (zombieList.Count == 0 || !zombieList[zombieList.Count - 1].CollisionRect.Contains(0, 30)))
            {
                zombieList.Add(new NormalZombie(new Vector2(-30, 30), modifer));
                return true;
            }

            return false;
        }

        private bool GoFemaleZombie(ref int i, double modifer)
        {
            if (i > 0 && (zombieList.Count == 0 || !zombieList[zombieList.Count - 1].CollisionRect.Contains(0, 30)))
            {
                zombieList.Add(new FemaleZombie(new Vector2(-30, 30), modifer));
                return true;
            }

            return false;
        }

        private bool GoHellHound(ref int i, double modifer)
        {
            if (i > 0 && (zombieList.Count == 0 || !zombieList[zombieList.Count - 1].CollisionRect.Contains(0, 30)))
            {
                zombieList.Add(new HellHound(new Vector2(-30, 30), modifer));
                return true;
            }

            return false;
        }

        private bool GoTorsoZombie(ref int i, double modifer)
        {
            if (i > 0 && (zombieList.Count == 0 || !zombieList[zombieList.Count - 1].CollisionRect.Contains(0, 30)))
            {
                zombieList.Add(new TorsoZombie(new Vector2(-30, 30), modifer));
                return true;
            }

            return false;
        }

        private bool GoStrongZombie(ref int i, double modifer)
        {
            if (i > 0 && (zombieList.Count == 0 || !zombieList[zombieList.Count - 1].CollisionRect.Contains(0, 30)))
            {
                zombieList.Add(new StrongZombie(new Vector2(-30, 30), modifer));
                return true;
            }

            return false;
        }

        private bool GoAbominationZombie(ref int i, double modifer)
        {
            if (i > 0 && (zombieList.Count == 0 || !zombieList[zombieList.Count - 1].CollisionRect.Contains(0, 30)))
            {
                zombieList.Add(new AbominationZombie(new Vector2(-30, 30), modifer));
                return true;
            }

            return false;
        }
    }
}