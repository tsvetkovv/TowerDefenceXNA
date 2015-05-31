using Microsoft.Xna.Framework;

namespace TowerDefenceXNA.Zombies
{
    public class TorsoZombie : Zombie
    {
        private string name = "Creeping Torso";
        private int speed = 32;
        private int health = 14;
        private double armor = 0.75;
        private int score = 35;
        private int bounty = 1;
        private int maxBounty = 9;
        private int fps = 120;
        private bool flying = false;
        private readonly Point frame = new Point(0, 6);

        public TorsoZombie(Vector2 position, double modifier = 1) :
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