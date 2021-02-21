using System;

namespace Lesson3._2
{
    class Program
    {
        static void Main(string[] args)
        {
            int cursor = 0;
            string selected = null;
            Console.WriteLine("Press any key to start....");
            var move = Console.ReadKey(false);
            string[,] contacts =
                { {"Виктор","Андрей","Александр","Фёдор","Пётр","exit"},
                {"1234567890","6549873215","1234578965","1597534562","7891238524","" },
                {"njhau@turututu.ru","afahaha@turututu.ru","sagaga@turututu.ru","ejukljk@turututu.ru","agsgdffdht@turututu.ru","" } };
            do
            {
                Console.Clear();
                Console.WriteLine("Выберите контакт и нажмите Enter.");
                for (int i = 0; i < contacts.GetLength(1); i++)
                {
                    if (i == cursor)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(contacts[0, i]);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        continue;
                    }
                    Console.WriteLine(contacts[0, i]);
                }
                move = Console.ReadKey(false);
                if (move.Key == ConsoleKey.DownArrow)
                {
                    if (cursor < contacts.GetLength(1) - 1)
                    {
                        cursor = cursor + 1;
                    }

                }
                else if (move.Key == ConsoleKey.UpArrow)
                {
                    if (cursor > 0)
                    {
                        cursor = cursor - 1;
                    }
                }
                else if (move.Key == ConsoleKey.Enter & selected != "exit")
                {
                    selected = contacts[0, cursor];
                    Console.WriteLine($"\n\n{contacts[0, cursor].PadRight(25 - contacts[1, cursor].Length)} {contacts[1, cursor]}{("").PadRight(5)} {contacts[2, cursor]}");
                    move = Console.ReadKey(false);
                }
            } while (selected != "exit");            
        }
    }
}
