﻿using Microsoft.AspNetCore.Identity;

namespace Shop.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string? DisplayName { get; set; }
        public Address Address { get; set; } = new Address();
    }
}
