using MWTest.Model;
using System;

namespace MWTest.Managers
{
    public interface IAuthenticationManager
    {
        bool IsPasswordCorrect(User user, string password);
    }
}
