using System;

namespace Lesson2_2
{
    class Program02
    {
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
        static void Main(string[] args)
        {
            Console.WriteLine($"Введите номер месяца или Now:");
            string str = Console.ReadLine();
            switch (str)
            {
                case "Now":
                    Console.WriteLine($" {(Months)DateTime.Now.Month}");
                    break;
                default:
                    if (byte.TryParse(str, out byte value))
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
