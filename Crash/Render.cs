//  Date: 1-24-2020
//  C# Mini Project - Cras# a Text Adventure
//  The Dextrous Devs - Jing Xie, Matt Juel, Radiah Jones
//  Purpose:  Class that displays information to player
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace Crash
{
    internal static class Render
    {
        // reads text files with beginning information and asks if wants to play
        internal static void IntroScreen()
        {
            //Console.SetWindowSize(80, 30);
            Console.Clear();
            Console.Title = "Cras# - by the Dextrous Devs";
            //Sound.PlaySound("copterStarting.mp3");

            // read in helicopter ASCII art file
            string[] inFile = File.ReadAllLines(@"flyrightCopter.txt");
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine(inFile[i]);
            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            for (int i = 9; i < inFile.Length; i++)
            {
                Console.WriteLine(inFile[i]);
            }
            Console.WriteLine();
            Thread.Sleep(500);

            // reads in intro text file
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome to Flyright Copters!\n");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("What's your name? ");
            //Game.player.Name = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            //Console.WriteLine($"\nWell hello {Game.player.Name}!");
            Thread.Sleep(1000);
            Console.WriteLine("\nI show you are scheduled for a quick trip over the mountain");
            Thread.Sleep(1000);
            Console.WriteLine("\nWe should be there in under an hour.");
            Thread.Sleep(1000);
            Console.WriteLine($"\nSit back, relax, and enjoy the ride!");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("\nAre you ready to go? Y / N: ");
        }
    }
}
