using System;

namespace MenuSpace
{
    public abstract class Work
    {
        public Menu.Runner[] AllRuns { get; }
        string[] Names { get; }
        public virtual string[] GetNames() { return this.Names; }
        public virtual Menu.Runner[] GetRunners()
        {
            return AllRuns;
        }
    }

}
