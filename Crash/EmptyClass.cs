using System;
using System.Timers;
using System.Drawing;
using System.Collections.Generic;
//using System.Threading;

namespace Tetris
{

    public enum states { Free, Current, Base };
    public class Game
    {

        private Timer timer;
        private Timer lineTimer;
        private int timerCounter;
        private int x;
        private int y;
        private Board board;
        private Shape current;
        private List<Block> state;
        private Random rando = new Random();
        private int delay = 0;
        private int score = 0;
        private int lines = 0;
        private int level = 1;
        private int nextLevel = 0;
        private int ms;
        private List<Point> edges = new List<Point>();
        private object threadLock = new object();
        private int nextRando;
        bool notDead = true;


        public Game(int x, int y, int height, int width, int ms)
        {
            this.x = x;
            this.y = y;
            this.ms = ms;
            Point p = new Point(x, y);
            board = new Board(width, height, p, ConsoleColor.DarkGray);
            timer = new Timer(ms);
            lineTimer = new Timer(ms / 4);
            state = new List<Block>();

        }

        public bool playGame()
        {

            //Set up game.  Instantiate current block next block and board  

            Console.Clear();
            Console.CursorVisible = false;

            board.createBoard();
            board.drawBoard();
            board.getHighScoreBoard().displayScores();

            current = new Shape(board.SpawnPoint.X - Block.Width, board.SpawnPoint.Y, rando.Next(7));
            current.Arrange();
            current.render();

            nextRando = rando.Next(7);

            foreach (Point p in board.getBorders())
            {
                edges.Add(p);
            }

            setStats();

            current.Move(ConsoleKey.DownArrow);

            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Enabled = true;

            ConsoleKey userInput = ConsoleKey.UpArrow;
            //bool notDead = true;



            //-----------Handles all user input---------------


            while (notDead && (userInput == ConsoleKey.UpArrow || userInput == ConsoleKey.DownArrow || userInput == ConsoleKey.LeftArrow || userInput == ConsoleKey.RightArrow))
            {
                while (Console.KeyAvailable == false) //Without this loop control hangs up on the readkey and creates strange behavior in critical section.
                    if (!notDead) break;

                if (!notDead) break;

                userInput = Console.ReadKey().Key;

                notDead = processInput(userInput); // Returns a bool determining whether or not the game is over, which governs the loop.

            }

            //Receives user's decision to play or quit after game and returns to Program class.

            ConsoleKey playAgain = gameOver();

            if (playAgain == ConsoleKey.Q)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        //Event Handler for main program timer (instance variable = timer) that controls block falling.

        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            processInput(ConsoleKey.DownArrow);

        }

        //Event Handler that controls animations on level up and game over.

        public void UponMyDeath(Object source, ElapsedEventArgs e)
        {
            timerCounter++;
        }



        //-----------------------------------Primary Logic-------------------------------------

        //Processes input from user and events to control game, move blocks, detect collisions.


        public bool processInput(ConsoleKey action)
        {
            bool alive = true;

            if (action == ConsoleKey.UpArrow)
            {
                current.Mutate(edges, state);
            }
            else
            {

                bool hitEdge = false;
                bool hitBlock = false;


                if (action == ConsoleKey.LeftArrow)
                {

                    hitEdge = current.checkCollision(edges, Block.Width, 0);
                    hitBlock = current.checkCollision(state, Block.Width, 0);

                    if (!hitEdge && !hitBlock) current.Move(action);
                }

                if (action == ConsoleKey.RightArrow)
                {

                    hitEdge = current.checkCollision(edges, -Block.Width, 0);
                    hitBlock = current.checkCollision(state, -Block.Width, 0);

                    if (!hitEdge && !hitBlock) current.Move(action);
                }



                //-----------------------Critical Section-----------------------
                //Both the timer and user input threads access this code and change state

                lock (threadLock)
                {
                    if (action == ConsoleKey.DownArrow)
                    {

                        hitEdge = current.checkCollision(edges, 0, -Block.Height);
                        hitBlock = current.checkCollision(state, 0, -Block.Height);

                        if (!hitEdge && !hitBlock) current.Move(action);

                        else
                        {
                            if (delay == 1) //Pauses fall for one cycle upon contact with surface below to allow player to slide block under another
                            {
                                foreach (Block blk in current.getBlocks())
                                {
                                    state.Add(blk);
                                }

                                Lines();

                                Console.Clear();

                                foreach (Block b in state)
                                {
                                    b.inflate();
                                    b.draw();

                                }


                                current = new Shape(board.SpawnPoint.X - Block.Width, board.SpawnPoint.Y, nextRando);
                                current.Arrange();
                                current.render();

                                board.drawBoard();

                                nextRando = rando.Next(7);

                                setStats();


                                if (nextLevel >= 2)
                                {
                                    nextLevel = 0;
                                    level++;
                                    levelUp();
                                    board.drawBoard();
                                    setStats();
                                }

                                alive = !current.checkCollision(state, 0, -1);

                                delay = 0;

                                if (!alive)
                                {
                                    notDead = alive;
                                    return alive;
                                }

                            }

                            else
                            {
                                delay++;

                            }

                        }
                    }
                }

                //-----------------------End Critical Section-----------------------

            }

            return alive;
        }







        //Checks for completed lines deletes any, tracks score and moves any blocks above them to fall onto lower blocks.
        //Inelegant.  Consider replacing with Linq queries if time allows.

        public void Lines()
        {

            int[] YLines = new int[1000]; //Each index represents a y value and stores a count of the number of points with that y value



            //If value for given index in YLines becomes equal to board width then line is complete and is added to Yindexes and toDelete

            List<Block> toDelete = new List<Block>();
            List<int> Yindexes = new List<int>();


            //Any block with a point above the max Y value in Yindexes must fall to lower blocks and is added to fallingBlocks

            List<Block> fallingBlocks = new List<Block>();


            int maxY = 0;
            int minY = int.MaxValue;
            int localLines = 0;
            bool lineCompleted = false;



            //Determine the number of blocks at a given Y value (complete lines)

            foreach (Block blk in state)
            {
                foreach (Point pt in blk.getArea())
                {

                    int temp = YLines[pt.Y];
                    temp++;
                    YLines[pt.Y] = temp;
                    if (temp >= board.Width - Program.margin)
                    {
                        Yindexes.Add(pt.Y);
                        if (pt.Y > maxY) maxY = pt.Y;
                        if (pt.Y < minY) minY = pt.Y;
                        lineCompleted = true;
                    }
                }
            }

            if (!lineCompleted) return;


            //Count the number of completed lines

            for (int i = 0; i < YLines.Length; i++)
            {
                if (YLines[i] >= board.Width - Program.margin)
                {
                    localLines++;
                }
            }

            localLines = localLines / Block.Height;


            //Determine which blocks are in complete lines or above a completed line and are added to toDelete and fallingBlocks respectively

            foreach (Block bk in state)
            {
                foreach (Point p in bk.getArea())
                {
                    bool wasFound = false;
                    foreach (int lin in Yindexes)
                    {
                        if (lin == p.Y)
                        {
                            toDelete.Add(bk);
                            wasFound = true;
                            break;
                        }

                        if (p.Y < minY)
                        {
                            fallingBlocks.Add(bk);
                            wasFound = true;
                            break;
                        }

                    }
                    if (wasFound) break;
                }
            }

            this.lines += localLines;
            this.nextLevel += localLines;
            this.score += (int)Math.Pow(2, localLines);


            foreach (Block b in toDelete)
            {
                state.Remove(b);

                b.erase();
            }


            debrisFall(fallingBlocks, localLines);

        }







        //Causes any block resting on a deleted line to fall to a lower block

        public void debrisFall(List<Block> fallingDebris, int lines)
        {

            for (int i = 0; i < lines; i++)
            {
                foreach (Block bk in fallingDebris)
                {
                    bk.Y += Block.Height;
                    bk.inflate();
                }
            }

        }







        //Sets lines, score, level and displays what the next shape will be.

        public void setStats()
        {
            Console.SetCursorPosition(board.getScore().GetCursorPosition().X, board.getScore().GetCursorPosition().Y);
            Console.ForegroundColor = board.getScore().GetTextColor();
            Console.Write(this.score);
            Console.SetCursorPosition(board.getLines().GetCursorPosition().X, board.getLines().GetCursorPosition().Y);
            Console.ForegroundColor = board.getLines().GetTextColor();
            Console.Write(this.lines);
            Console.SetCursorPosition(board.getLevel().GetCursorPosition().X, board.getLevel().GetCursorPosition().Y);
            Console.ForegroundColor = board.getLevel().GetTextColor();
            Console.Write(this.level);
            Console.ResetColor();
            Shape next;
            //if (nextRando == 5) { next = new Shape(board.getNext().GetCursorPosition().X - Block.Width, board.getNext().GetCursorPosition().Y, nextRando); }
            //else { next = new Shape(board.getNext().GetCursorPosition().X, board.getNext().GetCursorPosition().Y, nextRando); }
            switch (nextRando)
            {
                case 0:
                    next = new Shape(board.getNext().GetCursorPosition().X + Block.Height, board.getNext().GetCursorPosition().Y - 1, nextRando);
                    break;
                case 1:
                    next = new Shape(board.getNext().GetCursorPosition().X - Block.Height, board.getNext().GetCursorPosition().Y - 1, nextRando);
                    break;
                case 4:
                    next = new Shape(board.getNext().GetCursorPosition().X - Block.Height, board.getNext().GetCursorPosition().Y, nextRando);
                    break;
                case 5:
                    next = new Shape(board.getNext().GetCursorPosition().X - Block.Height, board.getNext().GetCursorPosition().Y + 1, nextRando);
                    break;
                default:
                    next = new Shape(board.getNext().GetCursorPosition().X, board.getNext().GetCursorPosition().Y, nextRando);
                    break;
            }
            next.Arrange();
            next.render();
        }









        //displays animation and game over message. Determines if player achieved a high score. Receives user name and decision to play again. Returns to playGame. 

        public ConsoleKey gameOver()
        {
            int length = board.Width - 2;
            int height = 10;
            int centerX = board.Start.X + (board.Width - length) / 2;
            int centerY = board.Start.Y + (board.Height - height) / 2;
            string message = "****GAME OVER****";
            string line2 = "q to quit enter to play again";
            int messX = length > message.Length ? (length - message.Length) / 2 : 0;
            int messY = height / 2;
            int line2X = length > line2.Length ? (length - line2.Length) / 2 : 0;

            timerCounter = 0;
            timer.Interval = ms / 4;
            timer.Elapsed -= OnTimedEvent;
            timer.Elapsed += new ElapsedEventHandler(UponMyDeath);


            while (timerCounter < board.Height)
            {
                int YCursor = board.Start.X + timerCounter;
                for (int j = 0; j < board.Width; j++)
                {
                    int XCursor = board.Start.X + j;
                    Console.SetCursorPosition(XCursor, YCursor);
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write(" ");
                }
            }

            timer.Stop();
            timer.Elapsed -= UponMyDeath;
            timerCounter = 0;

            board.getHighScoreBoard().updateScores(score, board.Start.X + 2, centerY);
            board.getHighScoreBoard().displayScores();

            Console.BackgroundColor = ConsoleColor.Black;
            for (int i = 0; i < height; i++)
            {
                int YCursor = centerY + i;
                for (int j = 0; j < length; j++)
                {
                    int XCursor = centerX + j;
                    Console.SetCursorPosition(XCursor, YCursor);
                    Console.Write(" ");
                }
            }

            Console.ResetColor();

            //Console.SetCursorPosition(centerX + messX, centerY + messY);
            //board.getHighScoreBoard().updateScores(score, board.Start.X + 2, centerY);
            //board.getHighScoreBoard().displayScores();
            Console.SetCursorPosition(centerX + messX, centerY + messY);
            Console.WriteLine(message);
            Console.SetCursorPosition(centerX + line2X, centerY + messY + 1);
            Console.WriteLine(line2);

            ConsoleKeyInfo playAgain;

            do
            {
                playAgain = Console.ReadKey();

            } while (playAgain.Key == ConsoleKey.DownArrow);


            return playAgain.Key;
        }








        //Displays animation on level up.

        public void levelUp()
        {
            timer.Elapsed -= OnTimedEvent;
            ElapsedEventHandler OnLevelUp = UponMyDeath;
            lineTimer.Elapsed += OnLevelUp;
            lineTimer.Enabled = true;
            while (timerCounter < 8)
            {
                if (timerCounter % 2 == 0)
                {
                    board.drawBoard(ConsoleColor.White);
                }
                else
                {
                    board.drawBoard();
                }

            }

            lineTimer.Stop();
            lineTimer.Elapsed -= OnLevelUp;
            timerCounter = 0;
            timer.Elapsed += OnTimedEvent;
            timer.Interval = ms / level;
        }

    }
}

