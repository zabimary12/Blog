using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class ArticleDto
    {
        public Guid Id { get; set; }

        [Required] [StringLength(50)] public string Title { get; set; }

        [Required] [StringLength(50)] public string Text { get; set; }

        public ICollection<TagDto> Tags { get; set; }
        public Guid UserId { get; set; }
        public UserDto User { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
    }
}