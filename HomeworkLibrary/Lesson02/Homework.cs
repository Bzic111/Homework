using System;
namespace Lesson02
{
    public class HomeWork : MenuSpace.Work
    {
        public MenuSpace.Menu.Runner[] AllRuns { get; }
        public string[] Name { get; } =
            {
            "Номер месяца",
            "Средняя температура",
            "Чётное или нечётное число.",
            "Температура, сезоны, месяца",
            "Расписание работы офисов",
            "Вывод \"чек\".",
            "Определение високосного года."
            };
        public override string[] GetNames()
        {
            return Name;
        }

        const int GregorianCalendarStartYear = 1582;
        const int GregorianCalendar10k = 11582;
        public HomeWork()
        {
            AllRuns = new MenuSpace.Menu.Runner[]
            {
                NumberOfMonth,
                MiddleTemperature,
                EvenOrOdd,
                MiddleTempInSeason,
                OfficeTime,
                PrintBill,
                LeapYear
            };
        }

        enum Mon
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

        void NumberOfMonth()
        {
            Console.WriteLine($"Введите номер месяца или Now:");
            string str = Console.ReadLine();
            switch (str)
            {
                case "Now":
                    Console.WriteLine($" {(Mon)DateTime.Now.Month}");
                    break;
                default:
                    if (Int32.TryParse(str, out int value))
                    {
                        if (value > 0 & value <= 12)
                        {
                            Console.WriteLine((Mon)value);
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
        void MiddleTemperature()
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
        void EvenOrOdd()
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
        void MiddleTempInSeason()
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
        void OfficeTime()
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
        void PrintBill()
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
        void LeapYear()
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
