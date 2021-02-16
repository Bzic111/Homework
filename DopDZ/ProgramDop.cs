using System;

namespace DopDZ
{
    class Program
    {
        static void Main(string[] args)
        {
            int GregorianCalendarStartYear = 1582;
            int GregorianCalendar10k = 11582;
            int someYear = 0;
            int stopYaer = 0;
                string output = null;
            bool leapYear = false;
            bool nonLeapYear = false;
        Start:
            Console.WriteLine($"1. Установка диапазона отображения\n"+
                $"2. Вывод только високосные года\n" +
                $"3. Вывод только Невисокосные года\n" +
                $"4. вывод все года с подписями\n" +
                $"5. Выход");
            switch (Console.ReadLine())
            {
                case "1": // установка диапазона отображения
                case "2": // вывод только високосные года
                case "3": // вывод только Невисокосные года
                case "4":// вывод все года с подписями
                    //
                    break;
                default:
                    break;
            }
            Console.WriteLine("Введите год, но не раньше 1582");
            if (Int32.TryParse(Console.ReadLine(), out int val))
            {
                someYear = val;
                Console.WriteLine("Введите год, но не позже 11582");
                if (Int32.TryParse(Console.ReadLine(), out val))
                {
                    stopYaer = val;
                    if (someYear < stopYaer && someYear > GregorianCalendarStartYear && stopYaer < GregorianCalendar10k)
                    {

                        for (int i = someYear; i <= stopYaer; i++)
                        {
                            output += i + " год - ";
                            if (i % 400 == 0)
                            {
                                output += " високосный\n";
                            }
                            else if(i % 100 == 0)
                            {
                                output += " невисокосный\n";
                            }
                            else if (i % 4 == 0)
                            {
                                output += " високосный\n";
                            }
                            else if (i > GregorianCalendarStartYear)
                            {
                                output += " невисокосный\n";
                            }
                        }   
                    }else
                    {
                        Console.WriteLine("Period Fail...");
                    }
                }
            }
            else
            {
                goto Start;
            }
            Console.WriteLine(output);
        }
    }
}
