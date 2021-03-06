﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace Shopper.Models
{
    public class EmailService
    {

        public void sendEmail(string emailTo,string subject, string content)
        {
            string fromMail = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            string displayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
            string emailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            string host = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            string port = ConfigurationManager.AppSettings["SMTPPort"].ToString();
            bool ssl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());
            string body = content;
            MailMessage message = new MailMessage(new MailAddress(fromMail), new MailAddress(emailTo));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(fromMail, emailPassword);
            client.Host = host;
            client.EnableSsl = ssl;
            client.Port = int.Parse(port);
            client.Send(message);

        }

    }
}