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
            Collection collection = new Collection();

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


            // Переопределяем пункты в подменю
            List<Dictionary<string, Menu.Runner>[]> SubmenuLessons = new List<Dictionary<string, Menu.Runner>[]>
            {
                collection.SetSubmenu(Lesson01, MenuNames),
                collection.SetSubmenu(Lesson02, MenuNames),
                collection.SetSubmenu(Lesson03, MenuNames),
                collection.SetSubmenu(Lesson04, MenuNames),
                collection.SetSubmenu(Lesson05, MenuNames)
            };
            
            Lesson03.Work01 wrk = new Lesson03.Work01();
            
            string[] newEntryes = { "Слева направо, сверху вниз", "Справа налево, снизу вверх", MenuNames[1] };
            Menu.Runner[] newRuner = { wrk.DiagonalLR, wrk.DiagonalRL, wrk.GetCode };
            
            collection.ReSetRunner(ref SubmenuLessons,3,0, newEntryes, newRuner);

            Dictionary<string, Menu.Cycler>[] MainMenuCycle = new Dictionary<string, Menu.Cycler>[5]
            { 
                collection.SetCycler(Lesson01, MainMenu),
                collection.SetCycler(Lesson02, MainMenu),
                collection.SetCycler(Lesson03, MainMenu),
                collection.SetCycler(Lesson04, MainMenu),
                collection.SetCycler(Lesson05, MainMenu)
            };

            Console.Clear();
            MainMenu.Cycle(MainMenuCycle, SubmenuLessons);
        }
    }
}