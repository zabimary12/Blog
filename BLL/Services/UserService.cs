using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Create(UserDto userDto)
        {
            await _unitOfWork.Users.Create(_mapper.Map<User>(userDto));
        }

        public async Task Delete(Guid id)
        {
            if (await _unitOfWork.Users.Get(id) == null)
                throw new UserException("You don't delete this user.User doesn't exist");
            await _unitOfWork.Users.Delete(id);
        }

        public async Task<UserDto> Get(Guid id)
        {
            var user = await _unitOfWork.Users.Get(id);

            return _mapper.Map<UserDto>(user) ?? throw new UserException("You don't get this user.User doesn't exist");
        }

        public async Task<ICollection<UserDto>> GetAll()
        {
            var users = await _unitOfWork.Users.GetAll();

            return _mapper.Map<ICollection<UserDto>>(users) ?? throw new UserException("List of Users is empty");
        }

        public async Task Update(Guid id, UserDto userDto)
        {
            if (await _unitOfWork.Users.Get(id) == null)
                throw new UserException("You don't update this user.User doesn't exist");

            var updateUser = _mapper.Map<User>(userDto);
            updateUser.Id = id;

            await _unitOfWork.Users.Update(updateUser);
        }
    }
}