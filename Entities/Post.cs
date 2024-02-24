using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }

        public DateTime DateCreated { get; set; }

    }
}
