using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Serilog;

namespace CrossCutting.Helpers.Helpers
{
    /// <summary>
    /// Simple email sender helper class.
    /// </summary>
    public class EmailHelper
    {
        /// <summary>
        /// Logger instance.
        /// </summary>
        private readonly ILogger Logger;

        /// <summary>
        /// Email account user name.
        /// </summary>
        private readonly string _accountUserName;

        /// <summary>
        /// Email account password.
        /// </summary>
        private readonly string _accountPassword;

        /// <summary>
        /// Email send server address.
        /// </summary>
        private readonly string _smtpHost;

        
        public EmailHelper(ILogger logger, string accountUserName, string accountPassword, string smtpHost)
        {
            Logger = logger;
            _accountUserName = accountUserName;
            _accountPassword = accountPassword;
            _smtpHost = smtpHost;
        }

        /// <summary>
        /// Sends an email from a specific account to a recipient email.
        /// </summary>
        /// <param name="recipient">The email message recipient</param>
        /// <param name="subject">The email subject.</param>
        /// <param name="messageBody">The message to send to the recipient.</param>
        /// <param name="isBodyHtml">Indicates if the <param name="messageBody"> an HTML fragment.</param></param>
        /// <param name="attachments">A list of file paths to attach to the email.</param>
        /// <returns>True if the message was sent, False if any error occured.</returns>
        public bool SendEmail(string recipient, string subject, string messageBody, bool isBodyHtml, IList<string> attachments)
        {
            bool isMessageSent;
            SmtpClient client = new SmtpClient(_smtpHost)
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(_accountUserName, _accountPassword);
            client.EnableSsl = true;
            client.Credentials = credentials;

            try
            {
                MailMessage mail = new MailMessage(_accountUserName.Trim(), recipient.Trim())
                {
                    Subject = subject,
                    Body = messageBody,
                    IsBodyHtml = isBodyHtml
                };

                if (attachments?.Any() == true)
                {
                    foreach (string attachment in attachments)
                    {
                        Attachment newAttachment = new Attachment(attachment);
                        mail.Attachments.Add(newAttachment);
                    }
                }

                client.Send(mail);
                isMessageSent = true;
            }
            catch (Exception)
            {
                isMessageSent = false;
            }

            return isMessageSent;
        }

        /// <summary>
        /// Sends an email, asynchronously, from the default email account (specified on the application configuration file) to a recipient email.
        /// </summary>
        /// <param name="recipients">The email message recipients, multiple e-mail addresses must be separated by a comma character (",")</param>
        /// <param name="subject">The email subject.</param>
        /// <param name="messageBody">The message to send to the recipient.</param>
        /// <param name="isBodyHtml">Indicates if the <param name="messageBody"> an HTML fragment.</param></param>
        /// <param name="attachments">A list of file paths to attach to the email.</param>
        public static async Task<bool> SendEmailAsync(ILogger logger, string recipients, string subject, string messageBody, bool isBodyHtml, IList<string> attachments = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(recipients))
                {
                    throw new ApplicationException("Null or empty recipient email address.");
                }

                if (string.IsNullOrWhiteSpace(subject))
                {
                    throw new ApplicationException("Null or empty email subject.");
                }

                if (string.IsNullOrWhiteSpace(messageBody))
                {
                    throw new ApplicationException("Null or empty email body.");
                }

                using (SmtpClient smtpClient = new SmtpClient())
                using (MailMessage mail = new MailMessage())
                {
                    mail.Subject = subject;
                    mail.Body = messageBody;
                    mail.IsBodyHtml = isBodyHtml;
                    mail.Bcc.Add(recipients);

                    if (attachments?.Any() == true)
                    {
                        foreach (string attachment in attachments)
                        {
                            Attachment newAttachment = new Attachment(attachment);
                            mail.Attachments.Add(newAttachment);
                        }
                    }

                    await smtpClient.SendMailAsync(mail);

                    return true;
                }
            }
            catch (Exception ex)
            {
                logger.Error("[EmailHelper] Error sending email", ex);
                return false;
            }
        }
    }
}