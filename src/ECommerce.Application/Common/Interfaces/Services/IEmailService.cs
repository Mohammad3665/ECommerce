namespace ECommerce.Application.Common.Interfaces.Services;

/// <summary>
/// Defines a service for sending emails.
/// </summary>
/// <remarks>
/// This interface is designed for sending simple emails.
/// Implementations can use SMTP protocols, cloud service REST APIs like SendGrid, or other methods.
/// </remarks>
public interface IEmailService
{
    /// <summary>
    /// Sends an email asynchronously.
    /// </summary>
    /// <param name="to">
    /// The recipient's email address.
    /// <para>Example: "user@example.com"</para>
    /// </param>
    /// <param name="subject">
    /// The subject line of the email.
    /// <para>Example: "Account Confirmation"</para>
    /// </param>
    /// <param name="body">
    /// The content of the email. Can be plain text or HTML.
    /// <para>Example: "&lt;h1&gt;Hello&lt;/h1&gt;&lt;p&gt;Please click the link...&lt;/p&gt;"</para>
    /// </param>
    Task SendEmailAsync(string toEmail, string subject, string body);
}
