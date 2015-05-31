using Microsoft.Xna.Framework;

namespace TowerDefenceXNA.Zombies
{
    public class AbominationZombie : Zombie
    {
        private string name = "Abomination";
        private int speed = 24;
        private int health = 24;
        private double armor = 0;
        private int score = 50;
        private int bounty = 1;
        private int maxBounty = 12;
        private int fps = 280;
        private bool flying = false;
        private readonly Point frame = new Point(0, 7);

        public AbominationZombie(Vector2 position, double modifier = 1) :
            base(position)
        {
            Name = name;
            base.speed = speed;
            Health = (int)(health * modifier);
            MaxHealth = Health;
            Armor = (int)(armor * modifier);
            if ((modifier / 10) * bounty > maxBounty) Bounty = maxBounty;
            else Bounty = (int)(modifier * bounty);
            Score = (int)(score * modifier);
            currentFrame = frame;
            millisecondsPerFrame = fps;
        }
    }
}