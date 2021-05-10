using RestaurantInformation.Core.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformation.Core.Interfaces
{
    public interface IRestaurantInfo
    {
        Task<RestaurantInfoResponse> GetRestaurantInfo(string postCode);
    }
}
