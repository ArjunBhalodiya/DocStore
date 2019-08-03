using System.Net.Mail;
using System.Threading.Tasks;
using DocStore.Contract.Configurations;

namespace DocStore.Domain.Helper
{
    public class EmailHelper
    {
        private readonly EmailConfigurations emailConfigurations;

        public EmailHelper(EmailConfigurations emailConfigurations)
        {
            this.emailConfigurations = emailConfigurations;
        }

        public bool SendEmail(string to, string cc, string bcc, string subject, string content)
        {
            try
            {
                var mailMessage = new MailMessage(emailConfigurations.Sender, to, subject, content);
                mailMessage.CC.Add(cc);
                mailMessage.Bcc.Add(bcc);

                using (var client = new SmtpClient())
                {
                    var messageSendingTask = Task.Run(async () =>
                                             {
                                                 await client.SendMailAsync(mailMessage).ConfigureAwait(false);
                                             });
                    messageSendingTask.Wait();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
