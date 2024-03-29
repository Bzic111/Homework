﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson07
{
    
    public class HomeWork : MenuSpace.Work
    {
        enum GameStatus
        {
            Win,
            Draw,
            Break,
            Play
        }
        public new MenuSpace.Menu.Runner[] AllRuns { get; }
        string[] Names { get; } = { "Один игрок", "Два игрока", "Установить размер поля", "Установить рамер выигрышной строки"};
        public override string[] GetNames() { return this.Names; }
        public override MenuSpace.Menu.Runner[] GetRunners()
        {
            return AllRuns;
        }
        public HomeWork()
        {
            AllRuns = new MenuSpace.Menu.Runner[]
            {
                PlayOne,
                PlayTwo,
                SetSize,
                SetWinSerie
            };
        }

        int SizeX;
        int SizeY;
        char[,] Field;
        int[,] IntField;
        int WinSerie { get; set; } = 3;
        char[] Symbols { get; } = { ' ', 'X', 'O' };
        GameStatus Status { get; set; } = GameStatus.Play;
        int MoveCounter { get; set; }
        void SetSize()
        {
            Console.Clear();
            Console.WriteLine("Set Size of field");
            if (Int32.TryParse(Console.ReadLine(), out int val))
            {
                SizeY = val;
                SizeX = val;
            }
            else
            {
                Console.WriteLine("bad input");

                Console.ReadLine();
            }
            Console.Clear();
        }
        void SetWinSerie()
        {
            Console.Clear();
            Console.WriteLine("Set Line Length for win");
            if (Int32.TryParse(Console.ReadLine(), out int val))
            {
                WinSerie = val;
            }
            else
            {
                Console.WriteLine("bad input");

                Console.ReadLine();
            }
            Console.Clear();
        }
        void PlayOne()
        {
            Console.Clear();
            char PlayerOneDot = Symbols[1];
            char PlayerTwoDot = Symbols[2];
            if (SizeX < 3)
            {
                SizeX = 3;
                SizeY = 3;
            }
            if (WinSerie > SizeX)
            {
                WinSerie = SizeX;
            }

            MoveCounter = SizeX * SizeY;
            Field = GetField();
            IntField = GetIntField();
            FillEmpty(Field);
            FillInts(IntField);
            Show();
            Console.CursorVisible = false;
            int X = 0, Y = 0;
            int aiY = 0, aiX = 0;
            Status = GameStatus.Play;
            int[] lastOne = { Y, X };
            int[] lastTwo = { aiY, aiX };
            int[,] Ofield = new int[SizeY, SizeX];
            FillInts(Ofield);
            int moveCounter = MoveCounter;
            PrintGreen($"Линия для победы должна быть {WinSerie}".PadRight(SizeX * 2 + 1), 0, SizeY * 2 + 3);
            do
            {
                Selector(SizeX, SizeY, ref Field, ref IntField, ref Ofield, PlayerOneDot, ref X, ref Y);
                lastOne[0] = Y;
                lastOne[1] = X;
                if (WinCheck(PlayerOneDot, lastOne, Field, ref IntField))
                {
                    EndGame(GameStatus.Win, "Player One");
                    Console.ReadKey();
                    goto EndPoint;
                }
                moveCounter--;
                if (moveCounter == 0)
                {
                    Status = GameStatus.Draw;
                    goto EndPoint;
                }
                if (Status == GameStatus.Break)
                {
                    goto EndPoint;
                }
                ReSetCost(Field, ref IntField);
                ReSetCost(Field, ref Ofield);
                SetCost(PlayerOneDot, Field, ref IntField);
                SetCost(PlayerTwoDot, Field, ref Ofield);
                AIMove(SizeX, SizeY, ref Field, ref IntField, ref Ofield, PlayerTwoDot, ref aiX, ref aiY);
                lastTwo[0] = aiY;
                lastTwo[1] = aiX;
                if (WinCheck(PlayerTwoDot, lastTwo, Field, ref Ofield))
                {
                    EndGame(GameStatus.Win, "CPU");
                    Console.ReadKey();
                    goto EndPoint;
                }
                moveCounter--;
                if (moveCounter == 0)
                {
                    Status = GameStatus.Draw;
                    goto EndPoint;
                }
            EndPoint:;
            } while (Status == GameStatus.Play);
        }
        void PlayTwo()
        {
            Console.Clear();
            char PlayerOneDot = Symbols[1];
            char PlayerTwoDot = Symbols[2];
            if (SizeX < 3)
            {
                SizeX = 3;
                SizeY = 3;
            }
            if (WinSerie <= 1)
            {
                WinSerie = SizeX;
            }
            MoveCounter = SizeX * SizeY;
            Field = GetField();
            IntField = GetIntField();
            FillEmpty(Field);
            FillInts(IntField);
            Show();
            Console.CursorVisible = false;
            int X = 0, Y = 0;
            int Y2 = 0, X2 = 0;
            Status = GameStatus.Play;
            int[] lastOne = { Y, X };
            int[] lastTwo = { Y2, X2 };
            int[,] Ofield = new int[SizeY, SizeX];
            int moveCounter = MoveCounter;
            PrintGreen($"Линия для победы должна быть {WinSerie}".PadRight(SizeX * 2 + 1), 0, SizeY * 2 + 4);
            do
            {
                PrintGreen("Ход первого игрока".PadRight(SizeX * 2 + 1), 0, SizeY * 2 + 3);
                Selector(SizeX, SizeY, ref Field, ref IntField, ref Ofield, PlayerOneDot, ref X, ref Y);
                lastOne[0] = Y;
                lastOne[1] = X;
                if (WinCheck(PlayerOneDot, lastOne, Field, ref IntField))
                {
                    EndGame(GameStatus.Win, "Player One");
                    goto EndPoint;
                }
                moveCounter--;
                if (moveCounter == 0)
                {
                    Status = GameStatus.Draw;
                    goto EndPoint;
                }
                if (Status == GameStatus.Break)
                {
                    goto EndPoint;
                }
                PrintRed("Ход второго игрока".PadRight(SizeX * 2 + 1), 0, SizeY * 2 + 3);
                Selector(SizeX, SizeY, ref Field, ref Ofield, ref IntField, PlayerTwoDot, ref X2, ref Y2);
                lastTwo[0] = Y2;
                lastTwo[1] = X2;
                if (WinCheck(PlayerTwoDot, lastTwo, Field, ref Ofield))
                {
                    EndGame(GameStatus.Win, "Player Two");
                    goto EndPoint;
                }
                moveCounter--;
                if (moveCounter == 0)
                {
                    Status = GameStatus.Draw;
                    goto EndPoint;
                }
            EndPoint:;
            } while (Status == GameStatus.Play);
        }
        char[,] GetField() => new char[SizeY, SizeX];
        int[,] GetIntField() => new int[SizeY, SizeX];
        void FillEmpty(char[,] str)
        {
            for (int i = 0; i < str.GetLength(0); i++)
            {
                for (int j = 0; j < str.GetLength(1); j++)
                {
                    str[i, j] = Symbols[0];
                }
            }
        }
        void FillInts(int[,] field)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    field[i, j] = 0;
                }
            }
        }
        void Selector(int maxX, int maxY, ref char[,] chr, ref int[,] IntField, ref int[,] secondField, char dot, ref int X, ref int Y)
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
                        if (chr[Y, X] == Symbols[0])
                        {
                            SetDot(Y, X, ref IntField, ref secondField, ref chr, dot);
                            PrintBlack(chr[Y, X], Y, X);
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
            } while (key.Key != ConsoleKey.Enter & key.Key != ConsoleKey.Escape);
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
        void PrintRed(string str, int left, int top)
        {
            Console.CursorLeft = left;
            Console.CursorTop = top;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(str);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        void PrintGreen(string str, int left, int top)
        {
            Console.CursorLeft = left;
            Console.CursorTop = top;
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(str);
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
        void ExitGame()
        {
            Status = GameStatus.Break;
            Console.Clear();
            Console.WriteLine("Press Any Key to return in Menu.");
        }
        bool EndGame(GameStatus type, string player)
        {
            switch (type)
            {
                case GameStatus.Win:
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"Congratulation!!! Player {player} WIN!!!");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadKey(false);
                    ExitGame();
                    break;
                case GameStatus.Draw:
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("It is DRAW.");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadKey(false);
                    ExitGame();
                    break;
                case GameStatus.Break:
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Exit game without ending, progress not saved.");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadKey(false);
                    ExitGame();
                    break;
                default:
                    break;
            }
            return false;
        }
        void SetDot(int Y, int X, ref int[,] intField, ref int[,] SecondField, ref char[,] Field, char playerDot)
        {
            intField[Y, X] = 0;
            SecondField[Y, X] = 0;
            Field[Y, X] = playerDot;
        }
        void AIMove(int maxX, int maxY, ref char[,] str, ref int[,] intField, ref int[,] SecondField, char playerDot, ref int X, ref int Y)
        {
            int[] plYX = FindGoodPoint(intField, out _);
            int[] aiYX = FindGoodPoint(SecondField, out int valAi);
            if (valAi >= 3)
            {
                Y = aiYX[0];
                X = aiYX[1];
                SetDot(Y, X, ref SecondField, ref intField, ref str, playerDot);
                PrintBlack(str[Y, X], Y, X);
                Status = GameStatus.Play;
            }
            else
            {
                Y = plYX[0];
                X = plYX[1];
                SetDot(Y, X, ref SecondField, ref intField, ref str, playerDot);
                PrintBlack(str[Y, X], Y, X);
                Status = GameStatus.Play;
            }
        }
        int[] FindGoodPoint(int[,] intField, out int temp)
        {
            temp = 0;
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
            if (temp == 0)
            {
                for (int i = 0; i < intField.GetLength(0); i++)
                {
                    for (int j = 0; j < intField.GetLength(1); j++)
                    {
                        if (intField[i, j] >= temp)
                        {
                            temp = intField[i, j];
                            XY[0] = i;
                            XY[1] = j;
                        }
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
                    if (field[i, j] == Symbols[0])
                    {
                        intField[i, j] = 0;
                    }
                }
            }
        }
        void SetCost(char playerChar, char[,] field, ref int[,] intField)
        {
            int[] Dot = new int[2];
            for (int fieldY = 0; fieldY < field.GetLength(0); fieldY++)
            {
                for (int fieldX = 0; fieldX < field.GetLength(1); fieldX++)
                {
                    if (field[fieldY, fieldX] == Symbols[0])
                    {
                        Dot[0] = fieldY; Dot[1] = fieldX;
                        int NW = GetCostNW(Dot, field, intField, playerChar);
                        int N = GetCostN(Dot, field, intField, playerChar);
                        int NE = GetCostNE(Dot, field, intField, playerChar);
                        int E = GetCostE(Dot, field, intField, playerChar);
                        int SE = GetCostSE(Dot, field, intField, playerChar);
                        int S = GetCostS(Dot, field, intField, playerChar);
                        int SW = GetCostSW(Dot, field, intField, playerChar);
                        int W = GetCostW(Dot, field, intField, playerChar);
                        int[] NWSE = { N + S, W + E, NW + SE, NE + SW };
                        int result = 0;
                        for (int i = 0; i < NWSE.Length; i++)
                        {
                            if (NWSE[i] > result)
                            {
                                result = NWSE[i];
                            }
                        }
                        intField[fieldY, fieldX] = result;
                    }
                }
            }
        }
        public bool InRange(int Y, int X, char[,] arr)
        {
            return (Y >= 0 & Y < arr.GetLength(0)) & (X >= 0 & X < arr.GetLength(1));
        }
        int GetCostNW(int[] Dot, char[,] field, int[,] intField, char playerChar)
        {
            int result = 0;
            int counter = 0;
            for (int i = Dot[0] - 1, j = Dot[1] - 1; (i >= Dot[0] - WinSerie) & (j >= Dot[1] - WinSerie); i--, j--)
            {
                if (InRange(i, j, field))
                {
                    if (field[i, j] == playerChar)
                    {
                        counter++;
                    }
                    else
                    {
                        result = counter > intField[Dot[0], Dot[1]] ? counter : intField[Dot[0], Dot[1]];
                        break;
                    }
                }
                else
                {
                    result = counter > intField[Dot[0], Dot[1]] ? counter : intField[Dot[0], Dot[1]];
                }
            }
            return result;
        }
        int GetCostN(int[] Dot, char[,] field, int[,] intField, char playerChar)
        {
            int result = 0;
            int counter = 0;
            for (int i = Dot[0] - 1; i >= Dot[0] - WinSerie; i--)
            {
                if (InRange(i, Dot[1], field))
                {
                    if (field[i, Dot[1]] == playerChar)
                    {
                        counter++;
                    }
                    else
                    {
                        result = counter > intField[Dot[0], Dot[1]] ? counter : intField[Dot[0], Dot[1]];
                        break;
                    }
                }
                else
                {
                    result = counter > intField[Dot[0], Dot[1]] ? counter : intField[Dot[0], Dot[1]];
                }
            }
            return result;
        }
        int GetCostNE(int[] Dot, char[,] field, int[,] intField, char playerChar)
        {
            int result = 0;
            int counter = 0;
            for (int i = Dot[0] - 1, j = Dot[1] + 1; (i >= Dot[0] - WinSerie) & (j <= Dot[1] + WinSerie); i--, j++)
            {
                if (InRange(i, j, field))
                {
                    if (field[i, j] == playerChar)
                    {
                        counter++;
                    }
                    else
                    {
                        result = counter > intField[Dot[0], Dot[1]] ? counter : intField[Dot[0], Dot[1]];
                        break;
                    }

                }
                else
                {
                    result = counter > intField[Dot[0], Dot[1]] ? counter : intField[Dot[0], Dot[1]];
                }
            }
            return result;
        }
        int GetCostE(int[] Dot, char[,] field, int[,] intField, char playerChar)
        {
            int result = 0;
            int counter = 0;
            for (int i = Dot[1] + 1; i <= Dot[1] + WinSerie; i++)
            {
                if (InRange(Dot[0], i, field))
                {
                    if (field[Dot[0], i] == playerChar)
                    {
                        counter++;
                    }
                    else
                    {
                        result = counter > intField[Dot[0], Dot[1]] ? counter : intField[Dot[0], Dot[1]];
                        break;
                    }
                }
                else
                {
                    result = counter > intField[Dot[0], Dot[1]] ? counter : intField[Dot[0], Dot[1]];
                }
            }
            return result;
        }
        int GetCostSE(int[] Dot, char[,] field, int[,] intField, char playerChar)
        {
            int result = 0;
            int counter = 0;
            for (int i = Dot[0] + 1, j = Dot[1] + 1; (i <= Dot[0] + WinSerie) & (j <= Dot[1] + WinSerie); i++, j++)
            {
                if (InRange(i, j, field))
                {
                    if (field[i, j] == playerChar)
                    {
                        counter++;
                    }
                    else
                    {
                        result = counter > intField[Dot[0], Dot[1]] ? counter : intField[Dot[0], Dot[1]];
                        break;
                    }
                }
                else
                {
                    result = counter > intField[Dot[0], Dot[1]] ? counter : intField[Dot[0], Dot[1]];
                }
            }
            return result;
        }
        int GetCostS(int[] Dot, char[,] field, int[,] intField, char playerChar)
        {
            int result = 0;
            int counter = 0;
            for (int i = Dot[0] + 1; i <= Dot[0] + WinSerie; i++)
            {
                if (InRange(i, Dot[1], field))
                {
                    if (field[i, Dot[1]] == playerChar)
                    {
                        counter++;
                    }
                    else
                    {
                        result = counter > intField[Dot[0], Dot[1]] ? counter : intField[Dot[0], Dot[1]];
                        break;
                    }
                }
                else
                {
                    result = counter > intField[Dot[0], Dot[1]] ? counter : intField[Dot[0], Dot[1]];
                }
            }
            return result;
        }
        int GetCostSW(int[] Dot, char[,] field, int[,] intField, char playerChar)
        {
            int result = 0;
            int counter = 0;
            for (int i = Dot[0] + 1, j = Dot[1] - 1; (i <= Dot[0] + WinSerie) & (j >= Dot[1] - WinSerie); i++, j--)
            {
                if (InRange(i, j, field))
                {
                    if (field[i, j] == playerChar)
                    {
                        counter++;
                    }
                    else
                    {
                        result = counter > intField[Dot[0], Dot[1]] ? counter : intField[Dot[0], Dot[1]];
                        break;
                    }
                }
                else
                {
                    result = counter > intField[Dot[0], Dot[1]] ? counter : intField[Dot[0], Dot[1]];
                }
            }
            return result;
        }
        int GetCostW(int[] Dot, char[,] field, int[,] intField, char playerChar)
        {
            int result = 0;
            int counter = 0;
            for (int i = Dot[1] - 1; i >= Dot[1] - WinSerie; i--)
            {
                if (InRange(Dot[0], i, field))
                {
                    if (field[Dot[0], i] == playerChar)
                    {
                        counter++;
                    }
                    else
                    {
                        result = counter > intField[Dot[0], Dot[1]] ? counter : intField[Dot[0], Dot[1]];
                        break;
                    }
                }
                else
                {
                    result = counter > intField[Dot[0], Dot[1]] ? counter : intField[Dot[0], Dot[1]];
                }
            }
            return result;
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
                        if (counter == WinSerie)
                        {
                            return true;
                        }
                    }
                    else if (field[i, j] != playerChar)
                    {
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
                        if (counter == WinSerie)
                        {
                            return true;
                        }
                    }
                    else if (field[i, j] != playerChar)
                    {
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
                        if (counter == WinSerie)
                        {
                            return true;
                        }
                    }
                    else if (field[lastDot[0], i] != playerChar)
                    {
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
                        if (counter == WinSerie)
                        {
                            return true;
                        }
                    }
                    else if (field[i, lastDot[1]] != playerChar)
                    {
                        counter = 0;
                    }
                }
            }
            return false;
        }
        bool WinCheck(char playerChar, int[] lastDot, char[,] field, ref int[,] intField)
        {
            bool ver = CheckVertical(playerChar, lastDot, field, ref intField);
            bool hor = CheckHorizontal(playerChar, lastDot, field, ref intField);
            bool up = CheckDiagonalUp(playerChar, lastDot, field, ref intField);
            bool dn = CheckDiagonalDown(playerChar, lastDot, field, ref intField);
            return ver | hor | up | dn;
        }
    }
}
