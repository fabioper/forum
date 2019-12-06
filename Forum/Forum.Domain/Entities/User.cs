using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Forum.Domain.Entities
{
    public class User : Entity
    {
        public User() => Topics = new HashSet<Topic>();

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public ICollection<Topic> Topics { get; set; }
    }
}
