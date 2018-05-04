using MWTest.Model;
using System;
using System.Threading.Tasks;

namespace MWTest.Auth
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(User user);
    }
}
