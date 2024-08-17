using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using OnlineLibraryAdmin.Web.Models;

namespace OnlineLibraryAdmin.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly string onlineLibraryUrl;
        public UserController(IConfiguration configuration)
        {
            onlineLibraryUrl = configuration["OnlineLibraryUrl"];
        }

        public async Task<IActionResult> Index()
        {
            var data = await GetAllMembers();
            
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> ExportAllUsers()
        {
            var members = await GetAllMembers();
            string fileName = "Users.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using var workbook = new XLWorkbook();
            IXLWorksheet worksheet = workbook.Worksheets.Add("Users");

            int counter = 1;

            foreach (var member in members)
            {
                worksheet.Cell(counter, 1).Value = member.Id.ToString();
                worksheet.Cell(counter, 2).Value = member.Name;
                worksheet.Cell(counter, 3).Value = member.LastName;
                worksheet.Cell(counter, 4).Value = member.Email;
                worksheet.Cell(counter, 5).Value = member.Password;

                counter++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, contentType, fileName);
        }

        public async Task ImportAllUsers(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("The file is null or empty.", nameof(file));
            }

            using var httpClient = new HttpClient();

            using var content = new MultipartFormDataContent();
            var fileStream = file.OpenReadStream();
            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);

            content.Add(fileContent, "file", file.FileName);

            var response = await httpClient.PostAsync(onlineLibraryUrl + "api/admin/ImportAllUsers", content);

            response.EnsureSuccessStatusCode();

            fileStream.Dispose();
        }

        private async Task<List<LibraryMember>> GetAllMembers()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(onlineLibraryUrl + "api/admin/Users");

            return await response.Content.ReadAsAsync<List<LibraryMember>>();
        }
    }
}
