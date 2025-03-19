using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Wave.Domain.Entities;

namespace Wave.Application.Services.Interfaces
{
    public interface IAutenticationService
    {
        Task<IdentityResult> RegisterUserAsync(Usuario user, string password);
        Task<string> LoginUserAsync(string userName, string password);
        Task LogoutUserAsync();
    }
}
