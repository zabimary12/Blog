using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IArticleService
    {
        Task Create(ArticleDto articleDto, string userId);
        Task Delete(Guid id);
        Task<ArticleDto> Get(Guid id);
        Task<ICollection<ArticleDto>> GetAll();
        Task<ICollection<ArticleDto>> GetArticlesByTag(Guid tagId);
        Task<ICollection<ArticleDto>> GetUserArticles(Guid userId);
        Task Update(Guid id, ArticleDto articleDto);
        Task AddTag(Guid article, TagDto tagDto);
        Task<ArticleDto> GetArticleByText(string text);
    }
}