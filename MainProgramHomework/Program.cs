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
            // Объявление переменных
            string[] MenuNames = { "Начать", "Показать основной код" };
            Menu MainMenu = new Menu();
            Collection Collection = new Collection();

            // Объявление массивов
            List<Work> Lesson01 = new List<Work>();
            List<Work> Lesson02 = new List<Work>();
            List<Work> Lesson03 = new List<Work>();
            List<Work> Lesson04 = new List<Work>();
            List<Work> Lesson05 = new List<Work>();

            Dictionary<string, Menu.Cycler> Lesson01Cycle = new Dictionary<string, Menu.Cycler>();
            Dictionary<string, Menu.Cycler> Lesson02Cycle = new Dictionary<string, Menu.Cycler>();
            Dictionary<string, Menu.Cycler> Lesson03Cycle = new Dictionary<string, Menu.Cycler>();
            Dictionary<string, Menu.Cycler> Lesson04Cycle = new Dictionary<string, Menu.Cycler>();
            Dictionary<string, Menu.Cycler> Lesson05Cycle = new Dictionary<string, Menu.Cycler>();

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
            Lesson05.Add(new Lesson05.Work01());
            Lesson05.Add(new Lesson05.Work02());
            Lesson05.Add(new Lesson05.Work03());

            // Объявление и Заполнение массивов, методы Start() и GetCode()
            //Dictionary<string, Menu.Runner>[] SubmenuLesson01 = Collection.SetSubmenu(Lesson01, MenuNames);
            //Dictionary<string, Menu.Runner>[] SubmenuLesson02 = Collection.SetSubmenu(Lesson02, MenuNames);
            //Dictionary<string, Menu.Runner>[] SubmenuLesson03 = Collection.SetSubmenu(Lesson03, MenuNames);
            //Dictionary<string, Menu.Runner>[] SubmenuLesson04 = Collection.SetSubmenu(Lesson04, MenuNames);
            //Dictionary<string, Menu.Runner>[] SubmenuLesson05 = Collection.SetSubmenu(Lesson05, MenuNames);


            Dictionary<string, Menu.Cycler>[] MainMenuCycle = new Dictionary<string, Menu.Cycler>[5];

            // Переопределяем пункты в подменю
            Lesson03.Work01 wrk = new Lesson03.Work01();

            string[] newEntryes = { "Слева направо, сверху вниз", "Справа налево, снизу вверх", MenuNames[1] };

            Menu.Runner[] newRuner = { wrk.DiagonalLR, wrk.DiagonalRL, wrk.GetCode };

            //SubmenuLesson03[0] = new Dictionary<string, Menu.Runner>
            //{ { "Слева направо, сверху вниз", wrk.DiagonalLR },
            //    {"Справа налево, снизу вверх",wrk.DiagonalRL },
            //    { MenuNames[1], wrk.GetCode } };

            // Заполнение коллекции List<T>Submenu
            List<Dictionary<string, Menu.Runner>[]> SubmenuLessons = new List<Dictionary<string, Menu.Runner>[]>
            {
                Collection.SetSubmenu(Lesson01, MenuNames),
                Collection.SetSubmenu(Lesson02, MenuNames),
                Collection.SetSubmenu(Lesson03, MenuNames),
                Collection.SetSubmenu(Lesson04, MenuNames),
                Collection.SetSubmenu(Lesson05, MenuNames)
            };
            Collection.ReSetRunner(ref SubmenuLessons,3, newEntryes, newRuner);

            // Заполнение массива MainMenuCycle
            MainMenuCycle[0] = Collection.SetCycler(Lesson01, MainMenu);
            MainMenuCycle[1] = Collection.SetCycler(Lesson02, MainMenu);
            MainMenuCycle[2] = Collection.SetCycler(Lesson03, MainMenu);
            MainMenuCycle[3] = Collection.SetCycler(Lesson04, MainMenu);
            MainMenuCycle[4] = Collection.SetCycler(Lesson05, MainMenu);

            Console.Clear();
            MainMenu.Cycle(MainMenuCycle, SubmenuLessons);
        }
    }
}