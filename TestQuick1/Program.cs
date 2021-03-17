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
            string lineUp = "╔".PadRight(48, '═') + "╤".PadRight(11, '═') + "╦".PadRight(48, '═') + "╤".PadRight(11, '═') + "╗\n";
            string border = "║ ".PadRight(48, ' ') + "│".PadRight(11, ' ') + "║".PadRight(48, ' ') + "│".PadRight(11, ' ') + "║";
            string lineDown = "╚".PadRight(48, '═') + "╧".PadRight(11, '═') + "╩".PadRight(48, '═') + "╧".PadRight(11, '═') + "╝\n";
            int firstFrame = 3;
            int secondFrame = 60;

            Console.Write(lineUp);
            for (int i = 0; i < lister.Count; i++)
            {
                for (int j = 0; j < lister[i].Length; j++)
                {
                    if (j==0)
                    {
                        Console.Write(border);
                        Console.CursorLeft = 3;
                        Console.Write(lister[i][j].ProcessName.PadRight(44).Remove(43));
                        Console.CursorLeft = 50;
                        Console.Write(lister[i][j].Id.ToString().PadLeft(8));
                        //Console.CursorLeft = 118;
                        //Console.WriteLine("");
                    }
                    else if (j % 2 == 0 & j == lister[i].Length - 1 & i == lister.Count - 1)
                    {
                        Console.Write(border);
                        Console.CursorLeft = 3;
                        Console.Write(lister[i][j].ProcessName.PadRight(44).Remove(43));
                        Console.CursorLeft = 50;
                        Console.Write(lister[i][j].Id.ToString().PadLeft(8));
                        Console.CursorLeft = 117;
                        Console.WriteLine("");
                    }
                    else if (j % 2 == 0)
                    {
                        Console.Write(border);
                        Console.CursorLeft = 3;
                        Console.Write(lister[i][j].ProcessName.PadRight(44).Remove(43));
                        Console.CursorLeft = 50;
                        Console.Write(lister[i][j].Id.ToString().PadLeft(8));
                    }
                    
                    else if(j % 2 != 0)
                    {
                        //Console.Write(border);
                        Console.CursorLeft = 61;
                        Console.Write(lister[i][j].ProcessName.PadRight(44).Remove(43));
                        Console.CursorLeft = 109;
                        Console.Write(lister[i][j].Id.ToString().PadLeft(8));
                        Console.CursorLeft = 117;
                        Console.WriteLine("");
                    }

                }
            }
            Console.Write(lineDown);

        }

        static void ShowProcesses()
        {
            Process[] ListOfProcesses = Process.GetProcesses();
            List<Process[]> lister = FrameList(ListOfProcesses);
        }
        static void FrameBuild(List<Process[]> lst)
        {
            string lineUp = "╔".PadRight(48, '═') + "╤".PadRight(11, '═') + "╦".PadRight(48, '═') + "╤".PadRight(11, '═') + "╗\n";
            string border = "║ ".PadRight(48, ' ') + "│".PadRight(11, ' ') + "║".PadRight(48, ' ') + "│".PadRight(11, ' ') + "║\n";
            string lineDown = "╚".PadRight(48, '═') + "╧".PadRight(11, '═') + "╩".PadRight(48, '═') + "╧".PadRight(11, '═') + "╝\n";
        }
        static List<Process[]> FrameList(Process[] listOfProcesses)
        {
            List<Process[]> FrameList = new List<Process[]>();
            for (int i = 0; i < listOfProcesses.Length;)
            {
                if (FrameList.Count == listOfProcesses.Length / 20)
                {
                    FrameList.Add(new Process[listOfProcesses.Length % 20]);
                    for (int j = 0; j < 20 & i < listOfProcesses.Length; j++, i++)
                    {
                        FrameList[^1][j] = listOfProcesses[i];
                    }
                }
                else
                {
                    FrameList.Add(new Process[20]);

                    for (int j = 0; j < 20 & i < listOfProcesses.Length; j++, i++)
                    {
                        FrameList[^1][j] = listOfProcesses[i];
                    }
                }

            }
            return FrameList;
        }
    }
}
