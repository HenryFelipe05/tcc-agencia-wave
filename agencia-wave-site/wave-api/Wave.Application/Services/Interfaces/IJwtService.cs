using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wave.Application.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string userRole);
    }
}
