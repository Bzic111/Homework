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
            List<Work> Lesson01 = new List<Work>();
            List<Work> Lesson02 = new List<Work>();
            List<Work> Lesson03 = new List<Work>();
            List<Work> Lesson04 = new List<Work>();

            Dictionary<string, Menu.Cycler> Lesson01Cycle = new Dictionary<string, Menu.Cycler>(); 
            Dictionary<string, Menu.Cycler> Lesson02Cycle = new Dictionary<string, Menu.Cycler>();
            Dictionary<string, Menu.Cycler> Lesson03Cycle = new Dictionary<string, Menu.Cycler>();
            Dictionary<string, Menu.Cycler> Lesson04Cycle = new Dictionary<string, Menu.Cycler>();


            // Заполнение коллекций List<IWork>Lesson с инициализацией объектов
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
            Lesson04.Add(new Lesson04.Work01());
            Lesson04.Add(new Lesson04.Work02());
            Lesson04.Add(new Lesson04.Work03());
            Lesson04.Add(new Lesson04.Work04());


            // Объявление сложных массивов
            Dictionary<string, Menu.Runner>[] SubmenuLesson01 = new Dictionary<string, Menu.Runner>[Lesson01.Count];
            Dictionary<string, Menu.Runner>[] SubmenuLesson02 = new Dictionary<string, Menu.Runner>[Lesson02.Count];
            Dictionary<string, Menu.Runner>[] SubmenuLesson03 = new Dictionary<string, Menu.Runner>[Lesson03.Count];
            Dictionary<string, Menu.Runner>[] SubmenuLesson04 = new Dictionary<string, Menu.Runner>[Lesson03.Count];

            List<Dictionary<string, Menu.Runner>[]> SubmenuLessons = new List<Dictionary<string, Menu.Runner>[]>();
            Dictionary<string, Menu.Cycler>[] MainMenuCycle = new Dictionary<string, Menu.Cycler>[4];

            // Заполнение массивов Submenu, методы Start() и GetCode()
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
            for (int i = 0; i < Lesson04.Count; i++)
            {
                SubmenuLesson04[i] = new Dictionary<string, Menu.Runner>
                {
                    { MenuNames[0],Lesson04[i].Start },
                    { MenuNames[1],Lesson04[i].GetCode }
                };
            }
            
            // Переопределяем пункты в подменю
            Lesson03.Work01 wrk = new Lesson03.Work01();            
            SubmenuLesson03[0] = new Dictionary<string, Menu.Runner> 
            { { "Слева направо, сверху вниз", wrk.DiagonalLR },
                {"Справа налево, снизу вверх",wrk.DiagonalRL },
                { MenuNames[1], wrk.GetCode } };
            
            // Заполнение массива Lesson__Cycle
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
            for (int i = 0; i < Lesson04.Count; i++)
            {
                Lesson04Cycle.Add(Lesson04[i].GetName(), MainMenu.Cycle);
            }
            
            // Заполнение коллекции List<T>Submenu
            SubmenuLessons.Add(SubmenuLesson01);
            SubmenuLessons.Add(SubmenuLesson02);
            SubmenuLessons.Add(SubmenuLesson03);
            SubmenuLessons.Add(SubmenuLesson04);
            
            // Заполнение массива MainMenuCycle
            MainMenuCycle[0] = Lesson01Cycle;
            MainMenuCycle[1] = Lesson02Cycle;
            MainMenuCycle[2] = Lesson03Cycle;
            MainMenuCycle[3] = Lesson04Cycle;
            
            Console.Clear();
            MainMenu.Cycle(MainMenuCycle, SubmenuLessons);
        }
    }
}