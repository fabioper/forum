using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Forum.Domain.Entities
{
    public class Section : Entity
    {
        public Section()
        {
            Categories = new HashSet<Category>();
        }

        [Required]
        public string Name { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
