using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wave.Domain.Commands
{
    public class LoginCommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
