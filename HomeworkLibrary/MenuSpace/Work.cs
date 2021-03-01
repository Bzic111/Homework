using System;

namespace MenuSpace
{
    public abstract class Work
    {
        string Name { get; }
        string Code { get; }
        public virtual void GetCode() { Console.WriteLine(this.Code); }
        public virtual string GetName() { return this.Name; }
        public abstract void Start();
    }

}
