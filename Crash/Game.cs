//  Date: 1-24-2020
//  C# Mini Project - Cras# a Text Adventure
//  The Dextrous Devs - Jing Xie, Matt Juel, Radiah Jones
//  Purpose:  Class that controls game logic and flow. 
using System;
using System.Timers;
using System.Linq;
using System.Collections.Generic;

namespace Crash
{
    public class Game

    {
        //  ***********
        //  variables and properties
        //  ***********
        Player player = new Player();
        Timer GameClock;
        Timer CrashTimer;
        int CrashTimerSeconds = 300;
        bool isNewGame;
        Tool[] ItemsInCraft = {new Tool("A ball of steel wool", 8),
                                    new Tool("A small axe",9),
                                    new Tool("A loaded pistol",3),
                                    new Tool("Can of vegetable oil",6),
                                    new Tool("Newspapers (one per person)",7),
                                    new Tool("Cigarette lighter (without fluid)",12),
                                    new Tool("Extra shirt and pants for each survivor",10),
                                    new Tool("20 x 20 ft. piece of heavy-duty canvas",11),
                                    new Tool("A map made of plastic",2),
                                    new Tool("Some whiskey",4),
                                    new Tool("A compass",1),
                                    new Tool("Family-size chocolate bars (one per person)",5)};

        //  ***********
        //  constructors
        //  ***********
        public Game()
        {
            Console.WriteLine("Player points: " + player.LifePoints);
        }

        //  ***********
        //  methods
        //  ***********
        public bool PlayGame()
        {
            const int CRASH_TIMER_INTERVAL = 1000;

            CrashTimer = new Timer(CRASH_TIMER_INTERVAL);
            player.Name = Render.IntroScreen();
            //Render.IntroScreen2();
            isNewGame = Console.ReadKey().Key == ConsoleKey.Y ? true : false;

            if (isNewGame)
            {
                Render.CrashScreen();
                CollectItemsAfterCrash();
            }
            else
            {
                return false;
            }

            bool isWin = CheckForWin();
            Render.DisplayFareWellMessage(isWin, player.BackPackItemsScore);

            Console.Write("\n\nDo you want to play again?  Press Y to play or any other key to Exit: ");
            return Console.ReadKey().Key == ConsoleKey.Y ? true : false;
        }

        public void SetPlayerName(string name)
        {
            player.Name = name;
        }

        public void CollectItemsAfterCrash()
        {

            Console.WriteLine($"You estimate that you have about 5 minutes until the helicopter explodes.  Your bag can hold 5 items so choose wisely."); //{player.backpack.Length} instead of 5

            Console.WriteLine("Press Enter to Continue");
            Console.ReadKey();

            CrashTimer.Elapsed += new ElapsedEventHandler(CrashTimerEvent);
            CrashTimer.Enabled = true;

            Console.WriteLine("\nYou glance around and see the following items:");
            PrintItemsInCraftWithIndexes();
            SelectBackpackItems();
            DisplayItemsInBackpack();
        }

        private void SelectBackpackItems()
        {
            Tool item;
            int itemNumber;
            string selectedNumber;
            int NUM_ITEMS_IN_CRAFT = ItemsInCraft.Length;
            const int MAX_BACKPACK_ITEMS = 5;

            while (player.backpack.Count < MAX_BACKPACK_ITEMS)
            {
                Console.Write($"Enter the number of selected item {player.backpack.Count + 1} => ");
                selectedNumber = Console.ReadLine();
                bool validNumber = Int32.TryParse(selectedNumber, out itemNumber);
                if (validNumber && itemNumber > 0 && itemNumber <= NUM_ITEMS_IN_CRAFT)
                {
                    item = ItemsInCraft[itemNumber - 1];
                    if (!player.backpack.ContainsKey(item.Name))
                    {
                        player.backpack.Add(item.Name, item);
                        player.BackPackItemsScore += item.Point;
                        item.Use(player);
                    }
                }
            }
        }

        public void DisplayItemsInBackpack()
        {
            Console.WriteLine();
            Console.WriteLine($"Your backpack contents:");
            foreach (var item in player.backpack)
            {
                Console.WriteLine($"* {item.Key}");
            }
            Console.WriteLine();
            Console.WriteLine("Player total lifepoints: " + player.LifePoints);
        }

        private void PrintItemsInCraftWithIndexes()
        {
            for (int i = 0; i < ItemsInCraft.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {ItemsInCraft[i].Name}");
            }
            Console.WriteLine();
        }

        private bool CheckForWin()
        {
            int WinningScoreThreshhold = 45;
            return player.BackPackItemsScore > WinningScoreThreshhold ? true : false;
        }

        public void CrashTimerEvent(Object source, ElapsedEventArgs e)
        {
            FormatAndDisplayTime();
            CrashTimerSeconds--;
        }

        public void FormatAndDisplayTime()
        {
            //The coordinates should be moved to a field of a separate class.
            //Console.SetCursorPosition(55, 10);

            int minutes = CrashTimerSeconds / 60;
            int seconds = CrashTimerSeconds % (minutes * 60);

            //Console.Write($"{minutes}:{seconds}");
        }
    }
}
