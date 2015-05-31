using Microsoft.Xna.Framework;

namespace TowerDefenceXNA.Zombies
{
    public class FemaleZombie : Zombie
    {
        private string name = "Hot Dead Girl";
        private int speed = 43;
        private int health = 12;
        private double armor = 0.35;
        private int score = 25;
        private int bounty = 1;
        private int maxBounty = 8;
        private int fps = 100;
        private bool flying = false;
        private readonly Point frame = new Point(0, 2);

        public FemaleZombie(Vector2 position, double modifier = 1) :
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