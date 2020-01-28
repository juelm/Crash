//  Date: 1-24-2020
//  C# Mini Project - Cras# a Text Adventure
//  The Dextrous Devs - Jing Xie, Matt Juel, Radiah Jones
//  Purpose: Class used as template for tools
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
