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
        static public string Selecting(in string[,] str, ref int[] inCursorUDRL,string mod)
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
                str[inCursorUDRL[0], inCursorUDRL[1]] = mod;
                selected = Selector(inCursorUDRL[0], inCursorUDRL[1], str);

            }

            return selected;
        }
        static public string Selector(int entry, string[] str)
        {
            return str[entry];
        }
        static public string Selector(int entryUD, int entryRL, string[,] str,bool modify=false)
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
        static public void Show(int[] moverUDRL, string[,] str)
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
                    Console.Write(str[i, j]);
                }
                Console.Write("\n");
            }
        }
        static public void Modify(string str)
        {
            
            str = Console.ReadKey(false).ToString();
        }
    }
    class Ships
    {
        static int[] shipsCount = { 4, 3, 2, 1 };
        static int ShipSum = shipsCount[3] + shipsCount[1] + shipsCount[2] + shipsCount[0];
        static string sea = "O";
        static string ship = "X";
        static int x=10, y=10;
        static string[,] field = new string[y,x];
        static public string[,] GetField()
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    field[i, j] = sea;
                }
            }
            return field;
        }

        static public bool FindNear(string[,] str, int pointerY, int pointerX)
        {
            bool result = false;
            return result;
        }
        static public void FillField()
        {
            do
            {

                ShipSum -= 1;
            } while (ShipSum != 0);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string[,] field = Ships.GetField();

            Random num = new Random();
            int startPointX = num.Next(0,10);
            int startPointY = num.Next(0, 10);




            int[] UDRL = { 0, 0 };
                string[,] fld = field;
            string mod = "X";
            do
            {
                Console.Clear();
                SelectorUDRL.Show(UDRL, fld);

                SelectorUDRL.Selecting(fld,ref UDRL,mod);
                
            } while (true);


        }

    }

}