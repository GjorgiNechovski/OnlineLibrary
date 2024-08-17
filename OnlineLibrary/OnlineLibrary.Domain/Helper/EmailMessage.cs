namespace OnlineLibrary.Domain.Helper
{
    public class EmailMessage(string? mailTo, string? subject, string? content)
    {
        public string? MailTo { get; set; } = mailTo;
        public string? Subject { get; set; } = subject;
        public string? Content { get; set; } = content;
    }
}
