using System;
namespace Lesson03
{
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
}
