using System;

namespace Тест
{
    class ArrPlay
    {
        static public string[] MenuList = { "Слева направо", "Справа налево", "Показать исходный код", "Назад" };
        static string code = @"static public void DiagonalLR(int[,] arr)
{
    int count = 0;
    for (int i = 0; i < arr.GetLength(0); i++)
    {
        for (int j = 0; j < arr.GetLength(1); j++)
        {
            Console.WriteLine($""{("""").PadLeft(count)}{arr[i, j]}"");
            count++;
        }
    }
}
static public void DiagonalRL(int[,] arr)
{
    int count = arr.Length - 1;
    for (int j = arr.GetLength(1) - 1; j >= 0; j--)
    {
        for (int i = arr.GetLength(0)-1; i >= 0; i--)
        {
            Console.WriteLine($""{("""").PadLeft(count)}{arr[i, j]}"");
            count--;
        }
    }
}";
        /// <summary>
        /// Выводит значения массива <paramref name="arr"/> в консоль по диагонали Слева сверху направо, с отступом ввиде пробелов.
        /// </summary>
        /// <param name="arr">Двумерный массив</param>
        static public void DiagonalLR(int[,] arr)
        {
            int count = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.WriteLine($"{("").PadLeft(count)}{arr[i, j]}");
                    count++;
                }
            }
        }
        /// <summary>
        /// Выводит значения массива <paramref name="arr"/> в консоль по диагонали Справа сверху налево, с отступом ввиде пробелов.
        /// </summary>
        /// <param name="arr">Двумерный массив</param>
        static public void DiagonalRL(int[,] arr)
        {
            int count = arr.Length - 1;
            for (int j = arr.GetLength(1) - 1; j >= 0; j--)
            {
                for (int i = arr.GetLength(0) - 1; i >= 0; i--)
                {
                    Console.WriteLine($"{("").PadLeft(count)}{arr[i, j]}");
                    count--;
                }
            }
        }
        /// <summary>
        /// Возвращает Исходный код главных методов: <code>DiagonalLR</code> <code>DiagonalRL</code>
        /// </summary>
        /// <returns>string</returns>
        static public string ShowCode()
        {
            return code;
        }
    }
    /// <summary>
    /// Класс селектор.
    /// </summary>
    class Selector
    {
        /// <summary>
        /// Реализует переключение между строками в массиве посредством клавиш "вверх" и "вниз".
        /// Возвращает строку из массива <paramref name="str"/> под индексом <paramref name="inCursor"/> 
        /// и меняет значение начального индекса на <paramref name="outCursor"/>
        /// </summary>
        /// <param name="str">Массив строк с пунктами меню</param>
        /// <param name="inCursor">Переменная индекса массива</param>
        /// <param name="outCursor">Изменённая переменная индекса массива</param>
        /// <returns>string[inCursor]</returns>
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
                selected = Select(inCursor, str);
            }
            return selected;
        }

        /// <summary>
        /// Метод отображения массива строк в виде меню. Выделенный пункт меню отображается с белым фоном и чёрным текстом.
        /// </summary>
        /// <param name="mover">индекс строки в массиве</param>
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
        /// <summary>
        /// Возвращает пункт меню в виде строки
        /// </summary>
        /// <param name="entry">индекс строки в массиве</param>
        /// <returns></returns>
        static public string Select(int entry, string[] str)
        {
            return str[entry];
        }
    }
    class MainMenu
    {
        static public string[] MenuList = { "HelloWorld", "Вторая строка", "Третья строка", "Выход" };
    }
    class HelloWorld
    {
        static public string[] Menu = { "На русском", "In english", "Показать исходный код", "Назад" };
        static string code = "Console.WriteLine(\"Hello World!\");";
        static public string ShowCode()
        {
            return code;
        }
        static public void ProgStart(string command)
        {
            if (command == Menu[0])
            {
                Console.WriteLine("Привет Мир");
                Console.ReadLine();
            }
            else if (command == Menu[1])
            {
                Console.WriteLine("Hello World");
                Console.ReadLine();
            }
            else if (command == Menu[2])
            {
                Console.WriteLine(ShowCode());
                Console.ReadLine();
            }
        }

    }
    class MidTemp
    {
        static public void ProgStart()
        {
            float minTemp, maxTemp, midTemp = 0;                                        // Переменные
            bool trying;
            bool exit = true;
            Console.Clear();                                                            // чистим консоль
            do
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("Введите минимальную температуру:");
                    trying = float.TryParse(Console.ReadLine(), out float valMin);
                    minTemp = valMin;
                    if (!trying)
                    {
                        Console.WriteLine("Что-то пошло не так, попробуйте ещё раз.");
                        Console.ReadLine();
                    }
                } while (!trying);
                do
                {
                    Console.Clear();
                    Console.WriteLine("Введите максимальную температуру:");
                    trying = float.TryParse(Console.ReadLine(), out float valMax);
                    maxTemp = valMax;
                    if (!trying)
                    {
                        Console.WriteLine("Что-то пошло не так, попробуйте ещё раз.");
                        Console.ReadLine();
                    }
                } while (!trying);

                if (minTemp < maxTemp)
                {
                    midTemp = (minTemp + maxTemp) / 2;                                           // вычисление средней температуры (a+b)/2
                }
                else
                {
                    Console.WriteLine("Значение минимальной температуры выше значения максимально температуры.\n");
                    Console.ReadLine();
                    exit = false;
                }
            } while ((minTemp >= maxTemp) & exit);
            Console.WriteLine($"Средняя температура: {Math.Round(midTemp, 1)}");         // выводим округлённое значение
            Console.ReadLine();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int cursor = 0;
            string selected = null;
            do
            {
                Console.Clear();
                Selector.Show(cursor, MainMenu.MenuList);
                selected = Selector.Selecting(MainMenu.MenuList, cursor, out cursor);
                if (selected == MainMenu.MenuList[0])
                {
                    cursor = 0;
                    selected = null;
                    do
                    {
                        Console.Clear();
                        Selector.Show(cursor, HelloWorld.Menu);
                        selected = Selector.Selecting(HelloWorld.Menu, cursor, out cursor);
                        HelloWorld.ProgStart(selected);
                    } while (selected != "Назад");
                    cursor = 0;
                    selected = null;
                }
                else if (selected == MainMenu.MenuList[1])
                {
                    MidTemp.ProgStart();
                }
                else if (selected == MainMenu.MenuList[2])
                {
                    int[,] arr = { { 1, 2, 3, 4 }, { 5, 6, 7, 8 } };
                    cursor = 0;
                    selected = null;
                    do
                    {
                        Console.Clear();
                        Selector.Show(cursor, ArrPlay.MenuList);
                        selected = Selector.Selecting(ArrPlay.MenuList, cursor, out cursor);
                        if (selected == "Слева направо")
                        {
                            Console.Clear();
                            ArrPlay.DiagonalLR(arr);
                            Console.ReadLine();
                        }
                        else if (selected == "Справа налево")
                        {
                            Console.Clear();
                            ArrPlay.DiagonalRL(arr);
                            Console.ReadLine();
                        }
                        else if (selected == "Показать исходный код")
                        {
                            Console.Clear();
                            Console.WriteLine(ArrPlay.ShowCode());
                            Console.ReadLine();
                        }
                    } while (selected != "Назад");
                    cursor = 0;
                    selected = null;
                }
            } while (selected != MainMenu.MenuList[3]);
            Console.WriteLine("Bye...");
        }
    }
}
