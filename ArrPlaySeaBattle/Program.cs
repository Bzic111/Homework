using System;

namespace ArrPlaySeaBattle
{
    class SelectorUDRL
    {
        static public string Selecting(string[] str, int inCursor, out int outCursor)
        {
            string selected = null;
            var move = Console.ReadKey(false);
            outCursor = inCursor;
            if (move.Key == ConsoleKey.DownArrow)
            {
                if (inCursor < str.Length - 1)
                {
                    outCursor = inCursor + 1;
                }

            }
            else if (move.Key == ConsoleKey.UpArrow)
            {
                if (inCursor > 0)
                {
                    outCursor = inCursor - 1;
                }
            }
            else if (move.Key == ConsoleKey.Enter)
            {
                selected = Selector(inCursor, str);
            }
            return selected;
        }
        static public string Selecting(string[,] str, int inCursorUD, int inCursorRL, out int outCursorUD, out int outCursorRL)
        {
            string selected = null;
            var move = Console.ReadKey(false);
            outCursorUD = inCursorUD;
            outCursorRL = inCursorRL;
            if (move.Key == ConsoleKey.DownArrow)
            {
                if (inCursorUD < str.GetLength(0) - 1)
                {
                    outCursorUD = inCursorUD + 1;
                }

            }
            else if (move.Key == ConsoleKey.UpArrow)
            {
                if (inCursorUD > 0)
                {
                    outCursorUD = inCursorUD - 1;
                }
            }
            else if (move.Key == ConsoleKey.LeftArrow)
            {
                if (inCursorRL > 0)
                {
                    outCursorRL = inCursorRL - 1;
                }
            }
            else if (move.Key == ConsoleKey.RightArrow)
            {
                if (inCursorRL < str.GetLength(1) - 1)
                {
                    outCursorRL = inCursorRL + 1;
                }
            }
            else if (move.Key == ConsoleKey.Enter)
            {
                selected = Selector(inCursorUD, inCursorRL, str);
            }
            return selected;
        }
        static public string Selector(int entry, string[] str)
        {
            return str[entry];
        }
        static public string Selector(int entryUD, int entryRL, string[,] str)
        {
            return str[entryUD, entryRL];
        }
        static public void Show(int mover, string[] str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (i == mover)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(str[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    continue;
                }
                Console.WriteLine(str[i]);
            }
        }
        static public void Show(int moverUD, int moverRL, string[,] str)
        {
            for (int i = 0; i < str.GetLength(0); i++)
            {
                for (int j = 0; j < str.GetLength(1); j++)
                {

                    if (i == moverUD & j == moverRL)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(str[i, j]);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        continue;
                    }
                    Console.Write(str[i, j]);
                }
                Console.Write("\n");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string[,] arr = { { "O","O","O","O","O" }, { "O", "O", "O", "O", "O" }, { "O", "O", "O", "O", "O" }, { "O", "O", "O", "O", "O" }, { "O", "O", "O", "O", "O" } };
            int UD = 0;
            int RL = 0;
            do
            {
                Console.Clear();
                SelectorUDRL.Show(UD, RL, arr);
                SelectorUDRL.Selecting(arr, UD, RL, out UD, out RL);
                
            } while (true);


        }

    }

}