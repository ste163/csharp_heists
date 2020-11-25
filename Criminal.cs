using System;

namespace heist
{
    public class Criminal
    {
        public string Name { get; set; }
        // Skill level must be 1 - 100
        public int BaseSkill { get; set; }

        // 1 -100
        public int Trust { get; set; }
        public string TrustDescription
        {
            get
            {
                string d = "";

                if (Trust <= 9) d = "*Eyes keep shifting from the van window to the cash.*";
                if (Trust >= 10 && Trust <= 19) d = "'You're the worst.'";
                if (Trust >= 20 && Trust <= 29) d = "'You better know what you're doing, or else.'";
                if (Trust >= 30 && Trust <= 39) d = "'Things aren't great right now.'";
                if (Trust >= 40 && Trust <= 49) d = "'Could be going better.'";
                if (Trust >= 50 && Trust <= 59) d = "'Doing okay.'";
                if (Trust >= 60 && Trust <= 69) d = "'Going well, boss.'";
                if (Trust >= 70 && Trust <= 79) d = "'Great work so far, boss!'";
                if (Trust >= 80 && Trust <= 89) d = "'Let's keep going! We're on a roll!'";
                if (Trust >= 90 && Trust <= 100) d = "'I've got your back, boss, through anything.'";

                return d;
            }
        }
        
        // Courage Factor must be 0.1 - 1.0
        public double CourageFactor { get; set; }

        public int CrewTotalCash { get; set; } = 0;

        public bool IsPlayer;

        public int PlayerContactCount { get; set; }

        public string Face;

        public double CourageLevel
        // REPLACE COURAGE LEVEL WITH COURAGE FACTOR. COURAGE IS
        // DIRECTLY RELATED TO TRUST. MORE TRUST, MORE COURAGE
        {
            get {
                double newCourage = CourageFactor;

                // CONTINUE THROUGH THIS
                if (Trust <= 10) return newCourage = 0.1;
                if (Trust >= 11 || Trust <= 20) return newCourage = 0.2;

                    // The Trust level is shown to user as a sentence about how the member is feeling
                        // Trust 1 - 10 - always try to shoot another member and take their money
                        // Trust 11 - 20 - high chance of shooting another member and taking their money
                        // Trust 21 - 30 - low chance of turning on crew
                        // Trust 30 - 40 - very low chance of turning on crew, 0.1 Courage
                        // Trust 41 - 49 - almost no chance of turning on crew, 0.2 Courage
                        // Trust 50 - 59 - no chance of turning, 0.3 courage
                        // Trust 60 - 69 - 0.4 Courage
                        // Trust 70 -79 - 0.5 Courage
                        // Trust 80 - 89 - 0.7 Courage
                        // Trust 90 - 99 - 0.9 Courage
                        // Trust 100 - 1.0 Courage

                return newCourage;
                }
        }

        public double SkillLevel 
        {
            get 
            {
               return (((CourageFactor * BaseSkill) / 5) + BaseSkill);
            }
        }

        public Criminal(string name, bool player)
        {
            ASCII ASCII = new ASCII();
            
            Name = name;
            BaseSkill = new Random().Next(1, 51);
            CourageFactor = new Random().NextDouble();
            Face = ASCII.DisplayCriminalFace();
            if (player)
            {
                IsPlayer = true;
                PlayerContactCount = 4;
            }
            else
            {
                IsPlayer = false;
                PlayerContactCount = 0;
                Trust = new Random().Next(20, 61);
            }
        }
    }
}