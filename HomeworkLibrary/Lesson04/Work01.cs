using System;

namespace Lesson04
{
    public class Work01 : MenuSpace.Work
    {
        string Name { get; } = "Метод GetFullName();";
        string Code { get; } = @"void GetFullName(string firstName, string lastName, string patronymic)
{
    Console.WriteLine($""{lastName} {firstName} {patronymic}"");
}
    
public override void Start()
{
    string firstName;   // 
    string lastName;    // 
    string patronymic;  //             

    Console.WriteLine(""Введите имя: "");
    firstName = Console.ReadLine();
    
    Console.WriteLine(""Введите фамилию: "");
    lastName = Console.ReadLine();

    Console.WriteLine(""Введите отчество: "");
    patronymic = Console.ReadLine();

    GetFullName(firstName, lastName, patronymic);

    string[] firstNames = { ""Иван"", ""Дмитрий"", ""Андрей"", ""Александр"", ""Евгений"" };
    string[] lastNames = { ""Сикорский"", ""Менделеев"", ""Троцкий"", ""Рязанов"", ""Перельман"" };
    string[] patronymics = { ""Евгеньевич"", ""Иванович"", ""Дмитриевич"", ""Андреевич"", ""Александрович"" };
    string[,] theNames = new string[3, 5];

    for (int i = 0; i<theNames.GetLength(1); i++)
    {
        theNames[0, i] = firstNames[i];
        theNames[1, i] = lastNames[i];
        theNames[2, i] = patronymics[i];
    }

    for (int i = 0; i<theNames.GetLength(1); i++)
    {
        GetFullName(theNames[0, i], theNames[1, i], theNames[2, i]);
    }
}";
        public override void GetCode() { Console.WriteLine(this.Code); }
        public override string GetName() { return this.Name; }

        public override void Start()
        {
            string firstName;   // 
            string lastName;    // 
            string patronymic;  //             

            Console.WriteLine("Введите имя: ");
            firstName = Console.ReadLine();

            Console.WriteLine("Введите фамилию: ");
            lastName = Console.ReadLine();

            Console.WriteLine("Введите отчество: ");
            patronymic = Console.ReadLine();

            GetFullName(firstName, lastName, patronymic);

            string[] firstNames = { "Иван", "Дмитрий", "Андрей", "Александр", "Евгений" };
            string[] lastNames = { "Сикорский", "Менделеев", "Троцкий", "Рязанов", "Перельман" };
            string[] patronymics = { "Евгеньевич", "Иванович", "Дмитриевич", "Андреевич", "Александрович" };
            string[,] theNames = new string[3, 5];

            for (int i = 0; i < theNames.GetLength(1); i++)
            {
                theNames[0, i] = firstNames[i];
                theNames[1, i] = lastNames[i];
                theNames[2, i] = patronymics[i];
            }

            for (int i = 0; i < theNames.GetLength(1); i++)
            {
                GetFullName(theNames[0, i], theNames[1, i], theNames[2, i]);
            }
        }
        void GetFullName(string firstName, string lastName, string patronymic)
        {
            Console.WriteLine($"{lastName} {firstName} {patronymic}");
        }
    }
}
