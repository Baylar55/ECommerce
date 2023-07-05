using EcommerceAPI.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Infrastructure.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
            => await SendMailAsync(new[] { to }, subject, body, isBodyHtml);

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = isBodyHtml;
            foreach (var to in tos)
                mail.To.Add(to);
            mail.From = new(_configuration["Mail:Username"], "ECommerce", Encoding.UTF8);

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Host = _configuration["Mail:Host"];
            await smtp.SendMailAsync(mail);
        }

        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {
            StringBuilder mail = new();
            mail.AppendLine("Hello<br>If you have requested a new password, you can update your password from the link below.<br><strong><a target=\"_blank\" href=\"");
            mail.AppendLine(_configuration["AngularClientUrl"]);
            mail.AppendLine("/update-password/");
            mail.AppendLine(userId);
            mail.AppendLine("/");
            mail.AppendLine(resetToken);
            mail.AppendLine("\">Click for new password request...</a></strong><br><br><span style=\"font-size:12px;\">NOTE : If this request has not been fulfilled by you, please do not take this e-mail seriously.</span><br>Best Regards...<br><br><br>E-Commerce");

            await SendMailAsync(to, "Password reset request", mail.ToString());
        }

        public async Task SendCompletedOrderMailAsync(string to, string username, string orderCode, DateTime orderDate)
        {
            string mail = $"Hi, {username}" + $" your order with code {orderCode} that you placed on {orderDate} has been completed and given to the cargo company.";

            await SendMailAsync(to, $"Completed your order with code {orderCode}", mail);
        }
    }
}
