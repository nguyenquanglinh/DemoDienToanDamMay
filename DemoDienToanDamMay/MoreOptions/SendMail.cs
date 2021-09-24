using System.Net;
using System.Net.Mail;

namespace DemoDienToanDamMay.MoreOptions
{
    public class SendMail
    {
        private static string FromMailAddress = "loccigo@gmail.com";
        private static string pass = "Loc1504*";
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
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(FromMailAddress, pass)
                };

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