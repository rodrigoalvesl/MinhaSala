using System;
using System.Threading.Tasks;

namespace MinhaSala.Interfaces
{
    public interface IAuthenticationService
    {
        //IsSignIn();
        Task<bool> CreateUser(string username, string email, string password);
        void SignOut();
        Task<string> SignIn(string email, string password);
    }
}

