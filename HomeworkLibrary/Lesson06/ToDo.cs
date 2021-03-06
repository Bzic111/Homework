namespace Lesson06
{
    class ToDo
    {
        public string Title { get; set; }
        public bool IsDone { get; set; }
        public ToDo() { }
        public ToDo(string title, bool done = false)
        {
            Title = title;
            IsDone = done;
        }
    }
}
