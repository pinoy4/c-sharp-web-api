using MWTest.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MWTest.Managers
{
    public interface IUserManager
    {
        Task<User> UserWithIdAsync(int id);
        IEnumerable<User> AllUsers();
        User UserWithUsername(string username);
    }
}
