using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestaurantInformation.Core.Interfaces;
using RestaurantInformation.Core.Responses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestaurantInformation.Infrastructure.Services
{
    public class RestaurantService : IRestaurant
    {
        private readonly ILogger<RestaurantService> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public RestaurantService(IHttpClientFactory clientFactory, ILogger<RestaurantService> logger, IConfiguration configuration)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public async Task<QueryByPostCodeResponse> GetRestaurantInfoByPostCode(string postCode)
        {
            try
            {
                var restaurantInfo = new QueryByPostCodeResponse();
                if (string.IsNullOrWhiteSpace(postCode))
                {
                    _logger.LogError("postCode is null {postCode}", postCode);

                    throw new ArgumentNullException(nameof(postCode));
                }


                var baseUrl = _configuration["JustEat:baseUrl"];
                var key = _configuration["JustEat:authorization"];
                var code = _configuration["JustEat:countryCode"];

                var request = new HttpRequestMessage(HttpMethod.Get,
                                   $"{baseUrl}restaurants/bypostcode//{postCode}");
                request.Headers.Add("Authorization", $"Bearer {key}");
                request.Headers.Add("Accept-Tenant", code);

                var client = _clientFactory.CreateClient();

                var stopWatch = Stopwatch.StartNew();
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string requestResponse = await response.Content.ReadAsStringAsync();

                    _logger.LogDebug($"ResponseTime from JustEat {stopWatch.Elapsed.TotalSeconds}");
                    _logger.LogDebug($"Successful response from JustEatApi {requestResponse}");

                    restaurantInfo = JsonConvert.DeserializeObject<QueryByPostCodeResponse>(requestResponse);
                    return restaurantInfo;
                }
                else
                {
                    _logger.LogDebug("Failed response from JustEatApi", response.Content);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while trying getRestaurantInfo");
                return null;
            }
        }
    }
}
