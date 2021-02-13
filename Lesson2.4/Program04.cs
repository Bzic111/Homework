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
            var season = Months.Error;                                                  // Сезоны
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
                    season = Months.Зима;
                    normalMidTempDay = -8f;
                    normalMidTempNight = -9f;
                    month = "Январе";
                    break;
                case 2:
                    season = Months.Зима;
                    normalMidTempDay = -8f;
                    normalMidTempNight = -10f;
                    month = "Феврале";
                    break;
                case 3:
                    season = Months.Весна;
                    normalMidTempDay = 0f;
                    normalMidTempNight = -3f;
                    month = "Марте";
                    break;
                case 4:
                    season = Months.Весна;
                    normalMidTempDay = 8f;
                    normalMidTempNight = 5f;
                    month = "Апреле";
                    break;
                case 5:
                    season = Months.Весна;
                    normalMidTempDay = 16f;
                    normalMidTempNight = 13f;
                    month = "Мае";
                    break;
                case 6:
                    season = Months.Лето;
                    normalMidTempDay = 19f;
                    normalMidTempNight = 16f;
                    month = "Июне";
                    break;
                case 7:
                    season = Months.Лето;
                    normalMidTempDay = 23f;
                    normalMidTempNight = 19f;
                    month = "Июле";
                    break;
                case 8:
                    season = Months.Лето;
                    normalMidTempDay = 21f;
                    normalMidTempNight = 16f;
                    month = "Августе";
                    break;
                case 9:
                    season = Months.Осень;
                    normalMidTempDay = 15f;
                    normalMidTempNight = 11f;
                    month = "Сентябре";
                    break;
                case 10:
                    season = Months.Осень;
                    normalMidTempDay = 7f;
                    normalMidTempNight = 4f;
                    month = "Октябре";
                    break;
                case 11:
                    season = Months.Осень;
                    normalMidTempDay = 0f;
                    normalMidTempNight = -1f;
                    month = "Ноябре";
                    break;
                case 12:
                    season = Months.Зима;
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
            if (season == Months.Зима)
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
            else if (season == Months.Весна)
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
            else if (season == Months.Лето)
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
            else if (season == Months.Осень)
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
            else
            {
                Console.WriteLine($"Что-то пошло не так....");
            }
        }
    }
}
