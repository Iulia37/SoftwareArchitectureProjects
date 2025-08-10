using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Models;

namespace TaskManager.Domain.Interfaces
{
    public interface IAuthService
    {
        User? Register(User user);
        string? Login(User user);
    }
}
