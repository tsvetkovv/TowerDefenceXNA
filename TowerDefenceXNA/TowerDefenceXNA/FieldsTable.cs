using System;
using Microsoft.Xna.Framework;

namespace TowerDefenceXNA
{
    public struct FieldsTable
    {
        public static readonly Point cellTable = new Point(30, 30);

        /// <summary>
        /// 1 - путь
        /// 2 - башня
        /// </summary>
        public static int[,] easyMap = new int[16, 16]
        {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0},
            {0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0},
            {0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 0, 1, 0, 0, 1, 0},
            {0, 0, 1, 1, 1, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0},
            {0, 0, 1, 0, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0},
            {0, 0, 1, 0, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0},
            {0, 0, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 1, 0},
            {0, 0, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 1, 1, 1, 0},
            {0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 1, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0},
            {1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
            {0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        };

        /// <summary>
        /// Получает значение поля
        /// </summary>
        /// <param name="x">пиксели</param>
        /// <param name="y">пиксели</param>
        /// <returns></returns>
        public static int EasyMapGet(int x, int y)
        {
            int GridX = (int)Math.Round((double)x / cellTable.X);
            int GridY = (int)Math.Round((double)y / cellTable.Y);
            if (GridX < 0 || GridX >= easyMap.GetLength(1) || GridY < 0 || GridY >= easyMap.GetLength(0))
                return -1;
            return easyMap[GridY, GridX];

        }
        /// <summary>
        /// Задание значения полю
        /// </summary>
        /// <param name="x">пиксели</param>
        /// <param name="y">пиксели</param>
        /// <param name="value">значение</param>
        public static bool EasyMapSet(int x, int y, int value)
        {
            {
                int GridX = x / cellTable.X;
                int GridY = y / cellTable.Y;
                if (easyMap[GridY, GridX] == 0)
                {
                    easyMap[GridY, GridX] = value;
                    return true;
                }
                return false;
            }
        }
    }
}