using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Email
{
    public class EmailService : IEmailService
    {
        public void SendEmailWithAttachment(string recipient, MemoryStream stream, string fileName, string contentType)
        {
            MailMessage mail = new MailMessage();

            var attachment = new Attachment(stream, new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Text.Xml));
            attachment.ContentDisposition.FileName = "interactions.xml";
            mail.Attachments.Add(attachment);

            mail.Subject = "";

            mail.To.Add(recipient);
            
           // Connect to mail servoce and send.
        }
    }
}
