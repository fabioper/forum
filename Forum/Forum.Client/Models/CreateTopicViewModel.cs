using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Client.Models
{
    public class CreateTopicViewModel
    {
        [MinLength(10)]
        [MaxLength(125)]
        public string Title { get; set; }

        [MinLength(10)]
        public string Content { get; set; }

        public long UserId { get; set; }

        [Required]
        public long CategoryId { get; set; }

        public SelectList CategoriesAvailable { get; set; }
    }
}
