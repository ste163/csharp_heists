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

        public Criminal(string name,int skill, double courage)
        {
            Name = name;
            SkillLevel = skill;
            CourageFactor = courage;
        }
    }
}