using CitaFacil.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace CitaFacil.Services
{
    public class EMailServices : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EMailServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendEmail(EMail solicitud)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration["Email:UserName"]));
            email.To.Add(MailboxAddress.Parse(solicitud.Para));
            email.Subject = solicitud.Asunto;
            email.Body = new TextPart(TextFormat.Html) 
            {
                Text = solicitud.Contenido 
            };
            using var smtp = new SmtpClient();
            smtp.Connect(_configuration.GetSection("Email:Host").Value, Convert.ToInt32(_configuration.GetSection("Email:Port").Value), SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("Email:UserName").Value, _configuration.GetSection("Email:Password").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
