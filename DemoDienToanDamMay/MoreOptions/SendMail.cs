using System.Net;
using System.Net.Mail;

namespace DemoDienToanDamMay.MoreOptions
{
    public class SendMail
    {
        private static string FromMailAddress = "laptopnql@gmail.com";
        private static string pass = "Linhnguyen99x";
        private static SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(FromMailAddress, pass)
        };
        public static bool Send(string ToMailAddress, int code)
        {
            try
            {
                var msg = new MailMessage();
                msg.From = new MailAddress(FromMailAddress);
                msg.To.Add(new MailAddress(ToMailAddress));
                msg.Subject = "Account verification";
                msg.Body = $"code: {code}";
                msg.IsBodyHtml = true;


                smtpClient.Send(msg);
                return true;
            }
            catch
            {
                return false;
            }

        }

    }
}