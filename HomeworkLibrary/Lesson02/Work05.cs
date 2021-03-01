using System;
namespace Lesson02
{
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
}
