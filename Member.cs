using System;

namespace heist
{
    public class Criminal
    {
        public string Name { get; set; }
        // Skill level must be positive
        public int SkillLevel { get; set; }
        
        // Courage Factor must be 0.0 - 2.0
        public double CourageFactor { get; set; }

        public Criminal(string name,int skill, double courage)
        {
            Name = name;
            SkillLevel = skill;
            CourageFactor = courage;
        }
    }
}