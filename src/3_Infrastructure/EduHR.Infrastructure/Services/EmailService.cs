using EduHR.Application.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System; // InvalidOperationException için eklendi
using System.Threading.Tasks;

namespace EduHR.Infrastructure.Services;

/// <summary>
/// Implements the IEmailService for sending emails.
/// </summary>
public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        // appsettings.json'dan ayarları oku ve null olup olmadıklarını kontrol et.
        var fromAddress = _configuration["EmailSettings:From"] 
            ?? throw new InvalidOperationException("Email 'From' address is not configured.");
        var smtpServer = _configuration["EmailSettings:SmtpServer"] 
            ?? throw new InvalidOperationException("Email 'SmtpServer' is not configured.");
        var portString = _configuration["EmailSettings:Port"] 
            ?? throw new InvalidOperationException("Email 'Port' is not configured.");
        var username = _configuration["EmailSettings:Username"] 
            ?? throw new InvalidOperationException("Email 'Username' is not configured.");
        var password = _configuration["EmailSettings:Password"] 
            ?? throw new InvalidOperationException("Email 'Password' is not configured.");

        // Port değerini güvenli bir şekilde parse et.
        if (!int.TryParse(portString, out var port))
        {
            throw new InvalidOperationException($"Email 'Port' value '{portString}' is not a valid integer.");
        }

        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(fromAddress));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(smtpServer, port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(username, password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}