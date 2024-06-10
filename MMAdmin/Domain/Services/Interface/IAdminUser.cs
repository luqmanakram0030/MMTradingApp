using System;
using MMAdmin.Domain.Models;

namespace MMAdmin.Domain.Services.Interface
{
    public interface IAdminUser
    {
        
        Task<AdminUserModel> LoginAsync(string email);
        Task<bool> RegisterAsync(AdminUserModel user);
    }
}

