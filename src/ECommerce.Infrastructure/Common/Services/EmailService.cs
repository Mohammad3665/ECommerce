using ECommerce.Application.Common.Interfaces.Services;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace ECommerce.Infrastructure.Common.Services;

public class EmailService(IConfiguration configuration) : IEmailService
{
    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(
            configuration["SmtpSettings:SenderName"], 
            configuration["SmtpSettings:SenderEmail"]!));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;

        var bodyBuilder = new BodyBuilder { HtmlBody = body };
        email.Body = bodyBuilder.ToMessageBody();

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(
            configuration["SmtpSettings:Host"]!, 
            int.Parse(configuration["SmtpSettings:Port"]!), 
            MailKit.Security.SecureSocketOptions.None);

        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
