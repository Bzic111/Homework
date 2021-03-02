using System;
using System.Collections.Generic;

namespace MenuSpace
{
    public class Menu
    {
        public delegate void Cycler(Dictionary<string, Runner> Dict);
        public delegate void Runner();

        /// <summary>
        /// Метод вывода текста с определённой позиции.
        /// </summary>
        /// <param name="text">Текст</param>
        /// <param name="row">позиция строки</param>
        /// <param name="col">позиция столбца</param>
        public void Print(string text, int row, int col)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(col, row);
            Console.Write(text);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Метод создания массива строк меню на основе массива строк <paramref name="str"/>. Массив строк копируется в пункты меню.
        /// </summary>
        /// <param name="str">Массива строк</param>
        /// <returns>Массива строк для меню, последний элемент "Exit"</returns>
        string[] CreateMenu(string[] str)
        {
            string[] menu = new string[str.Length + 1];
            for (int i = 0; i < menu.Length; i++)
            {
                if (i < menu.Length - 1)
                {

                    menu[i] = str[i];
                }
                else if (i == menu.Length - 1)
                {
                    menu[i] = "Exit";
                }
            }
            return menu;
        }

        /// <summary>
        /// Метод создания массива строк меню. 
        /// </summary>
        /// <param name="length">Число основных пунктов</param>
        /// <param name="name">Имя пункта. По умоляанию "Defaul name"</param>
        /// <returns>Массив строк с именами <paramref name="name"/> и номером, последний элемент "Exit"</returns>
        string[] CreateMenu(int length, string name = "Defaul name")
        {
            string[] menu = new string[length + 1];
            for (int i = 0; i < length + 1; i++)
            {
                if (i < menu.Length - 1)
                {

                    menu[i] = name + $" {i + 1}";
                }
                else if (i == menu.Length - 1)
                {
                    menu[i] = "Exit";
                }
            }
            return menu;
        }

        /// <summary>
        /// Селектор для меню ввиде массива строк. Управляется стрелками клавиатуры Вверх, Вниз и Ввод или Пробел. Escape - назад или выход.
        /// </summary>
        /// <param name="str">Массив строк меню</param>
        /// <param name="selected">Выбранный пункт меню</param>
        public void Selector(string[] str, out string selected, ref int cursorRow)
        {
            Console.CursorVisible = false;
            selected = null;
            Print(str[cursorRow], cursorRow, 0);
            var move = Console.ReadKey(false);
            if ((move.Key == ConsoleKey.DownArrow) & (cursorRow < str.Length - 1))
            {
                Console.SetCursorPosition(0, cursorRow);
                Console.Write(str[cursorRow]);
                cursorRow++;
                Print(str[cursorRow], cursorRow, 0);
            }
            else if ((move.Key == ConsoleKey.UpArrow) & (cursorRow > 0))
            {
                Console.SetCursorPosition(0, cursorRow);
                Console.Write(str[cursorRow]);
                cursorRow--;
                Print(str[cursorRow], cursorRow, 0);
            }
            else if (move.Key == ConsoleKey.Enter)
            {
                selected = str[cursorRow];
            }
            else if (move.Key == ConsoleKey.Escape)
            {
                selected = "Exit";
            }
            else if (move.Key == ConsoleKey.Spacebar)
            {
                selected = str[cursorRow];
            }
        }

        /// <summary>
        /// Цикл вывода массива строк
        /// </summary>
        /// <param name="str">массив строк</param>
        public void Show(string[] str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                Console.WriteLine(str[i]);
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
    }

    public class Collection
    {
        public Dictionary<string, Menu.Runner>[] SetSubmenu(List<Work> List, string[] menuNames)
        {
            Dictionary<string, Menu.Runner>[] dict = new Dictionary<string, Menu.Runner>[List.Count];
            for (int i = 0; i < List.Count; i++)
            {
                dict[i] = new Dictionary<string, Menu.Runner>
                {
                    { menuNames[0],List[i].Start },
                    { menuNames[1],List[i].GetCode }
                };
            }
            return dict;
        }

        public Dictionary<string, Menu.Cycler> SetCycler(List<Work> List, Menu Menu)
        {
            Dictionary<string, Menu.Cycler> Cycler = new Dictionary<string, Menu.Cycler>();
            for (int i = 0; i < List.Count; i++)
            {
                Cycler.Add(List[i].GetName(), Menu.Cycle);
            }
            return Cycler;
        }
        public void ReSetRunner(ref List<Dictionary<string, Menu.Runner>[]> dict, int entry, string[] menuNames, Menu.Runner[] runner)
        {
            Dictionary<string, Menu.Runner>[] newDict = new Dictionary<string, Menu.Runner>[runner.Length];
            int i = 0;
            for ( i = 0; i < runner.Length; i++)
            {
                //dict[entry - 1][i].Clear();
                dict[entry - 1][i].Add(menuNames[i], runner[i]);                
            }
        }
        
    }


}
