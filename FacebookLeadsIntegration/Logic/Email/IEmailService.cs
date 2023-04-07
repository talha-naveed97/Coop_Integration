namespace Logic.Email
{
    public interface IEmailService
    {
        void SendEmailWithAttachment(IEnumerable<string> recipients, MemoryStream stream, string fileName, string contentType);
    }
}
