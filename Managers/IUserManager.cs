using MWTest.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MWTest.Managers
{
    public interface IUserManager
    {
        Task<User> UserWithIdAsync(int id);
        IEnumerable<User> AllUsers();
        User UserWithEmail(string email);
        User UserWithUsername(string username);
        Task AddUserAsync(User user);
    }
}
