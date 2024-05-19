using System;

namespace BLL.Exceptions
{
    public class CommentException : Exception
    {
        public CommentException(string message) : base(message)
        {
        }
    }
}