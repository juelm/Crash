using System;
namespace Crash
{
    public class Player
    {
        public string Name { get; set; }
        public String[] backpack = new String[5];
        public Player()
        {
            
        }

        public void getPlayerInfo()
        {
            Console.WriteLine("Hello...");
            Console.Write("\nBefore we go any further.  What do you want your character's name to be: ");
            Name = Console.ReadLine();
        }
    }
}
