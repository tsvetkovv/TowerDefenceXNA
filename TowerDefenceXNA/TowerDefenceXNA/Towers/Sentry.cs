using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefenceXNA.Sprites;

namespace TowerDefenceXNA.Towers
{
    public class Sentry : Tower
    {
        private string name = "Sentry";
        private int range = 75;
        private double towerSpeed = 0.5;
        private int damage = 5;
        private int splashDamage = 0;
        private int splashRange = 0;
        private int bulletSpeed = 5;
        public static int cost = 10;
        private int upgradeCost = 20;
        private bool ground = true;
        private bool air = true;

        static Point currentFrame = new Point(0, 0);
        static Point portraitIndex = new Point(0, 0);
        static int podiumIndex = 0;

        public Sentry(Vector2 position) :
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
            Range += 10;
            base.currentFrame.X = Level - 1;
            circle = CreateCircle(Range);

            switch (Level)
            {
                case 2:
                    {
                        Damage = 14;
                        Cost += 20;
                        UpgradeCost = 40;
                    } break;
                case 3:
                    {
                        Damage = 32;
                        Cost += 40;
                        UpgradeCost = 56;
                    } break;
                case 4:
                    {
                        Damage = 56;
                        Cost += 56;
                        UpgradeCost = 115;
                    } break;

                case 5:
                    {
                        Damage = 110;
                        Cost += 115;
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
                "Sentry", 75, 5, "0,5 per/sec", 10), new Vector2(490, 20), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.96f);
        }
    }
}