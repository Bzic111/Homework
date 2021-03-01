using System;
namespace Lesson03
{
    public class Work04 : MenuSpace.Work
    {
        public string Name { get; } = "Морской бой";
        public string Code { get; } = @" Вывод сгенерированного игрового поля осуществляется циклом в методе Show(int[] moverUDRL, string[,] str) класса SelectorUDRL,
где int[] moverUDRL - индекс выделенной строки, а string[,] str массив поля.

for (int i = 0; i < str.GetLength(0); i++)
{
    for (int j = 0; j < str.GetLength(1); j++)
    {
        if (i == moverUDRL[0] & j == moverUDRL[1])                                      // Выделение строки массива белым цветом
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(str[i, j]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            continue;
        }
        if (str[i, j] == ""X"")                                                         // Выделение строки ""корабль"" красным цветом
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(str[i, j]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            continue;
        }
        Console.Write(str[i, j]);
    }
Console.Write(""\n"");
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
            bool[,] boolField;
            string[,] field = Ships.GetField(out boolField);

            int[] UDRL = { 0, 0 };
            string[,] fld = field;
            string mod = "X";
            string selected = null;
            int good1 = 0;
            SelectorUDRL Selector = new SelectorUDRL();


            do
            {
                int good4 = Ships.GetShipFour(ref fld, ref boolField);
                if (good4 == 1)
                {
                    do
                    {
                        int good3 = Ships.GetShipThree(ref fld, ref boolField);
                        if (good3 == 1)
                        {
                            do
                            {
                                int good2 = Ships.GetShipTwo(ref fld, ref boolField);
                                if (good2 == 1)
                                {
                                    do
                                    {
                                        good1 = Ships.GetShipOne(ref fld, ref boolField);
                                    } while (good1 != 1);
                                }
                            } while (good1 != 1);
                        }
                    } while (good1 != 1);
                }
            } while (good1 != 1);

            do
            {
                Console.Clear();
                Selector.Show(UDRL, fld);
                selected = Selector.Selecting(fld, ref UDRL, mod);
            } while (selected != "Exit");


        }
    }
}
