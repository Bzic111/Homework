using System;
using System.Collections.Generic;
using System.IO;

namespace MenuSpace
{
    /*
    public class DoNotUse
    {
        public delegate void Cycler(Dictionary<string, Runner> Dict);
        public delegate void Runner();
        /// <summary>
        /// Новый вид отображения меню.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="col"></param>
        /// <param name="row"></param>
        public void Show(string[] str, int col, int row)
        {
            for (int i = 0; i < str.Length; i++)
            {
                Console.SetCursorPosition(col, row);
                Console.Write(str[i]);
            }
        }

        public void Show(string[] str, string sort, int cols = 2)
        {
            int position = 0;
            if (sort == "Vertical")
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (i < str.Length / 2)
                    {
                        Console.WriteLine(str[i]);
                    }
                    else
                    {
                        Console.SetCursorPosition(50, position++);
                        Console.Write(str[i]);
                    }
                }
            }
            else if (sort == "Horizontal")
            {
                for (int i = 0; i < str.Length - 1; i++)
                {
                    if (i % 2 == 0)
                    {
                        Console.SetCursorPosition(0, position);
                        Console.Write(str[i]);

                    }
                    else
                    {
                        Console.SetCursorPosition(50, position++);
                        Console.Write(str[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Цикл для отображения меню и выбора метода из колекции <paramref name="Dict"/>.
        /// </summary>
        /// <param name="Dict">Коллекция строка + метод для меню, </param>
        public void Cycle(Dictionary<string, Runner> Dict)
        {
            int cursor = 0;                                                             // Устанавливаем курсор выделения текста на 0 (верхняя строка и первый элемент в массиве)
            string[] keys = new string[Dict.Count];                                     // Создание массива для ключей из списка ключей словаря Dict
            Dict.Keys.CopyTo(keys, 0);                                                  // Заполнение массива ключей Dict
            string[] entryes = CreateMenu(keys);                                        // Создание массива для вывода пунктов меню
            string selected;                                                            // Переменная для возврата строки пункта меню
            Console.Clear();                                                            // Очистка консоли
            this.Show(entryes);                                                         // Вывод массива пунктов меню

            do
            {
                this.Selector(entryes, out selected, ref cursor);                       // Метод селектора для меню
                if (selected == entryes[entryes.Length - 1])                            // Условие выхода из меню - последний элемент
                {
                    continue;
                }
                else if (selected == entryes[cursor])                                   // Условие выбора пункта меню
                {
                    Console.Clear();
                    Dict.GetValueOrDefault(entryes[cursor])();                          // Выполнение метода из словаря
                    Console.ReadKey(true);                                              // Ожидание клавиши возврата в меню
                    Console.Clear();                                                    // Очистка консоли
                    this.Show(entryes);                                                 // Вывод массива пунктов меню
                }

            } while (selected != entryes[entryes.Length - 1]);
        }

        /// <summary>
        /// Цикл для вывода массива делегатов объектов ввиде меню.
        /// </summary>
        /// <param name="Dict">Массив меню</param>
        /// <param name="List">Массив подменю</param>
        /// <param name="entryName">Название для строк меню</param>
        public void Cycle(Dictionary<string, Cycler>[] Dict, List<Dictionary<string, Runner>[]> List, string entryName = "Homework ")
        {
            int cursor = 0;                                                             // Устанавливаем курсор выделения текста на 0 (верхняя строка и первый элемент в массиве)
            string[] entryes = CreateMenu(Dict.Length, entryName);                      // Создание массива для вывода пунктов меню
            string selected;                                                            // Переменная для возврата строки пункта меню

            Console.Title = "MainMenu";                                                 // Установка названия окна
            Console.Clear();                                                            // Очистка консоли
            this.Show(entryes);                                                         // Вывод массива пунктов меню

            do
            {
                this.Selector(entryes, out selected, ref cursor);                       // Метод селектора для меню

                if (selected == entryes[entryes.Length - 1])                            // Условие выхода из меню - последний элемент
                {
                    continue;
                }
                else if (selected == entryes[cursor])                                   // Условие выбора пункта меню
                {
                    Console.Title = entryes[cursor];                                    // Установка названия окна
                    Console.Clear();                                                    // Очистка консоли
                    MainCycle(Dict[cursor], List[cursor]);                              // Переход к подменю
                    Console.Title = "MainMenu";                                         // Установка названия окна
                    Console.Clear();                                                    // Очистка консоли
                    this.Show(entryes);                                                 // Вывод массива пунктов меню
                }

            } while (selected != entryes[entryes.Length - 1]);
        }

        /// <summary>
        /// Цикл для отображения основной коллекции методов с названиями ввиде меню. и выбора набора методов 
        /// </summary>
        /// <param name="Dict">Коллекция методов <c>Cycle()</c> для выбора коллеции методов класса <c>IWork</c></param>
        /// <param name="SubDict">Коллекция методов класса <c>IWork</c></param>
        public void MainCycle(Dictionary<string, Cycler> Dict, Dictionary<string, Menu.Runner>[] SubDict)
        {
            int cursor = 0;                                                             // Устанавливаем курсор выделения текста на 0 (верхняя строка и первый элемент в массиве)
            string[] keys = new string[Dict.Count];                                     // Создание массива для ключей из списка ключей словаря SubDict
            Dict.Keys.CopyTo(keys, 0);                                                  // Заполнение массива ключей SubDict
            string[] entryes = CreateMenu(keys);                                        // Создание массива для вывода пунктов меню
            string Title = Console.Title;                                               // Сохранение названия окна
            string selected;                                                            // Переменная для возврата строки пункта меню
            Console.Title = Title;                                                      // Установка названия окна
            Console.Clear();                                                            // Очистка консоли
            this.Show(entryes);                                                         // Вывод массива пунктов меню

            do                                                                          // Цикл переключения между пунктами меню
            {
                this.Selector(entryes, out selected, ref cursor);                       // Метод селектора для меню

                if (selected == entryes[entryes.Length - 1])                            // Условие выхода из меню - последний элемент
                {
                    continue;
                }
                if (selected == entryes[cursor])                                        // Условие выбора пункта меню
                {
                    Console.Title = entryes[cursor];                                    // Установка названия окна ключом словаря Dict
                    Dict.GetValueOrDefault(entryes[cursor])(SubDict[cursor]);           // Выполнение метода из словаря
                    Console.Title = Title;                                              // Установка названия окна
                    Console.Clear();                                                    // Очистка консоли
                    this.Show(entryes);                                                 // Вывод массива пунктов меню
                }

            } while (selected != entryes[entryes.Length - 1]);
        }
        public void SaveToFile(string path, string text)
        {
            File.AppendAllText(path, text);
        }
        public void SaveToFile(string path, string[] text)
        {
            File.AppendAllLines(path, text);
        }
        /// <summary>
        /// Селектор для меню ввиде массива строк. Управляется стрелками клавиатуры и Ввод. Escape - назад или выход.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="inCursorUDRL"></param>
        /// <param name="mod"></param>
        /// <returns></returns>
        public string Selector(in string[,] str, ref int[] inCursorUDRL, string mod)
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
                    selected = str[inCursorUDRL[0], inCursorUDRL[1]];
                }
                else
                {
                    str[inCursorUDRL[0], inCursorUDRL[1]] = mod;
                    selected = str[inCursorUDRL[0], inCursorUDRL[1]];
                }
            }
            else if (move.Key == ConsoleKey.Escape)
            {
                selected = "Exit";
            }
            else if (move.Key == ConsoleKey.Spacebar)
            {
                selected = str[inCursorUDRL[0], inCursorUDRL[1]];
            }
            return selected;
        }

        /// <summary>
        /// Селектор для меню ввиде массива строк. Управляется стрелками клавиатуры Вверх, Вниз и Ввод. Escape - назад или выход.
        /// </summary>
        /// <param name="str">Массив строк</param>
        /// <param name="inCursor">Индекс массива</param>
        /// <param name="selected">Ссылка на строку d массиве <paramref name="str"/>[]</param>
        /// <returns>Строка массива</returns>
        public void Selector(string[] str, ref int inCursor, out string selected)
        {
            selected = null;
            var move = Console.ReadKey(true);
            if (move.Key == ConsoleKey.DownArrow)
            {
                if (inCursor < str.Length - 1)
                {
                    inCursor++;
                }
            }
            else if (move.Key == ConsoleKey.UpArrow)
            {
                if (inCursor > 0)
                {
                    inCursor--;
                }
            }
            else if (move.Key == ConsoleKey.Enter)
            {
                selected = str[inCursor];
            }
            else if (move.Key == ConsoleKey.Escape)
            {
                selected = str[str.Length - 1];
            }
        }

        /// <summary>
        /// Выводит массив строк <paramref name="str"/> в консоль ввиде меню с выделенным элементом массива по индексу <paramref name="mover"/>.
        /// </summary>
        /// <param name="mover">Индекс массива для выделения</param>
        /// <param name="str">Массив строк</param>
        /// <param name="entryColor">Цвет выделения строки</param>
        /// <param name="textColor">Цвет выделенного текста</param>
        void Show(int mover, string[] str, ConsoleColor entryColor = ConsoleColor.White, ConsoleColor textColor = ConsoleColor.Black)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (i == mover)
                {
                    Console.BackgroundColor = entryColor;
                    Console.ForegroundColor = textColor;
                    Console.WriteLine(str[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    continue;
                }
                Console.WriteLine(str[i]);
            }
        }

        /// <summary>
        /// Выводит массив строк <paramref name="str"/> в консоль ввиде таблицы с выделенным элементом массива по индексу <paramref name="moverUDRL"/>.
        /// Выделяет строку массива цветом <paramref name="entryColor"/> и текст этой строки <paramref name="textColor"/>
        /// </summary>
        /// <param name="mover">Одномерныый массив индексов [Y,X]</param>
        /// <param name="str">Массив строк</param>
        /// <param name="entryColor">Цвет выделения строки</param>
        /// <param name="textColor">Цвет выделенного текста</param>
        public void Show(int[] mover, string[,] str, ConsoleColor entryColor = ConsoleColor.White, ConsoleColor textColor = ConsoleColor.Black)
        {
            for (int i = 0; i < str.GetLength(0); i++)
            {
                for (int j = 0; j < str.GetLength(1); j++)
                {

                    if (i == mover[0] & j == mover[1])
                    {
                        Console.BackgroundColor = entryColor;
                        Console.ForegroundColor = textColor;
                        Console.Write(str[i, j]);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        continue;
                    }
                }
                Console.Write("\n");
            }
        }

        /// <summary>
        /// Выводит массив строк <paramref name="str"/> в консоль ввиде таблицы с выделенным элементом массива по индексу <paramref name="mover"/>.
        /// Выделяет строку массива цветом <paramref name="colors" index="[0]"/> и текст этой строки <paramref name="colors" index="[1]"/>.
        /// Так же выделяет строку массива равную <paramref name="entryName"/>. Цвет особой строки <paramref name="colors" index="[2]"/>
        /// и текст <paramref name="colors" index="[3]"/>
        /// </summary>
        /// <param name="mover">Одномерныый массив индексов [Y,X]</param>
        /// <param name="str">Массив строк</param>
        /// <param name="entryName">Особая строка</param>
        /// <param name="colors">Массив цветов</param>
        public void Show(int[] mover, string[,] str, string entryName, ConsoleColor[] colors)
        {
            for (int i = 0; i < str.GetLength(0); i++)
            {
                for (int j = 0; j < str.GetLength(1); j++)
                {

                    if (i == mover[0] & j == mover[1])
                    {
                        Console.BackgroundColor = colors[0];
                        Console.ForegroundColor = colors[1];
                        Console.Write(str[i, j]);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        continue;
                    }
                    if (str[i, j] == entryName)
                    {
                        Console.BackgroundColor = colors[2];
                        Console.ForegroundColor = colors[3];
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
    */
}
