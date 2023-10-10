using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace PersonalLibrary.Utilities.Managers;

public class EmailManager
{
    private readonly IConfiguration _configuration;

    public EmailManager(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmail(string resetPasswordLink, string toEmailAddress)
    {
        var host = _configuration["EmailSettings:Host"];
        var fromEmail = _configuration["EmailSettings:FromEmail"];
        var fromPassword = _configuration["EmailSettings:FromPassword"];

        var smtpClient = new SmtpClient();
        
        smtpClient.Host = host!;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Port = 587;
        smtpClient.Credentials = new NetworkCredential(fromEmail, fromPassword);
        smtpClient.EnableSsl = true;
        
        var message = new MailMessage();
        
        message.From = new MailAddress(fromEmail!);
        message.To.Add(toEmailAddress);
        message.Subject = "Reset Password";
        message.Body = $"To reset your password, <a href='{resetPasswordLink}'>click here</a>";
        message.IsBodyHtml = true;
        
        await smtpClient.SendMailAsync(message);
    }
}