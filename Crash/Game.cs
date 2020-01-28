//  Date: 1-24-2020
//  C# Mini Project - Cras# a Text Adventure
//  The Dextrous Devs - Jing Xie, Matt Juel, Radiah Jones
//  Purpose:  Class that controls game logic and flow. 
using System;
using System.Timers;
using System.Linq;

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
        string[] ItemsInCraft = {"A ball of steel wool",
                                    "A small axe",
                                    "A loaded pistol",
                                    "Can of vegetable oil",
                                    "Newspapers (one per person)",
                                    "Cigarette lighter (without fluid)",
                                    "Extra shirt and pants for each survivor",
                                    "20 x 20 ft. piece of heavy-duty canvas",
                                    "A map made of plastic",
                                    "Some whiskey",
                                    "A compass",
                                    "Family-size chocolate bars (one per person)"};

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
        public void PlayGame()
        {
            const int CRASH_TIMER_INTERVAL = 1000;

            CrashTimer = new Timer(CRASH_TIMER_INTERVAL);
            player.Name = Render.IntroScreen();
            isNewGame = Console.ReadKey().Key == ConsoleKey.Y ? true : false;

            if (isNewGame)
            {
                Render.CrashScreen();
                CollectItemsAfterCrash();
            }

            Render.EndScreen();
            ////Sound.PlaySound("themeMusic.mp4", 10000);

            //// clean up unmanaged audio resources
            //Sound.DisposeAudio();
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
            PrintArrayWithIndexes(ItemsInCraft);
            SelectBackpackItems();
            Console.WriteLine($"Your backpack contents:");
            foreach (var item in player.backpack)
            {
                Console.WriteLine($"* {item.Key};");
            }
        }

        private void SelectBackpackItems()
        {
            string item;
            int itemNumber;
            string itemName;

            while (player.backpack.Count < 5)
            {

                Console.Write($"Enter the number of selected item {player.backpack.Count + 1} => ");
                item = Console.ReadLine();
                bool validNumber = Int32.TryParse(item, out itemNumber);
                if (validNumber && itemNumber > 0 && itemNumber < 13)
                {
                    itemName = ItemsInCraft[itemNumber - 1];
                    Tool newTool = new Tool(itemName, 10); // instantiate a new tool and increase the player's life point by 10
                    if (!player.backpack.ContainsKey(newTool.Name))
                    {
                        player.backpack.Add(newTool.Name, newTool);
                        newTool.Use(player);
                    }
                }
            }
        }

        public void PrintArrayWithIndexes(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {array[i]}");
            }
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
