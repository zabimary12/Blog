using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task Create(UserDto userDto);
        Task Delete(Guid id);
        Task<UserDto> Get(Guid id);
        Task<ICollection<UserDto>> GetAll();
        Task Update(Guid id, UserDto userDto);
    }
}