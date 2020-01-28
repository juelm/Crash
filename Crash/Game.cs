//  Date: 1-24-2020
//  C# Mini Project - Cras# a Text Adventure
//  The Dextrous Devs - Jing Xie, Matt Juel, Radiah Jones
//  Purpose:  Class that controls game logic and flow. 
using System;
using System.Timers;

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
            //player = new Player();
            //CrashTimer = new Timer(1000);
            //Intro();
            //CollectItemsAfterCrash();
            Console.WriteLine("Player points: " + player.LifePoints);
        }

        //  ***********
        //  methods
        //  ***********
        public /*static*/ void PlayGame()
        {
            CrashTimer = new Timer(1000);
            Render.IntroScreen();
            CollectItemsAfterCrash();
            //isNewGame = Console.ReadKey().Key == ConsoleKey.Y ? true : false;

            //if (isNewGame)
            //{
            //    Render.CrashScreen();
            //}

            //Render.EndScreen();
            ////Sound.PlaySound("themeMusic.mp4", 10000);

            //// clean up unmanaged audio resources
            //Sound.DisposeAudio();
        }

        //public void PrintScreenPartition()
        //{
        //    Console.WriteLine("\n---------------------------------------------------------------------------");
        //}

        //public void Intro()
        //{
        //    Console.Write("\n\n\n\n\n-----------------ASCII ART Goes Here--------------------\n\n\n\n\n");
        //    PrintScreenPartition();
        //    Console.WriteLine("The pilot can't hear you over the din as the smoke fills the cabin.  The helicopter is losing altitude fast.  You buckle your seat belt and brace for impact...");
        //    Console.WriteLine("\nCRAS#​");
        //    Console.WriteLine("\nAs the shock wears off you realize that you have crash landed in the mountains.The last town you flew over must have been 40 miles back.Avgas is spilling out of the tank dangerously close to the fire in the cabin.You must act quickly!");
        //    Console.WriteLine("You glance around and see a handful of items that might help you make it back to civilization.");
        //    Console.WriteLine("Press enter to continue: ");
        //    Console.ReadKey();
        //}

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
