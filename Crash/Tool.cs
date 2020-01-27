using System;
using System.Collections.Generic;
using System.Text;

namespace Crash
{
    class Tool : Usable, Items
    {
        public string Name { get; set; }
        public Tool(string name)
        {
            Name = name;
        }

        public void Use(Player player)
        {
            player.LifePoints += 10;
        }
    }
}
