using System;

namespace ArrPlaySeaBattle
{
    class SelectorUDRL
    {
        static public string Selecting(in string[,] str, ref int[] inCursorUDRL, string mod)
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
                    selected = Selector(inCursorUDRL[0], inCursorUDRL[1], str);
                }
                else
                {
                    str[inCursorUDRL[0], inCursorUDRL[1]] = mod;
                    selected = Selector(inCursorUDRL[0], inCursorUDRL[1], str);
                }
            }
            else if (move.Key == ConsoleKey.Escape)
            {
                selected = "Exit";
            }
            return selected;
        }        
        static public string Selector(int entryUD, int entryRL, string[,] str, bool modify = false)
        {
            return str[entryUD, entryRL];
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
        static int x = 11, y = 11;
        static bool[,] boolField = new bool[y + 1, x + 1];
        static string[,] field = new string[y, x];
        static public string[,] GetField(out bool[,] bfld)
        {
            char letter = 'A';

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            field[i, j] = "   ";
                            boolField[i, j] = false;
                        }
                        else
                        {
                            field[i, j] = letter.ToString();
                            boolField[i, j] = false;
                            letter++;
                        }
                    }
                    else if (j == 0)
                    {
                        field[i, j] = i.ToString() + "  ";
                        boolField[i, j] = false;
                    }
                    else
                    {
                        field[i, j] = sea;
                        boolField[i, j] = true;
                    }
                }
            }
            for (int i = boolField.GetLength(0) - 2; i < boolField.GetLength(0); i++)
            {
                for (int j = boolField.GetLength(1) - 2; j < boolField.GetLength(1); j++)
                {
                    boolField[i, j] = false;
                }
            }
            boolField[10, 10] = true;
            field[10, 0] = "10 ";
            bfld = boolField;
            return field;
        }

        static public bool FindShipInRange(string[,] str, int pointerY, int pointerX)
        {
            bool result = false;
            for (int i = pointerY - 1; (i <= pointerY + 1) & (i < str.GetLength(0)); i++)
            {
                for (int j = pointerX - 1; (j <= pointerX + 1) & (j < str.GetLength(1)); j++)
                {
                    if (str[i, j] == "X")
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        static public bool CanGo(string[,] str, bool[,] blfld, int pointerY, int pointerX, out string direction)
        {
            bool result = false;
            direction = null;
            int rangeStartY = pointerY - 1;
            int rangeEndY = pointerY + 1;
            int rangeStartX = pointerX - 1;
            int raneEndX = pointerX + 1;
            bool[,] canMap = new bool[3, 3];

            if (FindShipInRange(str, pointerY, pointerX) == false)
            {


                for (int i = rangeStartY, mapI = 0; i <= rangeEndY; i++, mapI++)
                {
                    for (int j = rangeStartX, mapJ = 0; j <= raneEndX; j++, mapJ++)
                    {
                        canMap[mapI, mapJ] = blfld[i, j];
                    }
                }
                if (canMap[0, 0] & canMap[1, 0] & canMap[2, 0])
                {
                    direction = "Left";
                    result = true;
                }
                else if (canMap[0, 0] & canMap[0, 1] & canMap[0, 2])
                {
                    direction = "Up";
                    result = true;
                }
                else if (canMap[0, 2] & canMap[1, 2] & canMap[2, 2])
                {
                    direction = "Right";
                    result = true;
                }
                else if (canMap[2, 0] & canMap[2, 1] & canMap[2, 2])
                {
                    direction = "Down";
                    result = true;
                }
                else if (canMap[1, 1])
                {
                    direction = "Stop";
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }
        static public int GetShipOne(ref string[,] str, ref bool[,] blfld)
        {
            string direction;
            int count = 0;
            int tryes = 0;
            bool[,] newblfld;
            Random num = new Random();
            int pointerX = num.Next(1, 10);
            int pointerY = num.Next(1, 10);
            do
            {
                newblfld = blfld;
                pointerY = num.Next(1, 10);
                pointerX = num.Next(1, 10);
                if (CanGo(str, newblfld, pointerY, pointerX, out direction))
                {
                    str[pointerY, pointerX] = ship;
                    for (int i = pointerY - 1; i <= pointerY + 1; i++)
                    {
                        for (int j = pointerX - 1; j <= pointerX + 1; j++)
                        {
                            newblfld[i, j] = false;
                        }
                    }
                    count++;
                    blfld = newblfld;
                }
                Console.Write("|");
                if (tryes == 100)
                {
                    return 0;
                }
            } while (count != 4);
            return 1;
        }
        static public int GetShipTwo(ref string[,] str, ref bool[,] blfld)
        {
            string direction;
            int count = 0;
            int tryes = 0;
            Random num = new Random();
            int pointerX = num.Next(1, 10);
            int pointerY = num.Next(1, 10);
            do
            {
                pointerY = num.Next(1, 10);
                pointerX = num.Next(1, 10);
                if (CanGo(str, blfld, pointerY, pointerX, out direction) & (direction == "Up"))
                {

                    if (CanGo(str, blfld, pointerY - 1, pointerX, out direction))
                    {
                        str[pointerY - 1, pointerX] = ship;
                        str[pointerY, pointerX] = ship;
                        for (int i = pointerY - 2; i <= pointerY + 1; i++)
                        {
                            for (int j = pointerX - 1; j <= pointerX + 1; j++)
                            {
                                blfld[i, j] = false;
                            }
                        }
                    }
                    count++;
                }
                else if (CanGo(str, blfld, pointerY, pointerX, out direction) & (direction == "Down"))
                {
                    if (CanGo(str, blfld, pointerY + 1, pointerX, out direction))
                    {
                        str[pointerY, pointerX] = ship;
                        str[pointerY + 1, pointerX] = ship;
                        for (int i = pointerY - 1; i <= pointerY + 2; i++)
                        {
                            for (int j = pointerX - 1; j <= pointerX + 1; j++)
                            {
                                blfld[i, j] = false;
                            }
                        }
                    }
                    count++;
                }
                else if (CanGo(str, blfld, pointerY, pointerX, out direction) & (direction == "Left"))
                {
                    if (CanGo(str, blfld, pointerY, pointerX - 1, out direction))
                    {
                        str[pointerY, pointerX - 1] = ship;
                        str[pointerY, pointerX] = ship;
                        for (int i = pointerY - 1; i <= pointerY + 1; i++)
                        {
                            for (int j = pointerX - 2; j <= pointerX + 1; j++)
                            {
                                blfld[i, j] = false;
                            }
                        }
                    }
                    count++;
                }
                else if (CanGo(str, blfld, pointerY, pointerX, out direction) & (direction == "Right"))
                {
                    if (CanGo(str, blfld, pointerY, pointerX + 1, out direction))
                    {
                        str[pointerY, pointerX + 1] = ship;
                        str[pointerY, pointerX] = ship;
                        for (int i = pointerY - 1; i <= pointerY + 1; i++)
                        {
                            for (int j = pointerX - 1; j <= pointerX + 2; j++)
                            {
                                blfld[i, j] = false;
                            }
                        }
                    }
                    count++;
                }

                Console.Write("|");
                if (tryes == 100)
                {
                    return 0;
                }
            } while (count != 3);
            return 1;
        }
        static public int GetShipThree(ref string[,] str, ref bool[,] blfld)
        {
            string direction;
            int count = 0;
            int tryes = 0;
            Random num = new Random();
            int pointerX = num.Next(1, 10);
            int pointerY = num.Next(1, 10);
            do
            {
                pointerY = num.Next(1, 10);
                pointerX = num.Next(1, 10);
                if (CanGo(str, blfld, pointerY, pointerX, out direction) & (direction == "Up"))
                {
                    if ((CanGo(str, blfld, pointerY - 1, pointerX, out direction)) & (direction == "Up"))
                    {

                        if (CanGo(str, blfld, pointerY - 2, pointerX, out direction))
                        {
                            str[pointerY - 2, pointerX] = ship;
                            str[pointerY - 1, pointerX] = ship;
                            str[pointerY, pointerX] = ship;
                            for (int i = pointerY - 3; i <= pointerY + 1; i++)
                            {
                                for (int j = pointerX - 1; j <= pointerX + 1; j++)
                                {
                                    blfld[i, j] = false;
                                }
                            }
                            count++;
                        }
                    }
                }
                else if (CanGo(str, blfld, pointerY, pointerX, out direction) & (direction == "Down"))
                {
                    if (CanGo(str, blfld, pointerY + 1, pointerX, out direction) & (direction == "Down"))
                    {
                        if (CanGo(str, blfld, pointerY + 2, pointerX, out direction))
                        {
                            str[pointerY, pointerX] = ship;
                            str[pointerY + 1, pointerX] = ship;
                            str[pointerY + 2, pointerX] = ship;
                            for (int i = pointerY - 1; i <= pointerY + 3; i++)
                            {
                                for (int j = pointerX - 1; j <= pointerX + 1; j++)
                                {
                                    blfld[i, j] = false;
                                }
                            }
                            count++;
                        }
                    }

                }
                else if (CanGo(str, blfld, pointerY, pointerX, out direction) & (direction == "Left"))
                {
                    if (CanGo(str, blfld, pointerY, pointerX - 1, out direction) & (direction == "Left"))
                    {
                        if (CanGo(str, blfld, pointerY, pointerX - 2, out direction))
                        {
                            str[pointerY, pointerX - 2] = ship;
                            str[pointerY, pointerX - 1] = ship;
                            str[pointerY, pointerX] = ship;
                            for (int i = pointerY - 1; i <= pointerY + 1; i++)
                            {
                                for (int j = pointerX - 3; j <= pointerX + 1; j++)
                                {
                                    blfld[i, j] = false;
                                }
                            }
                            count++;
                        }
                    }
                }
                else if (CanGo(str, blfld, pointerY, pointerX, out direction) & (direction == "Right"))
                {
                    if (CanGo(str, blfld, pointerY, pointerX + 1, out direction) & (direction == "Right"))
                    {

                        if (CanGo(str, blfld, pointerY, pointerX + 2, out direction))
                        {
                            str[pointerY, pointerX + 2] = ship;
                            str[pointerY, pointerX + 1] = ship;
                            str[pointerY, pointerX] = ship;
                            for (int i = pointerY - 1; i <= pointerY + 1; i++)
                            {
                                for (int j = pointerX - 1; j <= pointerX + 3; j++)
                                {
                                    blfld[i, j] = false;
                                }
                            }
                            count++;
                        }
                    }

                }

                Console.Write("|");
                if (tryes == 100)
                {
                    return 0;
                }
            } while (count != 2);
            return 1;
        }
        static public int GetShipFour(ref string[,] str, ref bool[,] blfld)
        {
            string direction;
            int count = 0;
            int tryes = 0;
            Random num = new Random();
            int pointerX = num.Next(1, 10);
            int pointerY = num.Next(1, 10);
            do
            {
                pointerY = num.Next(1, 10);
                pointerX = num.Next(1, 10);
                if (CanGo(str, blfld, pointerY, pointerX, out direction) & (direction == "Up"))
                {
                    if ((CanGo(str, blfld, pointerY - 1, pointerX, out direction)) & (direction == "Up"))
                    {
                        if ((CanGo(str, blfld, pointerY - 2, pointerX, out direction)) & (direction == "Up"))
                        {
                            if (CanGo(str, blfld, pointerY - 3, pointerX, out direction))
                            {
                                str[pointerY - 3, pointerX] = ship;
                                str[pointerY - 2, pointerX] = ship;
                                str[pointerY - 1, pointerX] = ship;
                                str[pointerY, pointerX] = ship;
                                for (int i = pointerY - 4; i <= pointerY + 1; i++)
                                {
                                    for (int j = pointerX - 1; j <= pointerX + 1; j++)
                                    {
                                        blfld[i, j] = false;
                                    }
                                }
                                count++;
                            }
                        }
                    }
                }
                else if (CanGo(str, blfld, pointerY, pointerX, out direction) & (direction == "Down"))
                {
                    if (CanGo(str, blfld, pointerY + 1, pointerX, out direction) & (direction == "Down"))
                    {
                        if ((CanGo(str, blfld, pointerY + 2, pointerX, out direction)) & (direction == "Down"))
                        {
                            if (CanGo(str, blfld, pointerY + 3, pointerX, out direction))
                            {
                                str[pointerY, pointerX] = ship;
                                str[pointerY + 1, pointerX] = ship;
                                str[pointerY + 2, pointerX] = ship;
                                str[pointerY + 3, pointerX] = ship;
                                for (int i = pointerY - 1; i <= pointerY + 4; i++)
                                {
                                    for (int j = pointerX - 1; j <= pointerX + 1; j++)
                                    {
                                        blfld[i, j] = false;
                                    }
                                }
                                count++;
                            }
                        }
                    }
                }
                else if (CanGo(str, blfld, pointerY, pointerX, out direction) & (direction == "Left"))
                {
                    if (CanGo(str, blfld, pointerY, pointerX - 1, out direction) & (direction == "Left"))
                    {
                        if ((CanGo(str, blfld, pointerY, pointerX - 2, out direction)) & (direction == "Left"))
                        {

                            if (CanGo(str, blfld, pointerY, pointerX - 3, out direction))
                            {
                                str[pointerY, pointerX - 3] = ship;
                                str[pointerY, pointerX - 2] = ship;
                                str[pointerY, pointerX - 1] = ship;
                                str[pointerY, pointerX] = ship;
                                for (int i = pointerY - 1; i <= pointerY + 1; i++)
                                {
                                    for (int j = pointerX - 4; j <= pointerX + 1; j++)
                                    {
                                        blfld[i, j] = false;
                                    }
                                }
                                count++;
                            }
                        }
                    }
                }
                else if (CanGo(str, blfld, pointerY, pointerX, out direction) & (direction == "Right"))
                {
                    if (CanGo(str, blfld, pointerY, pointerX + 1, out direction) & (direction == "Right"))
                    {
                        if ((CanGo(str, blfld, pointerY, pointerX + 2, out direction)) & (direction == "Right"))
                        {

                            if (CanGo(str, blfld, pointerY, pointerX + 3, out direction))
                            {
                                str[pointerY, pointerX + 3] = ship;
                                str[pointerY, pointerX + 2] = ship;
                                str[pointerY, pointerX + 1] = ship;
                                str[pointerY, pointerX] = ship;
                                for (int i = pointerY - 1; i <= pointerY + 1; i++)
                                {
                                    for (int j = pointerX - 1; j <= pointerX + 4; j++)
                                    {
                                        blfld[i, j] = false;
                                    }
                                }
                                count++;
                            }
                        }
                    }

                }

                Console.Write("|");
                if (tryes == 100)
                {
                    return 0;
                }
            } while (count != 1);
            return 1;
        }
        static public void ShowBlfld(bool[,] fld)
        {
            for (int i = 0; i < boolField.GetLength(0); i++)
            {
                for (int j = 0; j < boolField.GetLength(1); j++)
                {
                    if (boolField[i, j] == false)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(boolField[i, j]);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        continue;
                    }
                    Console.Write(boolField[i, j]);
                }
                Console.Write("\n");
            }
            Console.ReadKey();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            bool[,] boolField;
            string[,] field = Ships.GetField(out boolField);

            int[] UDRL = { 0, 0 };
            string[,] fld = field;
            string mod = "X";
            string selected = null;
            int good4 = 0;
            int good3 = 0;
            int good2 = 0;
            int good1 = 0;

            do
            {
                good4 = Ships.GetShipFour(ref fld, ref boolField);
                if (good4 == 1)
                {
                    do
                    {
                        good3 = Ships.GetShipThree(ref fld, ref boolField);
                        if (good3 == 1)
                        {
                            do
                            {
                                good2 = Ships.GetShipTwo(ref fld, ref boolField);
                                if (good2 == 1)
                                {
                                    do
                                    {
                                        good1 = Ships.GetShipOne(ref fld, ref boolField);
                                    } while (good1 != 1);
                                }
                            } while (good1 != 1);
                        }
                    } while (good1 != 1);
                }
            } while (good1 != 1);

            do
            {
                Console.Clear();
                SelectorUDRL.Show(UDRL, fld);

                selected = SelectorUDRL.Selecting(fld, ref UDRL, mod);

            } while (selected != "Exit");


        }

    }

}