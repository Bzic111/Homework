using System;
using System.Collections.Generic;

namespace Lesson01
{
    public class HomeWork : MenuSpace.Work
    {
        string[] Names { get; } = { "Программа приветствие." };
        public new MenuSpace.Menu.Runner[] AllRuns { get; }

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

        public HomeWork()
        {
            AllRuns = new MenuSpace.Menu.Runner[] { HelloNameDate };
        }
        public override string[] GetNames() { return this.Names; }

        void HelloNameDate()
        {
            Console.WriteLine("Введите Имя.");
            string name = Console.ReadLine();
            DateTime currentDateTime = DateTime.Now;
            string dayName = DayOfWeek.GetValueOrDefault(currentDateTime.DayOfWeek.ToString());
            Console.WriteLine($"Здравствуйте! {name} Сегодня {currentDateTime.ToString("D")} {dayName}");
        }
    }
}
