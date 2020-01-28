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
        internal static string IntroScreen()
        {
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
            // TODO: add validation for empty name
            string name = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\nWell hello {name}!");
            Thread.Sleep(1000);
            Console.WriteLine("\nI show you are scheduled for a quick trip over the mountain");
            Thread.Sleep(1000);
            Console.WriteLine("\nWe should be there in under an hour.");
            Thread.Sleep(1000);
            Console.WriteLine($"\nSit back, relax, and enjoy the ride!");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("\nAre you ready to go? Y / N: ");
            return name;
        }

        internal static void EndScreen()
        {
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("                    T H A N K S  F O R  P L A Y I N G");
            Console.WriteLine();
            CrashLogo();
            Console.WriteLine();
            string[] inFile = File.ReadAllLines(@"authors.txt");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Array.ForEach(inFile, line => Console.WriteLine(line));
        }


        internal static void CrashScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nYou are rattled awake by a severe jolt.");
            Thread.Sleep(1000);
            Console.WriteLine("\nThe pilot can't hear you over the din as the smoke fills the cabin.");
            Thread.Sleep(1000);
            Console.WriteLine("\nThe helicopter is losing altitude - fast.");
            Thread.Sleep(1000);
            Console.WriteLine("\nYou buckle your seat belt and brace for impact...");
            Thread.Sleep(2000);
            Console.Clear();

            CrashLogo();
            Thread.Sleep(3000);
            Console.ResetColor();

            Console.WriteLine("\nAs the shock wears off you realize that you have crash landed in the mountains.");
            Thread.Sleep(1000);
            Console.WriteLine("\nYou don't see the pilot anywhere.");
            Thread.Sleep(1000);
            Console.WriteLine("\nThe last town you flew over must have been 10 miles back.");
            Thread.Sleep(1000);
            Console.WriteLine("\nAvgas is spilling out of the tank dangerously close to the fire in the cabin.");
            Thread.Sleep(1000);
            Console.WriteLine("\nYou must act quickly!");
            Thread.Sleep(1000);
            Console.Write("\nPress any key to continue ... ");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("\nYou glance around and see a backpack and handful of items that might help you");
            Console.WriteLine("make it back to civilization.");
        }

        private static void CrashLogo()
        {
            string[] inFile = File.ReadAllLines(@"crashScreen.txt");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Array.ForEach(inFile, line => Console.WriteLine(line));
        }
    }
}