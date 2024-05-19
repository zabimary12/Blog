using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface ITagService
    {
        Task Create(TagDto tagDto);
        Task Delete(Guid id);
        Task<TagDto> Get(Guid id);
        Task<ICollection<TagDto>> GetAll();
        Task Update(Guid id, TagDto tagDto);
    }
}