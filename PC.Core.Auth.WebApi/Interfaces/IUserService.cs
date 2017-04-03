using PC.Core.Auth.WebApi.Models;

namespace PC.Core.Auth.WebApi.Interfaces
{
    public interface IUserService
    {
        User IsValid(string userName, string userPassword, out string token, out int minutesExpired);
    }
}