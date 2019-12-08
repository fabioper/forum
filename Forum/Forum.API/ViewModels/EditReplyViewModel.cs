using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.API.ViewModels
{
    public class EditReplyViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public long TopicId { get; set; }

        [Required]
        public long UserId { get; set; }
    }
}
