using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using PC.Core.Auth.WebApi.Interfaces;
using PC.Core.Auth.WebApi.Models;
using System.Security.Principal;
using System.Collections.Generic;
using PC.Core.Auth.Configuration;

namespace PC.Core.Auth.WebApi.Services
{
	public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IssuerOptions _jwtOptions;
        
        public UserService(IUserRepository userRepository, IOptions<IssuerOptions> jwtOptions)
        {
            _userRepository = userRepository;
            _jwtOptions = jwtOptions.Value;
        }

        public User IsValid(string userName, string userPassword, out string token, out int minutesExpired)
        {
            var user = _userRepository.GetUser(userName, userPassword);
            token = CreateToken(user);
            minutesExpired = _jwtOptions.ValidFor.Minutes;
            return user;
        }

        private string CreateToken(User user)
        {
            if (user == null)
            {
                return null;
            }
            var identityClaim = GetClaimsIdentity(user);
            var claims = new List<Claim>
            {
                new Claim(SecurityConfiguration.CLAIM_NAME, user.Name),
				new Claim(SecurityConfiguration.CLAIM_USERNAME, user.UserName)
			};
			claims.AddRange(identityClaim.FindAll(SecurityConfiguration.CLAIM_PERMISSION));

			var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                expires: _jwtOptions.Expiration,
				notBefore: _jwtOptions.NotBefore,
				signingCredentials: _jwtOptions.SigningCredentials,
				claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private ClaimsIdentity GetClaimsIdentity(User user)
        {
			if (user.UserName == "MickeyMouse" && user.Password == "MickeyMouseIsBoss123")
			{
				return new ClaimsIdentity(new GenericIdentity(user.UserName, "Token"), new[]
				{
					new Claim(SecurityConfiguration.CLAIM_PERMISSION, "IAmDisney"),
					new Claim(SecurityConfiguration.CLAIM_PERMISSION, "IAmBoss")
				});
			}

			if (user.UserName == "NotMickeyMouse" && user.Password == "NotMickeyMouseIsBoss123")
			{
				return new ClaimsIdentity(new GenericIdentity(user.UserName, "Token"), new [] 
				{
					new Claim(SecurityConfiguration.CLAIM_PERMISSION, "IAmDisney")
				});
			}

			return null;
        }
    }
}