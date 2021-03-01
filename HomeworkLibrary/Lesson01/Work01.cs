using System;
using System.Collections.Generic;

namespace Lesson01
{
    public class Work01 : MenuSpace.Work
    {
        public string Name { get; } = "Программа приветствие.";
        public string Code { get; } = @"Dictionary<string, string> DayOfWeek = new Dictionary<string, string>
        {
            { ""Monday"" , ""Понедельник"" },
            {""Tuesday"",""Вторник"" },
            {""Wednesday"",""Среда"" },
            {""Thursday"",""Четверг"" },
            {""Friday"",""Пятница"" },
            {""Saturday"",""Суббота"" },
            {""Sunday"",""Воскресенье"" }
        };


    DateTime currentDateTime = DateTime.Now;
    void Start()
    {

        Console.WriteLine(""Введите Имя."");
        string name = Console.ReadLine();
        string dayName = DayOfWeek.GetValueOrDefault(currentDateTime.DayOfWeek.ToString());
        Console.WriteLine($""Здравствуйте! {name} Сегодня {currentDateTime.ToString(""D"")} {dayName}"");
    }";

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
        public override void GetCode()
        {
            Console.WriteLine(Code);
        }
        public override string GetName()
        {
            return Name;
        }


        static DateTime currentDateTime = DateTime.Now;
        public override void Start()
        {

            Console.WriteLine("Введите Имя.");
            string name = Console.ReadLine();
            string dayName = DayOfWeek.GetValueOrDefault(currentDateTime.DayOfWeek.ToString());
            Console.WriteLine($"Здравствуйте! {name} Сегодня {currentDateTime.ToString("D")} {dayName}");
        }
    }
}
