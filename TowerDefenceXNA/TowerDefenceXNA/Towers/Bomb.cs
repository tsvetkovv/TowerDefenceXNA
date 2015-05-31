using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefenceXNA.Sprites;

namespace TowerDefenceXNA.Towers
{
    public class Bomb : Tower
    {
        private string name = "Cannot";
        private int range = 50;
        private double towerSpeed = 0.5;
        private int damage = 8;
        private int splashDamage = 2;
        private int splashRange = 20;
        private int bulletSpeed = 5;
        public static int cost = 14;
        private int upgradeCost = 28;
        private bool ground = true;
        private bool air = false;

        static Point currentFrame = new Point(0, 1);
        static Point portraitIndex = new Point(0, 1);
        static int podiumIndex = 1;

        public Bomb(Vector2 position) :
            base(position, portraitIndex, podiumIndex, currentFrame)
        {
            Level = 1;
            Name = string.Format("{0} {1} lvl", name, Level);
            Range = range;
            TowerSpeed = (1 / towerSpeed) * 1000;
            Damage = damage;
            SplashDamage = splashDamage;
            SplashRange = splashRange;
            Cost = cost;
            SpeedText = string.Format("{0} per/sec", towerSpeed);
            UpgradeCost = upgradeCost;
            UpgradeCostText = string.Format("Upgrade cost: {0}\nPress 'U' to Upgrade", upgradeCost);
            Ground = ground;
            Air = air;
            timeLastUpdate = 0;
            BulletSpeed = bulletSpeed;
            circle = CreateCircle(range);
        }

        public override void Upgrade()
        {
            if (Level == 5 || GlobalVars.money - UpgradeCost < 0)
                return;
            Level++;
            GlobalVars.money -= UpgradeCost;
            Name = string.Format("{0} {1} lvl", name, Level);
            Range += 10;
            base.currentFrame = new Point(Level - 1, 1);
            circle = CreateCircle(Range);
            towerSpeed += 0.05;
            TowerSpeed = (1 / towerSpeed) * 1000;
            SplashRange += 5;

            switch (Level)
            {
                case 2:
                    {
                        Damage = 16;
                        SplashDamage = 4;
                        Cost += 28;
                        UpgradeCost = 55;
                    } break;
                case 3:
                    {
                        Damage = 32;
                        SplashDamage = 8;
                        Cost += 55;
                        UpgradeCost = 86;
                    } break;
                case 4:
                    {
                        Damage = 56;
                        SplashDamage = 18;
                        Cost += 86;
                        UpgradeCost = 148;
                    } break;

                case 5:
                    {
                        Damage = 90;
                        SplashDamage = 35;
                        Cost += 148;
                        UpgradeCost = 0;
                    } break;
            }

            UpgradeCostText = string.Format("Upgrade cost: {0}\nPress 'U' to Upgrade", UpgradeCost);

            if (UpgradeCost == 0)
                UpgradeCostText = "Max upgrade";
        }


        public static void DrowStaticTable(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tableBackgroundTower,
    new Vector2(480, 10), null, Color.White, 0, Vector2.Zero, 0.8f, SpriteEffects.None, 0.95f);
            spriteBatch.DrawString(SpriteManager.myFont, string.Format("Name: {0}\nRange: {1}\nDamage: {2}\nSpeed: {3}\nCost: {4}",
                "Cannot", 50, 8, "0,5 per/sec", 14), new Vector2(490, 20), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.96f);
        }
    }
}