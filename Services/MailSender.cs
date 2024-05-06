using System.Net.Mail;
using System.Net;
using System.Text.Json;

namespace ScPlayerAPI.Services
{
    public class MailSender
    {
        public string SendMessage(string messageBody)
        {
            string result = "ok";
            try
            {//

                //123_ewfijuhf;lfytgjckeifqntvtyz
                var fromAddress = new MailAddress("sakhacontent@outlook.com", "From Name");
                var toAddress = new MailAddress("glider.4d5a@gmail.com", "To Name");
                const string fromPassword = "";
                


                const string subject = "from form";
                string body = messageBody;

                var smtp = new SmtpClient
                {
                    Host = "smtp-mail.outlook.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    //                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                smtp.SendMailAsync(new MailMessage(from: "sakhacontent@outlook.com", "glider.4d5a@gmail.com", subject: subject, body: messageBody));
 
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}
