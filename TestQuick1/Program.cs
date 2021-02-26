using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;

namespace TestQuick1
{
    public class Program
    {
        static void Main(string[] args)
        {
            
        }
        static int Fibonachi(int n)
        {
            if (n > 1)
            {
                return (Fibonachi(n - 1) + Fibonachi(n - 2));
            }
            return n;
        }
    }
}
