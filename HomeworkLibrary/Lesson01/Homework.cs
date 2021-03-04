using System;
using System.Collections.Generic;

namespace Lesson01
{
    public class HomeWork : MenuSpace.Work
    {
        public string Name { get; } = "Программа приветствие.";
        public MenuSpace.Menu.Runner Runner;
        static Dictionary<string, string> DayOfWeek = new Dictionary<string, string>
        {
            { "Monday" , "Понедельник" },
            {"Tuesday","Вторник" },
            {"Wednesday","Среда" },
            {"Thursday","Четверг" },
            {"Friday","Пятница" },
            {"Saturday","Суббота" },
            {"Sunday","Воскресенье" }
        };

        public HomeWork()
        {
            Runner = HelloNameDate;
        }


        static DateTime currentDateTime = DateTime.Now;
        void HelloNameDate()
        {

            Console.WriteLine("Введите Имя.");
            string name = Console.ReadLine();
            string dayName = DayOfWeek.GetValueOrDefault(currentDateTime.DayOfWeek.ToString());
            Console.WriteLine($"Здравствуйте! {name} Сегодня {currentDateTime.ToString("D")} {dayName}");
        }
    }
}
