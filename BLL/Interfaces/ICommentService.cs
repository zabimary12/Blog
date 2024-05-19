using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface ICommentService
    {
        Task Create(Guid articleId, CommentDto commentDto);
        Task Delete(Guid id);
        Task<CommentDto> Get(Guid id);
        Task<ICollection<CommentDto>> GetAll();
        Task Update(Guid id, CommentDto commentDto);
    }
}