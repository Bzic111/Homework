using System;
using System.Collections.Generic;
using System.Collections;

namespace Lesson01
{    
    class Program01
    {
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


        static DateTime currentDateTime = DateTime.Now;
        static void Main(string[] args)
        {

            Console.WriteLine("Введите Имя.");
            string name = Console.ReadLine();
            string dayName = DayOfWeek.GetValueOrDefault(currentDateTime.DayOfWeek.ToString());
            Console.WriteLine($"Здравствуйте! {name} Сегодня {currentDateTime.ToString("D")} {dayName}");
        }
    }
}
