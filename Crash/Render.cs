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
        //  ***********
        //  variables and properties
        //  ***********
        const int SHORT_PAUSE_MS = 500;
        const int MEDIUM_PAUSE_MS = 1000;
        const int LONG_PAUSE_MS = 3000;
        static string[] inFile;

        //  ***********
        //  methods
        //  ***********
        // reads text files with beginning information and asks if wants to play
        internal static string IntroScreen()
        {
            Console.Clear();
            Console.Title = "Cras# - by the Dextrous Devs";
            // TODO: turn on sound
            Sound.PlaySound("copterStarting.mp3");

            ReadFile("flyrightCopter.txt");
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
            Thread.Sleep(SHORT_PAUSE_MS);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome to Flyright Copters!\n");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            string name = "";
            while (name == "")
            {
                Console.Write("What's your name? ");
                name = Console.ReadLine();
                name = name.Trim();
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\nWell hello {name}!");
            Thread.Sleep(MEDIUM_PAUSE_MS);
            Console.WriteLine("\nI show you are scheduled for a quick trip over the mountain");
            Thread.Sleep(MEDIUM_PAUSE_MS);
            Console.WriteLine("\nWe should be there in under an hour.");
            Thread.Sleep(MEDIUM_PAUSE_MS);
            Console.WriteLine($"\nSit back, relax, and enjoy the ride!");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("\nAre you ready to go? Press Y to play or any other key to Exit: ");
            return name;
        }

        // TODO: decide if you want to use introscreen2
        internal static void IntroScreen2()
        {
            Console.Title = "Cras# - by the Dextrous Devs";
//            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to Your Choice Travel!\n");
            //Console.Write("What's your name? => ");  // TODO: validate
            //string name = Console.ReadLine();
            //Console.WriteLine($"Well hello, {name}! It looks like a lovely day to travel today.");
            Console.WriteLine("Here at Your Choice Travel, we let you pick your mode of transportation.");
            Console.Write($"Would you like to travel by");
            Console.WriteLine("1) Helicopter");
            Console.WriteLine("2) Train");
            Console.WriteLine("3) Balloon");
            Console.Write("Enter your choice here => ");
            string choice = Console.ReadLine().ToLower();
            switch (choice)
            {
                case "1":
                case "helicopter":
                    IntroScreen();
                    break;
                //case "2":
                //case "train":
                //    IntroScreenTrain();
                //    break;
                //case "3":
                //case "balloon":
                //    IntroScreenBalloon();
                //    break;
                default:
                    Console.WriteLine($"Sorry, we don't offer travel by {choice}");
                    break;
            }
        }

        internal static void CrashScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nYou are rattled awake by a severe jolt.");
            Thread.Sleep(MEDIUM_PAUSE_MS);
            Console.WriteLine("\nThe pilot can't hear you over the din as the smoke fills the cabin.");
            Thread.Sleep(MEDIUM_PAUSE_MS);
            Console.WriteLine("\nThe helicopter is losing altitude - fast.");
            Thread.Sleep(MEDIUM_PAUSE_MS);
            Console.WriteLine("\nYou buckle your seat belt and brace for impact...");
            Thread.Sleep(LONG_PAUSE_MS);
            Sound.StopSound();
            Console.Clear();

            CrashLogo();
            // TODO: turn on sound
            Sound.PlaySound("crashSound.mp3", LONG_PAUSE_MS);
            //Thread.Sleep(LONG_PAUSE_MS);
            Console.ResetColor();

            Console.WriteLine("\nAs the shock wears off you realize that you have crash landed in the mountains.");
            Thread.Sleep(MEDIUM_PAUSE_MS);
            Console.WriteLine("\nYou don't see the pilot anywhere.");
            Thread.Sleep(MEDIUM_PAUSE_MS);
            Console.WriteLine("\nThe last town you flew over must have been 10 miles back.");
            Thread.Sleep(MEDIUM_PAUSE_MS);
            Console.WriteLine("\nAvgas is spilling out of the tank dangerously close to the fire in the cabin.");
            Thread.Sleep(MEDIUM_PAUSE_MS);
            Console.WriteLine("\nYou must act quickly!");
            Thread.Sleep(MEDIUM_PAUSE_MS);
            Console.Write("\nPress any key to continue ... ");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("\nYou glance around and see a backpack and handful of items that might help you");
            Console.WriteLine("make it back to civilization.");
        }

        internal static void EndScreen()
        {
            Console.Clear();
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            // TODO: ADD PLAYER NAME ON ENDSCREEN
            Console.WriteLine($"                    T H A N K S  F O R  P L A Y I N G");
            Console.WriteLine();
            CrashLogo();
            Console.WriteLine();
            ReadFile("authors.txt");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Array.ForEach(inFile, line => Console.WriteLine(line));
            // TODO: turn on sound
            Sound.PlaySound("copterStarting.mp3", LONG_PAUSE_MS);
        }

        private static void CrashLogo()
        {
            ReadFile("crashScreen.txt");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Array.ForEach(inFile, line => Console.WriteLine(line));
        }

        private static void ReadFile(string fileName)
        {
            inFile = File.ReadAllLines(fileName);
        }

        public static void DisplayFareWellMessage(bool isWinner, int score)
        {
            if (isWinner)
            {
                Console.WriteLine($"\n\nCongratulations! Based on the items you picked you got a survival score of {score} and made it to the nearest town");
            }
            else
            {
                Console.WriteLine($"\n\nOh Bummer, Based on the items you picked you got a survival score of {score} and never found your way back to town.");
                Console.WriteLine("The search and rescue team said they found you hypothermic half dead muttering something about ternary operators");
            }
        }
    }
}