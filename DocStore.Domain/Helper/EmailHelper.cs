using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using DocStore.Contract.Configurations;
using Microsoft.Extensions.Logging;

namespace DocStore.Domain.Helper
{
    public class EmailHelper
    {
        private readonly EmailConfigurations emailConfigurations;
        private readonly ILogger<EmailHelper> logger;

        public EmailHelper(EmailConfigurations _emailConfigurations, ILogger<EmailHelper> _logger)
        {
            emailConfigurations = _emailConfigurations;
            logger = _logger;
        }

        public bool SendEmail(string to, string[] cc, string[] bcc, string subject, string content)
        {
            try
            {
                var mailMessage = new MailMessage(emailConfigurations.Sender, to, subject, content);

                if (cc != null && cc.Length != 0)
                {
                    cc.ToList().ForEach(p => mailMessage.CC.Add(p));
                }

                if (bcc != null && bcc.Length != 0)
                {
                    bcc.ToList().ForEach(p => mailMessage.Bcc.Add(p));
                }

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
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return false;
            }
        }
    }
}
