using MWTest.Model;
using System;

namespace MWTest.Managers
{
    public interface IUserManager
    {
        User UserWithUsername(string username);
    }
}
