using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Domain.Helper;
using OnlineLibrary.Service.Interface;
using System.Security.Claims;

namespace OnlineLibrary.Web.Controllers
{
    public class OrderController(IOrderService orderService) : Controller
    {
        public async Task<IActionResult> CheckShoppingCartAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cart = orderService.GetBookCart(userId);

            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(Guid bookId, int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            orderService.AddToCart(bookId, quantity, userId);

            return RedirectToAction("Index", "Books");
        }

        public IActionResult DeleteFromShoppingCart(Guid bookId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            orderService.DeleteBookFromCart(bookId, userId);

            return RedirectToAction("CheckShoppingCart", "Order");
        }

        public IActionResult RentBooks()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            orderService.RentBooks(userId);

            return RedirectToAction("Index", "Books");
        }
    }
}
