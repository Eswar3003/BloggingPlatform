using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "PostId is required")]
        public int PostId { get; set; }

        public string Content { get; set; }

        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }

        public DateTime DateCreated { get; set; }

    }
}
