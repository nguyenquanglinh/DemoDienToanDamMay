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
        public static bool Send(string ToMailAddress, int code, bool Iscreate=true)
        {
            try
            {
                var msg = new MailMessage();
                msg.From = new MailAddress(FromMailAddress);
                msg.To.Add(new MailAddress(ToMailAddress));
                if (Iscreate)
                    msg.Subject = "Account verification";
                else
                    msg.Subject = "Resend activation code when user forgot";
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