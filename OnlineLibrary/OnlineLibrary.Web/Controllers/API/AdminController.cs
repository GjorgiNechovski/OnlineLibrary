using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Domain.Domain;
using OnlineLibrary.Domain.Dto;
using OnlineLibrary.Service.Interface;

namespace OnlineLibrary.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(IBookService bookService, IUserService userService, IFileService fileService) : ControllerBase
    {
        [HttpGet("RentedBookForUser/{userId}")]
        public List<RentedBook> GetRentedBooksForUser(string userId)
        {
            return bookService.GetRentedBooksForUser(userId);
        }

        [HttpGet("AllBooks")]
        public List<Book> GetAllBooksInLibrary(string? title, Guid? authorId, bool? inStock)
        {
            return bookService.GetAllBooksFiltered(title, authorId, inStock).ToList();
        }

        [HttpGet("Users")]
        public List<LibraryMemberDto> GetAllUsers()
        {
            return userService.GetAllLibraryMembersDto().ToList();
        }

        [HttpPost("[action]")]
        public async Task<bool> ImportAllUsers(IFormFile file)
        {
            return await fileService.ImportUsersFromFile(file);
        }
    }
}
