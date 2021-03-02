using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lesson05
{
    public class Work01 : MenuSpace.Work
    {
        string Name { get; } = "Сохранение введённой строки в файл";
        string Code { get; } = @"public override void Start()
{
    Console.WriteLine(""введите текст для сохранения"");
    string text = Console.ReadLine();
    string path = ""Lesson5_Work01_text_file.txt"";
    Console.WriteLine($""Файл {path} сохранён с содержимым {text}"");
    File.WriteAllText(path, text);
}";
        public override void GetCode() { Console.WriteLine(this.Code); }
        public override string GetName() { return this.Name; }
        public override void Start()
        {
            Console.WriteLine("введите текст для сохранения");
            string text = Console.ReadLine();
            string path = "Lesson5_Work01_text_file.txt";
            Console.WriteLine($"Файл {path} сохранён с содержимым {text}");
            File.WriteAllText(path, text);
        }        
    }
}
