using Moq;
using RestaurantInformation.Core.Interfaces;
using RestaurantInformation.Core.Responses;
using RestaurantInformation.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Logging;
using RestaurantInformation.Infrastructure.Services;
using StackExchange.Redis;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http;
using Moq.Contrib.HttpClient;
using RestaurantInformation.Test.Mocks;

namespace RestaurantInformation.Test
{
    public class RestaurantInfoServiceTest
    {
        // 1. Create moq object
        IRestaurant restaurant;
        ILogger<RestaurantInfoService> logger;
        ICache cache;
        IHttpClientFactory factory;
        
        private void SetupMocks()
        {
            // 1. Create moq object
            var restarantInfoMoq  = new Mock<IRestaurant>();


            //2. Setup the returnables

            var moqRestaurant = new MockRestaurantService();


            // 3. Assign to Object when needed
            var handler = new Mock<HttpMessageHandler>();
            factory = handler.CreateClientFactory();


            cache = new InMemoryCacheService(new MemoryCache(new MemoryCacheOptions()));
            logger = new Mock<ILogger<RestaurantInfoService>>().Object;
            restaurant = moqRestaurant;
        
        } 



        [Fact]
        public async Task GetRestaurantInfo_WhenCalled_ReturnResponse()
        {

            //Arrange the resources
            SetupMocks();
            var service = new RestaurantInfoService(logger, restaurant, cache);
            string initial = "sw1A";

            //Act on the functionality
            var response = await service.GetRestaurantInfo(initial);

            //Assert the result against the expected
            Assert.NotNull(response);

        }

        [Fact]
        public async Task GetRestaurantInfo_WhenCalled_ReturnsRestaurantList()
        {
            //Arrange the resources
            SetupMocks();
            var service = new RestaurantInfoService(logger, restaurant, cache);
            string initial = "sw1A";

            //Act on the functionality
            var response = await service.GetRestaurantInfo(initial);

            //Assert the result against the expected
            Assert.True(response.Datas.Count > 1);
        }

      
        [Fact] 
        public async Task GetRestaurantInfo_WhenCalledWithEmptyPostCode_ReturnsNul()
        {
            //Arrange the resources
            SetupMocks();
            var service = new RestaurantInfoService(logger, restaurant, cache);

            //Act on the functionality
            var response = await service.GetRestaurantInfo(null);

            //Assert the result against the expected
            Assert.Null(response);
        }

        [Fact]
        public async Task GetRestaurantInfo_WhenCalled_ReturnsNameOfRestaurant()
        {
            //Arrange the resources
            SetupMocks();
            var service = new RestaurantInfoService(logger, restaurant, cache);
            string initial = "sw1A";

            //Act on the functionality
            var response = await service.GetRestaurantInfo(initial);

            //Assert the result against the expected
            Assert.True(response.Datas.Exists(x => x.Name != null));
        }

        [Fact]
        public async Task GetRestaurantInfo_WhenCalled_ReturnsRatingOfRestaurant()
        {
            //Arrange the resources
            SetupMocks();
            var service = new RestaurantInfoService(logger, restaurant, cache);
            string initial = "sw1A";

            //Act on the functionality
            var response = await service.GetRestaurantInfo(initial);

            //Assert the result against the expected
            Assert.True(response.Datas.Exists(x => x.Rating != null));
        }
    }
}
