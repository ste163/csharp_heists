using System;

namespace heist
{
    public class Criminal
    {
        public string Name { get; set; }
        // Skill level must be 1 - 100
        public int SkillLevel { get; set; }

        // 1 -100
        public int Trust { get; set; }
        
        // Courage Factor must be 0.1 - 1.0
        public double CourageFactor { get; set; }

        private bool _isPlayer;

        public Criminal(string name,int skill, double courage)
        {
            Name = name;
            SkillLevel = skill;
            CourageFactor = courage;
            Trust = new Random().Next(20, 61);
            _isPlayer = false;
        }

        public void Player(string name)
        {
            Name = name;
            SkillLevel = new Random().Next(1, 51);
            CourageFactor = new Random().NextDouble();
            _isPlayer = true;

        }
    }
}