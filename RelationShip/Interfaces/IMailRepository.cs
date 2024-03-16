using RelationShip.Model;

namespace RelationShip.Interfaces
{
    public interface IMailRepository
    {
        public void SendEmail(MailBox mail,string content);
        public void SendOTPEmail(string toEmail, string otpCode);
    }
}
