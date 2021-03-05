using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace TestQuick1
{

    class Programm
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            char c = '\u2500';
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 100; i++)
            {
                Console.Write(c++);
            }            
        }
        static void DirMeth(string path)
        {
            string[] directory = Directory.GetFileSystemEntries(path);
            
            for (int i = 0; i < directory.Length; i++)
            {
                Console.Write(("|_".PadRight(1, '_') + directory[i].Split('\\')[^1] + '\n'));
                Console.CursorLeft = directory[i].Split('\\').Length - 2;

                if (Directory.Exists(directory[i]))
                {
                    DirMeth(directory[i]);
                }
            }
        }
    }
}
