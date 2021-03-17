using MenuSpace;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson08
{
    public class HomeWork : MenuSpace.Work
    {
        public Menu.Runner[] AllRuns { get; }
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
    }
}
