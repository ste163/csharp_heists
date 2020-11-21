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

        public bool IsPlayer;

        public string Face;

        public Criminal(string name, bool player)
        {
            ASCII ASCII = new ASCII();

            Name = name;
            SkillLevel = new Random().Next(1, 51);;
            CourageFactor = new Random().NextDouble();;
            Trust = new Random().Next(20, 61);
            Face = ASCII.DisplayCriminalFace();
            if (player)
            {
                IsPlayer = true;
            }
            else
            {
                IsPlayer = false;
            }
        }
    }
}