using MailKit.Net.Smtp;
using MimeKit;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace dotNET.Core
{
    /// <summary>
    /// 发邮件
    /// </summary>
    public class Mailhelper
    {
        /// <summary>
        /// 发邮件
        /// </summary>
        /// <param name="config"></param>
        /// <param name="tos"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="attachments"></param>
        /// <returns></returns>
        public async static Task SendEmailAsync(Config config, List<MailAddress> tos, string subject, string message, params string[] attachments)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add((MailboxAddress)config.From);
            foreach (var to in tos)
                emailMessage.To.Add(to as MailAddress);

            emailMessage.Subject = subject;

            var alternative = new Multipart("alternative");
            if (config.IsHtml)
                alternative.Add(new TextPart("html") { Text = message });
            else
                alternative.Add(new TextPart("plain") { Text = message });

            if (attachments != null)
            {
                foreach (string f in attachments)
                {
                    var attachment = new MimePart()//("image", "png")
                    {
                        ContentObject = new ContentObject(File.OpenRead(f), ContentEncoding.Default),
                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                        ContentTransferEncoding = ContentEncoding.Base64,
                        FileName = Path.GetFileName(f)
                    };
                    alternative.Add(attachment);
                }
            }
            emailMessage.Body = alternative;

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(config.Host, config.Port, config.UseSsl).ConfigureAwait(false);// SecureSocketOptions.None
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                await client.AuthenticateAsync(config.MailFromAccount, config.MailPassword);
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }
    }

    public class Config
    {
        public int Port { get; set; } = 25; //25
        public string Host { get; set; } //smtp.hantianwei.cn

        public bool IsHtml { get; set; } = true;

        public bool UseSsl { get; set; } = false;

        public string MailFromAccount { get; set; }//mail@hantianwei.cn
        public string MailPassword { get; set; }

        public MailAddress From { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    public class MailAddress : MailboxAddress
    {
        public MailAddress(string name, string address) : base(name, address)
        {
        }

        public MailAddress(Encoding encoding, string name, string address) : base(encoding, name, address)
        {
        }
    }
}