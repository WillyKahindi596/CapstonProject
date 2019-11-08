using System.Net;
using System.Net.Mail;

namespace MessageSending
{
    public static class MessageSender
    {
        public static void SendEmail(string toAddr, string subject, string body)
        {
            var fromAddress = "TasteofTheWorld2019";
            var appSpecificPass = "tnsdlvwumrbgzckj";

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.EnableSsl = true;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;

                smtpClient.Credentials = new NetworkCredential(fromAddress, appSpecificPass);

                using (var msg = new MailMessage(fromAddress, toAddr))
                {
                    msg.Subject = subject;
                    msg.Body = body;

                    smtpClient.Send(msg);
                }
            }
        }
    }
}
