using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantInformation.Core.Interfaces;
using RestaurantInformation.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantInformation.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RestaurantInfoController : ControllerBase
    {
        private readonly ILogger<RestaurantInfoController> _logger;
        private IRestaurantInfo _restaurantInfo;
        public RestaurantInfoController(ILogger<RestaurantInfoController> logger, IRestaurantInfo restaurantInfo)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _restaurantInfo = restaurantInfo ?? throw new ArgumentNullException(nameof(restaurantInfo));
        }


        [HttpGet("{postCode}")]
        public async Task<ActionResult<RestaurantInfoResponse>> Get(string postCode)
        {
            try
            {
                var response = await _restaurantInfo.GetRestaurantInfo(postCode);
                if (response is null)
                {
                    return NotFound(new RestaurantInfoResponse()
                    {
                        Error = new Error()
                        {
                            ErrorCount = 1,
                            Message = "No data available for the postCode"
                        }
                    });
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in Get");
                return NotFound(new RestaurantInfoResponse()
                {
                    Error = new Error()
                    {
                        ErrorCount = 1,
                        Message = "No data available for the postCode"
                    }
                });
            }
        }
    }
}
