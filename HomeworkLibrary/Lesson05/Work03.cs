using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lesson05
{
    public class Work03 : MenuSpace.Work
    {
        string Name { get; } = "Запись чисел в бинарный файл";
        string Code { get; } = @"Start()
{
    string path = ""Lesson05_work03_binary_file.bin"";
    Console.WriteLine(""Введите набор чисел через пробел:"");
    string temp = Console.ReadLine();
    temp.Trim();
    string[] arr = temp.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    byte[] bytes = new byte[arr.Length];
    for (int i = 0; i<arr.Length; i++)
    {
        if (byte.TryParse(arr[i], out byte b))
        {
            bytes[i] = b;
        }
    }
    File.WriteAllBytes(path, bytes);
Console.WriteLine($""Введённые числа записаны в файл {path}"");
}
";
        public override void GetCode() { Console.WriteLine(this.Code); }
        public override string GetName() { return this.Name; }
        public override void Start()
        {
            string path = "Lesson05_work03_binary_file.bin";
            Console.WriteLine("Введите набор чисел через пробел:");
            string temp = Console.ReadLine();
            temp.Trim();
            string[] arr = temp.Split(' ',StringSplitOptions.RemoveEmptyEntries);
            byte[] bytes = new byte[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                if (byte.TryParse(arr[i],out byte b))
                {
                    bytes[i] = b;
                }
            }
            File.WriteAllBytes(path,bytes);
            Console.WriteLine($"Введённые числа записаны в файл {path}");
        }
    }
}
