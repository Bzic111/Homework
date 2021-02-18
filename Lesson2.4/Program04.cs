using System;

namespace Lesson2_4
{
    class Program04
    {
        [Flags]
        public enum Months
        {
            Error = 0b000000000000,
            Январь = 0b_000_000_000_001,
            Февраль = 0b_000_000_000_010,
            Март = 0b_000_000_000_100,
            Апрель = 0b_000_000_001_000,
            Май = 0b_000_000_010_000,
            Июнь = 0b_000_000_100_000,
            Июль = 0b_000_001_000_000,
            Август = 0b_000_010_000_000,
            Сентябрь = 0b_000_100_000_000,
            Октябрь = 0b_001_000_000_000,
            Ноябрь = 0b_010_000_000_000,
            Декабрь = 0b_100_000_000_000,
            Зима = Январь | Февраль | Декабрь,
            Весна = Март | Апрель | Май,
            Лето = Июнь | Июль | Август,
            Осень = Сентябрь | Октябрь | Ноябрь
        }

        static void Main(string[] args)
        {
            int a;
            Months mon = Months.Error;                                                  // Месяца
            float minTemp;                                                              // минимальная температура
            float maxTemp;                                                              // максимальная температура
            float midTemp;                                                              // средняя температура
            float normalMidTempDay = 0;                                                 // Нормальная температура в месяце днём
            float normalMidTempNight = 0;                                               // Нормальная температура в месяце ночью
            string month = "Месяце";                                                    // название месяца для текста вывода
            Console.WriteLine($"Введите номер месяца:");
        Again:                                                                          // точка возврата при ошибке ввода номера месяца
            if (Int32.TryParse(Console.ReadLine(), out int val))
            {
                a = val;
            }
            else
            {
                Console.WriteLine("Что-то пошло не так, попробуйте ещё раз.");
                goto Again;
            }
            switch (a)
            {
                case 1:
                    mon = Months.Январь;
                    normalMidTempDay = -8f;
                    normalMidTempNight = -9f;
                    month = "Январе";
                    break;
                case 2:
                    mon = Months.Февраль;
                    normalMidTempDay = -8f;
                    normalMidTempNight = -10f;
                    month = "Феврале";
                    break;
                case 3:
                    mon = Months.Март;
                    normalMidTempDay = 0f;
                    normalMidTempNight = -3f;
                    month = "Марте";
                    break;
                case 4:
                    mon = Months.Апрель;
                    normalMidTempDay = 8f;
                    normalMidTempNight = 5f;
                    month = "Апреле";
                    break;
                case 5:
                    mon = Months.Май;
                    normalMidTempDay = 16f;
                    normalMidTempNight = 13f;
                    month = "Мае";
                    break;
                case 6:
                    mon = Months.Июнь;
                    normalMidTempDay = 19f;
                    normalMidTempNight = 16f;
                    month = "Июне";
                    break;
                case 7:
                    mon = Months.Июль;
                    normalMidTempDay = 23f;
                    normalMidTempNight = 19f;
                    month = "Июле";
                    break;
                case 8:
                    mon = Months.Август;
                    normalMidTempDay = 21f;
                    normalMidTempNight = 16f;
                    month = "Августе";
                    break;
                case 9:
                    mon = Months.Сентябрь;
                    normalMidTempDay = 15f;
                    normalMidTempNight = 11f;
                    month = "Сентябре";
                    break;
                case 10:
                    mon = Months.Октябрь;
                    normalMidTempDay = 7f;
                    normalMidTempNight = 4f;
                    month = "Октябре";
                    break;
                case 11:
                    mon = Months.Ноябрь;
                    normalMidTempDay = 0f;
                    normalMidTempNight = -1f;
                    month = "Ноябре";
                    break;
                case 12:
                    mon = Months.Декабрь;
                    normalMidTempDay = -6f;
                    normalMidTempNight = -6f;
                    month = "Декабре";
                    break;
                default:
                    break;
            }
            Console.WriteLine($"Введите минимальную температуру воздуха в {month}:");
        GetMinTemp:                                                                     // точка возврата при ошибке ввода минимальной температуры
            if (float.TryParse(Console.ReadLine(), out float valMin))
            {
                minTemp = valMin;
            }
            else
            {
                Console.WriteLine("Что-то пошло не так, попробуйте ещё раз.");
                goto GetMinTemp;
            }
            Console.WriteLine($"Введите максимальную температуру воздуха в {month}:");
        GetMaxTemp:                                                                     // точка возврата при ошибке ввода максимальной температуры
            if (float.TryParse(Console.ReadLine(), out float valMax))
            {
                maxTemp = valMax;
            }
            else
            {
                Console.WriteLine("Что-то пошло не так, попробуйте ещё раз.");
                goto GetMaxTemp;
            }
            midTemp = (minTemp + maxTemp) / 2;                                          // вычисление средней температуры (a+b)/2
            Console.Clear();
            if (( mon |Months.Зима) == Months.Зима)
            {
                Console.WriteLine($"В Костроме средняя температура зимой в {month} должна быть {normalMidTempDay} днём и {normalMidTempNight} ночью.");
                if (midTemp > 0 & midTemp < 10)
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Дождливая Зима");
                }
                else if (midTemp <= -40 || midTemp >= 10)
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Аномальная температура для зимы");
                }
                else if (midTemp < 0 & midTemp > ((normalMidTempDay + normalMidTempNight) / 2))
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Нормальная Зима");
                }
                else if (midTemp < ((normalMidTempDay + normalMidTempNight) / 2))
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Холодная Зима");
                }
            }
            else if ((mon | Months.Весна) == Months.Весна)
            {
                Console.WriteLine($"В Костроме средняя температура Весной в {month} должна быть {normalMidTempDay} днём и {normalMidTempNight} ночью.");
                if (midTemp > ((normalMidTempDay + normalMidTempNight) / 2))
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Тёплая Весна");
                }
                else if (midTemp <= -40 || midTemp >= 25)
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Аномальная температура для Весны");
                }
                else if (midTemp < ((normalMidTempDay + normalMidTempNight) / 2))
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Прохладная Весна");
                }
                else if (midTemp == ((normalMidTempDay + normalMidTempNight) / 2))
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Нормальная Весна");
                }
            }
            else if ((mon | Months.Лето) == Months.Лето)
            {
                Console.WriteLine($"В Костроме средняя температура Летом в {month} должна быть {normalMidTempDay} днём и {normalMidTempNight} ночью.");
                if (midTemp > ((normalMidTempDay + normalMidTempNight) / 2))
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Жаркое Лето");
                }
                else if (midTemp <= -40 || midTemp >= 45)
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Аномальная температура для Лета");
                }
                else if (midTemp < ((normalMidTempDay + normalMidTempNight) / 2))
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Прохладное Лето");
                }
                else if (midTemp == ((normalMidTempDay + normalMidTempNight) / 2))
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Нормальное Лето");
                }
            }
            else if ((mon | Months.Осень)== Months.Осень)
            {
                Console.WriteLine($"В Костроме средняя температура Осенью в {month} должна быть {normalMidTempDay} днём и {normalMidTempNight} ночью.");
                if (midTemp > ((normalMidTempDay + normalMidTempNight) / 2))
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Тёплая Осень");
                }
                else if (midTemp <= -40 || midTemp >= 25)
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Аномальная температура для Осени");
                }
                else if (midTemp < ((normalMidTempDay + normalMidTempNight) / 2))
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Прохладная Осень");
                }
                else if (midTemp == ((normalMidTempDay + normalMidTempNight) / 2))
                {
                    Console.WriteLine($"Средняя температура воздуха сейчас составляет {Math.Round(midTemp, 2)}. Нормальная Осень");
                }
            }
            else                                                                        // Что-то пошло не так.
            {
                Console.WriteLine($"Что-то пошло не так....");
            }
        }
    }
}
