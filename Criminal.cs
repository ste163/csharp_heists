using System;
using System.Collections.Generic;

namespace heist
{
    public class Criminal
    {
        public string Name { get; set; }

        public int BaseSkill { get; set; }
        public int MoraleMin { get; set; }
        public int MoraleMax { get; set; }
        public int Morale { get; set; }
        public int MoraleSkillBonus
        {
            get {
                int bonus = 0;

                if (Morale <= 9) return bonus = -20;
                if (Morale >= 10 && Morale <= 19) return bonus = -10;
                if (Morale >= 20 && Morale <= 29) return bonus = -8;
                if (Morale >= 30 && Morale <= 39) return bonus =  -5;
                if (Morale >= 40 && Morale <= 49) return bonus =  0;
                if (Morale >= 50 && Morale <= 59) return bonus =  5;
                if (Morale >= 60 && Morale <= 69) return bonus =  10;
                if (Morale >= 70 && Morale <= 79) return bonus =  20;
                if (Morale >= 80 && Morale <= 89) return bonus =  30;
                if (Morale >= 90 && Morale <= 99) return bonus =  40;
                if (Morale >= 100) return bonus = 50;

                return bonus;
                }
        }
        public string MoraleDescription
        {
            get
            {
                string d = "";

                if (Morale <= 9) d = "*Eyes keep shifting from you to the back door of the van.*";
                if (Morale >= 10 && Morale <= 19) d = "'You're the worst.'";
                if (Morale >= 20 && Morale <= 29) d = "'You better know what you're doing, or else.'";
                if (Morale >= 30 && Morale <= 39) d = "'Things aren't great right now.'";
                if (Morale >= 40 && Morale <= 49) d = "'Could be going better.'";
                // All morale levels above this line have a chance of turning on crew/player
                if (Morale >= 50 && Morale <= 59) d = "'Doing okay.'";
                if (Morale >= 60 && Morale <= 69) d = "'Going well, boss.'";
                if (Morale >= 70 && Morale <= 79) d = "'Great work so far, boss!'";
                if (Morale >= 80 && Morale <= 89) d = "'We're kickin' ass, Boss! Nothing can stop us.'";
                if (Morale >= 90) d = "'I've got your back, boss, through anything.'";

                return d;
            }
        }
        
        public int CrewTotalCash { get; set; } = 0;

        public bool IsPlayer { get; set; }
        public bool IsPlayerArrested { get; set; } = false;
        public bool IsPlayerIced { get; set; } = false;
        public bool isAssociateIced { get; set; } = false;
        public bool HasPlayerEncouragedCrew { get; set; } = false;
        public int PlayerContactCount { get; set; }
        public bool HasRanInFear { get; set; } = false;
        public int FaceMatchingInt { get; set; }
        public string Face { get; set; }
        public string FaceIced { get; set; }

        public int TotalSkillLevel 
        {
            get 
            {
               return MoraleSkillBonus + BaseSkill;
            }
        }
        public Criminal(string name, bool player, List<Criminal> crew)
        {
            ASCII ASCII = new ASCII();
            Random r = new Random();
            
            Name = name;
            BaseSkill = new Random().Next(20, 40);
            
            bool checkFaces = true;

            while (checkFaces)
            {
                // Get random face value
                int faceInt = r.Next(1, 8);

                if (crew.Count == 0)
                {
                    Face = ASCII.DisplayCriminalFace(faceInt);
                    FaceIced = ASCII.DisplayCriminalFaceIced(faceInt);
                    FaceMatchingInt = faceInt;
                    checkFaces = false;
                }
                else
                {
                    // Check if face value is in the crew already
                    Criminal anyMatches = crew.Find(c => c.FaceMatchingInt == faceInt);
                    // If it's null, we didn't find a match!
                    if (anyMatches == null)
                    {
                        Face = ASCII.DisplayCriminalFace(faceInt);
                        FaceIced = ASCII.DisplayCriminalFaceIced(faceInt);
                        FaceMatchingInt = faceInt;
                        checkFaces = false;
                    }
                    // Reset the match
                    anyMatches = null;
                }

            }

            if (player)
            {
                IsPlayer = true;
                PlayerContactCount = 4;
                Morale = 35;
            }
            else
            {
                IsPlayer = false;
                PlayerContactCount = 0;
                MoraleMin = r.Next(5, 15);
                MoraleMax = r.Next(70, 101);
                // Set morale start at a max of 50
                Morale = r.Next(MoraleMin, 45);
            }
        }
    }
}