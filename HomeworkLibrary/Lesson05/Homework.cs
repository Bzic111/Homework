using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lesson05
{
    public class HomeWork : MenuSpace.Work
    {
        public new MenuSpace.Menu.Runner[] AllRuns { get; }

        string[] Names { get; } = 
        {
            "Сохранение введённой строки в файл",
            "Запись времени запуска в файл",
            "Запись чисел в бинарный файл"
        };
        public HomeWork()
        {
            AllRuns = new MenuSpace.Menu.Runner[]
            {
                SaveToTxtFile,
                LogWriteTime,
                SaveToBinary
            };
        }
        public override string[] GetNames()
        {
            return Names;
        }

        void SaveToTxtFile()
        {
            Console.WriteLine("введите текст для сохранения");
            string text = Console.ReadLine();
            string path = "Lesson5_Work01_text_file.txt";
            Console.WriteLine($"Файл {path} сохранён с содержимым {text}");
            File.WriteAllText(path, text);
        }
        void LogWriteTime()
        {
            string path = "startup.txt";
            DateTime dateTime = new DateTime();
            dateTime = DateTime.Now;
            File.WriteAllText(path, dateTime.ToString());
            Console.WriteLine($"В файл {path} записано {dateTime}");
        }
        void SaveToBinary()
        {
            string path = "Lesson05_work03_binary_file.bin";
            Console.WriteLine("Введите набор чисел через пробел:");
            string temp = Console.ReadLine();
            temp.Trim();
            string[] arr = temp.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            byte[] bytes = new byte[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                if (byte.TryParse(arr[i], out byte b))
                {
                    bytes[i] = b;
                }
            }
            File.WriteAllBytes(path, bytes);
            Console.WriteLine($"Введённые числа записаны в файл {path}");
        }
    }
}
