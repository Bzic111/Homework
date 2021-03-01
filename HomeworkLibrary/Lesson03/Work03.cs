using System;
namespace Lesson03
{
    public class Work03 : MenuSpace.Work
    {
        public string Name { get; } = "Вывод строки в обратном порядке.";
        public string Code { get; } = @"public void Start()
        {
            Console.WriteLine(""Введите текст."");
            string str = Console.ReadLine();
            for (int i = str.Length-1; i >= 0; i--)
            {
                Console.Write(str[i]);
            }
        }";
        public override void GetCode()
        {
            Console.WriteLine(Code);
        }
        public override string GetName()
        {
            return Name;
        }
        public override void Start()
        {
            Console.WriteLine("Введите текст.");
            string str = Console.ReadLine();
            for (int i = str.Length - 1; i >= 0; i--)
            {
                Console.Write(str[i]);
            }
        }
    }
}
