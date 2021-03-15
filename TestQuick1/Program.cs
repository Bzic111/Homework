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

    public class Cell
    {
        char Char { get; set; }
        int Cost { get; set; }
        int[] Coordinates;
        static int MaxY;
        static int MinY;
        static int MaxX;
        static int MinX;

        public Cell(int y,int x)
        {
            Char = ' ';
            Cost = 0;
            Coordinates = new int[]{ y,x};
        }
        public void SetChar(char chr)
        {
            Char = chr;
            Cost = 0;
        }
        public void Raise(char chr, Cell[,] cells)
        {
            for (int i = Coordinates[0]-1; i < Coordinates[0]+1; i++)
            {
                for (int j = Coordinates[1] - 1; j < Coordinates[1] + 1; j++)
                {
                    if (Game.InRange(i,j,cells))
                    {

                    }
                }
            }
            
        }
    }
    class Game
    {
        int SizeX { get; set; } = 5;
        int SizeY { get; set; } = 5;        
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
            Field = GetField();
            IntField = GetIntField();
            FillEmpty(Field);
            FillInts(IntField);
        }
        public void Play(Game game)
        {
            Show();
            Console.CursorVisible = false;
            int X = 0, Y = 0;
            Status = GameStatus.PlayerMove;
            int[] last = { Y, X };
            do
            {
                Selector(this.SizeX, this.SizeY, ref Field, ref IntField, PlayerOneDot, ref X, ref Y);
                last[0] = Y;
                last[1] = X;
                SetCost(PlayerOneDot, last, Field, ref IntField);
                if (CheckWin(PlayerOneDot, last, Field, ref IntField))
                {
                    EndGame(GameStatus.Win, "Player One");
                    Console.ReadKey();
                    goto EndPoint;
                }
                if (Status == GameStatus.Break)
                {
                    goto EndPoint;
                }
                AIMove(this.SizeX, this.SizeY, ref Field, ref IntField, PlayerTwoDot, ref X, ref Y);
                last[0] = Y;
                last[1] = X;
                SetCost(PlayerTwoDot, last, Field, ref IntField);
                
                if (CheckWin(PlayerTwoDot, last, Field, ref IntField))
                {
                    EndGame(GameStatus.Win, "CPU");
                    Console.ReadKey();
                    goto EndPoint;
                }
                ReSetCost(Field, ref IntField);
            } while (Status == GameStatus.PlayerMove | Status == GameStatus.AIMove);
        EndPoint:;
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
        void ExitGame()
        {
            Status = GameStatus.Break;

        }
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
        void Selector(int maxX, int maxY, ref char[,] chr, ref int[,] IntField, char dot, ref int X, ref int Y)
        {
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Spacebar:
                        Console.CursorLeft = X == 0 ? 1 : (X * 2 + 1);
                        Console.CursorTop = Y == 0 ? 1 : (Y * 2 + 1);
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(IntField[Y, X]);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case ConsoleKey.Enter:
                        if (IsEmpty(Y, X, chr))
                        {
                            SetDot(Y, X, ref IntField, ref chr, dot);
                            PrintBlack(chr[Y, X], Y, X);
                            Status = GameStatus.AIMove;
                        }
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
                    if (InRange(i, j, Field))
                    {
                        intField[i, j] += intField[i, j] == 0 ? 0 : 1;
                    }
                }
            }
        }
        bool IsEmpty(int Y, int X, char[,] chr)
        {
            return chr[Y, X] == Empty;
        }
        void AIMove(int maxX, int maxY, ref char[,] str, ref int[,] intField, char playerDot, ref int X, ref int Y)
        {
            int[] YX = FindGoodPoint(ref intField);
            Y = YX[0];
            X = YX[1];
            SetDot(Y,X,ref IntField,ref str,playerDot);
            PrintBlack(str[Y, X], Y, X);
            Status = GameStatus.PlayerMove;
        }
        int[] FindGoodPoint(ref int[,] intField)
        {
            int temp = 0;
            int[] XY = new int[2];
            for (int i = 0; i < intField.GetLength(0); i++)
            {
                for (int j = 0; j < intField.GetLength(1); j++)
                {
                    if (intField[i, j] > temp)
                    {
                        temp = intField[i, j];
                        XY[0] = i;
                        XY[1] = j;
                    }
                }
            }
            return XY;
        }
        void ReSetCost(char[,] field, ref int[,] intField)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i,j]==Empty)
                    {
                        intField[i, j] = 1;
                    }
                }
            }
        }
        void SetCost(char playerChar, int[] lastDot, char[,] field, ref int[,] intField)
        {
            int Xmin = lastDot[1] - WinSerie;
            int Xmax = lastDot[1] + WinSerie;
            int Ymin = lastDot[0] - WinSerie;
            int Ymax = lastDot[0] + WinSerie;
            int counter = 0;
            for (int i = Ymin, j = Xmax; (i <= Ymax) & (j >= Xmin); i++, j--)
            {
                if (InRange(i, j, field))
                {
                    if (field[i, j] == playerChar)
                    {
                        counter++;
                    }
                    if (field[i, j] == Empty & counter > 0)
                    {
                        intField[i, j] += counter;
                        counter = 0;
                    }
                }
            }
            counter = 0;
            for (int i = Ymax, j = Xmax; (i >= Ymin) & (j >= Xmin); i--, j--)
            {
                if (InRange(i, j, field))
                {
                    if (field[i, j] == playerChar)
                    {
                        counter++;
                    }
                    if (field[i, j] == Empty & counter > 0)
                    {
                        intField[i, j] += counter;
                        counter = 0;
                    }
                }
            }
            counter = 0;
            for (int i = Xmax; i >= Xmin; i--)
            {
                if (InRange(lastDot[0], i, field))
                {
                    if (field[lastDot[0], i] == playerChar)
                    {
                        counter++;
                    }
                    if (field[lastDot[0], i] == Empty & counter > 0)
                    {
                        intField[lastDot[0], i] += counter;
                        counter = 0;
                    }
                }
            }
            counter = 0;
            for (int i = Ymax; i >= Ymin; i--)
            {
                if (InRange(i, lastDot[1], field))
                {
                    if (field[i, lastDot[1]] == playerChar)
                    {
                        counter++;
                    }
                    if (field[i, lastDot[1]] == Empty & counter > 0)
                    {
                        intField[i, lastDot[1]] += counter;
                        counter = 0;
                    }
                }
            }
        }
        bool CheckDiagonalUp(char playerChar, int[] lastDot, char[,] field, ref int[,] intField)
        {
            int Xmin = lastDot[1] - WinSerie;
            int Xmax = lastDot[1] + WinSerie;
            int Ymin = lastDot[0] - WinSerie;
            int Ymax = lastDot[0] + WinSerie;
            int counter = 0;
            for (int i = Ymax, j = Xmin; (i >= Ymin) & (j <= Xmax); i--, j++)
            {
                if (InRange(i, j, field))
                {
                    if (field[i, j] == playerChar)
                    {
                        counter++;
                    }
                    if (counter == WinSerie)
                    {
                        return true;
                    }
                    if (field[i, j] == Empty & counter > 0)
                    {
                        intField[i, j]+= counter;
                        counter = 0;
                    }

                }
            }
            return false;
        }
        bool CheckDiagonalDown(char playerChar, int[] lastDot, char[,] field, ref int[,] intField)
        {
            int Xmin = lastDot[1] - WinSerie;
            int Xmax = lastDot[1] + WinSerie;
            int Ymin = lastDot[0] - WinSerie;
            int Ymax = lastDot[0] + WinSerie;
            int counter = 0;
            for (int i = Ymin, j = Xmin; (i <= Ymax) & (j <= Xmax); i++, j++)
            {
                if (InRange(i, j, field))
                {
                    if (field[i, j] == playerChar)
                    {
                        counter++;
                    }
                    if (counter == WinSerie)
                    {
                        return true;
                    }
                    if (field[i, j] == Empty & counter > 0)
                    {
                        intField[i, j]+= counter;
                        counter = 0;
                    }
                }
            }
            return false;
        }
        bool CheckHorizontal(char playerChar, int[] lastDot, char[,] field, ref int[,] intField)
        {
            int Xmin = lastDot[1] - WinSerie;
            int Xmax = lastDot[1] + WinSerie;
            int counter = 0;
            for (int i = Xmin; i <= Xmax; i++)
            {
                if (InRange(lastDot[0], i, field))
                {
                    if (field[lastDot[0], i] == playerChar)
                    {
                        counter++;
                    }
                    if (counter == WinSerie)
                    {
                        return true;
                    }
                    if (field[lastDot[0], i] == Empty & counter > 0)
                    {
                        intField[lastDot[0], i] += counter;
                        counter = 0;
                    }
                }
            }
            return false;
        }
        bool CheckVertical(char playerChar, int[] lastDot, char[,] field, ref int[,] intField)
        {
            int Ymin = lastDot[0] - WinSerie;
            int Ymax = lastDot[0] + WinSerie;
            int counter = 0;
            for (int i = Ymin; i <= Ymax; i++)
            {
                if (InRange(i, lastDot[1], field))
                {
                    if (field[i, lastDot[1]] == playerChar)
                    {
                        counter++;
                    }
                    if (counter == WinSerie)
                    {
                        return true;
                    }
                    if (field[i, lastDot[1]] == Empty & counter > 0)
                    {
                        intField[i, lastDot[1]] += counter;
                        counter = 0;
                    }
                }
            }
            return false;
        }
        bool CheckWin(char playerChar, int[] lastDot, char[,] field, ref int[,] intField)
        {
            bool ver = CheckVertical(playerChar, lastDot, field, ref intField);
            bool hor = CheckHorizontal(playerChar, lastDot, field, ref intField);
            bool up = CheckDiagonalUp(playerChar, lastDot, field, ref intField);
            bool dn = CheckDiagonalDown(playerChar, lastDot, field, ref intField);
            return ver | hor | up | dn;
        }
        public bool InRange(int Y, int X, char[,] arr)
        {
            return (Y >= 0 & Y < arr.GetLength(0)) & (X >= 0 & X < arr.GetLength(1));
        }
        static public bool InRange(int Y, int X, Cell[,] arr)
        {
            return (Y >= 0 & Y < arr.GetLength(0)) & (X >= 0 & X < arr.GetLength(1));
        }
        public bool InRange(int Y, int X, int lengthA, int lengthB)
        {
            return (Y >= 0 & Y < lengthA) & (X >= 0 & X < lengthB);
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
