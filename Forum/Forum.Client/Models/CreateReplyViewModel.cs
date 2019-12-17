using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Client.Models
{
    public class CreateReplyViewModel
    {
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public long TopicId { get; set; }

        public long UserId { get; set; }
    }
}
