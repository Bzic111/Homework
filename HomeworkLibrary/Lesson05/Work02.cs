using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lesson05
{
    public class Work02 : MenuSpace.Work
    {
        string Name { get; } = "Запись времени запуска в файл";
        string Code { get; } = @"public override void Start()
{
    string path = ""startup.txt"";
    DateTime dateTime = new DateTime();
    dateTime = DateTime.Now;
    File.WriteAllText(path, dateTime.ToString());
    Console.WriteLine($""В файл {path} записано {dateTime}"");
}";
        public override void GetCode() { Console.WriteLine(this.Code); }
        public override string GetName() { return this.Name; }
        public override void Start()
        {
            string path = "startup.txt";
            DateTime dateTime = new DateTime();
            dateTime = DateTime.Now;
            File.WriteAllText(path, dateTime.ToString());
            Console.WriteLine($"В файл {path} записано {dateTime}");
        }
    }
}
