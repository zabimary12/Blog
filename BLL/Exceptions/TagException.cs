using System;

namespace BLL.Exceptions
{
    public class TagException : Exception
    {
        public TagException(string message) : base(message)
        {
        }
    }
}