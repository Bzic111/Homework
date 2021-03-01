using System;

namespace MenuSpace
{
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
