using MenuSpace;
using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Lesson08
{
    public class HomeWork : MenuSpace.Work
    {
        public new Menu.Runner[] AllRuns { get; }
        string[] Names { get; } = { "Менеджер задач", "Запуск метода из другой библиотеки классов." };
        public override string[] GetNames()
        {
            return Names;
        }
        public override Menu.Runner[] GetRunners()
        {
            return AllRuns;
        }
        public HomeWork()
        {
            AllRuns = new Menu.Runner[]
            {
                Start,
                StartProcess
            };
        }
        void StartProcess()
        {
            ProcessStartInfo procInfo = new ProcessStartInfo();
            procInfo.WindowStyle = ProcessWindowStyle.Normal;
            string path = @"C:\Users\bzic1\Desktop\GBCsharpProject\Homework\HomeworkLibrary\";
            procInfo.FileName = path + "HelloLibRunner.exe";
            Console.Clear();
            Process secondaryProg = Process.Start(procInfo); //C:\Users\bzic1\Desktop\GBCsharpProject\Homework\HomeworkLibrary\
            secondaryProg.WaitForExit();
            Console.ReadKey();
            Console.Clear();
        }
        void Start()
        {
            int currentLine = 1;
            int index = 0;
            int currentColumn = 3;
            int page = 0;
            bool exit = false;
            Process[] ListOfProcesses = Process.GetProcesses();
            List<Process[]> lister = FrameList(ListOfProcesses);
            ShowFrame();
            FillFrame(lister, page);
            do
            {
                Selector(ref lister, ref page, ref index, ref currentLine, ref currentColumn, ref exit);
            } while (!exit);
        }
        void Selector(ref List<Process[]> lst, ref int page, ref int index, ref int cursorV, ref int cursorH, ref bool exit)
        {
            int liner = 1;
            int LeftColumn = 3;
            int RightColumn = 61;
            Console.CursorVisible = false;
            string PageSign = $"╡Страница {page + 1} из {lst.Count}╞";
            PrintGreen(PageSign, 5, 0);
            ConsoleKeyInfo key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    if (index > 19)
                    {
                        cursorH = RightColumn;
                        cursorV = index - 20;
                    }
                    else
                    {
                        cursorH = LeftColumn;
                        cursorV = index;
                    }
                    PrintBlack(lst[page][index].ProcessName.PadRight(44, ' ').Remove(43), cursorH, cursorV + liner);
                    if (index > 0)
                    {
                        index--;
                    }
                    else if (index == 0)
                    {
                        index = lst[page].Length - 1;
                    }
                    if (index > 19)
                    {
                        cursorH = RightColumn;
                        cursorV = index - 20;
                    }
                    else
                    {
                        cursorH = LeftColumn;
                        cursorV = index;
                    }
                    PrintWhite(lst[page][index].ProcessName.PadRight(44, ' ').Remove(43), cursorH, cursorV + liner);
                    break;
                case ConsoleKey.DownArrow:
                    if (index > 19)
                    {
                        cursorH = RightColumn;
                        cursorV = index - 20;
                    }
                    else
                    {
                        cursorH = LeftColumn;
                        cursorV = index;
                    }
                    PrintBlack(lst[page][index].ProcessName.PadRight(44, ' ').Remove(43), cursorH, cursorV + liner);
                    if (index < lst[page].Length - 1)
                    {
                        index++;
                    }
                    else if (index == lst[page].Length - 1)
                    {
                        index = 0;
                    }
                    if (index > 19)
                    {
                        cursorH = RightColumn;
                        cursorV = index - 20;
                    }
                    else
                    {
                        cursorH = LeftColumn;
                        cursorV = index;
                    }
                    PrintWhite(lst[page][index].ProcessName.PadRight(44, ' ').Remove(43), cursorH, cursorV + liner);
                    break;
                case ConsoleKey.LeftArrow:
                    if (index > 19)
                    {
                        cursorH = RightColumn;
                        cursorV = index - 20;
                    }
                    else
                    {
                        cursorH = LeftColumn;
                        cursorV = index;
                    }
                    PrintBlack(lst[page][index].ProcessName.PadRight(44, ' ').Remove(43), cursorH, cursorV + liner);
                    if (index - 20 >= 0)
                    {
                        index -= 20;
                    }
                    if (index > 19)
                    {
                        cursorH = RightColumn;
                        cursorV = index - 20;
                    }
                    else
                    {
                        cursorH = LeftColumn;
                        cursorV = index;
                    }
                    PrintWhite(lst[page][index].ProcessName.PadRight(44, ' ').Remove(43), cursorH, cursorV + liner);
                    break;
                case ConsoleKey.RightArrow:
                    if (index > 19)
                    {
                        cursorH = RightColumn;
                        cursorV = index - 20;
                    }
                    else
                    {
                        cursorH = LeftColumn;
                        cursorV = index;
                    }
                    PrintBlack(lst[page][index].ProcessName.PadRight(44, ' ').Remove(43), cursorH, cursorV + liner);
                    if (index + 20 <= lst[page].Length - 1)
                    {
                        index += 20;
                    }
                    else if (true)
                    {
                        index = lst[page].Length - 1;
                    }
                    if (index > 19)
                    {
                        cursorH = RightColumn;
                        cursorV = index - 20;
                    }
                    else
                    {
                        cursorH = LeftColumn;
                        cursorV = index;
                    }
                    PrintWhite(lst[page][index].ProcessName.PadRight(44, ' ').Remove(43), cursorH, cursorV + liner); break;
                case ConsoleKey.PageUp:
                    if (page == 0)
                    {
                        page = lst.Count - 1;
                    }
                    else
                    {
                        page--;
                    }
                    index = 0;
                    FillFrame(lst, page);
                    break;
                case ConsoleKey.PageDown:
                    if (page == lst.Count - 1)
                    {

                        page = 0;
                    }
                    else
                    {
                        page++;
                    }
                    index = 0;
                    FillFrame(lst, page);
                    break;
                case ConsoleKey.Tab:
                    break;
                case ConsoleKey.Enter:
                    Console.SetCursorPosition(0, 23);
                    Console.WriteLine("Введите \"Close\" для нормального завершения процесса или \"Kill\" для снятия процесса:");
                    ManualKillClose(ref lst, ref page, index);
                    break;
                case ConsoleKey.Escape:
                    exit = true;
                    break;
                case ConsoleKey.Applications:
                    ContextMenuSelector(ref lst, ref page, index, cursorH, cursorV);
                    break;
                default: break;
            }
        }
        void ManualKillClose(ref List<Process[]> lst, ref int page, int procIndex)
        {
            bool cycle = true;
            do
            {
                switch (Console.ReadLine())
                {
                    case "Close":
                    case "close":
                        lst[page][procIndex].CloseMainWindow();
                        lst[page][procIndex].Close();
                        cycle = false;
                        break;
                    case "Kill":
                    case "kill":
                        lst[page][procIndex].Kill();
                        cycle = false;
                        break;
                    case null:
                    case "":
                        cycle = false;
                        break;
                    default:
                        break;
                }
            } while (cycle);
            Console.ResetColor();
            ShowFrame();
            Process[] ListOfProcesses = Process.GetProcesses();
            lst = FrameList(ListOfProcesses);
            page = 0;
            FillFrame(lst, page);
        }
        void ShowFrame()
        {
            Console.Clear();
            string lineUp = "╔".PadRight(48, '═') + "╤".PadRight(11, '═') + "╦".PadRight(48, '═') + "╤".PadRight(11, '═') + "╗";
            string border = "║".PadRight(48, ' ') + "│".PadRight(11, ' ') + "║".PadRight(48, ' ') + "│".PadRight(11, ' ') + "║";
            string lineDown = "╚".PadRight(48, '═') + "╧".PadRight(11, '═') + "╩".PadRight(48, '═') + "╧".PadRight(11, '═') + "╝";
            Console.WriteLine(lineUp);
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine(border);
            }
            Console.WriteLine(lineDown);

        }
        void FillFrame(List<Process[]> lst, int page)
        {
            int upper = 1;
            int FFStart = 3;
            int SFStart = 61;
            int FFBorder = 50;
            int SFBorder = 109;
            int linerLeft = 0;
            int linerRight = 0;
            if (lst[page].Length == 40)
            {
                for (int i = 0; i < lst[page].Length; i++)
                {
                    if (linerLeft < 20 & i < 20)
                    {
                        Console.SetCursorPosition(FFStart, upper + linerLeft);
                        Console.Write(lst[page][i].ProcessName.PadRight(44).Remove(43));
                        Console.CursorLeft = FFBorder;
                        Console.Write(lst[page][i].Id.ToString().PadLeft(8));
                        linerLeft++;
                    }
                    if (i >= 20 & linerRight < 20)
                    {
                        Console.SetCursorPosition(SFStart, upper + linerRight);
                        Console.Write(lst[page][i].ProcessName.PadRight(44).Remove(43));
                        Console.SetCursorPosition(SFBorder, upper + linerRight);
                        Console.Write(lst[page][i].Id.ToString().PadLeft(8));
                        linerRight++;
                    }
                }
            }
            else if (lst[page].Length > 20 & lst[page].Length < 40)
            {
                for (int i = 0; i < 40; i++)
                {
                    if (linerLeft < 20 & i < 20)
                    {
                        Console.SetCursorPosition(FFStart, upper + linerLeft);
                        Console.Write(lst[page][i].ProcessName.PadRight(44).Remove(43));
                        Console.CursorLeft = FFBorder;
                        Console.Write(lst[page][i].Id.ToString().PadLeft(8));
                        linerLeft++;
                    }
                    else if (i >= 20 & linerRight < 20)
                    {
                        if (i < lst[page].Length)
                        {

                            Console.SetCursorPosition(SFStart, upper + linerRight);
                            Console.Write(lst[page][i].ProcessName.PadRight(44).Remove(43));
                            Console.SetCursorPosition(SFBorder, upper + linerRight);
                            Console.Write(lst[page][i].Id.ToString().PadLeft(8));
                            linerRight++;
                        }
                        else
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
            else if (lst[page].Length < 20)
            {
                for (int i = 0; i < 40; i++)
                {
                    if (i < lst[page].Length & linerLeft < 20)
                    {
                        Console.SetCursorPosition(FFStart, upper + linerLeft);
                        Console.Write(lst[page][i].ProcessName.PadRight(44).Remove(43));
                        Console.CursorLeft = FFBorder;
                        Console.Write(lst[page][i].Id.ToString().PadLeft(8));
                        linerLeft++;
                    }
                    else if (i >= lst[page].Length & i < 20 & linerLeft < 20)
                    {
                        Console.SetCursorPosition(FFStart, upper + linerLeft);
                        Console.Write(" ".PadRight(44, ' ').Remove(43));
                        Console.SetCursorPosition(FFBorder, upper + linerLeft);
                        Console.Write(" ".PadLeft(8, ' '));
                        linerLeft++;
                    }
                    else if (i >= 20 & linerLeft < 20)
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
        List<Process[]> FrameList(Process[] listOfProcesses)
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
        void ShowContext(string[] entryes, int cursorH, int cursorV)
        {
            int currentLeft = cursorH;
            int currentTop = cursorV + 2;
            for (int i = 0; i < entryes.Length; i++)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(currentLeft + 10, currentTop + i);
                Console.Write(entryes[i]);
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(currentLeft, currentTop);
        }
        void ContextMenuSelector(ref List<Process[]> lst, ref int page, int procIndex, int cursorH, int cursorV)
        {
            int index = 0;
            bool cycle = true;
            string[] entryes = { "Close", "Kill " };
            ShowContext(entryes, cursorH, cursorV);
            Console.SetCursorPosition(cursorH + 10, cursorV + 2);
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(entryes[index]);
            do
            {
                Console.CursorLeft = cursorH + 10;
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        Console.CursorLeft = cursorH + 10;
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(entryes[index]);
                        index--;
                        if (index < 0)
                        {
                            index = entryes.Length - 1;
                            Console.CursorTop += entryes.Length - 1;
                        }
                        else
                        {
                            Console.CursorTop--;
                        }
                        Console.CursorLeft = cursorH + 10;
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(entryes[index]);
                        break;
                    case ConsoleKey.DownArrow:
                        Console.CursorLeft = cursorH + 10;
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(entryes[index]);
                        index++;
                        if (index == entryes.Length)
                        {
                            index = 0;
                            Console.CursorTop -= entryes.Length - 1;
                        }
                        else
                        {
                            Console.CursorTop++;
                        }
                        Console.CursorLeft = cursorH + 10;
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(entryes[index]);
                        break;
                    case ConsoleKey.Enter:
                        ContextMenuEnter(ref lst, ref page, procIndex, index);
                        cycle = false;
                        break;
                    case ConsoleKey.Escape:
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        FillFrame(lst, page);
                        cycle = false;
                        break;
                    default:
                        break;
                }
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
            } while (cycle);
            Console.CursorLeft = cursorH;
            Console.CursorTop = cursorV;
        }
        void ContextMenuEnter(ref List<Process[]> lst, ref int page, int procIndex, int entry)
        {
            switch (entry)
            {
                case 0:
                    lst[page][procIndex].CloseMainWindow();
                    lst[page][procIndex].Close();
                    break;
                case 1:
                    lst[page][procIndex].Kill();
                    break;
                default:
                    break;
            }
            Console.ResetColor();
            ShowFrame();
            Process[] ListOfProcesses = Process.GetProcesses();
            lst = FrameList(ListOfProcesses);
            page = 0;
            FillFrame(lst, page);
        }
        void PrintWhite(string str, int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(str);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        void PrintBlack(string str, int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(str);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        void PrintRed(string str, int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(str);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        void PrintGreen(string str, int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(str);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}