using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace ApiGuide.Common
{
    public static class MailHelper
    {

        ///// <summary>
        ///// 发送邮件
        ///// </summary>
        ///// <param name="email"></param>
        ///// <param name="subject"></param>
        ///// <param name="message"></param>
        //public static void SendEmail(string email, string subject, string message)
        //{
        //    var emailMessage = new MimeMessage();
        //    emailMessage.From.Add(new MailboxAddress(emailAddressName, emailUserName));
        //    string[] emailArray = email.Split(',');
        //    foreach (var item in emailArray)
        //    {
        //        emailMessage.To.Add(new MailboxAddress(item));
        //    }
        //    emailMessage.Subject = subject;
        //    emailMessage.Body = new TextPart("plain") { Text = message };
        //    using (var client = new SmtpClient())
        //    {
        //        client.Connect(emailHost, 465, true);
        //        client.Authenticate(emailUserName, emailPassword);
        //        client.Send(emailMessage);
        //        client.Disconnect(true);
        //    }
        //}
        ///// <summary>
        ///// 异步发送邮件
        ///// </summary>
        ///// <param name="email"></param>
        ///// <param name="subject"></param>
        ///// <param name="message"></param>
        ///// <returns></returns>
        //public static async Task SendEmailAsync(string email, string subject, string message)
        //{
        //    var emailMessage = new MimeMessage();
        //    emailMessage.From.Add(new MailboxAddress(emailAddressName, emailUserName));
        //    emailMessage.To.Add(new MailboxAddress("mail", email));
        //    emailMessage.Subject = subject;
        //    emailMessage.Body = new TextPart("plain") { Text = message };

        //    using (var client = new SmtpClient())
        //    {
        //        await client.ConnectAsync(emailHost, 25, SecureSocketOptions.None).ConfigureAwait(false);
        //        await client.AuthenticateAsync(emailUserName, emailPassword);
        //        await client.SendAsync(emailMessage).ConfigureAwait(false);
        //        await client.DisconnectAsync(true).ConfigureAwait(false);
        //    }
        //}

        public static void SecondTry(string subject,string content)
        {
            var message = new MimeMessage();
            // new MailboxAddress()
            //   message.From.Add(new MailboxAddress("Joey Tribbiani", "free6girl@163.com"));
            message.From.Add(new MailboxAddress("Joey Tribbiani", "free6girl@163.com"));
            message.To.Add(new MailboxAddress("Mrs. Chanandler Bong", "516365717@qq.com"));

            message.Subject =subject;

            message.Body = new TextPart("plain") { Text =content};

            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.163.com", 25, false);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("free6girl@163.com", "2314155");

                client.Send(message);
                client.Disconnect(true);
            }

            //var bodyBuilder = new BodyBuilder();
            //bodyBuilder.HtmlBody = @"<b>This is bold and this is <i>italic</i></b>";
            //message.Body = bodyBuilder.ToMessageBody();
        }
    }
}
