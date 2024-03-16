using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using RelationShip.Model;
using RelationShip.Interfaces;

namespace RelationShip.Repository
{
    public class MailRepository : IMailRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public MailRepository(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public void SendEmail(MailBox mail, string content)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration["Mail:EmailUsername"]));
            email.To.Add(MailboxAddress.Parse(mail.To));
            email.Subject = mail.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = content };

            using var smtp = new SmtpClient();
            smtp.Connect(_configuration["Mail:Host"], 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration["Mail:EmailUsername"], _configuration["Mail:Password"]);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public void SendOTPEmail(string toEmail, string otpCode)
        {
            var contentMail = FormBodyMail(otpCode);
            SendEmail(new MailBox { To = toEmail, Subject = "Your OTP Code" }, contentMail);
        }

        private string FormBodyMail(string code)
        {
            string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "Html", "FormGmail.html");
            string fileContent = System.IO.File.ReadAllText(filePath);

            fileContent = fileContent.Replace("${randomCode}", code);
            return fileContent;
        }
    }
}
