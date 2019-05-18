using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace TGV_Eindproject_WebDevelopment.UI.Services
{
    public class EmailSender //: IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = new MailMessage();

            mail.To.Add(new MailAddress(email));
            mail.From = new MailAddress("order.tgnv@gmail.com");

            mail.Subject = subject;

            mail.Body = message;

            mail.IsBodyHtml = true;

            try
            {
                using (var smtp = new SmtpClient("smtp.gmail.com"))
                {
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential("order.tgnv@gmail.com", "Azerty-123");
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
