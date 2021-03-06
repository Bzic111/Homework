﻿using System;

namespace Lesson2_1
{
    class Program01
    {
        static void Main(string[] args)
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
            if (minTemp<maxTemp)
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
}
