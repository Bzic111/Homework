using System;
using System.Collections;
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
            for (int i = 0; i < length+1; i++)
            {
                if (i < menu.Length - 1)
                {

                    menu[i] = name+$" {i+1}";
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
            string[] entryes = CreateMenu(keys);                                        // Создание массива для вывода пунктов меню
            Dict.Keys.CopyTo(keys, 0);                                                  // Заполнение массива ключей Dict
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
            string[] entryes = CreateMenu(keys);                                        // Создание массива для вывода пунктов меню
            Dict.Keys.CopyTo(keys, 0);                                                  // Заполнение массива ключей SubDict
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
    public abstract class Work
    {
        string Name { get; }
        string Code { get; }
        public virtual void GetCode() { Console.WriteLine(this.Code); }
        public virtual string GetName() { return this.Name; }
        public abstract void Start();
    }

    public class DoNotUse
    {
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

}
namespace Lesson01
{
    public class Work01 : MenuSpace.Work
    {
        public string Name { get; } = "Программа приветствие.";
        public string Code { get; } = @"Dictionary<string, string> DayOfWeek = new Dictionary<string, string>
        {
            { ""Monday"" , ""Понедельник"" },
            {""Tuesday"",""Вторник"" },
            {""Wednesday"",""Среда"" },
            {""Thursday"",""Четверг"" },
            {""Friday"",""Пятница"" },
            {""Saturday"",""Суббота"" },
            {""Sunday"",""Воскресенье"" }
        };


    DateTime currentDateTime = DateTime.Now;
    void Start()
    {

        Console.WriteLine(""Введите Имя."");
        string name = Console.ReadLine();
        string dayName = DayOfWeek.GetValueOrDefault(currentDateTime.DayOfWeek.ToString());
        Console.WriteLine($""Здравствуйте! {name} Сегодня {currentDateTime.ToString(""D"")} {dayName}"");
    }";

        static Dictionary<string, string> DayOfWeek = new Dictionary<string, string>
        {
            { "Monday" , "Понедельник" },
            {"Tuesday","Вторник" },
            {"Wednesday","Среда" },
            {"Thursday","Четверг" },
            {"Friday","Пятница" },
            {"Saturday","Суббота" },
            {"Sunday","Воскресенье" }
        };
        public override void GetCode()
        {
            Console.WriteLine(Code);
        }
        public override string GetName()
        {
            return Name;
        }


        static DateTime currentDateTime = DateTime.Now;
        public override void Start()
        {

            Console.WriteLine("Введите Имя.");
            string name = Console.ReadLine();
            string dayName = DayOfWeek.GetValueOrDefault(currentDateTime.DayOfWeek.ToString());
            Console.WriteLine($"Здравствуйте! {name} Сегодня {currentDateTime.ToString("D")} {dayName}");
        }
    }
}
namespace Lesson02
{


    public class Work01 : MenuSpace.Work
    {
        public string Name { get; } = "Номер месяца";
        public string Code { get; } = @"[Flags]
public enum Months
{
    Январь = 1,
    Февраль,
    Март,
    Апрель,
    Май,
    Июнь,
    Июль,
    Август,
    Сентябрь,
    Октябрь,
    Ноябрь,
    Декабрь,
}
public void Start()
{
    Console.WriteLine($""Введите номер месяца или Now:"");
    string str = Console.ReadLine();
    switch (str)
    {
        case ""Now"":
            Console.WriteLine($"" {(Months)DateTime.Now.Month}"");
            break;
        default:
            if (Int32.TryParse(str, out int value))
            {
                if (value > 0 & value <= 12)
                {
                    Console.WriteLine((Months) value);
                }
            }
            else
            {
                Console.WriteLine($""Ошибка ввода"");
                break;
            }
            break;
    }
}";
        public override void GetCode()
        {
            Console.WriteLine(Code);
        }
        public override string GetName()
        {
            return Name;
        }
        [Flags]
        public enum Months
        {
            Январь = 1,
            Февраль,
            Март,
            Апрель,
            Май,
            Июнь,
            Июль,
            Август,
            Сентябрь,
            Октябрь,
            Ноябрь,
            Декабрь,
        }
        public override void Start()
        {
            Console.WriteLine($"Введите номер месяца или Now:");
            string str = Console.ReadLine();
            switch (str)
            {
                case "Now":
                    Console.WriteLine($" {(Months)DateTime.Now.Month}");
                    break;
                default:
                    if (Int32.TryParse(str, out int value))
                    {
                        if (value > 0 & value <= 12)
                        {
                            Console.WriteLine((Months)value);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка ввода");
                        break;
                    }
                    break;
            }
        }
    }
    public class Work02 : MenuSpace.Work
    {
        public string Name { get; } = "Средняя температура";
        public string Code { get; } = @"static void Start()
{
    float minTemp, maxTemp, midTemp = 0;                                        // Переменные
    Console.Clear();                                                            // чистим консоль
GetMinTemp:                                                                     // точка возврата при ошибке ввода 1
    Console.WriteLine(""Введите минимальную температуру:"");
        if (float.TryParse(Console.ReadLine(), out float valMin))
        {
            minTemp = valMin;
        }
        else
        {
            Console.WriteLine(""Что-то пошло не так, попробуйте ещё раз."");
            goto GetMinTemp;
        }


    Console.WriteLine(""Введите максимальную температуру:"");
GetMaxTemp:                                                                     // точка возврата при ошибке ввода 2
    if (float.TryParse(Console.ReadLine(), out float valMax))
    {
        maxTemp = valMax;
    }
    else
    {
        Console.WriteLine(""Что-то пошло не так, попробуйте ещё раз."");
        goto GetMaxTemp;
    }
    if (minTemp < maxTemp)
    {
        midTemp = (minTemp + maxTemp) / 2;                                           // вычисление средней температуры (a+b)/2
    }
    else
    {
        Console.WriteLine(""Значение минимальной температуры выше значения максимально температуры.\n Хотите начать заново? y|n"");
        switch (Console.ReadLine())
        {
            case ""Y"":
            case ""y"":
                goto GetMinTemp;
            default:
                break;
        }
    }
    Console.WriteLine($""Средняя температура: {Math.Round(midTemp, 1)}"");         // выводим округлённое значение
}";
        public override void GetCode()
        {
            Console.WriteLine(Code);
        }
        public override string GetName()
        {
            return Name;
        }
        public override void Start()
        {
            float minTemp, maxTemp, midTemp = 0;                                        // Переменные
            Console.Clear();                                                            // чистим консоль
        GetMinTemp:                                                                     // точка возврата при ошибке ввода 1
            Console.WriteLine("Введите минимальную температуру:");
            if (float.TryParse(Console.ReadLine(), out float valMin))
            {
                minTemp = valMin;
            }
            else
            {
                Console.WriteLine("Что-то пошло не так, попробуйте ещё раз.");
                goto GetMinTemp;
            }


            Console.WriteLine("Введите максимальную температуру:");
        GetMaxTemp:                                                                     // точка возврата при ошибке ввода 2
            if (float.TryParse(Console.ReadLine(), out float valMax))
            {
                maxTemp = valMax;
            }
            else
            {
                Console.WriteLine("Что-то пошло не так, попробуйте ещё раз.");
                goto GetMaxTemp;
            }
            if (minTemp < maxTemp)
            {
                midTemp = (minTemp + maxTemp) / 2;                                           // вычисление средней температуры (a+b)/2
            }
            else
            {
                Console.WriteLine("Значение минимальной температуры выше значения максимально температуры.\n Хотите начать заново? y|n");
                switch (Console.ReadLine())
                {
                    case "Y":
                    case "y":
                        goto GetMinTemp;
                    default:
                        break;
                }
            }
            Console.WriteLine($"Средняя температура: {Math.Round(midTemp, 1)}");         // выводим округлённое значение
        }
    }
    public class Work03 : MenuSpace.Work
    {
        public string Name { get; } = "Чётное или нечётное число.";

        public string Code { get; } = @"public void Start()
{
    Console.WriteLine($""Введите число"");
GetValue:
    if (int.TryParse(Console.ReadLine(), out int val))
    {
        if (val % 2 == 0)
        {
            Console.WriteLine($""Число чётное"");
        }
        else
        {
            Console.WriteLine($""Число не чётное"");
        }
    }
    else
    {
        Console.WriteLine(""Что-то пошло не так, попробуйте ещё раз."");
        goto GetValue;
    }
}";
        public override void GetCode()
        {
            Console.WriteLine(Code);
        }
        public override string GetName()
        {
            return Name;
        }
        public override void Start()
        {
            Console.WriteLine($"Введите число");
        GetValue:
            if (int.TryParse(Console.ReadLine(), out int val))
            {
                if (val % 2 == 0)
                {
                    Console.WriteLine($"Число чётное");
                }
                else
                {
                    Console.WriteLine($"Число не чётное");
                }
            }
            else
            {
                Console.WriteLine("Что-то пошло не так, попробуйте ещё раз.");
                goto GetValue;
            }
        }
    }
    public class Work04 : MenuSpace.Work
    {
        public string Name { get; } = "Температура, сезоны, месяца";

        public string Code { get; } = @"[Flags]
        public enum Months                                                              //Перечислитель enum
        {
            Error = 0b000000000000,
            Январь = 0b_000_000_000_001,
            Февраль = 0b_000_000_000_010,
            Март = 0b_000_000_000_100,
            Апрель = 0b_000_000_001_000,
            Май = 0b_000_000_010_000,
            Июнь = 0b_000_000_100_000,
            Июль = 0b_000_001_000_000,
            Август = 0b_000_010_000_000,
            Сентябрь = 0b_000_100_000_000,
            Октябрь = 0b_001_000_000_000,
            Ноябрь = 0b_010_000_000_000,
            Декабрь = 0b_100_000_000_000,
            Зима = Январь | Февраль | Декабрь,
            Весна = Март | Апрель | Май,
            Лето = Июнь | Июль | Август,
            Осень = Сентябрь | Октябрь | Ноябрь
        }
public void Start()
        {
            int a;
            Months mon = Months.Error;                                                  // Месяца
            float minTemp;                                                              // минимальная температура
            float maxTemp;                                                              // максимальная температура
            float midTemp;                                                              // средняя температура
            float normalMidTempDay = 0;                                                 // Нормальная температура в месяце днём
            float normalMidTempNight = 0;                                               // Нормальная температура в месяце ночью
            string month = ""Месяце"";                                                  // название месяца для текста вывода
            Console.WriteLine($""Введите номер месяца:"");
        Again:                                                                          // точка возврата при ошибке ввода номера месяца
            if (Int32.TryParse(Console.ReadLine(), out int val))
            {
                a = val;
            }
            else
            {
                Console.WriteLine(""Что-то пошло не так, попробуйте ещё раз."");
                goto Again;
            }
switch (a)
{                                                                                       // Остальные месяца выполнены по аналогии с Январём и Февралём.
    case 1:
        mon = Months.Январь;                                                            // Переменной mon присвое месяц из enum Months
        normalMidTempDay = -8f;                                                         // Нормальная средняя температура днём для текущего месяца
        normalMidTempNight = -9f;                                                       // Нормальная средняя температура ночью для текущего месяца
        month = ""Январе"";                                                             // название месяца для текста вывода
        break;                                                                          // Выход из Switch`а
    case 2:
        mon = Months.Февраль;
        normalMidTempDay = -8f;
        normalMidTempNight = -10f;
        month = ""Феврале"";
        break;    
    default:
        break;
}
                                                                                        // Вычисление средней температуры
Console.WriteLine($""Введите минимальную температуру воздуха в {month}:"");
GetMinTemp:                                                                             // точка возврата при ошибке ввода минимальной температуры
if (float.TryParse(Console.ReadLine(), out float valMin))
{
    minTemp = valMin;
}
else
{
    Console.WriteLine(""Что-то пошло не так, попробуйте ещё раз."");
    goto GetMinTemp;
}
Console.WriteLine($""Введите максимальную температуру воздуха в {month}:"");
GetMaxTemp:                                                                             // точка возврата при ошибке ввода максимальной температуры
if (float.TryParse(Console.ReadLine(), out float valMax))
{
    maxTemp = valMax;
}
else
{
    Console.WriteLine(""Что-то пошло не так, попробуйте ещё раз."");
    goto GetMaxTemp;
}
midTemp = (minTemp + maxTemp) / 2;                                                      // вычисление средней температуры (a+b)/2

Console.Clear();                                                                        // Очистка консоли
                                                                                        // Вывод сообщения о температуре в текущем сезоне.
if ((mon | Months.Зима) == Months.Зима)
{
    Console.WriteLine($""В Костроме средняя температура зимой в {month} должна быть {normalMidTempDay} днём и {normalMidTempNight} ночью."");
    if (midTemp > 0 & midTemp < 10)
    {
        Console.WriteLine($""Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Дождливая Зима"");
    }
    else if (midTemp <= -40 || midTemp >= 10)
    {
        Console.WriteLine($""Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Аномальная температура для зимы"");
    }
    else if (midTemp < 0 & midTemp > ((normalMidTempDay + normalMidTempNight) / 2))
    {
        Console.WriteLine($""Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Нормальная Зима"");
    }
    else if (midTemp < ((normalMidTempDay + normalMidTempNight) / 2))
    {
        Console.WriteLine($""Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Холодная Зима"");
    }
}
else if ((mon | Months.Весна) == Months.Весна)                                          // Остальные сезоны выполнены по той же схеме.
{...}
else if ((mon | Months.Лето) == Months.Лето)
{...}
else if ((mon | Months.Осень) == Months.Осень)
{...}
else                                                                                    // Что-то пошло не так.
{
    Console.WriteLine($""Что-то пошло не так...."");
}
        }";
        public override void GetCode()
        {
            Console.WriteLine(Code);
        }
        public override string GetName()
        {
            return Name;
        }
        [Flags]
        public enum Months
        {
            Error = 0b000000000000,
            Январь = 0b_000_000_000_001,
            Февраль = 0b_000_000_000_010,
            Март = 0b_000_000_000_100,
            Апрель = 0b_000_000_001_000,
            Май = 0b_000_000_010_000,
            Июнь = 0b_000_000_100_000,
            Июль = 0b_000_001_000_000,
            Август = 0b_000_010_000_000,
            Сентябрь = 0b_000_100_000_000,
            Октябрь = 0b_001_000_000_000,
            Ноябрь = 0b_010_000_000_000,
            Декабрь = 0b_100_000_000_000,
            Зима = Январь | Февраль | Декабрь,
            Весна = Март | Апрель | Май,
            Лето = Июнь | Июль | Август,
            Осень = Сентябрь | Октябрь | Ноябрь
        }

        public override void Start()
        {
            int a;
            Months mon = Months.Error;                                                  // Месяца
            float minTemp;                                                              // минимальная температура
            float maxTemp;                                                              // максимальная температура
            float midTemp;                                                              // средняя температура
            float normalMidTempDay = 0;                                                 // Нормальная температура в месяце днём
            float normalMidTempNight = 0;                                               // Нормальная температура в месяце ночью
            string month = "Месяце";                                                    // название месяца для текста вывода
            Console.WriteLine($"Введите номер месяца:");
        Again:                                                                          // точка возврата при ошибке ввода номера месяца
            if (Int32.TryParse(Console.ReadLine(), out int val))
            {
                a = val;
            }
            else
            {
                Console.WriteLine("Что-то пошло не так, попробуйте ещё раз.");
                goto Again;
            }
            switch (a)
            {
                case 1:
                    mon = Months.Январь;
                    normalMidTempDay = -8f;
                    normalMidTempNight = -9f;
                    month = "Январе";
                    break;
                case 2:
                    mon = Months.Февраль;
                    normalMidTempDay = -8f;
                    normalMidTempNight = -10f;
                    month = "Феврале";
                    break;
                case 3:
                    mon = Months.Март;
                    normalMidTempDay = 0f;
                    normalMidTempNight = -3f;
                    month = "Марте";
                    break;
                case 4:
                    mon = Months.Апрель;
                    normalMidTempDay = 8f;
                    normalMidTempNight = 5f;
                    month = "Апреле";
                    break;
                case 5:
                    mon = Months.Май;
                    normalMidTempDay = 16f;
                    normalMidTempNight = 13f;
                    month = "Мае";
                    break;
                case 6:
                    mon = Months.Июнь;
                    normalMidTempDay = 19f;
                    normalMidTempNight = 16f;
                    month = "Июне";
                    break;
                case 7:
                    mon = Months.Июль;
                    normalMidTempDay = 23f;
                    normalMidTempNight = 19f;
                    month = "Июле";
                    break;
                case 8:
                    mon = Months.Август;
                    normalMidTempDay = 21f;
                    normalMidTempNight = 16f;
                    month = "Августе";
                    break;
                case 9:
                    mon = Months.Сентябрь;
                    normalMidTempDay = 15f;
                    normalMidTempNight = 11f;
                    month = "Сентябре";
                    break;
                case 10:
                    mon = Months.Октябрь;
                    normalMidTempDay = 7f;
                    normalMidTempNight = 4f;
                    month = "Октябре";
                    break;
                case 11:
                    mon = Months.Ноябрь;
                    normalMidTempDay = 0f;
                    normalMidTempNight = -1f;
                    month = "Ноябре";
                    break;
                case 12:
                    mon = Months.Декабрь;
                    normalMidTempDay = -6f;
                    normalMidTempNight = -6f;
                    month = "Декабре";
                    break;
                default:
                    break;
            }
            Console.WriteLine($"Введите минимальную температуру воздуха в {month}:");
        GetMinTemp:                                                                     // точка возврата при ошибке ввода минимальной температуры
            if (float.TryParse(Console.ReadLine(), out float valMin))
            {
                minTemp = valMin;
            }
            else
            {
                Console.WriteLine("Что-то пошло не так, попробуйте ещё раз.");
                goto GetMinTemp;
            }
            Console.WriteLine($"Введите максимальную температуру воздуха в {month}:");
        GetMaxTemp:                                                                     // точка возврата при ошибке ввода максимальной температуры
            if (float.TryParse(Console.ReadLine(), out float valMax))
            {
                maxTemp = valMax;
            }
            else
            {
                Console.WriteLine("Что-то пошло не так, попробуйте ещё раз.");
                goto GetMaxTemp;
            }
            midTemp = (minTemp + maxTemp) / 2;                                          // вычисление средней температуры (a+b)/2
            Console.Clear();
            if ((mon | Months.Зима) == Months.Зима)
            {
                Console.WriteLine($"В Костроме средняя температура зимой в {month} должна быть {normalMidTempDay} днём и {normalMidTempNight} ночью.");
                if (midTemp > 0 & midTemp < 10)
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Дождливая Зима");
                }
                else if (midTemp <= -40 || midTemp >= 10)
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Аномальная температура для зимы");
                }
                else if (midTemp < 0 & midTemp > ((normalMidTempDay + normalMidTempNight) / 2))
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Нормальная Зима");
                }
                else if (midTemp < ((normalMidTempDay + normalMidTempNight) / 2))
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Холодная Зима");
                }
            }
            else if ((mon | Months.Весна) == Months.Весна)
            {
                Console.WriteLine($"В Костроме средняя температура Весной в {month} должна быть {normalMidTempDay} днём и {normalMidTempNight} ночью.");
                if (midTemp > ((normalMidTempDay + normalMidTempNight) / 2))
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Тёплая Весна");
                }
                else if (midTemp <= -40 || midTemp >= 25)
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Аномальная температура для Весны");
                }
                else if (midTemp < ((normalMidTempDay + normalMidTempNight) / 2))
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Прохладная Весна");
                }
                else if (midTemp == ((normalMidTempDay + normalMidTempNight) / 2))
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Нормальная Весна");
                }
            }
            else if ((mon | Months.Лето) == Months.Лето)
            {
                Console.WriteLine($"В Костроме средняя температура Летом в {month} должна быть {normalMidTempDay} днём и {normalMidTempNight} ночью.");
                if (midTemp > ((normalMidTempDay + normalMidTempNight) / 2))
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Жаркое Лето");
                }
                else if (midTemp <= -40 || midTemp >= 45)
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Аномальная температура для Лета");
                }
                else if (midTemp < ((normalMidTempDay + normalMidTempNight) / 2))
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Прохладное Лето");
                }
                else if (midTemp == ((normalMidTempDay + normalMidTempNight) / 2))
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Нормальное Лето");
                }
            }
            else if ((mon | Months.Осень) == Months.Осень)
            {
                Console.WriteLine($"В Костроме средняя температура Осенью в {month} должна быть {normalMidTempDay} днём и {normalMidTempNight} ночью.");
                if (midTemp > ((normalMidTempDay + normalMidTempNight) / 2))
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Тёплая Осень");
                }
                else if (midTemp <= -40 || midTemp >= 25)
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Аномальная температура для Осени");
                }
                else if (midTemp < ((normalMidTempDay + normalMidTempNight) / 2))
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Прохладная Осень");
                }
                else if (midTemp == ((normalMidTempDay + normalMidTempNight) / 2))
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Нормальная Осень");
                }
            }
            else                                                                        // Что-то пошло не так.
            {
                Console.WriteLine($"Что-то пошло не так....");
            }
        }
    }
    public class Work05 : MenuSpace.Work
    {
        public string Name { get; } = "Режим работы Офисов";
        public string Code { get; } = @"[Flags]
enum Days
{
    Error = 0b_0_000_000,
    Понедельник = 0b_0_000_001,
    Вторник = 0b_0_000_010,
    Среда = 0b_0_000_100,
    Четверг = 0b_0_001_000,
    Пятница = 0b_0_010_000,
    Суббота = 0b_0_100_000,
    Воскресенье = 0b_1_000_000
}
[Flags]
enum Time
{
    Error = 0b_0000,
    Утро = 0b_0001,
    Вечер = 0b_0010,
    Обед = 0b_0100,
    Сутки = 0b_1000
}
                                                                                        // Начало программы
public void Start()
{                                                                                       // Объявление и присвоение данных основных переменных
    Days workWeek = Days.Понедельник | Days.Вторник | Days.Среда | Days.Четверг | Days.Пятница;
    Days weekEnd = Days.Суббота | Days.Воскресенье;
    Days officeA = workWeek;
    Time OfficeTime = Time.Error;
    Days officeB = Days.Понедельник | Days.Вторник | Days.Четверг | Days.Пятница;
    Days officeC = weekEnd;
    Days OfficeDays = Days.Error;
    string dayOff = ""Выходной"";
    string morning, evening, dinnerS = null, dinnerE = null, summary = null;            // Переменая строки вывода времени работы и обеда.
Start:                                                                                  // точка возврата 1
    Console.Clear();                                                                    // Чистим консоль
    Console.WriteLine                                                                   // Выводим меню
    (
        $""Расписание работы офисов:"" +
        $""\nВыберите офис который вас интересует "" +
        $""\n1. Офис в Центре "" +
        $""\n2. Офис близко "" +
        $""\n3. Офис далеко"" +
        $""\n4. Ещё где-то"" +
        $""\n5. Выход""
    );
    switch (Console.ReadLine())                                                         // Меню выбора офиса, остальные сделаны по аналогии
    {
        case ""1"":
            Console.WriteLine(""Office A"");
            OfficeDays = officeA;
            OfficeTime = Time.Утро | Time.Обед;
            morning = ""8:00"";
            dinnerS = ""12:00"";
            dinnerE = ""13:00"";
            evening = ""17:00"";
            break;
        case ""2"":
            Console.WriteLine(""Office B"");
            OfficeDays = officeB;
            OfficeTime = Time.Обед | Time.Вечер;
            morning = ""8:00"";
            dinnerS = ""0:00"";
            dinnerE = ""1:00"";
            evening = ""17:00"";
            break;

        case ""5"":                                                                     // Выход из программы
            return;
        default:
            Console.WriteLine(""Что-то пошло не так. Попробуйте ещё раз"");
            Console.ReadKey();
            goto Start;
    }
                                                                                        // Основная конструкция
    if ((OfficeTime & Time.Утро) == Time.Утро)
    {
        summary = $""Работает с {morning} до {evening}."";
        if ((OfficeTime & Time.Обед) == Time.Обед)
        {
            summary += $"" Обед с {dinnerS} до {dinnerE}."";
        }
    }
    else if ((OfficeTime & Time.Вечер) == Time.Вечер)
    {
        summary = $""Работает с {evening} до {morning}."";
        if ((OfficeTime & Time.Обед) == Time.Обед)
        {
            summary += $"" Обед с {dinnerS} до {dinnerE}."";
        }
    }
                                                                                        // Конструкция вывода каждого дня с подписью о рабочем времени
    if ((OfficeDays & Days.Понедельник) == Days.Понедельник)
    {
        Console.WriteLine(Days.Понедельник + ""\t"" + summary);
    }
    else
    {
        Console.WriteLine($""{Days.Понедельник}     {dayOff}"");
    }
    if ((OfficeDays & Days.Вторник) == Days.Вторник)
    {
        Console.WriteLine(Days.Вторник + ""\t\t"" + summary);
    }
    else
    {
    Console.WriteLine($""{Days.Вторник}         {dayOff}"");
    }
                                                                                        // Остальные выполнены по аналогии
    Console.ReadKey();                                                                  // Задержка.
    goto Start;                                                                         // Возврат к меню";
        public override void GetCode()
        {
            Console.WriteLine(Code);
        }
        public override string GetName()
        {
            return Name;
        }
        [Flags]
        enum Days
        {
            Error = 0b_0_000_000,
            Понедельник = 0b_0_000_001,
            Вторник = 0b_0_000_010,
            Среда = 0b_0_000_100,
            Четверг = 0b_0_001_000,
            Пятница = 0b_0_010_000,
            Суббота = 0b_0_100_000,
            Воскресенье = 0b_1_000_000
        }
        [Flags]
        enum Time
        {
            Error = 0b_0000,
            Утро = 0b_0001,
            Вечер = 0b_0010,
            Обед = 0b_0100,
            Сутки = 0b_1000
        }
        public override void Start()
        {
            Days workWeek = Days.Понедельник | Days.Вторник | Days.Среда | Days.Четверг | Days.Пятница;
            Days weekEnd = Days.Суббота | Days.Воскресенье;
            Days officeA = workWeek;
            Time OfficeTime = Time.Error;
            Days officeB = Days.Понедельник | Days.Вторник | Days.Четверг | Days.Пятница;
            Days officeC = weekEnd;
            Days OfficeDays = Days.Error;
            string dayOff = "Выходной";
            string morning, evening, dinnerS = null, dinnerE = null, summary = null;
        Start:
            Console.Clear();
            Console.WriteLine
                (
                $"Расписание работы офисов:" +
                $"\nВыберите офис который вас интересует " +
                $"\n1. Офис в Центре " +
                $"\n2. Офис близко " +
                $"\n3. Офис далеко" +
                $"\n4. Ещё где-то" +
                $"\n5. Выход"
                );
            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Office A");
                    OfficeDays = officeA;
                    OfficeTime = Time.Утро | Time.Обед;
                    morning = "8:00";
                    dinnerS = "12:00";
                    dinnerE = "13:00";
                    evening = "17:00";
                    break;
                case "2":
                    Console.WriteLine("Office B");
                    OfficeDays = officeB;
                    OfficeTime = Time.Обед | Time.Вечер;
                    morning = "8:00";
                    dinnerS = "0:00";
                    dinnerE = "1:00";
                    evening = "17:00";
                    break;
                case "3":
                    Console.WriteLine("Office C");
                    OfficeDays = officeC;
                    OfficeTime = Time.Утро;
                    morning = "9:00";
                    evening = "17:00";
                    break;
                case "4":
                    Console.WriteLine("Office D");
                    OfficeDays = officeB;
                    OfficeTime = Time.Утро | Time.Обед;
                    morning = "9:00";
                    dinnerS = "13:00";
                    dinnerE = "14:00";
                    evening = "18:00";
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Что-то пошло не так. Попробуйте ещё раз");
                    Console.ReadKey();
                    goto Start;
            }

            if ((OfficeTime & Time.Утро) == Time.Утро)
            {
                summary = $"Работает с {morning} до {evening}.";
                if ((OfficeTime & Time.Обед) == Time.Обед)
                {
                    summary += $" Обед с {dinnerS} до {dinnerE}.";
                }
            }
            else if ((OfficeTime & Time.Вечер) == Time.Вечер)
            {
                summary = $"Работает с {evening} до {morning}.";
                if ((OfficeTime & Time.Обед) == Time.Обед)
                {
                    summary += $" Обед с {dinnerS} до {dinnerE}.";
                }
            }

            if ((OfficeDays & Days.Понедельник) == Days.Понедельник)
            {
                Console.WriteLine(Days.Понедельник + "\t" + summary);
            }
            else
            {
                Console.WriteLine($"{Days.Понедельник}     {dayOff}");
            }
            if ((OfficeDays & Days.Вторник) == Days.Вторник)
            {
                Console.WriteLine(Days.Вторник + "\t\t" + summary);
            }
            else
            {
                Console.WriteLine($"{Days.Вторник}         {dayOff}");
            }
            if ((OfficeDays & Days.Среда) == Days.Среда)
            {
                Console.WriteLine(Days.Среда + "\t\t" + summary);
            }
            else
            {
                Console.WriteLine($"{Days.Среда}           {dayOff}");
            }
            if ((OfficeDays & Days.Четверг) == Days.Четверг)
            {
                Console.WriteLine(Days.Четверг + "\t\t" + summary);
            }
            else
            {
                Console.WriteLine($"{Days.Четверг}         {dayOff}");
            }
            if ((OfficeDays & Days.Пятница) == Days.Пятница)
            {
                Console.WriteLine(Days.Пятница + "\t\t" + summary);
            }
            else
            {
                Console.WriteLine($"{Days.Пятница}         {dayOff}");
            }
            if ((OfficeDays & Days.Суббота) == Days.Суббота)
            {
                Console.WriteLine(Days.Суббота + "\t\t" + summary);
            }
            else
            {
                Console.WriteLine($"{Days.Суббота}         {dayOff}");
            }
            if ((OfficeDays & Days.Воскресенье) == Days.Воскресенье)
            {
                Console.WriteLine(Days.Воскресенье + "\t" + summary);
            }
            else
            {
                Console.WriteLine($"{Days.Воскресенье}     {dayOff}");
            }
            Console.ReadKey();
            goto Start;
        }
    }
    public class Work06 : MenuSpace.Work
    {
        public string Name { get; } = "Вывод \"чек\".";
        public string Code { get; } = @"public void Start()
        {
            string shopName, address, openName, moneyType, name1, name2, name3, endLine, commision;
            decimal line1, line2, line3, tax, summ, total;
            shopName = ""Магазин у бобра"";
            address = ""г.Кострома ул. Советская,1"";
            openName = ""ИП Бобёр"";
            moneyType = ""Cridit Card Visa"";
            name1 = ""Бревно"";
            name2 = ""Доска"";
            name3 = ""Щепа"";
            commision = ""Комиссия"";
            endLine = ""Итого"";
            line1 = 12.50m;
            line2 = 10.75m;
            line3 = 5.19m;
            tax = 0.09m;
            summ = line1 + line2 + line3;
            total = summ + summ* tax;

        Console.WriteLine($""{shopName.PadLeft(20)}\n{openName.PadLeft(16)}"");
            Console.WriteLine(address + ""\n"" + moneyType);
            Console.WriteLine(("""").PadRight(26, '_'));
            Console.WriteLine($""{name1}{line1.ToString().PadLeft(20, '.')}"");
            Console.WriteLine($""{name2}{line2.ToString().PadLeft(21, '.')}"");
            Console.WriteLine($""{name3}{line3.ToString().PadLeft(22, '.')}"");
            Console.WriteLine($""\n{commision}{("""").PadRight(12, '.')}{(summ * tax).ToString(""F"").PadLeft(6, '.')}"");
            Console.WriteLine(("""").PadRight(26, '_'));
            Console.WriteLine($""{endLine}{("""").PadRight(15, '.')}{total.ToString(""F"").PadLeft(6, '.')}\n"");
            Console.WriteLine((""Спасибо за покупку"").PadLeft(22));
        }";
        public override void GetCode()
        {
            Console.WriteLine(Code);
        }
        public override string GetName()
        {
            return Name;
        }
        public override void Start()
        {
            string shopName, address, openName, moneyType, name1, name2, name3, endLine, commision;
            decimal line1, line2, line3, tax, summ, total;
            shopName = "Магазин у бобра";
            address = "г.Кострома ул. Советская,1";
            openName = "ИП Бобёр";
            moneyType = "Cridit Card Visa";
            name1 = "Бревно";
            name2 = "Доска";
            name3 = "Щепа";
            commision = "Комиссия";
            endLine = "Итого";
            line1 = 12.50m;
            line2 = 10.75m;
            line3 = 5.19m;
            tax = 0.09m;
            summ = line1 + line2 + line3;
            total = summ + summ * tax;

            Console.WriteLine($"{shopName.PadLeft(20)}\n{openName.PadLeft(16)}");
            Console.WriteLine(address + "\n" + moneyType);
            Console.WriteLine(("").PadRight(26, '_'));
            Console.WriteLine($"{name1}{line1.ToString().PadLeft(20, '.')}");
            Console.WriteLine($"{name2}{line2.ToString().PadLeft(21, '.')}");
            Console.WriteLine($"{name3}{line3.ToString().PadLeft(22, '.')}");
            Console.WriteLine($"\n{commision}{("").PadRight(12, '.')}{(summ * tax).ToString("F").PadLeft(6, '.')}");
            Console.WriteLine(("").PadRight(26, '_'));
            Console.WriteLine($"{endLine}{("").PadRight(15, '.')}{total.ToString("F").PadLeft(6, '.')}\n");
            Console.WriteLine(("Спасибо за покупку").PadLeft(22));
        }
    }
    public class WorkDop1 : MenuSpace.Work
    {
        public string Name { get; } = "Определение високосного года.";
        public string Code { get; } = @"// Константы ограничения диапазона
const int GregorianCalendarStartYear = 1582;
const int GregorianCalendar10k = 11582;

public void Start()
{

    // Переменные

    int startYear = GregorianCalendarStartYear;                                         // начало диапазона
    int stopYear = GregorianCalendar10k;                                                // конец диапазона
    int leapYearCount = 0;                                                              // счётчик високосных лет в диапазоне
    int nonLeapYearCount = 0;                                                           // счётчик невисокосных лет в диапазоне
    string output = null;                                                               // строка хранения результата вычисления
    bool skipLeapYear = false;                                                          // пропуск високосных лет
    bool skipNonLeapYear = false;                                                       // пропуск невисокосных лет
    int counter = 0;                                                                    // переменная суммарного счётчика лет
    bool skipCounter = false;                                                           // пропуск счётчика

                                                                                        // Первый запуск

    Console.Clear();                                                                    // Очистка консоли

    Console.WriteLine(""Программа вычисления високосных лет по грегорианскому календарю.\n Грегорианский календарь принят в 1582 году,\n"" +
"" поэтому диапазон ограничен от 1582 года до 11582 года."");
                                                                                        // Меню

Start:
    Console.WriteLine(
    $""1. Установка диапазона\n"" +
    $""2. Вывод только високосные года\n"" +
    $""3. Вывод только Невисокосные года\n"" +
    $""4. Вывод все года с подписями\n"" +
    $""5. Сброс диапазона.\n"" +
    $""6. Точный год.\n"" +
    $""7. Выход"");
    switch (Console.ReadLine())
    {
        case ""1"":                                                                     // установка диапазона отображения

            Console.WriteLine(""Установка диапазона отображения\n Введите начальный год, но не раньше 1582"");
            if (Int32.TryParse(Console.ReadLine(), out int setVal) & (setVal<GregorianCalendar10k) & (setVal >= GregorianCalendarStartYear))
            {
                startYear = setVal;
            }
            else
            {
                startYear = GregorianCalendarStartYear;
                Console.WriteLine($""Ошибка ввода, установлен {GregorianCalendarStartYear} год"");
            }
            Console.WriteLine(""Введите конечный год, но не позже 11582"");
            if (Int32.TryParse(Console.ReadLine(), out setVal) & (setVal <= GregorianCalendar10k) & (setVal > GregorianCalendarStartYear))
            {
                stopYear = setVal;
            }
            else
            {
                stopYear = GregorianCalendar10k;
                Console.WriteLine($""Ошибка ввода, установлен {GregorianCalendar10k} год"");
            }
            Console.Clear();
            Console.WriteLine($""Установлен диапазон с {startYear} по {stopYear}"");
            goto Start;                                                                 // Точка возврата к началу программы

        case ""2"":                                                                     // вывод только високосные года

            skipNonLeapYear = true;
            skipLeapYear = false;
            skipCounter = false;
            Console.Clear();
            Console.WriteLine($""Установлен диапазон с {startYear} по {stopYear}"");
            goto case ""run"";                                                          // Переход к основному циклу вычисления високосного года

        case ""3"":                                                                     // вывод только Невисокосные года

            skipNonLeapYear = false;
            skipLeapYear = true;
            skipCounter = false;
            Console.Clear();
            Console.WriteLine($""Установлен диапазон с {startYear} по {stopYear}"");
            goto case ""run"";                                                          // Переход к основному циклу вычисления високосного года

        case ""4"":                                                                     // вывод все года с подписями

            skipNonLeapYear = false;
            skipLeapYear = false;
            skipCounter = false;
            Console.Clear();
            Console.WriteLine($""Установлен диапазон с {startYear} по {stopYear}"");
            goto case ""run"";                                                          // Переход к основному циклу вычисления високосного года

        case ""5"":                                                                     // Сброс диапазона

            startYear = GregorianCalendarStartYear;
            stopYear = GregorianCalendar10k;
            skipCounter = false;
            Console.Clear();
            Console.WriteLine($""Установлен диапазон с {startYear} по {stopYear}"");
            goto Start;                                                                 // Точка возврата к началу программы

        case ""6"":                                                                     // Выбор конкретного года

            skipNonLeapYear = false;
            skipLeapYear = false;
            skipCounter = true;
            Console.WriteLine($""Введите год от {GregorianCalendarStartYear} до {GregorianCalendar10k}"");
            if (Int32.TryParse(Console.ReadLine(), out setVal) & (setVal < GregorianCalendar10k) & (setVal >= GregorianCalendarStartYear))
            {
                startYear = setVal;
                stopYear = setVal;
            }
            else
            {
                Console.WriteLine($""Ошибка ввода, попробуйте ещё раз."");
                Console.Clear();
                goto case ""6"";
            }
            Console.Clear();
            goto case ""run"";                                                          // Переход к основному циклу вычисления високосного года

        case ""run"":                                                                   // Основной цикл вычисления високосного года.
                                                                                        // Каждый 4й и 400й, но не 100й год високосный.

                                                                                        // Сброс переменных результата
            output = null;
            leapYearCount = 0;
            nonLeapYearCount = 0;
            for (int i = startYear; i <= stopYear; i++)
            {
                if ((i % 4) == 0)
                {
                    if ((i % 100) != 0)
                    {
                        if (!skipLeapYear)
                        {
                            output += i + "" год - високосный\n"";
                            leapYearCount++;
                        }
                    }
                    else if ((i % 400) == 0)
                    {
                        if (!skipLeapYear)
                        {
                            output += i + "" год - високосный\n"";
                            leapYearCount++;
                        }
                    }
                    else
                    {
                        if (!skipNonLeapYear)
                        {
                            output += i + "" год - невисокосный\n"";
                            nonLeapYearCount++;
                        }
                    }
                }
                else
                {
                    if (!skipNonLeapYear)
                    {
                        output += i + "" год - невисокосный\n"";
                        nonLeapYearCount++;
                    }
                }
            }
                                                                                        // Суммирование записей
            if (!skipLeapYear)
            {
                if (!skipNonLeapYear)
                {
                    counter = nonLeapYearCount + leapYearCount;
                }
                else
                {
                    counter = leapYearCount;
                }
            }
            else
            {
                counter = nonLeapYearCount;
            }
                                                                                        // Вывод результата
            Console.WriteLine($""{output} \n\nВсего {counter} записей.\n\nВозврат к меню."");
            Console.ReadLine();
            Console.Clear();
            goto Start;                                                                 // Точка возврата к началу программы
        default:
            break;
    }
}";
        public override void GetCode()
        {
            Console.WriteLine(Code);
        }
        public override string GetName()
        {
            return Name;
        }
        // Константы ограничения диапазона
        const int GregorianCalendarStartYear = 1582;
        const int GregorianCalendar10k = 11582;
        public override void Start()
        {

            // Переменные

            int startYear = GregorianCalendarStartYear;     // начало диапазона
            int stopYear = GregorianCalendar10k;            // конец диапазона
            int leapYearCount = 0;                          // счётчик високосных лет в диапазоне
            int nonLeapYearCount = 0;                       // счётчик невисокосных лет в диапазоне
            string output = null;                           // строка хранения результата вычисления
            bool skipLeapYear = false;                      // пропуск високосных лет
            bool skipNonLeapYear = false;                   // пропуск невисокосных лет
            int counter = 0;                                // переменная суммарного счётчика лет
            bool skipCounter = false;                       // пропуск счётчика

            // Первый запуск

            Console.Clear();                                // Очистка консоли

            Console.WriteLine("Программа вычисления високосных лет по грегорианскому календарю.\n Грегорианский календарь принят в 1582 году,\n" +
                " поэтому диапазон ограничен от 1582 года до 11582 года.");
        // Меню

        Start:
            Console.WriteLine(
                $"1. Установка диапазона\n" +
                $"2. Вывод только високосные года\n" +
                $"3. Вывод только Невисокосные года\n" +
                $"4. Вывод все года с подписями\n" +
                $"5. Сброс диапазона.\n" +
                $"6. Точный год.\n" +
                $"7. Выход");
            switch (Console.ReadLine())
            {
                case "1":                                   // установка диапазона отображения

                    Console.WriteLine("Установка диапазона отображения\n Введите начальный год, но не раньше 1582");
                    if (Int32.TryParse(Console.ReadLine(), out int setVal) & (setVal < GregorianCalendar10k) & (setVal >= GregorianCalendarStartYear))
                    {
                        startYear = setVal;
                    }
                    else
                    {
                        startYear = GregorianCalendarStartYear;
                        Console.WriteLine($"Ошибка ввода, установлен {GregorianCalendarStartYear} год");
                    }
                    Console.WriteLine("Введите конечный год, но не позже 11582");
                    if (Int32.TryParse(Console.ReadLine(), out setVal) & (setVal <= GregorianCalendar10k) & (setVal > GregorianCalendarStartYear))
                    {
                        stopYear = setVal;
                    }
                    else
                    {
                        stopYear = GregorianCalendar10k;
                        Console.WriteLine($"Ошибка ввода, установлен {GregorianCalendar10k} год");
                    }
                    Console.Clear();
                    Console.WriteLine($"Установлен диапазон с {startYear} по {stopYear}");
                    goto Start;                             // Точка возврата к началу программы

                case "2":                                   // вывод только високосные года

                    skipNonLeapYear = true;
                    skipLeapYear = false;
                    skipCounter = false;
                    Console.Clear();
                    Console.WriteLine($"Установлен диапазон с {startYear} по {stopYear}");
                    goto case "run";

                case "3":                                   // вывод только Невисокосные года

                    skipNonLeapYear = false;
                    skipLeapYear = true;
                    skipCounter = false;
                    Console.Clear();
                    Console.WriteLine($"Установлен диапазон с {startYear} по {stopYear}");
                    goto case "run";

                case "4":                                   // вывод все года с подписями

                    skipNonLeapYear = false;
                    skipLeapYear = false;
                    skipCounter = false;
                    Console.Clear();
                    Console.WriteLine($"Установлен диапазон с {startYear} по {stopYear}");
                    goto case "run";

                case "5":                                   // Сброс диапазона

                    startYear = GregorianCalendarStartYear;
                    stopYear = GregorianCalendar10k;
                    skipCounter = false;
                    Console.Clear();
                    Console.WriteLine($"Установлен диапазон с {startYear} по {stopYear}");
                    goto Start;                             // Точка возврата к началу программы

                case "6":                                   // Выбор конкретного года

                    skipNonLeapYear = false;
                    skipLeapYear = false;
                    skipCounter = true;
                    Console.WriteLine($"Введите год от {GregorianCalendarStartYear} до {GregorianCalendar10k}");
                    if (Int32.TryParse(Console.ReadLine(), out setVal) & (setVal < GregorianCalendar10k) & (setVal >= GregorianCalendarStartYear))
                    {
                        startYear = setVal;
                        stopYear = setVal;
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка ввода, попробуйте ещё раз.");
                        Console.Clear();
                        goto case "6";
                    }
                    Console.Clear();
                    goto case "run";

                case "run":                                 // Основной цикл вычисления високосного года. Каждый 4й и 400й, но не 100й год високосный.

                    // Сброс переменных результата
                    output = null;
                    leapYearCount = 0;
                    nonLeapYearCount = 0;
                    for (int i = startYear; i <= stopYear; i++)
                    {
                        if ((i % 4) == 0)
                        {
                            if ((i % 100) != 0)
                            {
                                if (!skipLeapYear)
                                {
                                    output += i + " год - високосный\n";
                                    leapYearCount++;
                                }
                            }
                            else if ((i % 400) == 0)
                            {
                                if (!skipLeapYear)
                                {
                                    output += i + " год - високосный\n";
                                    leapYearCount++;
                                }
                            }
                            else
                            {
                                if (!skipNonLeapYear)
                                {
                                    output += i + " год - невисокосный\n";
                                    nonLeapYearCount++;
                                }
                            }
                        }
                        else
                        {
                            if (!skipNonLeapYear)
                            {
                                output += i + " год - невисокосный\n";
                                nonLeapYearCount++;
                            }
                        }
                    }
                    // Суммирование записей
                    if (!skipLeapYear)
                    {
                        if (!skipNonLeapYear)
                        {
                            counter = nonLeapYearCount + leapYearCount;

                        }
                        else
                        {
                            counter = leapYearCount;
                        }
                    }
                    else
                    {
                        counter = nonLeapYearCount;
                    }
                    // Вывод результата
                    Console.WriteLine($"{output} \n\nВсего {counter} записей.\n\nВозврат к меню."); // Вывод полученных данных.
                    Console.ReadLine();
                    Console.Clear();
                    goto Start;                             // Точка возврата к началу программы
                default:
                    break;
            }
        }
    }
}
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

    public class Work01 : MenuSpace.Work
    {
        public string Name { get; } = "Вывод массива по диагонали.";

        public string Code { get; } = @"        public void Start()
        {
            int[,] arr = { { 1, 2, 3, 4 }, { 5, 6, 7, 8 } };
            int count = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.WriteLine($""{("""").PadLeft(count)}{arr[i, j]}"");
                    count++;
                }
            }
        }";
        public override void GetCode()
        {
            Console.WriteLine(Code);
        }
        public override string GetName()
        {
            return Name;
        }
        public override void Start()
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
        }

        /// <summary>
        /// Выводит значения массива <paramref name="arr"/> в консоль по диагонали Слева сверху направо, с отступом ввиде пробелов.
        /// </summary>
        /// <param name="arr">Двумерный массив</param>
        public void DiagonalLR()
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
        }
        /// <summary>
        /// Выводит значения массива <paramref name="arr"/> в консоль по диагонали Справа сверху налево, с отступом ввиде пробелов.
        /// </summary>
        /// <param name="arr">Двумерный массив</param>
        public void DiagonalRL()
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
        }
    }

    public class Work02 : MenuSpace.Work
    {
        public string Name { get; } = "Прототип списка контактов.";

        public string Code { get; } = @"
public void Start()
{
    int cursor = 0;
    string selected = null;
    Console.WriteLine(""Press any key to start...."");
    var move = Console.ReadKey(false);
    string[,] contacts =
    { {""Виктор"",""Андрей"",""Александр"",""Фёдор"",""Пётр"",""exit""},
      {""1234567890"",""6549873215"",""1234578965"",""1597534562"",""7891238524"","""" },
      {""njhau@turututu.ru"",""afahaha@turututu.ru"",""sagaga@turututu.ru"",""ejukljk@turututu.ru"",""agsgdffdht@turututu.ru"","""" } };
    do
    {
        Console.Clear();
        Console.WriteLine(""Выберите контакт и нажмите Enter."");
        for (int i = 0; i<contacts.GetLength(1); i++)
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
        else if (move.Key == ConsoleKey.Enter & selected != ""exit"")
        {
            selected = contacts[0, cursor];
            Console.WriteLine($""\n\n{contacts[0, cursor].PadRight(25 - contacts[1, cursor].Length)} {contacts[1, cursor]}{("""").PadRight(5)} {contacts[2, cursor]}"");
            move = Console.ReadKey(false);
        }
    } while (selected != ""exit"") ;
}";
        public override void GetCode()
        {
            Console.WriteLine(Code);
        }
        public override string GetName()
        {
            return Name;
        }
        public override void Start()
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
        }
    }

    public class Work03 : MenuSpace.Work
    {
        public string Name { get; } = "Вывод строки в обратном порядке.";
        public string Code { get; } = @"public void Start()
        {
            Console.WriteLine(""Введите текст."");
            string str = Console.ReadLine();
            for (int i = str.Length-1; i >= 0; i--)
            {
                Console.Write(str[i]);
            }
        }";
        public override void GetCode()
        {
            Console.WriteLine(Code);
        }
        public override string GetName()
        {
            return Name;
        }
        public override void Start()
        {
            Console.WriteLine("Введите текст.");
            string str = Console.ReadLine();
            for (int i = str.Length - 1; i >= 0; i--)
            {
                Console.Write(str[i]);
            }
        }
    }

    public class Work04 : MenuSpace.Work
    {
        public string Name { get; } = "Морской бой";
        public string Code { get; } = @" Вывод сгенерированного игрового поля осуществляется циклом в методе Show(int[] moverUDRL, string[,] str) класса SelectorUDRL,
где int[] moverUDRL - индекс выделенной строки, а string[,] str массив поля.

for (int i = 0; i < str.GetLength(0); i++)
{
    for (int j = 0; j < str.GetLength(1); j++)
    {
        if (i == moverUDRL[0] & j == moverUDRL[1])                                      // Выделение строки массива белым цветом
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(str[i, j]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            continue;
        }
        if (str[i, j] == ""X"")                                                         // Выделение строки ""корабль"" красным цветом
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
Console.Write(""\n"");
}";

        public override void GetCode()
        {
            Console.WriteLine(Code);
        }
        public override string GetName()
        {
            return Name;
        }
        public override void Start()
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


        }
    }
    public class WorkDop1 : MenuSpace.Work
    {
        public string Name { get; } = "Сдвиг массива";

        public string Code { get; } = @"public void Start()
{
                                                                                // Переменные
    string[] arr;                                                               // Массив
    string str;                                                                 // Строка для заполнения массива
    string mover;                                                               // Число индекса сдвига
            
    Console.WriteLine(""Введите значения для массива через пробел."");
    str = Console.ReadLine();
    Console.WriteLine(""Введите число сдвига массива"");
    mover = Console.ReadLine();

    if (Int32.TryParse(mover, out int move))
    {
        Console.WriteLine(""move = "" + move);
    }
    else
    {
        move = 0;
        Console.WriteLine(""Error. move = "" + move);
    }

    str = str.Trim();                                                           // Очистка строки от пробелов в начале и в конце строки
    arr = str.Split("" "", StringSplitOptions.RemoveEmptyEntries);              // Заполнение массива значениями
    if (move < 0)                                                               // Цикл сдвига ""влево""
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
    else if (move > 0)                                                          // Цикл сдвига ""вправо""
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
        Console.WriteLine(""move = "" + move + "" сдвиг не произведён."");
    }

    for (int i = 0; i < arr.Length; i++)                                        // Вывод цикла в консоль
    {
        Console.Write(arr[i] + "" "");
    }
}
";
        public override void GetCode()
        {
            Console.WriteLine(Code);
        }
        public override string GetName()
        {
            return Name;
        }
        public override void Start()
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
        }
    }
}
namespace Lesson04
{
    public class Work01 : MenuSpace.Work
    {
        string Name { get; } = "Метод GetFullName();";
        string Code { get; } = @"void GetFullName(string firstName, string lastName, string patronymic)
{
    Console.WriteLine($""{lastName} {firstName} {patronymic}"");
}
    
public override void Start()
{
    string firstName;   // 
    string lastName;    // 
    string patronymic;  //             

    Console.WriteLine(""Введите имя: "");
    firstName = Console.ReadLine();
    
    Console.WriteLine(""Введите фамилию: "");
    lastName = Console.ReadLine();

    Console.WriteLine(""Введите отчество: "");
    patronymic = Console.ReadLine();

    GetFullName(firstName, lastName, patronymic);

    string[] firstNames = { ""Иван"", ""Дмитрий"", ""Андрей"", ""Александр"", ""Евгений"" };
    string[] lastNames = { ""Сикорский"", ""Менделеев"", ""Троцкий"", ""Рязанов"", ""Перельман"" };
    string[] patronymics = { ""Евгеньевич"", ""Иванович"", ""Дмитриевич"", ""Андреевич"", ""Александрович"" };
    string[,] theNames = new string[3, 5];

    for (int i = 0; i<theNames.GetLength(1); i++)
    {
        theNames[0, i] = firstNames[i];
        theNames[1, i] = lastNames[i];
        theNames[2, i] = patronymics[i];
    }

    for (int i = 0; i<theNames.GetLength(1); i++)
    {
        GetFullName(theNames[0, i], theNames[1, i], theNames[2, i]);
    }
}";
        public override void GetCode() { Console.WriteLine(this.Code); }
        public override string GetName() { return this.Name; }

        public override void Start()
        {
            string firstName;   // 
            string lastName;    // 
            string patronymic;  //             

            Console.WriteLine("Введите имя: ");
            firstName = Console.ReadLine();

            Console.WriteLine("Введите фамилию: ");
            lastName = Console.ReadLine();

            Console.WriteLine("Введите отчество: ");
            patronymic = Console.ReadLine();

            GetFullName(firstName, lastName, patronymic);

            string[] firstNames = { "Иван", "Дмитрий", "Андрей", "Александр", "Евгений" };
            string[] lastNames = { "Сикорский", "Менделеев", "Троцкий", "Рязанов", "Перельман" };
            string[] patronymics = { "Евгеньевич", "Иванович", "Дмитриевич", "Андреевич", "Александрович" };
            string[,] theNames = new string[3, 5];

            for (int i = 0; i < theNames.GetLength(1); i++)
            {
                theNames[0, i] = firstNames[i];
                theNames[1, i] = lastNames[i];
                theNames[2, i] = patronymics[i];
            }

            for (int i = 0; i < theNames.GetLength(1); i++)
            {
                GetFullName(theNames[0, i], theNames[1, i], theNames[2, i]);
            }
        }
        void GetFullName(string firstName, string lastName, string patronymic)
        {
            Console.WriteLine($"{lastName} {firstName} {patronymic}");
        }
    }

    public class Work02 : MenuSpace.Work
    {
        string Name { get; } = "Метод возврата суммы чисел массива";
        string Code { get; } = @"public override void Start()
{
    Console.WriteLine(""Введите значения для массива через пробел."");
    string str = Console.ReadLine();
    Console.WriteLine(ArraySum(str));
}
    // Основной метод
    int ArraySum(string str)
{
    int sum = 0;
    str = str.Trim();
    string[] temp = str.Split("" "", StringSplitOptions.RemoveEmptyEntries);

    for (int i = 0; i<temp.Length; i++)
    {
        if (Int32.TryParse(temp[i], out int num))
        {
            sum += num;
        }
    }
return sum;
}";
        public override void GetCode() { Console.WriteLine(this.Code); }
        public override string GetName() { return this.Name; }
        public override void Start()
        {
            Console.WriteLine("Введите значения для массива через пробел.");
            string str = Console.ReadLine();
            Console.WriteLine(ArraySum(str));
        }
        int ArraySum(string str)
        {
            int sum = 0;
            str = str.Trim();
            string[] temp = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < temp.Length; i++)
            {
                if (Int32.TryParse(temp[i], out int num))
                {
                    sum += num;
                }
            }
            return sum;
        }
    }

    public class Work03 : MenuSpace.Work
    {
        string Name { get; } = "Метод по определению времени года";
        string Code { get; } = @"public override void Start() 
{
    Console.WriteLine(""Введите значени: "");
    int month = 0;
    do
    {
        if (Int32.TryParse(Console.ReadLine(), out month))
        {
            if (month > 0 & month <= 12)
            {
                break;
            }
        }
        Console.WriteLine(""Ошибка: введите число от 1 до 12"");
    } while (!((month > 0) & (month <= 12))) ;
    Console.WriteLine(GetSeason(GetMonth(month)));
}
Months GetMonth(int num)
{
    byte b = 0b_000_000_000_001;

    if (num > 0 & num <= 12)
    {
        b = (byte)(b << num - 1);
        return (Months)b;
    }
    else
    {
        return Months.Error;
    }
}
string GetSeason(Months month)
{
    if ((month | Months.Зима) == Months.Зима)
    {
        return Months.Зима.ToString();
    }
    else if ((month | Months.Весна) == Months.Весна)
    {
        return Months.Весна.ToString();
    }
    else if ((month | Months.Лето) == Months.Лето)
    {
        return Months.Лето.ToString();
    }
    else if ((month | Months.Осень) == Months.Осень)
    {
        return Months.Осень.ToString();
    }
    else
    {
        return Months.Error.ToString();
    }
}
";
        public override void GetCode() { Console.WriteLine(this.Code); }
        public override string GetName() { return this.Name; }

        [Flags]
        enum Months
        {
            Error = 0b000000000000,
            Январь = 0b_000_000_000_001,
            Февраль = 0b_000_000_000_010,
            Март = 0b_000_000_000_100,
            Апрель = 0b_000_000_001_000,
            Май = 0b_000_000_010_000,
            Июнь = 0b_000_000_100_000,
            Июль = 0b_000_001_000_000,
            Август = 0b_000_010_000_000,
            Сентябрь = 0b_000_100_000_000,
            Октябрь = 0b_001_000_000_000,
            Ноябрь = 0b_010_000_000_000,
            Декабрь = 0b_100_000_000_000,
            Зима = Январь | Февраль | Декабрь,
            Весна = Март | Апрель | Май,
            Лето = Июнь | Июль | Август,
            Осень = Сентябрь | Октябрь | Ноябрь
        }
        public override void Start()
        {
            Console.WriteLine("Введите значени: ");
            int month = 0;
            do
            {
                if (Int32.TryParse(Console.ReadLine(), out month))
                {
                    if (month > 0 & month <= 12)
                    {
                        break;
                    }
                }
                Console.WriteLine("Ошибка: введите число от 1 до 12");
            } while (!((month > 0) & (month <= 12)));
            Console.WriteLine(GetSeason(GetMonth(month)));
        }
        Months GetMonth(int num)
        {
            byte b = 0b_000_000_000_001;

            if (num > 0 & num <= 12)
            {
                b = (byte)(b << num - 1);
                return (Months)b;
            }
            else
            {
                return Months.Error;
            }
        }
        string GetSeason(Months month)
        {
            if ((month | Months.Зима) == Months.Зима)
            {
                return Months.Зима.ToString();
            }
            else if ((month | Months.Весна) == Months.Весна)
            {
                return Months.Весна.ToString();
            }
            else if ((month | Months.Лето) == Months.Лето)
            {
                return Months.Лето.ToString();
            }
            else if ((month | Months.Осень) == Months.Осень)
            {
                return Months.Осень.ToString();
            }
            else
            {
                return Months.Error.ToString();
            }
        }
    }

    public class Work04 : MenuSpace.Work
    {
        string Name { get; } = "Рекурсия Фибоначчи";
        string Code { get; } = @"public override void Start() 
{
    Console.WriteLine(""Введите порядковый номер последовательности Фибоначчи"");
    if (Int32.TryParse(Console.ReadLine(), out int n))
    {
        Console.WriteLine($""Fibonachi({n}) = {Fibonachi(n)}"");
    }
    else
    {
        Console.WriteLine(""Error"");
    }
}
int Fibonachi(int n)
    {
        if (n > 0)
    {
        return (Fibonachi(n - 1) + Fibonachi(n - 2));
    }
    else if (n<0)
    {
        return -n;
    }
    return n;
}";
        public override void GetCode() { Console.WriteLine(this.Code); }
        public override string GetName() { return this.Name; }
        public override void Start()
        {
            Console.WriteLine("Введите порядковый номер последовательности Фибоначчи");
            if (Int32.TryParse(Console.ReadLine(), out int n))
            {
                Console.WriteLine($"Fibonachi({n}) = {Fibonachi(n)}");
            }
            else
            {
                Console.WriteLine("Error");
            }
        }
        int Fibonachi(int n)
        {
            if (n > 0)
            {
                return (Fibonachi(n - 1) + Fibonachi(n - 2));
            }
            else if (n < 0)
            {
                return -n;
            }
            return n;
        }
    }
}
