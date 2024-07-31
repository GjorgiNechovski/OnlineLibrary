using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Domain.Helper
{
    public class MailSettings(string smtpServer, int smtpServerPort, string emailDisplayName, string sendersName, string smtpUserName, string smtpPassword, bool enableSsl)
    {
        public string SmtpServer { get; set; } = smtpServer;
        public int SmtpServerPort { get; set; } = smtpServerPort;
        public string EmailDisplayName { get; set; } = emailDisplayName;
        public string SendersName { get; set; } = sendersName;
        public string SmtpUserName { get; set; } = smtpUserName;
        public string SmtpPassword { get; set; } = smtpPassword;
        public bool EnableSsl { get; set; } = enableSsl;
    }
}
