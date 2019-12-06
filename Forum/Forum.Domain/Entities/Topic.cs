using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Forum.Domain.Entities
{
    public class Topic : Entity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public long UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
