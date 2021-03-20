using System;
using System.Collections.Generic;

namespace LibraryForHomework
{
    public class HelloClass
    {
        Dictionary<string, string> DayOfWeek = new Dictionary<string, string>
        {
            { "Monday" , "Понедельник" },
            {"Tuesday","Вторник" },
            {"Wednesday","Среда" },
            {"Thursday","Четверг" },
            {"Friday","Пятница" },
            {"Saturday","Суббота" },
            {"Sunday","Воскресенье" }
        };
        public HelloClass()
        {
            HelloMethod();
        }
        void HelloMethod()
        {
            Console.WriteLine("Введите Имя.");
            string name = Console.ReadLine();
            DateTime currentDateTime = DateTime.Now;
            string dayName = DayOfWeek.GetValueOrDefault(currentDateTime.DayOfWeek.ToString());
            Console.WriteLine($"Здравствуйте! {name} Сегодня {currentDateTime.ToString("D")} {dayName}");
        }
    }
}
