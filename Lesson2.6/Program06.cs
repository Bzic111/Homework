using System;

namespace Lesson2_6
{
    class Program06
    {
        static void Main(string[] args)
        {
            string shopName, address, openName, moneyType, name1,name2,name3, endLine, commision;
            decimal line1, line2, line3, tax,summ,total;
            shopName = "Магазин у бобра";
            address = "г.Кострома ул. Советская,1";
            openName = "ИП Бобёр";
            moneyType = "Cridit Card Visa";
            name1 = "Бревно";
            name2 = "Доска";
            name3 = "Щепа";
            commision = "Комиссия";
            endLine = "Итого";
            line1 = 12.50m;
            line2 = 10.75m;
            line3 = 5.19m;
            tax = 0.09m;
            summ = line1 + line2 + line3;
            total = summ + summ * tax;

            Console.WriteLine($"{shopName.PadLeft(20)}\n{openName.PadLeft(16)}");
            Console.WriteLine(address + "\n" + moneyType);
            Console.WriteLine(("").PadRight(26, '_'));
            Console.WriteLine($"{name1}{line1.ToString().PadLeft(20, '.')}");
            Console.WriteLine($"{name2}{line2.ToString().PadLeft(21, '.')}");
            Console.WriteLine($"{name3}{line3.ToString().PadLeft(22, '.')}");
            Console.WriteLine($"\n{commision}{("").PadRight(12, '.')}{(summ*tax).ToString("F").PadLeft(6,'.')}");
            Console.WriteLine(("").PadRight(26, '_'));
            Console.WriteLine($"{endLine}{("").PadRight(15, '.')}{total.ToString("F").PadLeft(6,'.')}\n");
            Console.WriteLine(("Спасибо за покупку").PadLeft(22));
        }
    }
}
