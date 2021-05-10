using Microsoft.AspNetCore.Mvc;
using RestaurantInformation.Core.Requests;
using RestaurantInformation.Core.Responses;
using RestaurantInformation.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantInformation.Api.Controllers
{
    [ApiController]
    public class AuthorizationController : Controller
    {
        private readonly JwtToken _jwtToken;

        public AuthorizationController(JwtToken jwtToken)
        {
            _jwtToken = jwtToken;
        }

        [Route("api/v1/GenerateToken")]
        [HttpPost]
        public IActionResult Auth([FromBody] UserModel user)
        {
            var token = _jwtToken.GenerateJSONWebToken(user);

            return Ok(new TokenResponse()
            {
                Status = token.Status,
                Token = token.Token
            });
        }
    }
}
