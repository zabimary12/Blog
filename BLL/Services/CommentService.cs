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
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Create(Guid articleId, CommentDto commentDto)
        {
            var article = await _unitOfWork.Articles.Get(articleId);

            if (article == null)
                throw new CommentException("Article doesn't exist.You don't create commentary because of that!");

            var comment = _mapper.Map<Comment>(commentDto);

            comment.ArticleId = article.Id;

            await _unitOfWork.Comments.Create(comment);
        }

        public async Task Delete(Guid id)
        {
            if (await _unitOfWork.Comments.Get(id) == null)
                throw new CommentException("You don't delete commentary.Because the commentary doesn't exist");

            await _unitOfWork.Comments.Delete(id);
        }

        public async Task<CommentDto> Get(Guid id)
        {
            var comment = await _unitOfWork.Comments.Get(id);

            return _mapper.Map<CommentDto>(comment) ?? throw new CommentException("Comment doesn't exist");
        }

        public async Task<ICollection<CommentDto>> GetAll()
        {
            var comments = _mapper.Map<ICollection<CommentDto>>(await _unitOfWork.Comments.GetAll());

            return comments ?? throw new CommentException("List of commentaries is empty");
        }

        public async Task Update(Guid id, CommentDto commentDto)
        {
            var comment = await _unitOfWork.Comments.Get(id);
            if (comment == null)
                throw new CommentException("You can't update this comment.Because commentary doesn't exist!");

            var updateComment = _mapper.Map<Comment>(commentDto);
            updateComment.Id = id;
            updateComment.ArticleId = comment.ArticleId;
            await _unitOfWork.Comments.Update(updateComment);
        }
    }
}