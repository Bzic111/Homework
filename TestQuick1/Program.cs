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
                row = (str.Length / cols)+1;
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
                    else if (j>0)
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
            else if ((move.Key == ConsoleKey.DownArrow) & (cursorRow < (str.Length - 1)/2))
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
