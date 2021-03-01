using System;
namespace Lesson04
{
    public class Work03 : MenuSpace.Work
    {
        string Name { get; } = "Метод по определению времени года";
        string Code { get; } = @"public override void Start() 
{
    Console.WriteLine(""Введите значени: "");
    int month = 0;
    do
    {
        if (Int32.TryParse(Console.ReadLine(), out month))
        {
            if (month > 0 & month <= 12)
            {
                break;
            }
        }
        Console.WriteLine(""Ошибка: введите число от 1 до 12"");
    } while (!((month > 0) & (month <= 12))) ;
    Console.WriteLine(GetSeason(GetMonth(month)));
}
Months GetMonth(int num)
{
    byte b = 0b_000_000_000_001;

    if (num > 0 & num <= 12)
    {
        b = (byte)(b << num - 1);
        return (Months)b;
    }
    else
    {
        return Months.Error;
    }
}
string GetSeason(Months month)
{
    if ((month | Months.Зима) == Months.Зима)
    {
        return Months.Зима.ToString();
    }
    else if ((month | Months.Весна) == Months.Весна)
    {
        return Months.Весна.ToString();
    }
    else if ((month | Months.Лето) == Months.Лето)
    {
        return Months.Лето.ToString();
    }
    else if ((month | Months.Осень) == Months.Осень)
    {
        return Months.Осень.ToString();
    }
    else
    {
        return Months.Error.ToString();
    }
}
";
        public override void GetCode() { Console.WriteLine(this.Code); }
        public override string GetName() { return this.Name; }

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
        public override void Start()
        {
            Console.WriteLine("Введите значени: ");
            int month = 0;
            do
            {
                if (Int32.TryParse(Console.ReadLine(), out month))
                {
                    if (month > 0 & month <= 12)
                    {
                        break;
                    }
                }
                Console.WriteLine("Ошибка: введите число от 1 до 12");
            } while (!((month > 0) & (month <= 12)));
            Console.WriteLine(GetSeason(GetMonth(month)));
        }
        Months GetMonth(int num)
        {
            byte b = 0b_000_000_000_001;

            if (num > 0 & num <= 12)
            {
                b = (byte)(b << num - 1);
                return (Months)b;
            }
            else
            {
                return Months.Error;
            }
        }
        string GetSeason(Months month)
        {
            if ((month | Months.Зима) == Months.Зима)
            {
                return Months.Зима.ToString();
            }
            else if ((month | Months.Весна) == Months.Весна)
            {
                return Months.Весна.ToString();
            }
            else if ((month | Months.Лето) == Months.Лето)
            {
                return Months.Лето.ToString();
            }
            else if ((month | Months.Осень) == Months.Осень)
            {
                return Months.Осень.ToString();
            }
            else
            {
                return Months.Error.ToString();
            }
        }
    }
}
