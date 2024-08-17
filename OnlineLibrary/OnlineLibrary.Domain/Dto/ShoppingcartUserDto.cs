using OnlineLibrary.Domain.Domain;
using OnlineLibrary.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Domain.Dto
{
    public class ShoppingcartUserDto
    {
        public List<BookInCart> books;
        public LibraryMember member;
    }
}
