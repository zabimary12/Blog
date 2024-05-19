using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BlogContext _db;

        public CommentRepository(BlogContext db)
        {
            _db = db;
        }

        public async Task Create(Comment item)
        {
            await _db.Comments.AddAsync(item);

            var article = await _db.Articles.FindAsync(item.ArticleId);

            article.Comments.Add(item);

            await _db.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            _db.Comments.Remove(await _db.Comments.FirstAsync(c => c.Id == id));

            await _db.SaveChangesAsync();
        }

        public async Task<Comment> Get(Guid id)
        {
            if (await _db.Comments.CountAsync(a => a.Id == id) == 0)
                return null;

            return await _db.Comments.FirstAsync(a => a.Id == id);
        }

        public async Task<ICollection<Comment>> GetAll()
        {
            return await _db.Comments.ToListAsync();
        }

        public async Task Update(Comment item)
        {
            _db.Comments.Remove(await _db.Comments.FirstAsync(a => a.Id == item.Id));

            await _db.Comments.AddAsync(item);

            await _db.SaveChangesAsync();
        }

        public async Task<ICollection<Comment>> GetCommentsByArticles(Guid articleId)
        {
            if (await _db.Comments.CountAsync(c => c.ArticleId == articleId) == 0)
                return null;

            return await _db.Comments.Where(a => a.ArticleId == articleId).ToListAsync();
        }
    }
}