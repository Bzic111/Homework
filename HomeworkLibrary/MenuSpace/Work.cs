using System;

namespace MenuSpace
{
    public abstract class Work
    {
        string[] Names { get; }
        public virtual string[] GetNames() { return this.Names; }
    }

}
