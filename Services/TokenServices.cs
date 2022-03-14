using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace API.Services
{
    public class TokenServices : ITokenServices
    {

        private readonly SymmetricSecurityKey _key;

        public TokenServices(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(AppUsers users)
        {

            var Clamis = new List<Claim>{

                new Claim(JwtRegisteredClaimNames.NameId, users.UserName)

            };

                var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);
                var tokenDesc = new SecurityTokenDescriptor{
                        Subject = new ClaimsIdentity(Clamis),
                        Expires=DateTime.Now.AddDays(7),
                        SigningCredentials = creds

                };

                var TokenHandeler = new JwtSecurityTokenHandler();

                var token = TokenHandeler.CreateToken(tokenDesc);

                return TokenHandeler.WriteToken(token);

        }
    }
}