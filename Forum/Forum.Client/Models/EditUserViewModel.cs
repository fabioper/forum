using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Client.Models
{
    public class EditUserViewModel
    {
        [Required]
        public IFormFile Avatar { get; set; }
    }
}
