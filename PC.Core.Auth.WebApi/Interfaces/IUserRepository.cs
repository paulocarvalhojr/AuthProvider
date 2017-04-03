using PC.Core.Auth.WebApi.Models;

namespace PC.Core.Auth.WebApi.Interfaces
{
    public interface IUserRepository
    {
         User GetUser (string userName, string userPassword);
    }
}