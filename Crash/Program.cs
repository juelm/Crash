//  Date: 1-24-2020
//  C# Mini Project - Cras# a Text Adventure
//  The Dextrous Devs - Jing Xie, Matt Juel, Radiah Jones
//  Purpose:  Class that starts the game. 
using System;

namespace Crash
{
    class Program
    {
        static void Main(string[] args)
        {
            bool PlayAgain = true;
            while (PlayAgain)
            {
                Game currentGame = new Game();
                PlayAgain = currentGame.PlayGame();
            }

            Console.Clear();
            Render.EndScreen();

        }
    }
}
