using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace TestQuick1
{
    enum EndGameType
    {
        Win,
        Lose,
        Draw,
        Break
    }
    class Game
    {
        int SizeX { get; set; }
        int SizeY { get; set; }
        int MoveCounter { get; set; }
        char[,] Field { get; set; }
        char Empty { get; } = '_';
        char PlayerOneDot { get; } = 'X';
        char PlayerTwoDot { get; } = 'O';
        int WinSerie { get; set; } = 3;
        bool GameStatus { get; set; } = true;
        void SetSize()
        {
            while (!Int32.TryParse(Console.ReadLine(), out int val))
            {
                SizeY = val;
                SizeX = val;
            }
        }
        public Game()
        {
            SetSize();
            MoveCounter = SizeX * SizeY;
            Field = GetField();
            FillEmpty(Field);
            Play(this);
        }
        void Play(Game game)
        {
            Show();
            do
            {

            } while (this.GameStatus);
        }
        char[,] GetField()
        {
            return new char[SizeY, SizeX];
        }
        void FillEmpty(char[,] str)
        {
            for (int i = 0; i < str.GetLength(0); i++)
            {
                for (int j = 0; j < str.GetLength(1); j++)
                {
                    str[i, j] = Empty;
                }
            }
        }
        void ExitGame()
        {
            GameStatus = false;
        }
        bool EndGame(EndGameType type, string player)
        {
            switch (type)
            {
                case EndGameType.Win:
                    Console.WriteLine($"Congratulation!!! Player {player} WIN!!!");
                    ExitGame();
                    break;
                case EndGameType.Lose:
                    Console.WriteLine("Sorry. Maybe nex time you`ll win. ");
                    ExitGame();
                    break;
                case EndGameType.Draw:
                    Console.WriteLine("It is DRAW.");
                    ExitGame();
                    break;
                case EndGameType.Break:
                    Console.WriteLine("Exit game without ending, progress not saved.");
                    ExitGame();
                    break;
                default:
                    break;
            }
            return false;
        }

        void Selector(int maxX, int maxY, ref char[,] chr, char dot)
        {
            int X = 0, Y = 0;
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        chr[Y, X] = dot;
                        break;
                    case ConsoleKey.Escape:

                        break;
                    case ConsoleKey.LeftArrow:
                        PrintBlack(chr[Y,X], Y, X);
                        X = (X > 0) ? --X : X = maxX;
                        PrintWhite(chr[Y, X], Y, X);
                        break;
                    case ConsoleKey.UpArrow:
                        PrintBlack(chr[Y, X], Y, X);
                        Y = (Y > 0) ? --Y : Y = maxY;
                        PrintWhite(chr[Y, X], Y, X);
                        break;
                    case ConsoleKey.RightArrow:
                        PrintBlack(chr[Y, X], Y, X);
                        X = (X < maxX) ? ++X : X = 0;
                        PrintWhite(chr[Y, X], Y, X);
                        break;
                    case ConsoleKey.DownArrow:
                        PrintBlack(chr[Y, X], Y, X);
                        Y = (Y < maxY) ? ++Y : Y = 0;
                        PrintWhite(chr[Y, X], Y, X);
                        break;
                    default:
                        break;
                }
            } while (true);
        }
        
        void PrintWhite(char chr, int Y, int X)
        {
            Console.CursorLeft = X == 0 ? 1 : (X + 2);
            Console.CursorTop = Y == 0 ? 1 : (Y + 2);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(chr);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        void PrintBlack(char chr, int Y, int X)
        {
            Console.CursorLeft = X == 0 ? 1 : (X + 2);
            Console.CursorTop = Y == 0 ? 1 : (Y + 2);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;            
            Console.Write(chr);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        void Show()
        {
            Console.Write("╔");
            for (int i = 0; i < Field.GetLength(1) - 1; i++)
            {
                Console.Write("═╦");
            }
            Console.Write("═╗\n");
            for (int i = 0; i < Field.GetLength(0); i++)
            {
                if (i < Field.GetLength(0) - 1)
                {

                    for (int j = 0; j < Field.GetLength(1); j++)
                    {
                        Console.Write("║" + Field[i, j]);
                    }
                    Console.Write("║\n╠");
                    for (int l = 0; l < Field.GetLength(1) - 1; l++)
                    {
                        Console.Write("═╬");
                    }
                    Console.WriteLine("═╣");
                }
                else
                {
                    for (int j = 0; j < Field.GetLength(1); j++)
                    {
                        Console.Write("║" + Field[i, j]);
                    }
                    Console.Write("║\n╚");
                    for (int l = 0; l < Field.GetLength(1) - 1; l++)
                    {
                        Console.Write("═╩");
                    }
                    Console.WriteLine("═╝");
                }
            }
        }
        void AIRandom(int maxX, int maxY, ref char[,] str, char playerDot)
        {
            Random rnd = new Random();
            int Y, X;
            do
            {
                Y = rnd.Next(0, maxY);
                X = rnd.Next(0, maxX);
            } while (str[Y, X] != 'X' & str[Y, X] != 'O');
            str[Y, X] = playerDot;
        }
        bool CheckWin(char playerChar, int[] lastDot, char[,] field)
        {
            bool result;
            StringBuilder sb = new StringBuilder();
            int Xmin = lastDot[1] - WinSerie;
            int Xmax = lastDot[1] + WinSerie;
            int Ymin = lastDot[0] - WinSerie;
            int Ymax = lastDot[0] + WinSerie;

            for (int i = Ymin, j = Xmin; (i < Ymax) & (j < Xmax); i++, j++)
            {
                if ((i >= field.GetLength(0)) & (i <= field.GetLength(0)))
                {
                    if ((j >= field.GetLength(1)) & (j <= field.GetLength(1)))
                    {
                        sb = field[i, j] == playerChar ? sb.Append(field[i, j]) : sb.Clear();
                        if (sb.Length == WinSerie)
                        {
                            return true;
                        }
                    }
                }
            }
            result = sb.Length == WinSerie;
            sb.Clear();
            for (int i = Ymin; i < Ymax; i++)
            {
                if ((i >= field.GetLength(0)) & (i <= field.GetLength(0)))
                {
                    sb = field[i, lastDot[1]] == playerChar ? sb.Append(field[i, lastDot[1]]) : sb.Clear();
                    if (sb.Length == WinSerie)
                    {
                        return true;
                    }
                }
            }
            result = sb.Length == WinSerie;
            sb.Clear();
            for (int i = Xmin; i < Xmax; i++)
            {
                if ((i >= field.GetLength(0)) & (i <= field.GetLength(0)))
                {
                    sb = field[lastDot[0], i] == playerChar ? sb.Append(field[lastDot[0], i]) : sb.Clear();
                    if (sb.Length == WinSerie)
                    {
                        return true;
                    }
                }
            }
            result = sb.Length == WinSerie;
            sb.Clear();
            return result;
        }
    }
    class Menu
    {
        string[] Entryes { get; }
        int Columns { get; }
        int Rows { get; }


        void Show()
        {

        }
        string Select(string[] str, int line) => str[line];


        void Selector(int maxX, int maxY, string[] str, ref string selected)
        {
            int X = 0, Y = 0;
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        selected = Select(str, Y + (X * Rows));
                        break;
                    case ConsoleKey.Escape:
                        selected = "Exit";
                        break;
                    case ConsoleKey.LeftArrow:
                        X = (X > 0) ? --X : X = maxX;
                        break;
                    case ConsoleKey.UpArrow:
                        Y = (Y > 0) ? --Y : Y = maxY;
                        break;
                    case ConsoleKey.RightArrow:
                        X = (X < maxX) ? ++X : X = 0;
                        break;
                    case ConsoleKey.DownArrow:
                        Y = (Y < maxY) ? ++Y : Y = 0;
                        break;
                    default:
                        break;
                }
            } while (true);
        }

    }
    class Programm
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            string[,] str = new string[10,10];
            int colStart = 10;
            string selected = null;
            int cursorRow = 0;

            for (int i = 0; i < str.GetLength(0); i++)
            {
                for (int j = 0; j < str.GetLength(1); j++)
                {
                    str[i, j] = i.ToString();
                }
            }



            
            Show(str);


            //do
            //{
            //    Selector(str, out selected, ref cursorRow, ref colStart);
            //} while (selected!="Exit");

        }
        static public string[,] ColsMenu(string[] str, int cols = 2)
        {
            int row = 0;
            if (str.Length % cols == 0)
            {
                row = str.Length / cols;
            }
            else
            {
                row = (str.Length / cols) + 1;
            }
            string[,] newStr = new string[row, cols];
            int l = 0;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < cols & l < str.Length; j++)
                {
                    newStr[i, j] = str[l];
                    l++;
                }
            }
            return newStr;
        }
        static public void Show(string[,] str, ref int step)
        {
            for (int i = 0; i < str.GetLength(0); i++)
            {
                for (int j = 0; j < str.GetLength(1); j++)
                {
                    if (j == 0)
                    {
                        Print(str[i, j], i, j);
                    }
                    else if (j > 0)
                    {
                        Print(str[i, j], i, j * step);
                    }
                }
            }
        }
        static void Show(string[,] Field)
        {
            Console.Write("╔");
            for (int i = 0; i < Field.GetLength(1) - 1; i++)
            {
                Console.Write("═╦");
            }
            Console.Write("═╗\n");
            for (int i = 0; i < Field.GetLength(0); i++)
            {
                if (i < Field.GetLength(0) - 1)
                {

                    for (int j = 0; j < Field.GetLength(1); j++)
                    {
                        Console.Write("║" + Field[i, j]);
                    }
                    Console.Write("║\n╠");
                    for (int l = 0; l < Field.GetLength(1) - 1; l++)
                    {
                        Console.Write("═╬");
                    }
                    Console.WriteLine("═╣");
                }
                else
                {
                    for (int j = 0; j < Field.GetLength(1); j++)
                    {
                        Console.Write("║" + Field[i, j]);
                    }
                    Console.Write("║\n╚");
                    for (int l = 0; l < Field.GetLength(1) - 1; l++)
                    {
                        Console.Write("═╩");
                    }
                    Console.WriteLine("═╝");
                }
            }
        }
        static public void Selector(string[] str, out string selected, ref int cursorRow, ref int cursorCol)
        {
            Console.CursorVisible = false;
            selected = null;
            Print(str[cursorRow], cursorCol, 0);
            var move = Console.ReadKey(false);
            if (move.Key == ConsoleKey.LeftArrow)
            {
                cursorCol = 0;
            }
            else if (move.Key == ConsoleKey.RightArrow)
            {
                cursorCol = 50;
            }
            else if ((move.Key == ConsoleKey.DownArrow) & (cursorRow < (str.Length - 1) / 2))
            {
                Console.SetCursorPosition(0, cursorRow);
                Console.Write(str[cursorRow]);
                cursorRow++;
                Print(str[cursorRow], cursorCol, 0);
            }
            else if ((move.Key == ConsoleKey.DownArrow) & (cursorRow == (str.Length - 1) / 2))
            {
                Console.SetCursorPosition(0, cursorRow);
                Console.Write(str[cursorRow]);
                cursorRow = 0;
                Print(str[cursorRow], cursorCol, 0);
            }
            else if ((move.Key == ConsoleKey.UpArrow) & (cursorRow > 0))
            {
                Console.SetCursorPosition(0, cursorRow);
                Console.Write(str[cursorRow]);
                cursorRow--;
                Print(str[cursorRow], cursorCol, 0);
            }
            else if ((move.Key == ConsoleKey.UpArrow) & (cursorRow == 0))
            {
                Console.SetCursorPosition(0, cursorRow);
                Console.Write(str[cursorRow]);
                cursorRow = (str.Length - 1) / 2;
                Print(str[cursorRow], cursorCol, 0);
            }
            else if (move.Key == ConsoleKey.Enter)
            {
                selected = str[cursorRow];
            }
            else if (move.Key == ConsoleKey.Escape)
            {
                selected = "Exit";
            }
            else if (move.Key == ConsoleKey.Spacebar)
            {
                selected = str[cursorRow];
            }
        }
        public void Print(string text, int row, int col)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(col, row);
            Console.Write(text);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        static public void Print(string text, int row, int col, ConsoleColor bgColor = ConsoleColor.Black, ConsoleColor fntColor = ConsoleColor.White)
        {
            Console.BackgroundColor = bgColor;
            Console.ForegroundColor = fntColor;
            Console.SetCursorPosition(col, row);
            Console.Write(text);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
