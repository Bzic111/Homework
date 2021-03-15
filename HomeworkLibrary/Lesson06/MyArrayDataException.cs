using System;

namespace Lesson06
{
    [Serializable]
    class MyArrayDataException : Exception
    {
        public MyArrayDataException()
        {
        }
        public MyArrayDataException(string message) : base(message)
        {
        }
    }
}
