// Outro class methods:
    // Prep crew for endgame
    // Handle the split cash view & its options
    // Display ending views/summaries
using System;
using System.Collections.Generic;
using System.Linq;
using CSharpHeists.ASCII;
using CSharpHeists.Criminal;
using CSharpHeists.GameSections.MainLoop;
using CSharpHeists.Location;
using CSharpHeists.UI;

namespace CSharpHeists.GameSections
{
    public class Outro
    {
        // Lowers all the crew member's morale because money is tempting
        public static List<BaseCriminal> PrepCrewForEndGame(List<BaseCriminal> crew)
        {
            // Ensure player can give a speech
            Random r = new Random();
            return crew.Select(c => {
                c.HasPlayerEncouragedCrew = false;
                c.Morale = c.Morale - r.Next(25, 41);
                return c;
            }).ToList();
        }

        // Show who lived
        public static void DisplayCrewMembersWhoSurvived(List<BaseCriminal> crew)
        {
            Console.WriteLine($"Crew members (besides you) who survived:");
            if (crew.Count() == 1) Console.WriteLine("  All your crew members either ran off, were arrested, or shot dead.");
            else
            {
                crew.ForEach(c =>
                {
                    if (!c.IsPlayer) Console.WriteLine($"  {c.Name}");
                });
            }
        }

        // Displays ending game message and handles input
        static void ExitGame()
        {
            while(true)
            {
            Console.WriteLine("");
            Console.WriteLine("_________");
            Console.WriteLine("");
            Console.Write("Play again or exit C# Heists [play/exit] ");
            string input = Console.ReadLine().ToLower();
            if (input == "play") Program.StartGame();
            else if (input == "exit") Environment.Exit(0);
            }
        }

        public static void SplitCash(List<BaseCriminal> crew, List<BaseLocation> locations)
        {
            // get crew cash
            int totalCash = 0;
            crew.ForEach(c => totalCash = c.CrewTotalCash);
            BaseCriminal player = crew.Find(c => c.IsPlayer);

            if (totalCash > 0)
            {
                // If there is a crew, split cash. Else, end game
                if (crew.Count() > 1)
                {
                    Console.Clear();

                    // Display headings and crew info
                    Console.WriteLine(Heading.DisplayHeadingSplit());
                    Console.WriteLine(Heading.DisplaySubheadingSplit());
                    CrewManagement.DisplayCrewInfoShortened(crew);

                    // Money bag image
                    Console.WriteLine(Misc.DisplayMoneyBag());

                    // Display crews name and current morale status
                    Console.WriteLine("Crew");
                    Console.WriteLine("-----");
                    crew.ForEach(c =>
                    {
                        if (!c.IsPlayer)
                        {
                            Console.WriteLine($"{c.Name} - {c.MoraleDescription}");
                        }
                    });

                    Console.WriteLine("");
                    Console.WriteLine("The more money you take, the better off you'll be when you skip town.");
                    Console.WriteLine("");
                    Console.WriteLine("1) attempt to split cash evenly among crew members and part ways");
                    Console.WriteLine("2) ice a crew member");
                    if (player.HasPlayerEncouragedCrew == false) Console.WriteLine("3) give congratulations speech");

                    int select = Menu.MenuInput(3);

                    switch (select)
                    {
                        case 1:
                            // Player gives up chance to shoot. Otherwise the player will always have the chance to ice the last person
                            Ice.WillCrewMemberShoot(crew, locations, true);
                            GameOver(crew, locations, true, player);
                            break;
                        case 2:
                            // Player ices an associate 
                            List<BaseCriminal> smallerCrew = Ice.IceCrewMember(crew, locations, true);
                            if (player.PlayerFiredWeapon) Ice.WillCrewMemberShoot(smallerCrew, locations, false);
                            smallerCrew.ForEach(c =>
                            {
                                if (c.IsPlayer) c.PlayerFiredWeapon = false;
                            });
                            SplitCash(smallerCrew, locations);
                            break;
                        case 3:
                            SplitCash(CrewManagement.EncourageCrew(crew, true), locations);
                            break;
                    }
                    GameOver(crew, locations, true, player);
                }
                else GameOver(crew, locations, true, player);
                
            }
            else GameOver(crew, locations, true, player);   
        }
        public static void GameOver(List<BaseCriminal> crew, List<BaseLocation> locations, bool playerAlive, BaseCriminal player)
        {
            BaseCriminal firstIndex = crew[0];
            int cashStolen = firstIndex.CrewTotalCash;
            int playersCut = cashStolen / crew.Count();
            int totalCashAvailable = 0;
            locations.ForEach(l => totalCashAvailable = l.Cash + totalCashAvailable);

            Console.Clear();
            Console.WriteLine(Heading.DisplayHeadingGameOver());
            Console.WriteLine(Heading.DisplaySubHeadingSummary());
            Console.WriteLine($"Total cash stolen: ${cashStolen} / ${totalCashAvailable}");

            if (playerAlive)
            {
                // Arrested Ending
                if (player.IsPlayerArrested == true)
                {
                    // If we have a crew
                    if (crew.Count() > 1)
                    {
                        // Minus you from crew.Count
                        Console.WriteLine($"The cut per members not in jail: ${cashStolen / crew.Count() - 1}");
                        DisplayCrewMembersWhoSurvived(crew);
                    }

                    if (player.PlayerContactCount > 0) Console.WriteLine($"Number of associates left you could have hired: {player.PlayerContactCount}");

                    Console.WriteLine("");
                    Console.WriteLine(Face.DisplayArrested());

                    Console.WriteLine("You should have known, crime never pays.");

                    ExitGame();
                }

                if (player.CrewTotalCash == 0)
                {
                    DisplayCrewMembersWhoSurvived(crew);
                    if (player.PlayerContactCount > 0) Console.WriteLine($"Number of associates left you could have hired: {player.PlayerContactCount}");

                    Console.WriteLine(Heading.DisplaySubheadingWanted());
                    Console.WriteLine($"{player.Name} for multiple attempts at robbery.");
                    Console.WriteLine($"{player.Face}");
                    Console.WriteLine("");
                    Console.WriteLine("You somehow ended up with nothing. You can't even get out of town.");
                    Console.WriteLine("Police all over Tennessee and the ajoining states are hunting you.");
                    Console.WriteLine($"Enjoy your freedom while you can, {player.Name}.");

                    ExitGame();
                }
                // the crew does have cash
                else
                {
                    if (crew.Count() > 1)
                    {
                        Console.WriteLine($"Cut per member: ${cashStolen / crew.Count()}");

                        DisplayCrewMembersWhoSurvived(crew);

                        if (player.PlayerContactCount > 0) Console.WriteLine($"Number of associates left you could have hired: {player.PlayerContactCount}");
                    }

                    // Based on how much the playersCut was, display ending message
                    if (playersCut > 0 && playersCut <= 10_000) Console.WriteLine(Ending.DisplayEndingCamp());
                    if (playersCut >= 10_001 && playersCut <= 999_999) Console.WriteLine(Ending.DisplayEndingRoad());
                    if (playersCut > 1_000_000) Console.WriteLine(Ending.DisplayEndingBeach());
                }
            }

            else if (!playerAlive)
            {
                // REMOVE the player from the crew
                crew = crew.Where(c => !c.IsPlayer || !c.isAssociateIced).ToList();

                Console.WriteLine($"The cut per member: ${cashStolen / crew.Count()}");

                if (crew.Count() == 1) Console.WriteLine($"The only survivor was: ");
                else Console.WriteLine($"Crew members who survived:");

                crew.ForEach(c => Console.WriteLine($"  {c.Name}"));

                if (player.PlayerContactCount > 0) Console.WriteLine($"Number of associates left you could have hired: {player.PlayerContactCount}");

                Console.WriteLine(player.FaceIced);
                Console.WriteLine("");
                Console.WriteLine("Crime doesn't pay when you're dead.");
            }

            ExitGame();
        }
    }
}
