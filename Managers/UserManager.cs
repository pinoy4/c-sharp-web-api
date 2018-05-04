using MWTest.Db;
using MWTest.Model;
using System;
using System.Linq;

namespace MWTest.Managers
{
    public class UserManager : IUserManager
    {
        private readonly MWTestDb _db;

        public UserManager(MWTestDb db)
        {
            _db = db;
        }

        public User UserWithUsername(string username)
        {
            return _db.Users.Where(u => u.Username == username).FirstOrDefault();
        }
    }
}
