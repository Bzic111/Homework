using System;

namespace DopDZ
{
    class Program
    {
        // Константы ограничения диапазона
        const int GregorianCalendarStartYear = 1582; 
        const int GregorianCalendar10k = 11582;
        
        static void Main(string[] args)
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

            Console.Clear(); // Очистка консоли
            
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
                case "1": // установка диапазона отображения

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
                    goto Start; // Точка возврата к началу программы

                case "2": // вывод только високосные года
                    
                    skipNonLeapYear = true;
                    skipLeapYear = false;
                    skipCounter = false;
                    Console.Clear();
                    Console.WriteLine($"Установлен диапазон с {startYear} по {stopYear}");
                    goto case "run";
                
                case "3": // вывод только Невисокосные года
                
                    skipNonLeapYear = false;
                    skipLeapYear = true;
                    skipCounter = false;
                    Console.Clear();
                    Console.WriteLine($"Установлен диапазон с {startYear} по {stopYear}");
                    goto case "run";
                
                case "4": // вывод все года с подписями
                
                    skipNonLeapYear = false;
                    skipLeapYear = false;
                    skipCounter = false;
                    Console.Clear();
                    Console.WriteLine($"Установлен диапазон с {startYear} по {stopYear}");
                    goto case "run";
               
                case "5": // Сброс диапазона
               
                    startYear = GregorianCalendarStartYear;
                    stopYear = GregorianCalendar10k;
                    skipCounter = false;
                    Console.Clear();
                    Console.WriteLine($"Установлен диапазон с {startYear} по {stopYear}");
                    goto Start; // Точка возврата к началу программы
               
                case "6": // Выбор конкретного года
                
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
                
                case "run": // Основной цикл вычисления високосного года. Каждый 4й и 400й, но не 100й год високосный.
                 
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
                    goto Start; // Точка возврата к началу программы
                default:
                    break;
            }
        }
    }
}
