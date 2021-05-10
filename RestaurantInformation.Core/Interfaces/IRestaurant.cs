using RestaurantInformation.Core.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformation.Core.Interfaces
{
    public interface IRestaurant
    {
        Task<QueryByPostCodeResponse> GetRestaurantInfoByPostCode(string postCode);

    }
}
