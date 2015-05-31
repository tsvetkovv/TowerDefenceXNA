using Microsoft.Xna.Framework;

namespace TowerDefenceXNA.Zombies
{
    public class HellHound : Zombie
    {
        private string name = "Hell Hound";
        private int speed = 67;
        private int health = 9;
        private double armor = 0.1;
        private int score = 25;
        private int bounty = 2;
        private int maxBounty = 11;
        private int fps = 100;
        private bool flying = false;
        private readonly Point frame = new Point(0, 3);

        public HellHound(Vector2 position, double modifier = 1) :
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