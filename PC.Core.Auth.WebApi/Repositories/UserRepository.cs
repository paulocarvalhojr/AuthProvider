using PC.Core.Auth.WebApi.Interfaces;
using PC.Core.Auth.WebApi.Models;

namespace PC.Core.Auth.WebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        public User GetUser(string userName, string userPassword)
        {
            if ((userName.Equals("MickeyMouse") && userPassword.Equals("MickeyMouseIsBoss123"))
                || (userName.Equals("NotMickeyMouse") && userPassword.Equals("NotMickeyMouseIsBoss123")))
            {
                return new User
                {
                    UserName = userName,
                    Password = userPassword,
                    Name = userName
                };
            }
            return null;
        }
    }
}