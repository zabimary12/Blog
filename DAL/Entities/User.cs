using System;
using System.Collections.Generic;
using DAL.Enums;

namespace DAL.Entities
{
    public class User
    {
        public User()
        {
            Articles = new List<Article>();
            Comments = new List<Comment>();
        }

        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Role Role { get; set; }
        public ICollection<Article> Articles { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}