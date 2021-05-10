using RestaurantInformation.Core.Interfaces;
using RestaurantInformation.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformation.Test.Mocks
{
    internal class MockRestaurantService : IRestaurant
    {
        public Task<QueryByPostCodeResponse> GetRestaurantInfoByPostCode(string postCode)
        {

            var cuisineType = new Cuisinetype()
            {
                Id = 1,
                IsTopCuisine = false,
                Name = "Sushi",
                SeoName = "Sushi"
            };

            var response = new QueryByPostCodeResponse()
            {
                MetaData = new MetaData()
                {
                    ResultCount = 1,
                },
                Restaurants = new Restaurant[]
                  {
                       new Restaurant ()
                       {
                             Name = "My restaurant",
                              Rating = new Rating()
                              {
                                   Average = 1,
                                    Count = 1,
                                     StarRating = 1,
                              },
                               CuisineTypes = new Cuisinetype[]
                               {
                                      cuisineType
                               },
                       },
                         new Restaurant ()
                       {
                             Name = "Tunde restuarant",
                              Rating = new Rating()
                              {
                                   Average = 1,
                                    Count = 1,
                                     StarRating = 1,
                              },
                               CuisineTypes = new Cuisinetype[]
                               {
                                      cuisineType
                               },
                       },
                  }
            };

            return Task.FromResult(response);
        }
    }
}
