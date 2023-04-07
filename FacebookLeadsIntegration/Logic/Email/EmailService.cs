using System.Net.Mail;

namespace Logic.Email
{
    public class EmailService : IEmailService
    {
        public void SendEmailWithAttachment(IEnumerable<string> recipients, MemoryStream stream, string fileName, string contentType)
        {
            MailMessage mail = new MailMessage();

            recipients.ToList().ForEach(r => mail.To.Add(r));
            var attachment = new Attachment(stream, new System.Net.Mime.ContentType(contentType));
            attachment.ContentDisposition.FileName = "interactions.xml";
            mail.Attachments.Add(attachment);

            mail.Subject = "";
           // Connect to mail servoce and send.
        }
    }
}
