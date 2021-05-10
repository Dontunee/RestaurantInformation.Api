using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestaurantInformation.Core.Interfaces;
using RestaurantInformation.Core.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace RestaurantInformation.Infrastructure.Services
{
    public class RestaurantInfoService : IRestaurantInfo
    {
        private readonly ILogger<RestaurantInfoService> _logger;
        private IRestaurant _restaurant;
        private ICache _cache;


        public RestaurantInfoService(ILogger<RestaurantInfoService> logger, IRestaurant restaurant, ICache cache)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _restaurant = restaurant ?? throw new ArgumentNullException(nameof(restaurant));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }
        public async Task<RestaurantInfoResponse> GetRestaurantInfo(string postCode)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(postCode))
                {
                    _logger.LogError("postCode is null {postCode}", postCode);
                    return null;
                }
                var response = new RestaurantInfoResponse();
                //check if item exists in redis
                var cache = await GetFromCache(postCode);
                if (cache != null)
                {
                    return response = cache;
                }

                //Get info from info provider
                var restaurant = await _restaurant.GetRestaurantInfoByPostCode(postCode);
                if (restaurant != null && restaurant.MetaData.ResultCount > 0)
                {
                    response.Datas.AddRange(restaurant.Restaurants.Select(x => new InfoData()
                    {
                        Name = x.Name,
                        Rating = x.Rating,
                        FoodTypes = x.CuisineTypes
                    }).ToList());

                    await _cache.AddItem(postCode, JsonConvert.SerializeObject(response), TimeSpan.FromMinutes(30).TotalSeconds);

                    return response;
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured in GetRestaurantInfo");
                return null;
            }
        }


        public async Task<RestaurantInfoResponse> GetFromCache(string postCode)
        {
            try
            {
                var getCache = await _cache.GetItem(postCode);
                if (getCache != null)
                {
                    var response = JsonConvert.DeserializeObject<RestaurantInfoResponse>(getCache);
                    return response;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured in GetFromCache");
                return null;
            }
        }
    }
}
