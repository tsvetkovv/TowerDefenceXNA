using Microsoft.Xna.Framework;

namespace TowerDefenceXNA.Zombies
{
    public class NormalZombie : Zombie
    {
        private string name = "Normal Zombie";
        private int speed = 16;
        private int health = 8;
        private double armor = 0.5;
        private int score = 25;
        private int bounty = 1;
        private int maxBounty = 5;
        private bool flying = false;
        private int fps = 400;
        private readonly Point frame = new Point(0, 0);

        public NormalZombie(Vector2 position, double modifier = 1) :
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