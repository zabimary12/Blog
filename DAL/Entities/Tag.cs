using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Tag
    {
        public Tag()
        {
            Articles = new List<Article>();
        }

        public Guid Id { get; set; }
        public string Text { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}