using System;
namespace Lesson04
{
    public class Work04 : MenuSpace.Work
    {
        string Name { get; } = "Рекурсия Фибоначчи";
        string Code { get; } = @"public override void Start() 
{
    Console.WriteLine(""Введите порядковый номер последовательности Фибоначчи"");
    if (Int32.TryParse(Console.ReadLine(), out int n))
    {
        Console.WriteLine($""Fibonachi({n}) = {Fibonachi(n)}"");
    }
    else
    {
        Console.WriteLine(""Error"");
    }
}
int Fibonachi(int n)
    {
        if (n > 0)
    {
        return (Fibonachi(n - 1) + Fibonachi(n - 2));
    }
    else if (n<0)
    {
        return -n;
    }
    return n;
}";
        public override void GetCode() { Console.WriteLine(this.Code); }
        public override string GetName() { return this.Name; }
        public override void Start()
        {
            Console.WriteLine("Введите порядковый номер последовательности Фибоначчи");
            if (Int32.TryParse(Console.ReadLine(), out int n))
            {
                Console.WriteLine($"Fibonachi({n}) = {Fibonachi(n)}");
            }
            else
            {
                Console.WriteLine("Error");
            }
        }
        int Fibonachi(int n)
        {
            if (n > 0)
            {
                return (Fibonachi(n - 1) + Fibonachi(n - 2));
            }
            else if (n < 0)
            {
                return -n;
            }
            return n;
        }
    }
}
