using Forum.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public User UserProfile { get; set; }
    }
}
