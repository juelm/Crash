using System;
using System.Collections.Generic;
using System.Text;

namespace Crash
{
    public class Tool : Usable, Item
    {
        public string Name { get; set; }
        public int Point { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is Tool))
            {
                return false;
            }
            return Name == ((Tool)obj).Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

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
