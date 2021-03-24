using System;

namespace Lesson04
{
    public class HomeWork : MenuSpace.Work
    {
        string[] Names { get; } =
            {
            "Метод GetFullName();",
            "Метод возврата суммы чисел массива",
            "Метод по определению времени года",
            "Рекурсия Фибоначчи"
            };
        new MenuSpace.Menu.Runner[] AllRuns;
        public override MenuSpace.Menu.Runner[] GetRunners()
        {
            return AllRuns;
        }
        public HomeWork()
        {
            AllRuns = new MenuSpace.Menu.Runner[]
            {
                MethodGFN,
                ArrPlaySum,
                SeasonCheck,
                ReFibonachi
            };
        }
        public override string[] GetNames() { return this.Names; }

        [Flags]
        enum Months
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

        void MethodGFN()
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

            Console.ReadKey(true);
        }
        void ArrPlaySum()
        {
            Console.WriteLine("Введите значения для массива через пробел.");
            string str = Console.ReadLine();
            Console.WriteLine(ArraySum(str));

            Console.ReadKey(true);
        }
        void SeasonCheck()
        {
            Console.WriteLine("Введите значени: ");
            int month = 0;
            do
            {
                if (Int32.TryParse(Console.ReadLine(), out month))
                {
                    if (month > 0 & month <= 12)
                    {
                        break;
                    }
                }
                Console.WriteLine("Ошибка: введите число от 1 до 12");
            } while (!((month > 0) & (month <= 12)));
            Console.WriteLine(GetSeason(GetMonth(month)));

            Console.ReadKey(true);
        }
        void ReFibonachi()
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

            Console.ReadKey(true);
        }
        void GetFullName(string firstName, string lastName, string patronymic)
        {
            Console.WriteLine($"{lastName} {firstName} {patronymic}");
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
        Months GetMonth(int num)
        {
            byte b = 0b_000_000_000_001;

            if (num > 0 & num <= 12)
            {
                b = (byte)(b << num - 1);
                return (Months)b;
            }
            else
            {
                return Months.Error;
            }
        }
        string GetSeason(Months month)
        {
            if ((month | Months.Зима) == Months.Зима)
            {
                return Months.Зима.ToString();
            }
            else if ((month | Months.Весна) == Months.Весна)
            {
                return Months.Весна.ToString();
            }
            else if ((month | Months.Лето) == Months.Лето)
            {
                return Months.Лето.ToString();
            }
            else if ((month | Months.Осень) == Months.Осень)
            {
                return Months.Осень.ToString();
            }
            else
            {
                return Months.Error.ToString();
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
