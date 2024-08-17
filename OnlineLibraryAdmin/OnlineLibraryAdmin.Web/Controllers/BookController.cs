using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using OnlineLibraryAdmin.Web.Models;

namespace OnlineLibraryAdmin.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly string onlineLibraryUrl;
        public BookController(IConfiguration configuration)
        {
            onlineLibraryUrl = configuration["OnlineLibraryUrl"];
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(onlineLibraryUrl + $"api/admin/allBooks");

            var data = await response.Content.ReadAsAsync<List<Book>>();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> ExportAllUsers(string id)
        {
            var books = await GetRentedBooksForUser(id);
            string fileName = $"BooksFor_{id}.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using var workbook = new XLWorkbook();
            IXLWorksheet worksheet = workbook.Worksheets.Add($"Rented Books for {id}");

            worksheet.Cell(1, 1).Value = "Id";
            worksheet.Cell(1, 2).Value = "Title";
            worksheet.Cell(1, 3).Value = "Author";
            worksheet.Cell(1, 4).Value = "Quantity";
            worksheet.Cell(1, 5).Value = "Date rented";
            worksheet.Cell(1, 6).Value = "Returned";
            worksheet.Cell(1, 7).Value = "Due Date";
            int counter = 2;

            foreach (var book in books)
            {
                worksheet.Cell(counter, 1).Value = book.Id.ToString();
                worksheet.Cell(counter, 2).Value = book.Book.Title;
                worksheet.Cell(counter, 3).Value = book.Book.Author.GetFullName();
                worksheet.Cell(counter, 4).Value = book.Quantity;
                worksheet.Cell(counter, 5).Value = book.DateRented;
                worksheet.Cell(counter, 6).Value = (book.DateReturned !=null) ? book.DateReturned.ToString() : false.ToString();
                worksheet.Cell(counter,7).Value = (book.DueDate == null) ? "Returned" : book.DueDate.ToString();
                counter++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, contentType, fileName);
        }

        public async Task<IActionResult> RentedBooksForUser(string id)
        {
            return View(await GetRentedBooksForUser(id));
        }

        private async Task<List<RentedBook>> GetRentedBooksForUser(string id)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(onlineLibraryUrl + $"api/admin/RentedBookForUser/{id}");

            return await response.Content.ReadAsAsync<List<RentedBook>>();
        }
    }
}
