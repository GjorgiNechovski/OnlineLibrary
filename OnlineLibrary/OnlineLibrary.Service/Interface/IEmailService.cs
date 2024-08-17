using OnlineLibrary.Domain.Helper;
using System.Net.Mail;

namespace OnlineLibrary.Service.Interface
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage email);
    }
}
