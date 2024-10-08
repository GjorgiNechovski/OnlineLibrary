﻿using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Domain.Dto;
using OnlineLibrary.Domain.Exceptions;
using OnlineLibrary.Service.Interface;
using System.Security.Claims;

namespace OnlineLibrary.Web.Controllers
{
    public class OrderController(IOrderService orderService, IUserService userService) : Controller
    {
        public IActionResult CheckShoppingCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cart = orderService.GetBookCart(userId);
            var user = userService.GetFullUser(userId);

            return View(new ShoppingcartUserDto{
                books = cart,
                member = user
            });
        }

        [HttpPost]
        public IActionResult AddToCart(Guid bookId, int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                orderService.AddToCart(bookId, quantity, userId);
                return RedirectToAction("Index", "Books");
            }
            catch (ArgumentException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Details", "Books", new { id = bookId });
            }
            catch (NowEnoughBooksException e)
            {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Details", "Books", new { id = bookId });
            }
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

        [HttpPost]
        public IActionResult PayOrder()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = userService.GetFullUser(userId);
            user.IsStripedConnected = true;
            userService.Update(user);

            return View();
        }
    }
}
