using MWTest.Model;
using System;

namespace MWTest.Managers
{
    public class AuthenticationManager : IAuthenticationManager
    {
        public bool IsPasswordCorrect(User user, string password)
        {
            return user.Password == password;
        }
    }
}
