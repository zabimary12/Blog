using System;

namespace BLL.Exceptions
{
    public class ArticleException : Exception
    {
        public ArticleException(string message) : base(message)
        {
        }
    }
}