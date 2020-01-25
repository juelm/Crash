using System;
namespace Crash
{
    public class Game
    {
        Player player;
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

        public Game()
        {
            Intro();
            CollectItemsAfterCrash();
            player = new Player();
        }

        public void PrintScreenPartition()
        {
            Console.WriteLine("\n---------------------------------------------------------------------------");
        }

        public void Intro()
        {
            Console.Write("\n\n\n\n\n-----------------ASCII ART Goes Here--------------------\n\n\n\n\n");
            PrintScreenPartition();
            Console.WriteLine("The pilot can't hear you over the din as the smoke fills the cabin.  The helicopter is losing altitude fast.  You buckle your seat belt and brace for impact...");
            Console.WriteLine("\nCRAS#​");
            Console.WriteLine("\nAs the shock wears off you realize that you have crash landed in the mountains.The last town you flew over must have been 40 miles back.Avgas is spilling out of the tank dangerously close to the fire in the cabin.You must act quickly!");
            Console.WriteLine("You glance around and see a handful of items that might help you make it back to civilization.");
            Console.WriteLine("Press enter to continue: ");
            Console.ReadKey();
        }

        public void CollectItemsAfterCrash()
        {

            Console.WriteLine($"You estimate that you have about 5 minutes until the helicopter explodes.  Your bag can hold 5 items so choose wisely.");
            Console.WriteLine("\nYou glance around and see the following items:");
            PrintArrayWithIndexes(ItemsInCraft);
            Console.WriteLine("\n\nPress Enter to Continue");
            Console.ReadKey();

        }

        public void PrintArrayWithIndexes(string[] array)
        {
            for(int i = 0; i < array.Length; i++)
            {
                Console.WriteLine($"{i}. {array[i]}");
            }
        }
    }
}
