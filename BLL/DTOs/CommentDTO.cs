using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class CommentDto
    {
        public Guid Id { get; set; }

        [Required] [StringLength(50)] public string Text { get; set; }

        public Guid ArticleId { get; set; }
        public ArticleDto Article { get; set; }
        public UserDto User { get; set; }
        public Guid UserId { get; set; }
    }
}