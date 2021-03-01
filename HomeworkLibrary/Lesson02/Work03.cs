using System;
namespace Lesson02
{
    public class Work03 : MenuSpace.Work
    {
        public string Name { get; } = "Чётное или нечётное число.";

        public string Code { get; } = @"public void Start()
{
    Console.WriteLine($""Введите число"");
GetValue:
    if (int.TryParse(Console.ReadLine(), out int val))
    {
        if (val % 2 == 0)
        {
            Console.WriteLine($""Число чётное"");
        }
        else
        {
            Console.WriteLine($""Число не чётное"");
        }
    }
    else
    {
        Console.WriteLine(""Что-то пошло не так, попробуйте ещё раз."");
        goto GetValue;
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
            Console.WriteLine($"Введите число");
        GetValue:
            if (int.TryParse(Console.ReadLine(), out int val))
            {
                if (val % 2 == 0)
                {
                    Console.WriteLine($"Число чётное");
                }
                else
                {
                    Console.WriteLine($"Число не чётное");
                }
            }
            else
            {
                Console.WriteLine("Что-то пошло не так, попробуйте ещё раз.");
                goto GetValue;
            }
        }
    }
}
