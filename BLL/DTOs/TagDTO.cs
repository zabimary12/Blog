using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class TagDto
    {
        public Guid Id { get; set; }

        [Required] [StringLength(50)] public string Text { get; set; }

        public ICollection<ArticleDto> Articles { get; set; }
    }
}