using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.IO;
using System.Globalization;
using System.Reflection;

namespace D2RaidTeamRandomizer
{
    class RaidTeamRandomizer
    {
        public List<string> Players = new List<string>();
        public List<string> ConfirmedPlayers = new List<string>();
        public void AddPlayer()
        {
            Console.Clear();
            Console.WriteLine("Please input the name of the player to add.");
            string player = Console.ReadLine().Trim();
            if (!Players.Contains(player))
            {
            Players.Add(player);
            Console.WriteLine(player + " has been added to the database.");
            }
            else
            {
                Console.WriteLine("Player already present in database.");
            }
            Players.Sort();
            Console.WriteLine();
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
            DisplayMenu();
        }
        public void LoadPlayerList()
        {
            Console.Clear();
            StreamReader load = new StreamReader("playerDatabase.txt");
            string line;
            while ((line = load.ReadLine()) != null)
            {
                if (!Players.Contains(line))
                {
                    Players.Add(line);
                }
            }
            load.Close();
            Players.Sort();
            Console.WriteLine(Players.Count + " total players loaded.");
            Console.WriteLine();
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
            DisplayMenu();
        }
        public void SavePlayerList()
        {
            StreamWriter save = new StreamWriter("playerDatabase.txt");
            foreach (string name in Players)
            {
                save.WriteLine(name);
            }
            save.Close();
        }
        public void ConfirmPlayers()
        {
            Console.Clear();
            Console.WriteLine("Please Select Players Based On Menu #");
            Console.WriteLine("=====================================");
            Console.WriteLine();
            for (int i = 0; i < Players.Count; i++)
            {
                Console.WriteLine(i+1 + ": " + Players[i]);
            }
            string response = Console.ReadLine().Trim();
            string[] selects = response.Split(' ');
            foreach (string num in selects)
            {
                ConfirmedPlayers.Add(Players[int.Parse(num)-1]);
            }
            ConfirmedPlayers.Sort();
            Console.Clear();
            Console.WriteLine("Are these players correct? (y/n)");
            Console.WriteLine("==========================");
            foreach (string name in ConfirmedPlayers)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();
            string yn = "";
            while (yn != "y" && yn != "n")
            {
                yn = Console.ReadLine().Trim().ToLower();
            }
            Console.WriteLine();
            if (yn == "y")
            {
                Console.WriteLine("Players Confirmed");
            }
            if (yn == "n")
            {
                ConfirmedPlayers = new List<string>();
                Console.WriteLine("Players not confirmed. Please try again from the menu.");
            }
            Console.WriteLine();
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
            DisplayMenu();
        }
        public void ShuffleTeams(List<string> eligiblePlayers)
        {
            Random rng = new Random();
            int startingNumber = eligiblePlayers.Count;
            string[,] result = new string[startingNumber/6,6];
            if (eligiblePlayers.Count % 6 != 0)
            {
                Console.WriteLine("Error: Incorrect Number of Players Entered. Please confirm players again.");
                ConfirmedPlayers = new List<string>();
            }
            else
            {
                for (int i = 0; i < startingNumber; i++)
                {
                    int select = rng.Next(0, eligiblePlayers.Count);
                    string player = eligiblePlayers[select];
                    eligiblePlayers.Remove(player);
                    result[i / 6, i % 6] = player;
                }
                Console.Clear();
                for (int b = 0; b < result.Length/6; b++)
                {
                    Console.WriteLine("Team " + (b+1) +":");
                    Console.WriteLine("====================");
                    for (int a = 0; a < 6; a++)
                    {
                        Console.WriteLine(result[b,a]);
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
            DisplayMenu();
        }
        public void Exit()
        {
            Console.Clear();
            SavePlayerList();
            Console.WriteLine(Players.Count + " players saved. Goodbye.");
            Environment.Exit(1);
        }
        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Please Select an Option.");
            Console.WriteLine("========================");
            Console.WriteLine("1: Load Players");
            Console.WriteLine("2: Add Player");
            Console.WriteLine("3: View Players");
            Console.WriteLine("4: Confirm Players");
            Console.WriteLine("5: Shuffle Teams");
            Console.WriteLine("6: Exit");
            Console.WriteLine("========================");
            Console.WriteLine();

            int selection = 0;
            while (selection <1 || selection > 6)
            {
                int.TryParse(Console.ReadLine().Trim(), out selection);
            }

            switch (selection)
            {
                case 1: 
                    LoadPlayerList();
                    break;
                case 2:
                    AddPlayer();
                    break;
                case 3:
                    ViewPlayers();
                    break;
                case 4:
                    ConfirmPlayers();
                    break;
                case 5:
                    ShuffleTeams(ConfirmedPlayers);
                    break;
                case 6:
                    Exit();
                    break;
            }
        }

        public void ViewPlayers()
        {
            Console.Clear();
            Players.Sort();
            foreach (string name in Players)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
            DisplayMenu();
        }
    }
}
