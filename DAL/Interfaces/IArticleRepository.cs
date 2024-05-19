using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IArticleRepository
    {
        Task Create(Article item);
        Task Update(Article item);
        Task Delete(Guid id);
        Task<ICollection<Article>> GetAll();
        Task<Article> Get(Guid id);
        Task<ICollection<Article>> GetUserArticles(Guid userId);
        Task<ICollection<Article>> GetArticlesByTag(Guid tagId);
        Task<Article> GetArticleByText(string text);
    }
}