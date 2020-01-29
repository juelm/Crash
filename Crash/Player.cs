//  Date: 1-24-2020
//  C# Mini Project - Cras# a Text Adventure
//  The Dextrous Devs - Jing Xie, Matt Juel, Radiah Jones
//  Purpose:  Class that allows creation of player object that hold name and points information
using System;
using System.Collections.Generic;

namespace Crash
{
    public class Player
    {
        //  ***********
        //  variables and properties
        //  ***********
        public string Name { get; set; }
        public Dictionary<string, Tool> backpack = new Dictionary<string, Tool>();
        public int LifePoints { get; set; }
        public int BackPackItemsScore { get; set; }

        //  ***********
        //  constructors
        //  ***********
        public Player()
        {
            LifePoints = 60;
        }

        //  ***********
        //  methods
        //  ***********
        public int GetBackPackItemsScore()
        {
            int TotalScore = 0;
            // TODO: make backpack dictionary private and add methods to add to backpackitemscore each time it's selected
            foreach(var item in backpack.Values)
            {
                TotalScore += item.Point;
            }

            return TotalScore;
        }
    }
}
