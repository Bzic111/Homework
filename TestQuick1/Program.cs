using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace TestQuick1
{

    class Programm
    {
        static void Main(string[] args)
        {
            Process[] ListOfProcesses = Process.GetProcesses();
            List<Process[]> lister = FrameList(ListOfProcesses);
            int page = 0;
            bool exit = false;
            ShowFrame();
            FillFrame(lister, page);
            do
            {

                switch (Console.ReadKey(false).Key)
                {
                    case ConsoleKey.PageDown:
                        page++;
                        if (page == lister.Count)
                        {
                            page = 0;
                        }
                        
                        break;
                    case ConsoleKey.PageUp:
                        page--;
                        if (page < 0)
                        {
                            page = lister.Count - 1;
                        }
                        break;
                    case ConsoleKey.Escape:
                        exit = true;
                        break;
                    default:
                        break;
                }
                FillFrame(lister, page);
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
            int liner = 0;

            for (int j = 0; j < lst[page].Length; j++)
            {
                if (j == 0)
                {
                    Console.SetCursorPosition(FFStart, upper + liner);
                    Console.Write(lst[page][j].ProcessName.PadRight(44).Remove(43));
                    Console.CursorLeft = 50;
                    Console.Write(lst[page][j].Id.ToString().PadLeft(8));
                }
                else if (j % 2 == 0 & j == lst[page].Length - 1 & page == lst.Count - 1)
                {
                    Console.SetCursorPosition(FFStart, upper + liner);
                    Console.Write(lst[page][j].ProcessName.PadRight(44).Remove(43));
                    Console.SetCursorPosition(FFBorder, upper + liner);
                    Console.Write(lst[page][j].Id.ToString().PadLeft(8));
                }
                else if (j % 2 == 0)
                {
                    Console.SetCursorPosition(FFStart, upper + liner);
                    Console.Write(lst[page][j].ProcessName.PadRight(44).Remove(43));
                    Console.SetCursorPosition(FFBorder, upper + liner);
                    Console.Write(lst[page][j].Id.ToString().PadLeft(8));
                }
                else if (j % 2 != 0)
                {
                    Console.SetCursorPosition(SFStart, upper + liner);
                    Console.Write(lst[page][j].ProcessName.PadRight(44).Remove(43));
                    Console.SetCursorPosition(SFBorder, upper + liner);
                    Console.Write(lst[page][j].Id.ToString().PadLeft(8));
                    liner++;
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
    }
}
