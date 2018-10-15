using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HealthyYou_V2.Utils
{
    public class EmailSender
    {        
        private const String API_KEY = "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";

        public void Send(String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("admin@healthyyou.com.au", "New message from Healthy You");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }

    }
}