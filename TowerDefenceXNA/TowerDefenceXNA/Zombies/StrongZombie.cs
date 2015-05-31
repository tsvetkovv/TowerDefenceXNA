using Microsoft.Xna.Framework;

namespace TowerDefenceXNA.Zombies
{
    public class StrongZombie : Zombie
    {
        private string name = "Strong Zombie";
        private int speed = 29;
        private int health = 18;
        private double armor = 1.25;
        private int score = 45;
        private int bounty = 3;
        private int maxBounty = 12;
        private bool flying = false;
        private int fps = 300;
        private readonly Point frame = new Point(0, 1);

        public StrongZombie(Vector2 position, double modifier = 1) :
            base(position)
        {
            Name = name;
            base.speed = speed;
            Health = (int)(health * modifier);
            MaxHealth = Health;
            Armor = (int)(armor * modifier);
            Bounty = (modifier / 3 * bounty > maxBounty) ? maxBounty : (int)(modifier * bounty);
            Score = (int)(score * modifier);
            currentFrame = frame;
            millisecondsPerFrame = fps;
        }
    }
}