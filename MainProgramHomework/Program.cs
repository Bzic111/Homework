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
                new Lesson05.HomeWork(),
                new Lesson06.HomeWork(),
                new Lesson07.HomeWork(),
                new Lesson08.HomeWork()
            };
            mainMenu.MainMenu(works, "Homework");
        }
    }
}