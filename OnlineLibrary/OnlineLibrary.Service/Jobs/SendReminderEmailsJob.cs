using OnlineLibrary.Domain.Helper;
using OnlineLibrary.Service.Interface;
using Quartz;

namespace OnlineLibrary.Service.Jobs
{
    public class SendReminderEmailsJob(IUserService userService, IEmailService emailService) : IJob
    {
        private readonly IUserService userService = userService;
        private readonly IEmailService emailService = emailService;

        public async Task Execute(IJobExecutionContext context)
        {
            var users = userService.GetAll();

            foreach (var user in users)
            {
                var rentedBooks = user.RentedBooks
                    ?.Where(rb => rb.DueDate.Date == DateTime.Today.AddDays(1) && rb.DateReturned == null)
                    .ToList();

                if (rentedBooks != null && rentedBooks.Any())
                {
                    var emailMessage = new EmailMessage(
                     user.Email,
                     "Reminder: Book Return Due Tomorrow",
                     $"Dear {user.Name},\n\nYou have books due to be returned tomorrow. Please make sure to return them on time."
                 );

                    await emailService.SendEmailAsync(emailMessage);
                }
            }
        }
    }
}
