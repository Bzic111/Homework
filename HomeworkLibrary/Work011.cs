using System;
namespace Lesson02
{
    public class Work01 : MenuSpace.Work
    {
        public string Name { get; } = "Номер месяца";
        public string Code { get; } = @"[Flags]
public enum Months
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
public void Start()
{
    Console.WriteLine($""Введите номер месяца или Now:"");
    string str = Console.ReadLine();
    switch (str)
    {
        case ""Now"":
            Console.WriteLine($"" {(Months)DateTime.Now.Month}"");
            break;
        default:
            if (Int32.TryParse(str, out int value))
            {
                if (value > 0 & value <= 12)
                {
                    Console.WriteLine((Months) value);
                }
            }
            else
            {
                Console.WriteLine($""Ошибка ввода"");
                break;
            }
            break;
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
        [Flags]
        public enum Months
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
        public override void Start()
        {
            Console.WriteLine($"Введите номер месяца или Now:");
            string str = Console.ReadLine();
            switch (str)
            {
                case "Now":
                    Console.WriteLine($" {(Months)DateTime.Now.Month}");
                    break;
                default:
                    if (Int32.TryParse(str, out int value))
                    {
                        if (value > 0 & value <= 12)
                        {
                            Console.WriteLine((Months)value);
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
    }
}
