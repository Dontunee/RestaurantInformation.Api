using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantInformation.Core.Responses
{
    public class RestaurantInfoResponse
    {

        public List<InfoData> Datas { get; set; }


        public Error Error { get; set; }

        public RestaurantInfoResponse()
        {
            Datas = new List<InfoData>();
        }
    }

    public class InfoData
    {
        public string Name { get; set; }
        public Rating Rating { get; set; } 
        public Cuisinetype[] FoodTypes { get; set; } 
    }
}
