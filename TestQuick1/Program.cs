using System;

namespace TestQuick1
{
    public class Program
    {
        static void Main(string[] args)
        {                                                                               // Переменные
            string[] arr;                                                               // Массив
            string str;                                                                 // Строка для заполнения массива
            string mover;                                                               // Число индекса сдвига
            
        static void Main(string[] args)
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
