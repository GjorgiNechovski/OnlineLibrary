using OnlineLibrary.Domain.FoodDeliveryDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Service.Interface
{
    public interface IFoodDeliveryService
    {
        Task<List<Order>> GetAllActiveOrdersAsync();
    }
}
