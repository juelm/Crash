using System;
using System.Collections.Generic;
namespace Crash
{
    public class Player
    {
        public string Name { get; set; }
        //public String[] backpack = new String[5];
        public Dictionary<Tool, int> backpack = new Dictionary<Tool, int>();
        public int LifePoints { get; set; } = 60;

        public Player()
        {
            getPlayerInfo();
        }

        public void getPlayerInfo()
        {
            Console.WriteLine("Hello...");
            Console.Write("\nBefore we go any further.  What do you want your character's name to be: ");
            Name = Console.ReadLine();
        }
    }
}
