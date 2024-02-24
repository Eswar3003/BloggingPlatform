using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CommentDto
    {
        public int Id { get; set; }

        [Required]
        public int PostId { get; set; }

        public string Content { get; set; }

        [Required]
        public int UserId { get; set; }

        public DateTime DateCreated { get; set; }

    }
}
