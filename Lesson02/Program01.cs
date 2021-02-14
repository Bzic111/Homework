using System;

namespace Lesson2_1
{
    class Program01
    {
        static void Main(string[] args)
        {
            float minTemp;                                                              // минимальная температура
            float maxTemp;                                                              // максимальная температура
            float midTemp;                                                              // средняя температура
            Console.Clear();                                                            // чистим консоль
            Console.WriteLine("Введите минимальную температуру:");
        GetMinTemp:                                                                     // точка возврата при ошибке ввода 1
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
            midTemp = (minTemp + maxTemp) / 2;                                           // вычисление средней температуры (a+b)/2


            Console.WriteLine($"Средняя температура: {Math.Round(midTemp, 1)}");         // выводим округлённое значение
        }
    }
}
