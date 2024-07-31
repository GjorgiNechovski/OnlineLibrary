using OnlineLibrary.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Service.Interface
{
    public interface IOrderService
    {
        void AddToCart(Guid bookId, int quantity, string userId);
        List<BookInCart> GetBookCart(string userId);
        void DeleteBookFromCart(Guid bookId, string userId);
        void RentBooks(string? userId);
    }
}
