using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Diagnostics;
using MenuSpace;

namespace MainProgramHomework
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu mainMenu = new Menu();
            Work[] works = new Work[]
            {
                new Lesson01.HomeWork(),
                new Lesson02.HomeWork(),
                new Lesson03.HomeWork(),
                new Lesson04.HomeWork(), 
                new Lesson05.HomeWork()
            };
            mainMenu.MainMenu(works, "Homework");
        }        
    }
}