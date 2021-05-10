using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformation.Core.Interfaces
{
    public interface ICache
    {
        Task<bool> AddItem(string key, string data, double time);
        Task<string> GetItem(string key);
    }
}
