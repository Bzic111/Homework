using System;
namespace Lesson03
{
    class SelectorUDRL
    {
        public string Selecting(in string[,] str, ref int[] inCursorUDRL, string mod)
        {
            string selected = null;
            var move = Console.ReadKey(false);

            if (move.Key == ConsoleKey.DownArrow)
            {
                if (inCursorUDRL[0] < str.GetLength(0) - 1)
                {
                    ++inCursorUDRL[0];
                }

            }
            else if (move.Key == ConsoleKey.UpArrow)
            {
                if (inCursorUDRL[0] > 0)
                {
                    --inCursorUDRL[0];
                }
            }
            else if (move.Key == ConsoleKey.LeftArrow)
            {
                if (inCursorUDRL[1] > 0)
                {
                    --inCursorUDRL[1];
                }
            }
            else if (move.Key == ConsoleKey.RightArrow)
            {
                if (inCursorUDRL[1] < str.GetLength(1) - 1)
                {
                    ++inCursorUDRL[1];
                }
            }
            else if (move.Key == ConsoleKey.Enter)
            {
                if ((inCursorUDRL[0] == 0) | (inCursorUDRL[1] == 0))
                {
                    selected = Selector(inCursorUDRL, str);
                }
                else
                {
                    str[inCursorUDRL[0], inCursorUDRL[1]] = mod;
                    selected = Selector(inCursorUDRL, str);
                }
            }
            else if (move.Key == ConsoleKey.Escape)
            {
                selected = "Exit";
            }
            else if (move.Key == ConsoleKey.Spacebar)
            {
                selected = Selector(inCursorUDRL, str);
            }
            return selected;
        }
        public string Selector(int[] inCursorUDRL, string[,] str, bool modify = false)
        {
            return str[inCursorUDRL[0], inCursorUDRL[1]];
        }
        public void Show(int[] moverUDRL, string[,] str)
        {
            for (int i = 0; i < str.GetLength(0); i++)
            {
                for (int j = 0; j < str.GetLength(1); j++)
                {

                    if (i == moverUDRL[0] & j == moverUDRL[1])
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(str[i, j]);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        continue;
                    }
                    if (str[i, j] == "X")
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
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
}
