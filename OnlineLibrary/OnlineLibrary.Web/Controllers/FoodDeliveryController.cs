using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineLibrary.Domain.Domain;
using OnlineLibrary.Domain.FoodDeliveryDomain;
using OnlineLibrary.Service.Implementation;
using OnlineLibrary.Service.Interface;

namespace OnlineLibrary.Web.Controllers
{
    public class FoodDeliveryController : Controller
    {
        private readonly IFoodDeliveryService _foodDeliveryService;

        public FoodDeliveryController(IFoodDeliveryService foodDeliveryService)
        {
            _foodDeliveryService = foodDeliveryService;
        }

        public async Task<IActionResult> Index()
        {
            var restaurants = await _foodDeliveryService.GetAllActiveOrdersAsync();

            return View(restaurants);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var orders = await _foodDeliveryService.GetAllActiveOrdersAsync();
            var order = orders.FirstOrDefault(o => o.Id.Equals(id));

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}
