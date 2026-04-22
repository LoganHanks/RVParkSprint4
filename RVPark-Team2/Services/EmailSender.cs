//This was entirely AI generated. How would I possibly know how to do this? :'(

using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using Microsoft.Extensions.Configuration; // Add this!

namespace RVPark_Team2.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        // This constructor "grabs" the appsettings.json data when the app starts
        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Pull the values from appsettings.json
            string senderEmail = _config["EmailSettings:SenderEmail"];
            string appPassword = _config["EmailSettings:AppPassword"];

            var emailToSend = new MimeMessage();
            emailToSend.From.Add(MailboxAddress.Parse(senderEmail));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

            using (var emailClient = new SmtpClient())
            {
                await emailClient.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await emailClient.AuthenticateAsync(senderEmail, appPassword);
                await emailClient.SendAsync(emailToSend);
                await emailClient.DisconnectAsync(true);
            }
        }
    }
}