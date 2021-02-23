using System;
using System.Collections;
using System.Collections.Generic;
using Lesson01;
using Lesson02;
using Lesson03;
using MenuSpace;

namespace MainProgramHomework
{
    class Program
    {
        
        static void Main(string[] args)
        {
            // Объявление переменных
            string[] MenuNames = { "Начать", "Показать основной код" };
            Menu MainMenu = new Menu();

            // Объявление массивов
            List<IWork> Lesson01 = new List<IWork>();
            List<IWork> Lesson02 = new List<IWork>();
            List<IWork> Lesson03 = new List<IWork>();
            Dictionary<string, Menu.Cycler> Lesson01Cycle = new Dictionary<string, Menu.Cycler>(); 
            Dictionary<string, Menu.Cycler> Lesson02Cycle = new Dictionary<string, Menu.Cycler>();
            Dictionary<string, Menu.Cycler> Lesson03Cycle = new Dictionary<string, Menu.Cycler>();


            // Заполнение массивов с инициализацией объектов
            Lesson01.Add(new Lesson01.Work01());
            Lesson02.Add(new Lesson02.Work01());
            Lesson02.Add(new Lesson02.Work02());
            Lesson02.Add(new Lesson02.Work03());
            Lesson02.Add(new Lesson02.Work04());
            Lesson02.Add(new Lesson02.Work05());
            Lesson02.Add(new Lesson02.Work06());
            Lesson02.Add(new Lesson02.WorkDop1());
            Lesson03.Add(new Lesson03.Work01());
            Lesson03.Add(new Lesson03.Work02());
            Lesson03.Add(new Lesson03.Work03());
            Lesson03.Add(new Lesson03.Work04());
            Lesson03.Add(new Lesson03.WorkDop1());

            // Объявление сложных массивов
            Dictionary<string, Menu.Runner>[] SubmenuLesson01 = new Dictionary<string, Menu.Runner>[Lesson01.Count];
            Dictionary<string, Menu.Runner>[] SubmenuLesson02 = new Dictionary<string, Menu.Runner>[Lesson02.Count];
            Dictionary<string, Menu.Runner>[] SubmenuLesson03 = new Dictionary<string, Menu.Runner>[Lesson03.Count];
            List<Dictionary<string, Menu.Runner>[]> SubmenuLessons = new List<Dictionary<string, Menu.Runner>[]>();
            Dictionary<string, Menu.Cycler>[] MainMenuCycle = new Dictionary<string, Menu.Cycler>[3];

            // Заполнение сложных массивов
            for (int i = 0; i < Lesson01.Count; i++)
            {
                SubmenuLesson01[i] = new Dictionary<string, Menu.Runner>
                {
                    { MenuNames[0],Lesson01[i].Start },
                    { MenuNames[1],Lesson01[i].GetCode }
                };
            }
            for (int i = 0; i < Lesson02.Count; i++)
            {
                SubmenuLesson02[i] = new Dictionary<string, Menu.Runner>
                {
                    { MenuNames[0],Lesson02[i].Start },
                    { MenuNames[1],Lesson02[i].GetCode }
                };
            }
            for (int i = 0; i < Lesson03.Count; i++)
            {
                SubmenuLesson03[i] = new Dictionary<string, Menu.Runner>
                {
                    { MenuNames[0],Lesson03[i].Start },
                    { MenuNames[1],Lesson03[i].GetCode }
                };
            }
            for (int i = 0; i < Lesson01.Count; i++)
            {
                Lesson01Cycle.Add(Lesson01[i].GetName(), MainMenu.Cycle);
            }
            for (int i = 0; i < Lesson02.Count; i++)
            {
                Lesson02Cycle.Add(Lesson02[i].GetName(), MainMenu.Cycle);
            }
            for (int i = 0; i < Lesson03.Count; i++)
            {
                Lesson03Cycle.Add(Lesson03[i].GetName(), MainMenu.Cycle);
            }
            SubmenuLessons.Add(SubmenuLesson01);
            SubmenuLessons.Add(SubmenuLesson02);
            SubmenuLessons.Add(SubmenuLesson03);
            MainMenuCycle[0] = Lesson01Cycle;
            MainMenuCycle[1] = Lesson02Cycle;
            MainMenuCycle[2] = Lesson03Cycle;

            Console.Clear();
            MainMenu.Cycle(MainMenuCycle, SubmenuLessons);
        }
    }
}
