using System.Net.Mail;
using System.Net;

namespace BusinessLogicLayer
{
    public class EmailServiceLogic 
    {
        private const string SmtpServer = "smtp.gmail.com";
        private const int SmtpPort = 587;
        private const string SenderEmail = "alexander.guacan2003@gmail.com";
        private const string SenderPassword = "ebkyofzshganplna";
        private const string SenderName = "SISTEMA DE PRODUCTOS CHIDOS";

        public async Task SendEmailAsync(string recipientEmail, string subject, string body)
        {
            using (var smtp = new SmtpClient(SmtpServer, SmtpPort))
            {
                smtp.Credentials = new NetworkCredential(SenderEmail, SenderPassword);
                smtp.EnableSsl = true;

                var mail = new MailMessage
                {
                    From = new MailAddress(SenderEmail, SenderName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mail.To.Add(recipientEmail);

                await smtp.SendMailAsync(mail);
            }
        }
    }
}
