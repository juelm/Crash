using System;
using System.Collections.Generic;
using System.Text;

namespace Crash
{
    public class Tool : Usable, Item
    {
        public string Name { get; set; }
        public int Point { get; set; }

        public Tool(string name, int point)
        {
            Name = name;
            Point = point;
        }

        public void Use(Player player)
        {
            player.LifePoints += Point;

        }
    }
}
