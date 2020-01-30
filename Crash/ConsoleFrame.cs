using System;
using System.Drawing;
namespace Crash
{
    public class ConsoleFrame
    {
        private static int FrameWidth = Console.WindowWidth;
        private static int FrameHeight = Console.WindowHeight;
        private int LeftMargin = 2;
        private int RightMargin = 2;
        private int TopMargin = 1;
        private int BottomMargin = 1;
        private int InnerMargins = 1;
        private int StoryFrameWidth = FrameWidth - 50;
        private int StatsFrameHeight = 20;
        private int BackPackFrameHeight = 10;



        Frame StatsFrame;
        Frame StoryFrame;
        Frame BackpackFrame;

        public ConsoleFrame()
        {
            StatsFrame = new Frame(StatsFrameHeight, FrameWidth - StoryFrameWidth - LeftMargin - RightMargin - InnerMargins, LeftMargin + StoryFrameWidth + InnerMargins, TopMargin, ConsoleColor.DarkGray, ConsoleColor.Magenta, "Stats");
            BackpackFrame = new Frame(BackPackFrameHeight, FrameWidth - StoryFrameWidth - LeftMargin - RightMargin - InnerMargins, LeftMargin + StoryFrameWidth + InnerMargins, StatsFrame.Height + TopMargin + InnerMargins, ConsoleColor.DarkGray, ConsoleColor.Yellow, "BackPack");
            StoryFrame = new Frame(FrameHeight - TopMargin - BottomMargin, StoryFrameWidth,LeftMargin, TopMargin, ConsoleColor.DarkGray, ConsoleColor.Cyan, "Story");
            
        }

        public void Draw()
        {
            StatsFrame.DrawFrame();
            StoryFrame.DrawFrame();
            BackpackFrame.DrawFrame();
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
        private int InnerTextLeftMargin = 3;
        private int InnerTextTopMargin = 2;

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
            statCursor = new Point(cursorX, cursorY + height / 2 + 1);
        }
    }

}
