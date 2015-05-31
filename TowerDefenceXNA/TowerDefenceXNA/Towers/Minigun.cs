using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefenceXNA.Sprites;

namespace TowerDefenceXNA.Towers
{
    public class Minigun : Tower
    {
        private string name = "Minigun";
        private int range = 75;
        private double towerSpeed = 1.5;
        private int damage = 6;
        private int splashDamage = 0;
        private int splashRange = 0;
        private int bulletSpeed = 5;
        public static int cost = 35;
        private int upgradeCost = 68;
        private bool ground = true;
        private bool air = true;

        static Point currentFrame = new Point(0, 7);
        static Point portraitIndex = new Point(0, 7);
        static int podiumIndex = 3;

        public Minigun(Vector2 position) :
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
            GlobalVars.money -= UpgradeCost;
            Level++;
            Name = string.Format("{0} {1} lvl", name, Level);
            base.currentFrame.X = Level - 1;
            circle = CreateCircle(Range);

            switch (Level)
            {
                case 2:
                    {
                        Range += 5;
                        Damage = 12;
                        towerSpeed = 2.5;
                        SpeedText = string.Format("{0} per/sec", towerSpeed);
                        TowerSpeed = (1 / towerSpeed) * 1000;
                        Cost += 68;
                        UpgradeCost = 108;
                    } break;
                case 3:
                    {
                        Range += 5;
                        Damage = 19;
                        towerSpeed = 3.5;
                        SpeedText = string.Format("{0} per/sec", towerSpeed);
                        TowerSpeed = (1 / towerSpeed) * 1000;
                        Cost += 108;
                        UpgradeCost = 160;
                    } break;
                case 4:
                    {
                        Range += 5;
                        Damage = 27;
                        towerSpeed = 4.75;
                        SpeedText = string.Format("{0} per/sec", towerSpeed);
                        TowerSpeed = (1 / towerSpeed) * 1000;
                        Cost += 160;
                        UpgradeCost = 210;
                    } break;

                case 5:
                    {
                        Range += 10;
                        Damage = 36;
                        towerSpeed = 6;
                        SpeedText = string.Format("{0} per/sec", towerSpeed);
                        TowerSpeed = (1 / towerSpeed) * 1000;
                        Cost += 210;
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
                "Minigun", 75, 6, "1,5 per/sec", 35), new Vector2(490, 20), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.96f);
        }
    }
}