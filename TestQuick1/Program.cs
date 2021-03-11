using System;
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
    class Game
    {
        int SizeX { get; }
        int SizeY { get; }
        char Empty { get; } = '_';
        char PlayerOneDot { get; } = 'X';
        char PlayerTwoDot { get; } = 'O';
        int WinSerie { get; set; } = 3;
        public Game()
        {
            char[,] field = GetField();
            FillEmpty(ref field);
        }
        char[,] GetField()
        {
            return new char[SizeY, SizeX];
        }
        void FillEmpty(ref char[,] str)
        {
            for (int i = 0; i < str.GetLength(0); i++)
            {
                for (int j = 0; j < str.GetLength(1); j++)
                {
                    str[i, j] = Empty;
                }
            }
        }
        bool CheckWin(char playerChar, int[] lastDot, char[,] field)
        {
            StringBuilder sbD = new StringBuilder();
            StringBuilder sbH = new StringBuilder();
            StringBuilder sbV = new StringBuilder();
            int Xmin = lastDot[1] - WinSerie;
            int Xmax = lastDot[1] + WinSerie;
            int Ymin = lastDot[0] - WinSerie;
            int Ymax = lastDot[0] + WinSerie;

            int counter = 0;
            for (int i = Ymin, j = Xmin; (i < Ymax) & (j < Xmax); i++, j++)
            {
                if ((i! < field.GetLength(0)) & (i! > field.GetLength(0)))
                {
                    if ((j! < field.GetLength(1)) & (j! > field.GetLength(1)))
                    {
                        sbD.Append(field[i, j]);
                    }
                }
            }
            for (int i = Ymin; i < Ymax; i++)
            {
                if ((i! < field.GetLength(0)) & (i! > field.GetLength(0)))
                {
                    sbV.Append(field[i, lastDot[1]]);
                }
            }
            for (int i = Xmin; i < Xmax; i++)
            {
                if ((i! < field.GetLength(0)) & (i! > field.GetLength(0)))
                {
                    sbH.Append(field[lastDot[0],i]);
                }
            }
            return false;
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
        string Select(int line) => Entryes[line];

        void Selector(int maxX, int maxY)
        {
            int X = 0, Y = 0;
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        break;
                    case ConsoleKey.Escape:
                        break;
                    case ConsoleKey.LeftArrow:
                        X = (X > 0) ? X-- : X = maxX;
                        break;
                    case ConsoleKey.UpArrow:
                        Y = (Y > 0) ? Y-- : Y = maxY;
                        break;
                    case ConsoleKey.RightArrow:
                        X = (X < maxX) ? X++ : X = 0;
                        break;
                    case ConsoleKey.DownArrow:
                        Y = (Y < maxY) ? Y++ : Y = 0;
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
            string[] str = new string[25];
            int colStart = 10;
            string selected = null;
            int cursorRow = 0;

            for (int i = 0; i < str.Length; i++)
            {
                str[i] = rnd.Next(1000, 2000).ToString();

            }



            str[24] = "Exit";
            Show(ColsMenu(str), ref colStart);
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
