using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OnlineLibrary.Domain.Identity;
using OnlineLibrary.Service.Interface;

namespace OnlineLibrary.Service.Implementation
{
    public class FileService(UserManager<LibraryMember> userManager) : IFileService
    {
        public async Task<bool> ImportUsersFromFile(IFormFile file)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using var stream = file.OpenReadStream();
            using var reader = ExcelReaderFactory.CreateReader(stream);

            do
            {
                while (reader.Read())
                {
                    var member = new LibraryMember
                    {
                        Id = reader.GetValue(0)?.ToString(),
                        Name = reader.GetValue(1)?.ToString(),
                        LastName = reader.GetValue(2)?.ToString(),
                        Email = reader.GetValue(3)?.ToString(),
                        UserName = reader.GetValue(3)?.ToString(),
                        EmailConfirmed = true
                    };

                    var memberResult = await userManager.CreateAsync(member, reader.GetValue(4)?.ToString());
                }
            } while (reader.NextResult());

            return true; 
        }
    }
}
