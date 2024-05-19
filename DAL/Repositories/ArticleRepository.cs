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
    public class ArticleRepository : IArticleRepository
    {
        private readonly BlogContext _db;

        public ArticleRepository(BlogContext db)
        {
            _db = db;
        }

        public async Task Create(Article item)
        {
            await _db.Articles.AddAsync(item);

            await _db.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            _db.Articles.Remove(await _db.Articles.FirstAsync(a => a.Id == id));

            await _db.SaveChangesAsync();
        }

        public async Task<Article> Get(Guid id)
        {
            var article = await _db.Articles.Include(c => c.User).ThenInclude(c => c.Articles).Include(c => c.Tags)
                .ThenInclude(c => c.Articles).Include(c => c.Comments).ThenInclude(c => c.User)
                .FirstOrDefaultAsync(a => a.Id == id);
            return article;
        }

        public async Task<ICollection<Article>> GetAll()
        {
            var articles = await _db.Articles.Include(c => c.User).Include(c => c.Comments).Include(s => s.Tags)
                .ThenInclude(c => c.Articles).ToListAsync();

            return articles.Count == 0 ? null : articles;
        }

        public async Task Update(Article item)
        {
            _db.Articles.Update(item);

            await _db.SaveChangesAsync();
        }

        public async Task<ICollection<Article>> GetUserArticles(Guid userId)
        {
            var articles = await _db.Articles.Where(x => x.UserId == userId).ToListAsync();

            return articles;
        }

        public async Task<Article> GetArticleByText(string text)
        {
            var article = await _db.Articles.Where(x => x.Text == text).FirstOrDefaultAsync();

            return article;
        }

        public async Task<ICollection<Article>> GetArticlesByTag(Guid tagId)
        {
            var currentTag = await _db.Tags.FindAsync(tagId);

            var articles = await _db.Articles.Where(a => a.Tags.Contains(currentTag)).Include(c => c.User)
                .Include(c => c.Comments).Include(s => s.Tags).ThenInclude(c => c.Articles).ToListAsync();

            return articles;
        }
    }
}