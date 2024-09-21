using mediAPI.Abstractions.Interfaces;
using mediAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace mediAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _securityKey;
        private readonly UserManager<Account> _userManager;

        public TokenService(IConfiguration config, UserManager<Account> userManager)
        {
            _config = config;
            _userManager = userManager;
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
        }
        public async Task<string> CreateToken(Account accountUser)
        {
            var credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha512Signature);


            // add accountId, email,username and roles in the token for retrieval later
            var cliams = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, accountUser.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, accountUser.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, accountUser.UserName)
            };

            var roles = await _userManager.GetRolesAsync(accountUser);

            cliams.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(cliams),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
