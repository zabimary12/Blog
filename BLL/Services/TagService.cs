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
    public class TagService : ITagService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TagService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Create(TagDto tagDto)
        {
            await _unitOfWork.Tags.Create(_mapper.Map<Tag>(tagDto));
        }

        public async Task Delete(Guid id)
        {
            if (await _unitOfWork.Tags.Get(id) == null)
                throw new TagException("Tag doesn't exist.You don't delete this tag");
            await _unitOfWork.Tags.Delete(id);
        }

        public async Task<TagDto> Get(Guid id)
        {
            var tag = _mapper.Map<TagDto>(await _unitOfWork.Tags.Get(id));

            return tag ?? throw new TagException("Tag doesn't exist.You don't get this tag");
        }

        public async Task<ICollection<TagDto>> GetAll()
        {
            var tags = _mapper.Map<ICollection<TagDto>>(await _unitOfWork.Tags.GetAll());

            return tags ?? throw new TagException("List of tags is empty");
        }

        public async Task Update(Guid id, TagDto tagDto)
        {
            if (await _unitOfWork.Tags.Get(id) == null)
                throw new TagException("Tag doesn't exist.You don't update this tag");

            var updateTag = _mapper.Map<Tag>(tagDto,
                t => t.AfterMap((_,
                    tag) => tag.Id = id));
            await _unitOfWork.Tags.Update(updateTag);
        }
    }
}