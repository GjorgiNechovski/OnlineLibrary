using Microsoft.AspNetCore.Http;

namespace OnlineLibrary.Service.Interface
{
    public interface IFileService
    {
        Task<bool> ImportUsersFromFile(IFormFile file);
    }
}
