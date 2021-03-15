using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace TestQuick1
{
    enum GameStatus
    {
        Win,
        Lose,
        Draw,
        Break,
        Play,
        PlayerMove,
        AIMove
    }



    public struct FieldRange
    {

        public int StartPositionY;
        public int StartPositionX;
        public int EndPositionY;
        public int EndPositionX;
        public Coordinates2D[] DiagonalUp;
        public Coordinates2D[] DiagonalDown;
        public Coordinates2D[] Horizontal;
        public Coordinates2D[] Vertical;
        public FieldRange(int[] lastDot, int length)
        {
            Coordinates2D Zero = new Coordinates2D(0, 0);
            Coordinates2D VectorX = new Coordinates2D(0, 1);
            Coordinates2D VectorY = new Coordinates2D(1, 0);
            Coordinates2D Vector2D = new Coordinates2D(1, 1);



            StartPositionX = lastDot[1] - length;
            EndPositionX = lastDot[1] + length;
            StartPositionY = lastDot[0] - length;
            EndPositionY = lastDot[0] + length;


            DiagonalUp = new Coordinates2D[length * 2 - 1];
            DiagonalDown = new Coordinates2D[length * 2 - 1];
            Horizontal = new Coordinates2D[length * 2 - 1];
            Vertical = new Coordinates2D[length * 2 - 1];
            int counter = 0;



            for (int i = StartPositionX, j = StartPositionY; i <= EndPositionX & j <= EndPositionY; j++, i++, counter++)
            {
                DiagonalDown[counter] = new Coordinates2D(j, i);
            }
            counter = 0;
            for (int i = StartPositionX, j = EndPositionY; i <= EndPositionX & j >= StartPositionY; i++)
            {
                DiagonalUp[counter] = new Coordinates2D(j, i);
            }
            counter = 0;
            for (int i = StartPositionX; i <= EndPositionX; i++)
            {
                Horizontal[counter] = new Coordinates2D(lastDot[0], i);
            }
            counter = 0;
            for (int i = StartPositionY; i < EndPositionY; i++)
            {
                Vertical[counter] = new Coordinates2D(i, lastDot[1]);
            }

        }
    }
    public struct Coordinates2D
    {
        public int X;
        public int Y;
        public Coordinates2D(int y, int x)
        {
            Y = y;
            X = x;
        }
        public Coordinates2D(Coordinates2D cd)
        {
            Y = cd.Y;
            X = cd.X;
        }
        public static bool operator >(Coordinates2D cd2dA, Coordinates2D cd2dB)
        {
            return cd2dA.X > cd2dB.X | cd2dA.Y > cd2dB.Y;
        }
        public static bool operator <(Coordinates2D cd2dA, Coordinates2D cd2dB)
        {
            return cd2dA.X < cd2dB.X | cd2dA.Y < cd2dB.Y;
        }
        public static bool operator !=(Coordinates2D cd2dA, Coordinates2D cd2dB)
        {
            return cd2dA.X != cd2dB.X || cd2dA.Y != cd2dB.Y;
        }
        public static bool operator ==(Coordinates2D cd2dA, Coordinates2D cd2dB)
        {
            return cd2dA.X == cd2dB.X & cd2dA.Y == cd2dB.Y;
        }
        public static Coordinates2D operator *(Coordinates2D cd2dA, int b)
        {
            return new Coordinates2D(cd2dA.Y * b, cd2dA.X * b);
        }
        public static Coordinates2D operator /(Coordinates2D cd2dA, int b)
        {
            return new Coordinates2D(cd2dA.Y / b, cd2dA.X / b);
        }
        public static Coordinates2D operator %(Coordinates2D cd2dA, int b)
        {
            return new Coordinates2D(cd2dA.Y % b, cd2dA.X % b);
        }
        public static Coordinates2D operator +(Coordinates2D cd2dA, Coordinates2D cd2dB)
        {
            int retY = cd2dA.Y + cd2dB.Y;
            int retX = cd2dA.X + cd2dB.X;
            return new Coordinates2D(retY, retX);
        }
        public static Coordinates2D operator -(Coordinates2D cd2dA, Coordinates2D cd2dB)
        {
            int retY = cd2dA.Y - cd2dB.Y;
            int retX = cd2dA.X - cd2dB.X;
            return new Coordinates2D(retY, retX);
        }
        public static Coordinates2D operator ++(Coordinates2D cd2dA)
        {
            int retY = cd2dA.Y + 1;
            int retX = cd2dA.X + 1;
            return new Coordinates2D(retY, retX);
        }
        public static Coordinates2D operator --(Coordinates2D cd2dA)
        {
            int retY = cd2dA.Y - 1;
            int retX = cd2dA.X - 1;
            return new Coordinates2D(retY, retX);
        }
        public static string ToString(Coordinates2D cd2dA)
        {
            return $"{cd2dA.Y};{cd2dA.X}";
        }

    }

    class Game
    {
        int SizeX { get; set; } = 5;
        int SizeY { get; set; } = 5;
        Coordinates2D Zero = new Coordinates2D(0, 0);
        Coordinates2D VectorX = new Coordinates2D(0, 1);
        Coordinates2D VectorY = new Coordinates2D(1, 0);
        Coordinates2D Vector2D = new Coordinates2D(1, 1);
        Coordinates2D Max;
        int MoveCounter { get; set; }
        char[,] Field;
        int[,] IntField;
        char Empty { get; } = ' ';
        char PlayerOneDot { get; } = 'X';
        char PlayerTwoDot { get; } = 'O';
        int WinSerie { get; set; } = 4;
        GameStatus Status { get; set; } = GameStatus.Play;
        void SetSize()
        {
            while (!Int32.TryParse(Console.ReadLine(), out int val))
            {
                SizeY = val;
                SizeX = val;
            }
        }
        public Game()
        {
            MoveCounter = SizeX * SizeY;
            Max = new Coordinates2D(SizeY, SizeX);
            Field = GetField();
            IntField = GetIntField();
            FillEmpty(Field);
            FillInts(IntField);
        }
        public void Play(Game game)
        {
            Show();
            int X = 0, Y = 0;
            Status = GameStatus.PlayerMove;
            do
            {
                Selector(this.SizeX, this.SizeY, ref Field, PlayerOneDot, ref X, ref Y);
                AIRandom(this.SizeX, this.SizeY, ref Field, PlayerTwoDot);
            } while (Status == GameStatus.PlayerMove | Status == GameStatus.AIMove);
        }
        char[,] GetField() => new char[SizeY, SizeX];
        int[,] GetIntField() => new int[SizeY, SizeX];
        void FillEmpty(char[,] str)
        {
            for (int i = 0; i < str.GetLength(0); i++)
            {
                for (int j = 0; j < str.GetLength(1); j++)
                {
                    str[i, j] = Empty;
                }
            }
        }
        void FillInts(int[,] field)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    field[i, j] = 1;
                }
            }
        }
        void ExitGame() => Status = GameStatus.Break;
        bool EndGame(GameStatus type, string player)
        {
            switch (type)
            {
                case GameStatus.Win:
                    Console.WriteLine($"Congratulation!!! Player {player} WIN!!!");
                    ExitGame();
                    break;
                case GameStatus.Lose:
                    Console.WriteLine("Sorry. Maybe nex time you`ll win. ");
                    ExitGame();
                    break;
                case GameStatus.Draw:
                    Console.WriteLine("It is DRAW.");
                    ExitGame();
                    break;
                case GameStatus.Break:
                    Console.WriteLine("Exit game without ending, progress not saved.");
                    ExitGame();
                    break;
                default:
                    break;
            }
            return false;
        }

        void Selector(int maxX, int maxY, ref char[,] chr, char dot, ref int X, ref int Y)
        {
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        chr[Y, X] = dot;
                        int[] last = { Y, X };
                        if (CheckWin(PlayerOneDot, last, chr))
                        {
                            EndGame(GameStatus.Win, "Player One");
                        }
                        Status = GameStatus.AIMove;
                        break;
                    case ConsoleKey.Escape:
                        ExitGame();
                        break;
                    case ConsoleKey.LeftArrow:
                        PrintBlack(chr[Y, X], Y, X);
                        X = (X > 0) ? --X : X = maxX - 1;
                        PrintWhite(chr[Y, X], Y, X);
                        break;
                    case ConsoleKey.UpArrow:
                        PrintBlack(chr[Y, X], Y, X);
                        Y = (Y > 0) ? --Y : Y = maxY - 1;
                        PrintWhite(chr[Y, X], Y, X);
                        break;
                    case ConsoleKey.RightArrow:
                        PrintBlack(chr[Y, X], Y, X);
                        X = (X < maxX - 1) ? ++X : X = 0;
                        PrintWhite(chr[Y, X], Y, X);
                        break;
                    case ConsoleKey.DownArrow:
                        PrintBlack(chr[Y, X], Y, X);
                        Y = (Y < maxY - 1) ? ++Y : Y = 0;
                        PrintWhite(chr[Y, X], Y, X);
                        break;
                    default:
                        break;
                }
            } while (Status == GameStatus.PlayerMove);
        }

        void PrintWhite(char chr, int Y, int X)
        {
            Console.CursorLeft = X == 0 ? 1 : (X * 2 + 1);
            Console.CursorTop = Y == 0 ? 1 : (Y * 2 + 1);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(chr);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        void PrintBlack(char chr, int Y, int X)
        {
            Console.CursorLeft = X == 0 ? 1 : (X * 2 + 1);
            Console.CursorTop = Y == 0 ? 1 : (Y * 2 + 1);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(chr);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        void Show()
        {
            Console.Write("╔");
            for (int i = 0; i < Field.GetLength(1) - 1; i++)
            {
                Console.Write("═╦");
            }
            Console.Write("═╗\n");
            for (int i = 0; i < Field.GetLength(0); i++)
            {
                if (i < Field.GetLength(0) - 1)
                {

                    for (int j = 0; j < Field.GetLength(1); j++)
                    {
                        Console.Write("║" + Field[i, j]);
                    }
                    Console.Write("║\n╠");
                    for (int l = 0; l < Field.GetLength(1) - 1; l++)
                    {
                        Console.Write("═╬");
                    }
                    Console.WriteLine("═╣");
                }
                else
                {
                    for (int j = 0; j < Field.GetLength(1); j++)
                    {
                        Console.Write("║" + Field[i, j]);
                    }
                    Console.Write("║\n╚");
                    for (int l = 0; l < Field.GetLength(1) - 1; l++)
                    {
                        Console.Write("═╩");
                    }
                    Console.WriteLine("═╝");
                }
            }
        }
        void SetDot(int Y, int X, ref int[,] intField, ref char[,] Field, char playerDot)
        {
            intField[Y, X] = 0;
            Field[Y, X] = playerDot;
            for (int i = Y - 1; i <= Y + 1; i++)
            {
                for (int j = X - 1; j <= X + 1; j++)
                {
                    if (Y > 0 & Y < intField.GetLength(0))
                    {
                        if (X > 0 & X < intField.GetLength(1))
                        {
                            intField[i, j] += intField[i, j] == 0 ? 0 : 1;
                        }
                    }
                }
            }
        }
        bool IsEmpty(int Y, int X, char[,] chr)
        {
            return chr[Y, X] == Empty;
        }
        void AIRandom(int maxX, int maxY, ref char[,] str, ref int[,] intField, char playerDot, ref int X, ref int Y)
        {
            Random rnd = new Random();
            int aiY, aiX;
            int[] last;
            do
            {
                aiY = rnd.Next(0, maxY);
                aiX = rnd.Next(0, maxX);
            } while (str[aiY, aiX] == 'X' | str[aiY, aiX] == 'O');
            str[aiY, aiX] = playerDot;
            PrintBlack(str[aiY, aiX], aiY, aiX);
            last = new int[] { aiY, aiX };
            Status = GameStatus.PlayerMove;
            if (CheckWin(playerDot, last, str))
            {
                Status = GameStatus.Lose;
                EndGame(GameStatus.Lose, "Player One");
            }
        }
        int[] FindGoodPoint(ref int[,] intField)
        {
            int temp = 0;
            int[] XY = new int[2];
            for (int i = 0; i < intField.GetLength(0); i++)
            {
                for (int j = 0; j < intField.GetLength(1); j++)
                {
                    if (intField[i,j]>temp)
                    {
                        temp = intField[i, j];
                        XY[0] = i;
                        XY[1] = j;
                    }
                }
            }
            return XY;
        }
        bool CheckDiagonalUp(char playerChar, int[] lastDot, char[,] field)
        {
            StringBuilder sb = new StringBuilder();
            string diagonalUp;
            int Xmin = lastDot[1] - WinSerie;
            int Xmax = lastDot[1] + WinSerie;
            int Ymin = lastDot[0] - WinSerie;
            int Ymax = lastDot[0] + WinSerie;
            for (int i = Ymax, j = Xmin; (i > Ymin) & (j < Xmax); i--, j++)
            {
                if ((i >= 0) & (i < field.GetLength(0)))
                {
                    if ((j >= 0) & (j < field.GetLength(1)))
                    {
                        sb = field[i, j] == playerChar ? sb.Append(field[i, j]) : sb.Clear();
                        if (sb.Length == WinSerie)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        bool CheckDiagonalDown(char playerChar, int[] lastDot, char[,] field)
        {
            StringBuilder sb = new StringBuilder();
            int Xmin = lastDot[1] - WinSerie;
            int Xmax = lastDot[1] + WinSerie;
            int Ymin = lastDot[0] - WinSerie;
            int Ymax = lastDot[0] + WinSerie;
            for (int i = Ymin, j = Xmin; (i < Ymax) & (j < Xmax); i++, j++)
            {
                if ((i >= 0) & (i < field.GetLength(0)))
                {
                    if ((j >= 0) & (j < field.GetLength(1)))
                    {
                        sb = field[i, j] == playerChar ? sb.Append(field[i, j]) : sb.Clear();
                        if (sb.Length == WinSerie)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        bool CheckHorizontal(char playerChar, int[] lastDot, char[,] field)
        {
            StringBuilder sb = new StringBuilder();
            int Xmin = lastDot[1] - WinSerie;
            int Xmax = lastDot[1] + WinSerie;
            int Ymin = lastDot[0] - WinSerie;
            int Ymax = lastDot[0] + WinSerie;
            for (int i = Xmin; i < Xmax; i++)
            {
                if ((i >= 0) & (i < field.GetLength(0)))
                {
                    sb = field[lastDot[0], i] == playerChar ? sb.Append(field[lastDot[0], i]) : sb.Clear();
                    if (sb.Length == WinSerie)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        bool CheckVertical(char playerChar, int[] lastDot, char[,] field)
        {
            StringBuilder sb = new StringBuilder();
            int Xmin = lastDot[1] - WinSerie;
            int Xmax = lastDot[1] + WinSerie;
            int Ymin = lastDot[0] - WinSerie;
            int Ymax = lastDot[0] + WinSerie;
            for (int i = Ymin; i < Ymax; i++)
            {
                if ((i >= 0) & (i < field.GetLength(0)))
                {
                    sb = field[i, lastDot[1]] == playerChar ? sb.Append(field[i, lastDot[1]]) : sb.Clear();
                    if (sb.Length == WinSerie)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        bool CheckWin(char playerChar, int[] lastDot, char[,] field)
        {
            return CheckVertical(playerChar, lastDot, field) | CheckHorizontal(playerChar, lastDot, field) | CheckDiagonalDown(playerChar, lastDot, field) | CheckDiagonalUp(playerChar, lastDot, field);
        }
    }
    class Menu
    {
        public string[] Entryes { get; set; }
        int Columns { get; }
        int Rows { get; }


        public void Show()
        {
            foreach (var item in Entryes)
            {
                Console.WriteLine(item);
            }
        }
        public void Show(Menu menu)
        {
            menu.Show();
        }
        string Select(string[] str, int line) => str[line];

        public void PrintWhite(string str, int Y, int X)
        {
            Console.CursorLeft = X;
            Console.CursorTop = Y;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(str);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void PrintBlack(string str, int Y, int X)
        {
            Console.CursorLeft = X;
            Console.CursorTop = Y;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(str);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void Selector(int maxX, int maxY, string[] str, ref string selected, ref int[] YX)
        {
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        PrintBlack(str[YX[0]], YX[0], YX[1]);
                        YX[0] = (YX[0] > 0) ? --YX[0] : YX[0] = maxY - 1;
                        PrintWhite(str[YX[0]], YX[0], YX[1]);
                        break;
                    case ConsoleKey.DownArrow:
                        PrintBlack(str[YX[0]], YX[0], YX[1]);
                        YX[0] = (YX[0] < maxY - 1) ? ++YX[0] : YX[0] = 0;
                        PrintWhite(str[YX[0]], YX[0], YX[1]);
                        break;
                    case ConsoleKey.Enter:
                        selected = Select(str, YX[0] + (YX[1] * Rows));
                        break;
                    case ConsoleKey.Escape:
                        selected = "Exit";
                        break;
                    //case ConsoleKey.LeftArrow:
                    //    YX[1] = (YX[1] > 0) ? --YX[1] : YX[1] = maxX;
                    //    break;
                    //case ConsoleKey.RightArrow:
                    //    YX[1] = (YX[1] < maxX) ? ++YX[1] : YX[1] = 0;
                    //    break;
                    default:
                        break;
                }
            } while ((key.Key != ConsoleKey.Enter) & (key.Key != ConsoleKey.Escape));
        }

    }
    class Programm
    {
        static void Main(string[] args)
        {
            string selected = null;
            int maxX = 0, maxY = 0;
            int[] YX = { 0, 0 };
            Menu Main = new Menu();
            Menu Settings = new Menu();
            Main.Entryes = new string[] { "Один игрок", "Два игрока", "Установки игры", "Exit" };
            Settings.Entryes = new string[] { "Установить размер поля", "Установить рамер выигрышной строки", "Exit" };
            Main.Show();
            Main.PrintWhite(Main.Entryes[YX[0]], YX[0], YX[1]);
            do
            {
                Main.Selector(0, Main.Entryes.Length, Main.Entryes, ref selected, ref YX);
                switch (selected)
                {
                    case "Один игрок":
                        Game crossess = new Game();
                        Console.Clear();
                        crossess.Play(crossess);
                        Console.Clear();
                        Main.Show();
                        Main.PrintWhite(Main.Entryes[YX[0]], YX[0], YX[1]);
                        break;
                    default:
                        break;
                }

            } while (selected != "Exit");




        }
        static public string[,] ColsMenu(string[] str, int cols = 2)
        {
            int row = 0;
            if (str.Length % cols == 0)
            {
                row = str.Length / cols;
            }
            else
            {
                row = (str.Length / cols) + 1;
            }
            string[,] newStr = new string[row, cols];
            int l = 0;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < cols & l < str.Length; j++)
                {
                    newStr[i, j] = str[l];
                    l++;
                }
            }
            return newStr;
        }
        static public void Show(string[,] str, ref int step)
        {
            for (int i = 0; i < str.GetLength(0); i++)
            {
                for (int j = 0; j < str.GetLength(1); j++)
                {
                    if (j == 0)
                    {
                        Print(str[i, j], i, j);
                    }
                    else if (j > 0)
                    {
                        Print(str[i, j], i, j * step);
                    }
                }
            }
        }
        static void Show(string[,] Field)
        {
            Console.Write("╔");
            for (int i = 0; i < Field.GetLength(1) - 1; i++)
            {
                Console.Write("═╦");
            }
            Console.Write("═╗\n");
            for (int i = 0; i < Field.GetLength(0); i++)
            {
                if (i < Field.GetLength(0) - 1)
                {

                    for (int j = 0; j < Field.GetLength(1); j++)
                    {
                        Console.Write("║" + Field[i, j]);
                    }
                    Console.Write("║\n╠");
                    for (int l = 0; l < Field.GetLength(1) - 1; l++)
                    {
                        Console.Write("═╬");
                    }
                    Console.WriteLine("═╣");
                }
                else
                {
                    for (int j = 0; j < Field.GetLength(1); j++)
                    {
                        Console.Write("║" + Field[i, j]);
                    }
                    Console.Write("║\n╚");
                    for (int l = 0; l < Field.GetLength(1) - 1; l++)
                    {
                        Console.Write("═╩");
                    }
                    Console.WriteLine("═╝");
                }
            }
        }
        static public void Selector(string[] str, out string selected, ref int cursorRow, ref int cursorCol)
        {
            Console.CursorVisible = false;
            selected = null;
            Print(str[cursorRow], cursorCol, 0);
            var move = Console.ReadKey(false);
            if (move.Key == ConsoleKey.LeftArrow)
            {
                cursorCol = 0;
            }
            else if (move.Key == ConsoleKey.RightArrow)
            {
                cursorCol = 50;
            }
            else if ((move.Key == ConsoleKey.DownArrow) & (cursorRow < (str.Length - 1) / 2))
            {
                Console.SetCursorPosition(0, cursorRow);
                Console.Write(str[cursorRow]);
                cursorRow++;
                Print(str[cursorRow], cursorCol, 0);
            }
            else if ((move.Key == ConsoleKey.DownArrow) & (cursorRow == (str.Length - 1) / 2))
            {
                Console.SetCursorPosition(0, cursorRow);
                Console.Write(str[cursorRow]);
                cursorRow = 0;
                Print(str[cursorRow], cursorCol, 0);
            }
            else if ((move.Key == ConsoleKey.UpArrow) & (cursorRow > 0))
            {
                Console.SetCursorPosition(0, cursorRow);
                Console.Write(str[cursorRow]);
                cursorRow--;
                Print(str[cursorRow], cursorCol, 0);
            }
            else if ((move.Key == ConsoleKey.UpArrow) & (cursorRow == 0))
            {
                Console.SetCursorPosition(0, cursorRow);
                Console.Write(str[cursorRow]);
                cursorRow = (str.Length - 1) / 2;
                Print(str[cursorRow], cursorCol, 0);
            }
            else if (move.Key == ConsoleKey.Enter)
            {
                selected = str[cursorRow];
            }
            else if (move.Key == ConsoleKey.Escape)
            {
                selected = "Exit";
            }
            else if (move.Key == ConsoleKey.Spacebar)
            {
                selected = str[cursorRow];
            }
        }
        public void Print(string text, int row, int col)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(col, row);
            Console.Write(text);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        static public void Print(string text, int row, int col, ConsoleColor bgColor = ConsoleColor.Black, ConsoleColor fntColor = ConsoleColor.White)
        {
            Console.BackgroundColor = bgColor;
            Console.ForegroundColor = fntColor;
            Console.SetCursorPosition(col, row);
            Console.Write(text);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
