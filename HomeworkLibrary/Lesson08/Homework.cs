using MenuSpace;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace Lesson08
{
    public class HomeWork : MenuSpace.Work
    {
        public new Menu.Runner[] AllRuns { get; }
        string[] Names { get; }
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

            };
        }
        
        void ShowProcesses()
        {
            Process[] ListOfProcesses = Process.GetProcesses();
        }
        void FrameBuild(Process[] listOfProcesses)
        {
            List<Process[]> FrameList = new List<Process[]>();
            int counter = 0;
            for (int i = 0; i < listOfProcesses.Length; i++)
            {
                if (i==0)
                {
                    FrameList[0] = new Process[20];
                }
                if (i > 0 & i % 19 == 0)
                {
                    FrameList.Add(new Process[20]);
                }
                else
                {
                    FrameList[^1][counter] = listOfProcesses[i];
                    counter++;
                }
            }
        }
    }
    class Frame
    {

    }
}
