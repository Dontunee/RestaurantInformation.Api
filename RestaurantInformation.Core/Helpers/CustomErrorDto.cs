using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantInformation.Core.Helpers
{
    public class CustomErrorDto
    {
        public string Error { get; set; }
        public int StatusCode { get; set; }
        public string RequestId { get; set; }
    }
}
