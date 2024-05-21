using System;
namespace MMAdmin.Domain.Models
{
	public class AdminUserModel
	{
        public Guid UserId { get; set; }

        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        
    }
}

