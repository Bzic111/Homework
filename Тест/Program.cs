using System;

namespace Тест
{
    class Selector
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
                selected = UI.Selector(inCursor, str);
            }
            return selected;
        }

    }
    abstract class UI
    {
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
        static public string Selector(int entry, string[] str)
        {
            return str[entry];
        }
    }
    class MainMenu : UI
    {
        static public string[] MenuList = { "HelloWorld", "Вторая строка", "Третья строка", "Выход" };
    }
    class HelloWorld : UI
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
            } while ((minTemp >= maxTemp)&exit);
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
                UI.Show(cursor, MainMenu.MenuList);
                selected = Selector.Selecting(MainMenu.MenuList, cursor, out cursor);
                if (selected == MainMenu.MenuList[0])
                {
                    cursor = 0;
                    selected = null;
                    do
                    {
                        Console.Clear();
                        UI.Show(cursor, HelloWorld.Menu);
                        selected = Selector.Selecting(HelloWorld.Menu, cursor, out cursor);
                        HelloWorld.ProgStart(selected);
                    } while (selected != "Назад");
                }
                else if (selected == MainMenu.MenuList[1])
                {
                    MidTemp.ProgStart();
                }
            } while (selected != MainMenu.MenuList[3]);
        }
    }
}
