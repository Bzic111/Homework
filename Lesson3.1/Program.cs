using System;

namespace Lesson3._1
{
    class Program
    {
        static void Main(string[] args)
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
    }
}
