using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RestaurantInformation.Core.Requests;
using RestaurantInformation.Core.Responses;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace RestaurantInformation.Infrastructure.Services
{
    public class JwtToken
    {
        private readonly IConfiguration _configuration;

        public JwtToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenResponse GenerateJSONWebToken(UserModel userInfo)
        {
            if (!AutheticationUser(userInfo))
                return new TokenResponse()
                {
                    Status = "Invalid User Details",
                    Token = null
                };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Audience"],
              null,
              expires: DateTime.Now.AddMinutes(5),
              signingCredentials: credentials);

            return new TokenResponse()
            {
                Status = "Successful",
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        private bool AutheticationUser(UserModel userInfo)
        {
            var verifyUser = userInfo.Username == _configuration["Jwt:User"] &&
                 userInfo.Password == _configuration["Jwt:Password"];
            return verifyUser == true ? true : false;
        }
    }
}
