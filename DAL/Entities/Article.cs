using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Article
    {
        public Article()
        {
            Tags = new List<Tag>();
            Comments = new List<Comment>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}