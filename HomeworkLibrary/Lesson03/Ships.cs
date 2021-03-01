using System;
namespace Lesson03
{
    class Ships
    {
        public string Shot(string str)
        {
            string result = null;
            return result;
        }
        static public string[,] GetField(out bool[,] bfld)
        {
            char letter = 'A';
            int x = 11, y = 11;
            string[,] field = new string[y, x];
            bool[,] boolField = new bool[12, 12];

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
                        field[i, j] = "O";
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

        static public bool FindShipInRange(string[,] str, int[] pointer)
        {
            bool result = false;
            for (int i = pointer[0] - 1; (i <= pointer[0] + 1) & (i < str.GetLength(0)); i++)
            {
                for (int j = pointer[1] - 1; (j <= pointer[1] + 1) & (j < str.GetLength(1)); j++)
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
            bool[,] canMap = new bool[3, 3];
            int[] pointer = { pointerY, pointerX };

            if (FindShipInRange(str, pointer) == false)
            {


                for (int i = pointer[0] - 1, mapI = 0; i <= pointer[0] + 1; i++, mapI++)
                {
                    for (int j = pointer[1] - 1, mapJ = 0; j <= pointer[1] + 1; j++, mapJ++)
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
                if (CanGo(str, newblfld, pointerY, pointerX, out _))
                {
                    str[pointerY, pointerX] = "X";
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
            int[] pointer = { num.Next(1, 10), num.Next(1, 10) };
            do
            {
                int pointerY = num.Next(1, 10);
                int pointerX = num.Next(1, 10);
                if (CanGo(str, blfld, pointerY, pointerX, out direction) & (direction == "Up"))
                {

                    if (CanGo(str, blfld, pointerY, pointerX, out _))
                    {
                        str[pointerY - 1, pointerX] = "X";
                        str[pointerY, pointerX] = "X";
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

                    if (CanGo(str, blfld, pointerY, pointerX, out _))
                    {
                        str[pointerY, pointerX] = "X";
                        str[pointerY + 1, pointerX] = "X";
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
                    if (CanGo(str, blfld, pointerY, pointerX - 1, out _))
                    {
                        str[pointerY, pointerX - 1] = "X";
                        str[pointerY, pointerX] = "X";
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
                    if (CanGo(str, blfld, pointerY, pointerX + 1, out _))
                    {
                        str[pointerY, pointerX + 1] = "X";
                        str[pointerY, pointerX] = "X";
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
                            str[pointerY - 2, pointerX] = "X";
                            str[pointerY - 1, pointerX] = "X";
                            str[pointerY, pointerX] = "X";
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
                            str[pointerY, pointerX] = "X";
                            str[pointerY + 1, pointerX] = "X";
                            str[pointerY + 2, pointerX] = "X";
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
                            str[pointerY, pointerX - 2] = "X";
                            str[pointerY, pointerX - 1] = "X";
                            str[pointerY, pointerX] = "X";
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
                            str[pointerY, pointerX + 2] = "X";
                            str[pointerY, pointerX + 1] = "X";
                            str[pointerY, pointerX] = "X";
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
                                str[pointerY - 3, pointerX] = "X";
                                str[pointerY - 2, pointerX] = "X";
                                str[pointerY - 1, pointerX] = "X";
                                str[pointerY, pointerX] = "X";
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
                                str[pointerY, pointerX] = "X";
                                str[pointerY + 1, pointerX] = "X";
                                str[pointerY + 2, pointerX] = "X";
                                str[pointerY + 3, pointerX] = "X";
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
                                str[pointerY, pointerX - 3] = "X";
                                str[pointerY, pointerX - 2] = "X";
                                str[pointerY, pointerX - 1] = "X";
                                str[pointerY, pointerX] = "X";
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
                                str[pointerY, pointerX + 3] = "X";
                                str[pointerY, pointerX + 2] = "X";
                                str[pointerY, pointerX + 1] = "X";
                                str[pointerY, pointerX] = "X";
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
    }
}
