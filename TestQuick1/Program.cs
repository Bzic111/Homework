using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;

namespace TestQuick1
{
    public class Program
    {
        static void Main(string[] args)
        {                                                                               // Переменные
            string[] arr;                                                               // Массив
            string str;                                                                 // Строка для заполнения массива
            string mover;                                                               // Число индекса сдвига
            
            Console.WriteLine("Введите значения для массива через пробел.");
            str = Console.ReadLine();
            Console.WriteLine("Введите число сдвига массива");
            mover = Console.ReadLine();

            if (Int32.TryParse(mover, out int move))
            {
            Console.WriteLine(Fibonachi(10));
            }
        static int Fibonachi(int n)
            {
            if (n>1)
            {
                Console.WriteLine(n);
                return (Fibonachi(n - 1) + Fibonachi(n - 2));
                        }
                        else
                        {
                return n;
            }
        }
    }
}
