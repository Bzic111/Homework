using System;

namespace TestQuick1
{
    class Program
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
                Console.WriteLine("move = " + move);
            }
            else
            {
                move = 0;
                Console.WriteLine("Error. move = " + move);
            }
            str = str.Trim();                                                           // Очистка строки от пробелов в начале и в конце строки
            arr = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);                // Заполнение массива значениями
            if (move < 0)                                                               // Цикл сдвига "влево"
            {
                string temp = arr[0];                                                   // временная переменная
                do
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (i < arr.Length - 1)
                        {
                            arr[i] = arr[i + 1];
                        }
                        else
                        {
                            arr[i] = temp;
                        }
                    }
                } while (move != 0);
            }
            else if (move > 0)                                                          // Цикл сдвига "вправо"
            {
                string temp = arr[arr.Length - 1];                                      // временная переменная
                do
                {
                    for (int i = arr.Length - 1; i >= 0; i--)
                    {
                        if (i > 0)
                        {
                            arr[i] = arr[i - 1];
                        }
                        else
                        {
                            arr[i] = temp;
                        }
                    }
                    move--;
                } while (move != 0);
            }
            else                                                                        // Сдвиг не производится
            {
                Console.WriteLine("move = " + move + " сдвиг не произведён.");
            }

            for (int i = 0; i < arr.Length; i++)                                        // Вывод цикла в консоль
            {
                Console.Write(arr[i] + " ");
            }
        }
    }
}
