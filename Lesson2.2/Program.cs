using System;

namespace Lesson2_2
{
    class Program
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
            Зима = Январь | Февраль | Декабрь, 
            Весна = Март| Апрель | Май,
            Лето = Июнь | Июль | Август,
            Осень = Сентябрь | Октябрь | Ноябрь
        }
        /*public enum Months
        {
            Январь =     0b_000_000_000_001,     
            Февраль =    0b_000_000_000_010,        
            Март =       0b_000_000_000_100,           
            Апрель =     0b_000_000_001_000,         
            Май =        0b_000_000_010_000,            
            Июнь =       0b_000_000_100_000,           
            Июль =       0b_000_001_000_000,           
            Август =     0b_000_010_000_000,         
            Сентябрь =   0b_000_100_000_000,       
            Октябрь =    0b_001_000_000_000,        
            Ноябрь =     0b_010_000_000_000,         
            Декабрь =    0b_100_000_000_000,        
            Зима = Январь | Февраль | Декабрь, 
            Весна = Март| Апрель | Май,
            Лето = Июнь | Июль | Август,
            Осень = Сентябрь | Октябрь | Ноябрь
        }*/
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
