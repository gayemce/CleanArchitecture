using CleanArchitecture.Application.Services;
using GenericEmailService;
using System.Net.Mail;

namespace CleanArchitecture.Infrastructure.Services;
public sealed class MailService : IMailService
{
    public async Task SendMailAsync(List<string> emails, string subject, 
        string body, List<Attachment>? attachments = null)
    {
        SendEmailModel sendEmailModel = new()
        {
            Body = body,
            Emails = emails,
            Email = "",
            Html = true,
            Password = "",
            Port = 587,
            Smtp = "",
            SSL = true,
            Subject = subject,
        };

        await EmailService.SendEmailAsync(sendEmailModel);
    }
}
