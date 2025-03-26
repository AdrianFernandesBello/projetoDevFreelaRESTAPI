using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace DevFreela.Infrastructure.Notifications
{
    public interface  IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }

    public class EmailService : IEmailService
    {
        private readonly ISendGridClient _sendGrid;
        private readonly string _fromEmail;
        private readonly string _fromName;
        private readonly IConfiguration _configuration;
        public EmailService(ISendGridClient sendGrid, IConfiguration configuration)
        {
            _sendGrid = sendGrid;
            _fromEmail = configuration.GetValue<string>("SendGrid: FromEmail") ?? "";
            _fromName = configuration.GetValue<string>("SendGrid: FromName") ?? "" ;

        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var sendGridMessage = new SendGridMessage
            {
                From = new EmailAddress(_fromEmail),
                Subject = subject,
            };

            sendGridMessage.AddContent(MimeType.Text, message);
            sendGridMessage.AddTo(new EmailAddress(email));

            var response = await _sendGrid.SendEmailAsync(sendGridMessage);
        }   
    }
}
