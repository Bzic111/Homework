using System;
namespace Lesson04
{
    public class Work02 : MenuSpace.Work
    {
        string Name { get; } = "Метод возврата суммы чисел массива";
        string Code { get; } = @"public override void Start()
{
    Console.WriteLine(""Введите значения для массива через пробел."");
    string str = Console.ReadLine();
    Console.WriteLine(ArraySum(str));
}
    // Основной метод
    int ArraySum(string str)
{
    int sum = 0;
    str = str.Trim();
    string[] temp = str.Split("" "", StringSplitOptions.RemoveEmptyEntries);

    for (int i = 0; i<temp.Length; i++)
    {
        if (Int32.TryParse(temp[i], out int num))
        {
            sum += num;
        }
    }
return sum;
}";
        public override void GetCode() { Console.WriteLine(this.Code); }
        public override string GetName() { return this.Name; }
        public override void Start()
        {
            Console.WriteLine("Введите значения для массива через пробел.");
            string str = Console.ReadLine();
            Console.WriteLine(ArraySum(str));
        }
        int ArraySum(string str)
        {
            int sum = 0;
            str = str.Trim();
            string[] temp = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < temp.Length; i++)
            {
                if (Int32.TryParse(temp[i], out int num))
                {
                    sum += num;
                }
            }
            return sum;
        }
    }
}
