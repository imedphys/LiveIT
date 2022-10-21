using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastructure.Entities
{
    public partial class User
    {
        public int UserId { get; set; }
        public string IdentityId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
