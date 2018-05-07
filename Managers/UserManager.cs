using MWTest.Db;
using MWTest.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MWTest.Managers
{
    public class UserManager : IUserManager
    {
        private readonly MWTestDb _db;

        public UserManager(MWTestDb db)
        {
            _db = db;
        }

        public async Task<User> UserWithIdAsync(int id)
        {
            return await _db.Users.FindAsync(id);
        }

        public IEnumerable<User> AllUsers()
        {
            return _db.Users.ToArray();
        }

        public User UserWithEmail(string email)
        {
            return _db.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        public User UserWithUsername(string username)
        {
            return _db.Users.Where(u => u.Username == username).FirstOrDefault();
        }

        public async Task AddUserAsync(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }
    }
}
