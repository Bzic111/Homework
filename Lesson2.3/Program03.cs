using System;

namespace Lesson2_3
{
    class Program03
    {
        static void Main(string[] args)
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
}
