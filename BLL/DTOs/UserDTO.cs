using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Enums;

namespace BLL.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required] [StringLength(50)] public string Password { get; set; }

        [Required] [StringLength(50)] public string Name { get; set; }

        [Required] [StringLength(50)] public string Surname { get; set; }

        [Required] public Role Role { get; set; }

        public ICollection<ArticleDto> Articles { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
    }
}