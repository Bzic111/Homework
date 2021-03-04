using System;
namespace Lesson03
{
    public class HomeWork : MenuSpace.Work
    {
        public string[] Names { get; } =
            {
            "Вывод массива по диагонали.",
            "Список контактов.",
            "Вывод строки в обратном порядке.",
            "SeaBattle",
            "Сдвиг массива"
            };
        public new MenuSpace.Menu.Runner[] AllRuns;
        public MenuSpace.Menu.Runner[] ArrPlayDiagonal;
        public override MenuSpace.Menu.Runner[] GetRunners()
        {
            return AllRuns;
        }
        public MenuSpace.Menu.Runner[] GetArrPlays()
        {
            return ArrPlayDiagonal;
        }
        public override string[] GetNames()
        {
            return Names;
        }
        public HomeWork()
        {
            ArrPlayDiagonal = new MenuSpace.Menu.Runner[]
            {
                DiagonalLR,
                DiagonalRL
            };
            AllRuns = new MenuSpace.Menu.Runner[]
            { 
                ArrToDiagonal,
                ContactList,
                RewindString,
                SeaBattle,
                ArrPlayMove
            };
        }
        void ArrToDiagonal()
        {
            int[,] arr = { { 1, 2, 3, 4 }, { 5, 6, 7, 8 } };
            int count = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.WriteLine($"{("").PadLeft(count)}{arr[i, j]}");
                    count++;
                }
            }

            Console.ReadKey(true);
        }

        /// <summary>
        /// Выводит значения массива <paramref name="arr"/> в консоль по диагонали Слева сверху направо, с отступом ввиде пробелов.
        /// </summary>
        /// <param name="arr">Двумерный массив</param>
        void DiagonalLR()
        {
            int[,] arr = { { 1, 2, 3, 4 }, { 5, 6, 7, 8 } };
            int count = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.WriteLine($"{("").PadLeft(count)}{arr[i, j]}");
                    count++;
                }
            }

            Console.ReadKey(true);
        }
        /// <summary>
        /// Выводит значения массива <paramref name="arr"/> в консоль по диагонали Справа сверху налево, с отступом ввиде пробелов.
        /// </summary>
        /// <param name="arr">Двумерный массив</param>
        void DiagonalRL()
        {
            int[,] arr = { { 1, 2, 3, 4 }, { 5, 6, 7, 8 } };
            int count = arr.Length - 1;
            for (int j = arr.GetLength(1) - 1; j >= 0; j--)
            {
                for (int i = arr.GetLength(0) - 1; i >= 0; i--)
                {
                    Console.WriteLine($"{("").PadLeft(count)}{arr[i, j]}");
                    count--;
                }
            }

            Console.ReadKey(true);
        }
        void ContactList()
        {
            int cursor = 0;
            string selected = null;
            Console.WriteLine("Press any key to start....");
            var move = Console.ReadKey(false);
            string[,] contacts =
                { {"Виктор","Андрей","Александр","Фёдор","Пётр","exit"},
                {"1234567890","6549873215","1234578965","1597534562","7891238524","" },
                {"njhau@turututu.ru","afahaha@turututu.ru","sagaga@turututu.ru","ejukljk@turututu.ru","agsgdffdht@turututu.ru","" } };
            do
            {
                Console.Clear();
                Console.WriteLine("Выберите контакт и нажмите Enter.");
                for (int i = 0; i < contacts.GetLength(1); i++)
                {
                    if (i == cursor)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(contacts[0, i]);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        continue;
                    }
                    Console.WriteLine(contacts[0, i]);
                }
                move = Console.ReadKey(false);
                if (move.Key == ConsoleKey.DownArrow)
                {
                    if (cursor < contacts.GetLength(1) - 1)
                    {
                        cursor = cursor + 1;
                    }

                }
                else if (move.Key == ConsoleKey.UpArrow)
                {
                    if (cursor > 0)
                    {
                        cursor = cursor - 1;
                    }
                }
                else if (move.Key == ConsoleKey.Enter & selected != "exit")
                {
                    selected = contacts[0, cursor];
                    Console.WriteLine($"\n\n{contacts[0, cursor].PadRight(25 - contacts[1, cursor].Length)} {contacts[1, cursor]}{("").PadRight(5)} {contacts[2, cursor]}");
                    move = Console.ReadKey(false);
                }
            } while (selected != "exit");

            Console.ReadKey(true);
        }
        void RewindString()
        {
            Console.WriteLine("Введите текст.");
            string str = Console.ReadLine();
            for (int i = str.Length - 1; i >= 0; i--)
            {
                Console.Write(str[i]);
            }

            Console.ReadKey(true);
        }
        void SeaBattle()
        {
            bool[,] boolField;
            string[,] field = Ships.GetField(out boolField);

            int[] UDRL = { 0, 0 };
            string[,] fld = field;
            string mod = "X";
            string selected = null;
            int good1 = 0;
            SelectorUDRL Selector = new SelectorUDRL();


            do
            {
                int good4 = Ships.GetShipFour(ref fld, ref boolField);
                if (good4 == 1)
                {
                    do
                    {
                        int good3 = Ships.GetShipThree(ref fld, ref boolField);
                        if (good3 == 1)
                        {
                            do
                            {
                                int good2 = Ships.GetShipTwo(ref fld, ref boolField);
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
                Selector.Show(UDRL, fld);
                selected = Selector.Selecting(fld, ref UDRL, mod);
            } while (selected != "Exit");

            Console.ReadKey(true);
        }
        void ArrPlayMove()
        {                                                                               // Переменные
            string[] arr;                                                               // Массив
            string str;                                                                 // Строка для заполнения массива
            string mover;                                                               // Число индекса сдвига

            Console.WriteLine("Введите значения для массива через пробел.");
            str = Console.ReadLine();
            Console.WriteLine("Введите число сдвига массива");
            mover = Console.ReadLine();

            if (Int32.TryParse(mover, out int move))
            {
                Console.WriteLine("move = " + move);
            }
            else
            {
                move = 0;
                Console.WriteLine("Error. move = " + move);
            }

            str = str.Trim();                                                           // Очистка строки от пробелов в начале и в конце строки
            arr = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);                // Заполнение массива значениями

            if (move < 0)                                                               // Цикл сдвига "влево"
            {
                do
                {
                    string temp = arr[0];                                               // временная переменная
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (i < arr.Length - 1)
                        {
                            arr[i] = arr[i + 1];
                        }
                        else
                        {
                            arr[i] = temp;
                        }
                    }
                    move++;
                } while (move != 0);
            }
            else if (move > 0)                                                          // Цикл сдвига "вправо"
            {
                do
                {
                    string temp = arr[arr.Length - 1];                                  // временная переменная
                    for (int i = arr.Length - 1; i >= 0; i--)
                    {
                        if (i > 0)
                        {
                            arr[i] = arr[i - 1];
                        }
                        else
                        {
                            arr[i] = temp;
                        }
                    }
                    move--;
                } while (move != 0);
            }
            else                                                                        // Сдвиг не производится
            {
                Console.WriteLine("move = " + move + " сдвиг не произведён.");
            }

            for (int i = 0; i < arr.Length; i++)                                        // Вывод цикла в консоль
            {
                Console.Write(arr[i] + " ");
            }

            Console.ReadKey(true);
        }
    }
}
