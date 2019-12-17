using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.API.ViewModels
{
    public class EditUserViewModel
    {
        public IFormFile Avatar { get; set; }
    }
}
