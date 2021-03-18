using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace TestQuick1
{
    class Preset
    {
        public int line = 0;
        public int lineUp = 1;
        public int lineDown = 21;
        public int currentLine = 1;
        public int columnLength = 43;
        public int index = 0;
        public int[] Column = { 3, 61 };
        public Preset()
        {

        }
    }

    class Programm
    {
        static void Main(string[] args)
        {
            Preset pr = new Preset();
            Process[] ListOfProcesses = Process.GetProcesses();
            List<Process[]> lister = FrameList(ListOfProcesses);
            int page = 0;
            bool exit = false;
            string PageSign = $"╡┃Страница {page + 1} из {lister.Count}┃╞";
            ShowFrame();
            FillFrame(lister, page);
            do
            {
                Selector(lister, ref page, ref pr, exit);

                //switch (Console.ReadKey(false).Key)
                //{
                //    case ConsoleKey.PageDown:
                //        page++;
                //        if (page == lister.Count)
                //        {
                //            page = 0;
                //        }

                //        break;
                //    case ConsoleKey.PageUp:
                //        page--;
                //        if (page < 0)
                //        {
                //            page = lister.Count - 1;
                //        }
                //        break;
                //    case ConsoleKey.Escape:
                //        exit = true;
                //        break;
                //    default:
                //        break;
                //}
                //FillFrame(lister, page);

            } while (!exit);



        }

        static void ShowProcesses()
        {
            Process[] ListOfProcesses = Process.GetProcesses();
            List<Process[]> lister = FrameList(ListOfProcesses);
        }
        static void ShowFrame()
        {
            string lineUp = "╔".PadRight(48, '═') + "╤".PadRight(11, '═') + "╦".PadRight(48, '═') + "╤".PadRight(11, '═') + "╗";
            string border = "║".PadRight(48, ' ') + "│".PadRight(11, ' ') + "║".PadRight(48, ' ') + "│".PadRight(11, ' ') + "║";
            string lineDown = "╚".PadRight(48, '═') + "╧".PadRight(11, '═') + "╩".PadRight(48, '═') + "╧".PadRight(11, '═') + "╝";
            Console.WriteLine(lineUp);
            for (int i = 0; i < 21; i++)
            {
                Console.WriteLine(border);
            }
            Console.WriteLine(lineDown);
        }
        static void FillFrame(List<Process[]> lst, int page)
        {
            int upper = 1;
            int FFStart = 3;
            int SFStart = 61;
            int FFBorder = 50;
            int SFBorder = 109;
            int linerLeft = 0;
            int linerRight = 0;

            for (int j = 0; j < lst[page].Length; j++)
            {
                if (j < 20)
                {
                    Console.SetCursorPosition(FFStart, upper + linerLeft);
                    Console.Write(lst[page][j].ProcessName.PadRight(44).Remove(43));
                    Console.CursorLeft = FFBorder;
                    Console.Write(lst[page][j].Id.ToString().PadLeft(8));
                    linerLeft++;
                    if (j == lst[page].Length - 1 & lst[page].Length < 20)
                    {
                        for (int i = 0; i < 20 - lst[page].Length; i++)
                        {
                            Console.SetCursorPosition(FFStart, upper + linerLeft);
                            Console.Write(" ".PadRight(44, ' ').Remove(43));
                            Console.SetCursorPosition(FFBorder, upper + linerLeft);
                            Console.Write(" ".PadLeft(8, ' '));
                            linerLeft++;
                        }
                        for (int i = 0; i < 20; i++)
                        {
                            Console.SetCursorPosition(SFStart, upper + linerRight);
                            Console.Write(" ".PadRight(44, ' ').Remove(43));
                            Console.SetCursorPosition(SFBorder, upper + linerRight);
                            Console.Write(" ".PadLeft(8, ' '));
                            linerRight++;
                        }
                    }
                }
                else
                {
                    Console.SetCursorPosition(SFStart, upper + linerRight);
                    Console.Write(lst[page][j].ProcessName.PadRight(44).Remove(43));
                    Console.SetCursorPosition(SFBorder, upper + linerRight);
                    Console.Write(lst[page][j].Id.ToString().PadLeft(8));
                    linerRight++;
                    if (lst[page].Length < 40)
                    {
                        for (int i = 0; i < 40 - lst[page].Length; i++)
                        {

                            Console.SetCursorPosition(SFStart, upper + linerRight);
                            Console.Write(" ".PadRight(44, ' ').Remove(43));
                            Console.SetCursorPosition(SFBorder, upper + linerRight);
                            Console.Write(" ".PadLeft(8, ' '));
                            linerRight++;
                        }
                    }
                }
            }
        }
        static List<Process[]> FrameList(Process[] listOfProcesses)
        {
            List<Process[]> FrameList = new List<Process[]>();
            for (int i = 0; i < listOfProcesses.Length;)
            {
                if (FrameList.Count == listOfProcesses.Length / 40)
                {
                    FrameList.Add(new Process[listOfProcesses.Length % 40]);
                    for (int j = 0; j < 40 & i < listOfProcesses.Length; j++, i++)
                    {
                        FrameList[^1][j] = listOfProcesses[i];
                    }
                }
                else
                {
                    FrameList.Add(new Process[40]);

                    for (int j = 0; j < 40 & i < listOfProcesses.Length; j++, i++)
                    {
                        FrameList[^1][j] = listOfProcesses[i];
                    }
                }

            }
            return FrameList;
        }

        static void Selector(List<Process[]> lst, ref int page, ref Preset pr, bool exit = false)
        {

            Console.CursorVisible = false;
            Console.SetCursorPosition(pr.index, pr.lineUp);
            string PageSign = $"╡Страница {page + 1} из {lst.Count}╞";
            PrintGreen(PageSign, 5, 0);
            ConsoleKeyInfo key = Console.ReadKey();
            PrintBlack(lst[page][pr.line].ProcessName.PadRight(pr.columnLength + 1, ' ').Remove(pr.columnLength), pr.Column[pr.index], pr.currentLine);
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    if (lst[page].Length > 20 & pr.index == 0)
                    {
                        pr.currentLine--;
                        if (pr.currentLine < pr.lineUp)
                        {
                            pr.currentLine = pr.lineDown;
                            pr.line += 19;
                        }
                        else
                        {
                            pr.line--;
                        }
                    }
                    else if (lst[page].Length < 20)
                    {
                        pr.currentLine--;
                        if (pr.currentLine< pr.lineUp)
                        {
                            pr.currentLine = lst[page].Length - 1;
                            pr.line = lst[page].Length - 1;
                        }
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (lst[page].Length > 20)
                    {
                        pr.index--;
                        if (pr.index < 0)
                        {
                            pr.index = pr.Column.Length - 1;
                            pr.line += 20;
                        }
                        else
                        {
                            pr.line -= 20;
                        }
                    }
                    else if (lst[page].Length > 20 & lst[page].Length < 40)
                    {
                        pr.index--;
                        if (pr.index < 0)
                        {
                            pr.index = pr.Column.Length - 1;
                            pr.line += 20;
                            if (pr.line > lst[page].Length - 1)
                            {
                                pr.line = lst[page].Length - 1;
                            }
                        }
                        else
                        {
                            pr.line -= 20;
                        }
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (lst[page].Length > 20)
                    {
                        pr.index++;
                        if (pr.index == pr.Column.Length)
                        {
                            pr.index = 0;
                            pr.line -= 20;
                        }
                        else
                        {
                            pr.line += 20;
                        }
                    }
                    else if (lst[page].Length > 20 & lst[page].Length < 40)
                    {
                        pr.index++;
                        if (pr.index == pr.Column.Length - 1)
                        {
                            pr.line = lst[page].Length - 1;
                        }
                        else if (pr.index == pr.Column.Length)
                        {
                            pr.index = 0;
                            pr.line -= 20;
                        }
                    }
                    break;
                case ConsoleKey.DownArrow:
                    pr.currentLine++;
                    if (pr.currentLine == lst[page].Length & pr.currentLine < pr.lineDown)
                    {
                        pr.currentLine = pr.lineUp;
                        pr.line = 0;
                    }
                    else if (pr.currentLine > pr.lineDown)
                    {
                        pr.currentLine = pr.lineUp;
                        pr.line -= 19;
                    }
                    else
                    {
                        pr.line++;
                    }
                    break;
                case ConsoleKey.PageUp:
                    page--;
                    if (page < 0)
                    {
                        page = lst.Count - 1;
                        pr.line = 0;
                        pr.index = 0;
                        pr.currentLine = pr.lineUp;
                    }
                    Console.SetCursorPosition(5, 0);
                    Console.Write(PageSign);
                    FillFrame(lst, page);
                    PrintWhite(lst[page][pr.line].ProcessName.PadRight(pr.columnLength + 1, ' ').Remove(pr.columnLength), pr.Column[pr.index], pr.currentLine);
                    break;
                case ConsoleKey.PageDown:
                    page++;
                    if (page == lst.Count)
                    {
                        page = 0;
                        pr.line = 0;
                        pr.index = 0;
                        pr.currentLine = pr.lineUp;
                    }
                    Console.SetCursorPosition(5, 0);
                    Console.Write(PageSign);
                    FillFrame(lst, page);
                    PrintWhite(lst[page][pr.line].ProcessName.PadRight(pr.columnLength + 1, ' ').Remove(pr.columnLength), pr.Column[pr.index], pr.currentLine);
                    break;
                case ConsoleKey.Tab:
                    break;
                case ConsoleKey.Enter:
                    break;
                case ConsoleKey.Escape:
                    exit = true;
                    break;
                case ConsoleKey.Applications:
                    break;
                default: break;
            }
            PrintWhite(lst[page][pr.line].ProcessName.PadRight(pr.columnLength + 1, ' ').Remove(pr.columnLength), pr.Column[pr.index], pr.currentLine);
        }
        static void PrintWhite(string str, int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(str);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void PrintBlack(string str, int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(str);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void PrintRed(string str, int left, int top)
        {
            Console.CursorLeft = left;
            Console.CursorTop = top;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(str);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void PrintGreen(string str, int left, int top)
        {
            Console.CursorLeft = left;
            Console.CursorTop = top;
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(str);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
