namespace Logic.Email
{
    public interface IEmailService
    {
        void SendEmailWithAttachment(string recipient, MemoryStream stream, string fileName, string contentType);
    }
}
