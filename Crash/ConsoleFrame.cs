using System;
using System.Drawing;
namespace Crash
{
    public class ConsoleFrame
    {
        private static int FrameWidth = Console.WindowWidth;
        private static int FrameHeight = Console.WindowHeight;


        Frame StatsFrame;
        Frame StoryFrame;
        Frame UserInputFrame;

        public ConsoleFrame()
        {
            StatsFrame = new Frame(FrameHeight / 3, FrameWidth, 0, 0, ConsoleColor.Gray, ConsoleColor.Magenta, "Stats");
            StoryFrame = new Frame(FrameHeight / 3, FrameWidth, 0, StatsFrame.Height, ConsoleColor.Gray, ConsoleColor.Magenta, "Story");
            UserInputFrame = new Frame(FrameHeight / 3, FrameWidth, 0, StatsFrame.Height + StoryFrame.Height, ConsoleColor.Gray, ConsoleColor.Magenta, "User Input");
        }

        public void Draw()
        {
            StatsFrame.DrawFrame();
            StoryFrame.DrawFrame();
            UserInputFrame.DrawFrame();
        }
    }

    /// <summary>
    /// Creates and renders a scoreboard object to track game statistics. 
    /// </summary>
    public class Frame
    {
        protected string title;
        protected int height;
        protected int width;
        protected int cursorX;
        protected int cursorY;
        protected ConsoleColor borderColor;
        protected ConsoleColor textColor;
        protected Point statCursor;

        public virtual Point GetCursorPosition()
        {
            return statCursor;
        }

        public ConsoleColor GetTextColor()
        {
            return textColor;
        }

        public int Height { get { return height; } }
        public int Width { get { return width; } }

        public Frame(int height, int width, int cursorX, int cursorY, ConsoleColor borderColor, ConsoleColor textColor, string text)
        {
            this.height = height;
            this.width = width;
            this.cursorX = cursorX;
            this.cursorY = cursorY;
            this.borderColor = borderColor;
            this.textColor = textColor;
            this.title = text;
        }

        public void DrawFrame()
        {
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(cursorX, cursorY + i);

                for (int j = 0; j < width; j++)
                {
                    if (i < 2 || i == height - 1)
                    {
                        Console.BackgroundColor = borderColor;
                        Console.Write(" ");
                        Console.ResetColor();
                    }
                    else
                    {
                        if (j == 0 || j == width - 1)
                        {
                            Console.BackgroundColor = borderColor;
                            Console.Write(" ");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                }
                Console.WriteLine();
            }

            int centerText = width > title.Length ? (width - title.Length) / 2 : 0;
            Console.SetCursorPosition(cursorX + centerText, cursorY + 1);
            Console.ForegroundColor = textColor;
            Console.BackgroundColor = borderColor;
            Console.Write(title);
            Console.ResetColor();
            statCursor = new Point(cursorX + 3, cursorY + height / 2 + 1);
        }
    }

}
