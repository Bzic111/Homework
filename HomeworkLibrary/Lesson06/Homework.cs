using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Lesson06
{
    public class Homework : MenuSpace.Work
    {
        public new MenuSpace.Menu.Runner[] AllRuns { get; }
        string[] Names { get; }
        public override string[] GetNames() { return this.Names; }
        public override MenuSpace.Menu.Runner[] GetRunners()
        {
            return AllRuns;
        }

        public Homework()
        {
            AllRuns = new MenuSpace.Menu.Runner[]
            {

            };
        }
        
        void SaveCatalogTree()
        {
            string path = @"E:\Program Files (x86)\Steam\steamapps\common";
            string[] directory = Directory.GetFileSystemEntries(path);
            for (int i = 0; i < directory.Length; i++)
            {
                Console.WriteLine(directory[i]);
            }
        }
    }
}
