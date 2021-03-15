using System;

namespace Lesson06
{
    [Serializable]
    class MyArraySizeException : Exception
    {
        public MyArraySizeException()
        {
        }

        public MyArraySizeException(string message) : base(message)
        {
        }
    }
}
