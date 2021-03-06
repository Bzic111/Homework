using System;

namespace Lesson06
{
    class Person
    {
        string FullName { get; }
        string Position { get; }
        string EMail { get; }
        string PhoneNumber { get; }
        decimal Salary { get; }
        public int Age { get; }
        public Person(string name, string position, string eMail, string phone, decimal salary, int age)
        {
            FullName = name;
            Position = position;
            EMail = eMail;
            PhoneNumber = phone;
            Salary = salary;
            Age = age;
        }
        public void GetInfo()
        {
            Console.WriteLine($"Сотрудник\t{FullName}\nДолжность\t{Position}");
            Console.Write($"Эл.Почта\t");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{EMail}\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"Номер телефона \t{PhoneNumber}\nЗарплата\t{Salary}\nВозраст\t\t{Age}\n");
        }
    }
}
