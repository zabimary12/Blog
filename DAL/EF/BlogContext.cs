using System;
using DAL.Entities;
using DAL.Enums;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    public sealed class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {

        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .HasConversion(x => x.ToString(),
                        x => (Role) Enum.Parse(typeof(Role), x));
            });

                        var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "Fan@gmail.com",
                Password = "12345",
                Name = "Josh",
                Surname = "Brown",
                Role = Role.User
            };
            var user1 = new User
            {
                Id = Guid.NewGuid(),
                Email = "Admin@gmail.com",
                Password = "54321",
                Name = "Nikita",
                Surname = "Viktorov",
                Role = Role.Admin
            };

            var tag = new Tag { Id = Guid.NewGuid(), Text = "#MU" };
            var tag1 = new Tag { Id = Guid.NewGuid(), Text = "#Football" };

            var article = new Article
            {
                Id = Guid.NewGuid(),
                Title = "Футбольный клуб Манчестер Юнайтед",
                Text = "МЮ - чемпион",
                UserId = user.Id
            };
            var article1 = new Article
            {
                Id = Guid.NewGuid(),
                Title = "Английский футбол",
                Text = "Англия - чемпион",
                UserId = user1.Id
            };
            var article2 = new Article
            {
                Id = Guid.NewGuid(),
                Title = "Испанский футбол",
                Text = "Испания - чемпион",
                UserId = user.Id
            };

            var comment = new Comment
            { Id = Guid.NewGuid(), Text = "MU - The Champions!", ArticleId = article.Id, UserId = user1.Id };

            modelBuilder.Entity<User>().HasData
                (user, user1);
            modelBuilder.Entity<Article>().HasData
                (article, article1, article2);
            modelBuilder.Entity<Tag>().HasData(tag, tag1);
            modelBuilder.Entity<Comment>().HasData(comment);

            modelBuilder.Entity<Comment>()
                .HasOne(m => m.User)
                .WithMany(t => t.Comments)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Article>()
                .HasMany(p => p.Tags)
                .WithMany(p => p.Articles)
                .UsingEntity(j => j.HasData(new { ArticlesId = article.Id, TagsId = tag.Id },
                    new { ArticlesId = article1.Id, TagsId = tag.Id }, new { ArticlesId = article2.Id, TagsId = tag1.Id }));
        }
    }
}
