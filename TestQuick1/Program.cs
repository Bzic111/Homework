using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;

namespace TestQuick1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Console.WriteLine("Loading... ");
            Print("Loading system...", 1000);
            Print("Всякая фигня", 500);
            Print("И т.д.", 2000);

            int pos = Console.CursorTop;
            Console.SetCursorPosition(11, 0);
            Console.Write("OK");
            Console.SetCursorPosition(0, pos);

            var rand = new Random();

            for (int i = 0; i <= 100; i++)
            {
                if (i < 25)
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (i < 50)
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                else if (i < 75)
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                else if (i < 100)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ForegroundColor = ConsoleColor.Green;

                string pct = string.Format("{0,-30} {1,3}%", new string((char)0x2592, i * 30 / 100), i);
                Console.CursorLeft = 0;
                Console.Write(pct);
                Thread.Sleep(rand.Next(0, 50));
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        static void Print(string message, int delay)
        {
            Thread.Sleep(delay);
            Console.WriteLine(message);
        }
    }
    }

