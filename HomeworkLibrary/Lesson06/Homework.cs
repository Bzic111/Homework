using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Lesson06
{
    public class HomeWork : MenuSpace.Work
    {
        public new MenuSpace.Menu.Runner[] AllRuns { get; }
        string path { get; } = @"C:\Users\bzic1\Desktop\GBCsharpProject\Homework";
        string[] Names { get; } =
        {
            "Сохранение дерева каталогов и файлов в текстовый файл",
            "Показать содержимое файла Tree.txt",
            "Показать содержимое файла FullTree.txt",
            "ToDo лист",
            "Сгенерировать ToDo.json",
            "Работа с исключениями",
            "Класс Person"
        };
        public override string[] GetNames() { return this.Names; }
        public override MenuSpace.Menu.Runner[] GetRunners()
        {
            return AllRuns;
        }

        public HomeWork()
        {
            AllRuns = new MenuSpace.Menu.Runner[]
            {
                SaveCatalogTree,
                ShowFile,
                ShowFullFile,
                ToDoList,
                CreateTestToDoJson,
                ArrlayExceptions,
                PersonsInfo
            };
        }

        void PersonsInfo()
        {
            Random rnd = new Random();

            Person[] persArray = new Person[5];
            persArray[0] = new Person("Антон Антонович Антонов",
                                      "Бухгалтер",
                                      "anton@mail.ru",
                                      $"+7({ rnd.Next(900, 999)}) { rnd.Next(0, 999)}-{ rnd.Next(0, 99)}-{ rnd.Next(0, 99)}",
                                      rnd.Next(15000, 30000),
                                      rnd.Next(35, 50));
            persArray[1] = new Person("Иван Иванович Иванов",
                                      "Монтажник",
                                      "ivan@mail.ru",
                                      $"+7({ rnd.Next(900, 999)}) { rnd.Next(0, 999)}-{ rnd.Next(0, 99)}-{ rnd.Next(0, 99)}",
                                      rnd.Next(15000, 30000),
                                      rnd.Next(35, 50));
            persArray[2] = new Person("Сергей Сергеевич Сергеев",
                                      "Директор",
                                      "sergey@mail.ru",
                                      $"+7({ rnd.Next(900, 999)}) { rnd.Next(0, 999)}-{ rnd.Next(0, 99)}-{ rnd.Next(0, 99)}",
                                      rnd.Next(15000, 30000),
                                      rnd.Next(35, 50));
            persArray[3] = new Person("Владимир Владимирович Владимиров",
                                      "Водитель",
                                      "vladimir@mail.ru",
                                      $"+7({ rnd.Next(900, 999)}) { rnd.Next(0, 999)}-{ rnd.Next(0, 99)}-{ rnd.Next(0, 99)}",
                                      rnd.Next(15000, 30000),
                                      rnd.Next(35, 50));
            persArray[4] = new Person("Леонид Леонидович Леонидов",
                                      "Сторож",
                                      "leonid@mail.ru",
                                      $"+7({ rnd.Next(900, 999)}) { rnd.Next(0, 999)}-{ rnd.Next(0, 99)}-{ rnd.Next(0, 99)}",
                                      rnd.Next(15000, 30000),
                                      rnd.Next(35, 50));

            foreach (var item in persArray)
            {
                if (item.Age > 40)
                {
                    item.GetInfo();
                }
            }
            Console.ReadKey();
        }
        void ArrlayExceptions()
        {
            Random rnd = new Random();
            string[,] test = new string[4, 5];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    test[i, j] = rnd.Next(0, 10).ToString();
                }
            }
            test[2, 2] = "454bjbjnkls";

            Console.WriteLine(ArrPlaySummary(test));
        }
        void SaveCatalogTree()
        {
            Console.Clear();

            StringBuilder sb = new StringBuilder();
            string path = this.path;
            Console.WriteLine($"Введите путь, или нажмите Enter.\nСтандартный путь {this.path}");
            string newPath = Console.ReadLine();
            int pad = path.Split('\\').Length - 1;

            if (Directory.Exists(newPath))
            {
                path = newPath;
                Console.WriteLine("Новый путь установлен\n" + path);
            }

            sb.Append("╚╦" + path.Split('\\')[^1] + "\n");
            sb.Append(GetTree(path, pad));
            File.WriteAllText(this.path + @"\Tree.txt", sb.ToString());
            Console.WriteLine("Tree.txt ready");

            sb.Clear();
            sb.Append("╚╦" + path.Split('\\')[^1] + "\n");
            sb.Append(GetFullTree(path, pad));
            File.WriteAllText(this.path + @"\FullTree.txt", sb.ToString());
            Console.WriteLine("FullTree.txt ready");
        }
        void ShowFile()
        {
            Console.Clear();
            Console.WriteLine(File.ReadAllText(this.path + @"\Tree.txt"));
            Console.ReadKey();
        }
        void ShowFullFile()
        {
            Console.Clear();
            Console.WriteLine(File.ReadAllText(this.path + @"\FullTree.txt"));
            Console.ReadKey();
        }
        void ToDoList()
        {
            ToDo[] reList;
            Console.Clear();
            Console.WriteLine($"Введите путь, или нажмите Enter.\nСтандартный путь {this.path}");
            string path = Console.ReadLine();
            if (File.Exists(path))
            {
                Console.WriteLine("Новый путь установлен\n" + path);
            }
            else
            {
                path = this.path + @"\ToDo.json";
            }
            string str;

            if (File.Exists(path))
            {
                int counter = 1;
                reList = JsonSerializer.Deserialize<ToDo[]>(File.ReadAllText(path));
                foreach (var item in reList)
                {
                    string done = item.IsDone ? "[x]" : "[ ]";
                    Console.WriteLine($"{done} {counter.ToString().PadLeft(2, '0')} {item.Title}");
                    counter++;
                }
                do
                {
                    int cursorTop = Console.CursorTop;
                    str = Console.ReadLine();
                    Console.SetCursorPosition(0, cursorTop);
                    Console.Write("".PadLeft(str.Length, ' '));
                    Console.SetCursorPosition(0, cursorTop);
                    if (Int32.TryParse(str, out int value))
                    {
                        if ((value > 0) & (value - 1 < reList.Length))
                        {
                            reList[value - 1].IsDone = !reList[value - 1].IsDone;
                        }
                    }
                    Console.SetCursorPosition(0, 0);
                    Console.Clear();
                    Console.WriteLine("Список задач. Цифра 0 - Выход");
                    foreach (var item in reList)
                    {
                        string done = item.IsDone ? "[x]" : "[ ]";
                        Console.WriteLine($"{done} {counter.ToString().PadLeft(2, '0')} {item.Title}");
                        counter++;
                    }
                } while (str != "0");

                File.WriteAllText(path, JsonSerializer.Serialize<ToDo[]>(reList));
            }
            else
            {
                Console.WriteLine("Путь задан не верно. Файл не существует.\nВведите другой путь");
            }
        }
        static int ArrPlaySummary(string[,] str)
        {
            int summary = 0;
            int i = 0;
            int j = 0;
            if ((str.GetLength(0) == 4) & (str.GetLength(1) == 4))
            {
                for (; i < str.GetLength(0); i++)
                {
                    for (; j < str.GetLength(1); j++)
                    {
                        try
                        {
                            summary += Int32.Parse(str[i, j]);
                            Console.Write(str[i, j] + " ");
                        }
                        catch
                        {
                            Console.WriteLine($"Элемент массива с адресом [{i},{j}] не является числом\n" + summary);
                            throw new MyArrayDataException($"Элемент массива с адресом [{i},{j}] не является числом");
                        }
                    }
                    j = 0;
                }
            }
            else if (str.GetLength(0) > 4)
            {
                throw new MyArraySizeException("Слишком много рядов");
            }
            else if (str.GetLength(0) < 4)
            {
                throw new MyArraySizeException("Слишком мало рядов");
            }
            else if (str.GetLength(1) > 4)
            {
                throw new MyArraySizeException("Слишком много столбцов");
            }
            else if (str.GetLength(1) < 4)
            {
                throw new MyArraySizeException("Слишком мало столбцов");
            }
            return summary;
        }
        void CreateTestToDoJson()
        {
            ToDo Task1 = new ToDo("First task");
            ToDo Task2 = new ToDo("Second task");
            ToDo Task3 = new ToDo("Third task");
            ToDo Task4 = new ToDo("Fourth task");
            ToDo Task5 = new ToDo("Fifth task");
            ToDo[] Tasks = { Task1, Task2, Task3, Task4, Task5 };
            string json = JsonSerializer.Serialize<ToDo[]>(Tasks);
            File.WriteAllText(this.path + @"\ToDo.json", json);
            Console.WriteLine("Создан файл ToDo.json");
        }
        string GetTree(string path, int padLeft)
        {
            string[] dirs;
            string[] files;
            StringBuilder sb = new StringBuilder();
            if (Directory.Exists(path))
            {
                dirs = Directory.GetDirectories(path);
                files = Directory.GetFiles(path);
                for (int i = 0; i < dirs.Length; i++)
                {
                    sb.Append(" " + "╠═".PadLeft(dirs[i].Split('\\').Length - padLeft, '║') + dirs[i].Split('\\')[^1] + "\n");

                }
                foreach (var item in files)
                {
                    sb.Append(" " + "╟─".PadLeft(path.Split('\\').Length - padLeft, '║') + item.Split('\\')[^1] + "\n");
                }
            }
            sb.Append(" " + "╚".PadLeft(path.Split('\\').Length - padLeft, '║') + "═".PadLeft(path.Split('\\').Length, '═') + "\n");

            return sb.ToString();
        }
        string GetFullTree(string path, int padLeft)
        {
            string[] dirs;
            string[] files;
            StringBuilder sb = new StringBuilder();
            if (Directory.Exists(path))
            {
                dirs = Directory.GetDirectories(path);
                files = Directory.GetFiles(path);
                for (int i = 0; i < dirs.Length; i++)
                {
                    sb.Append(" " + "╠╦".PadLeft(dirs[i].Split('\\').Length - padLeft, '║') + dirs[i].Split('\\')[^1] + "\n");
                    if (i == dirs.Length - 1)
                    {
                        sb.Append(" " + "╟".PadLeft(dirs[i].Split('\\').Length - padLeft, '║') + "─".PadLeft(dirs[i].Split('\\').Length, '─') + "\n");
                    }
                    sb.Append(GetFullTree(dirs[i], padLeft));
                }
                foreach (var item in files)
                {
                    sb.Append(" " + "╟─".PadLeft(path.Split('\\').Length + 1 - padLeft, '║') + item.Split('\\')[^1] + "\n");
                }
            }
            sb.Append(" " + "╚".PadLeft(path.Split('\\').Length - padLeft, '║') + "═".PadLeft(path.Split('\\').Length, '═') + "\n");
            return sb.ToString();
        }
    }
}
