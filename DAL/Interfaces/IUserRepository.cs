using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        Task Create(User item);
        Task Update(User item);
        Task Delete(Guid id);
        Task<ICollection<User>> GetAll();
        Task<User> Get(Guid id);
    }
}