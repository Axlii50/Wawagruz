using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Wawagruz.Models;

namespace Wawagruz
{
    public class Mail
    {
        const string host = "smtp.gmail.com";
        const string Receivmaile = "wawagruz@gmail.com";
        /// <summary>
        /// method used to send messages created in index in contact form for user
        /// </summary>
        /// <param name="model"></param>
        /// <param name="HostEmail"></param>
        /// <param name="HostPass"></param>
        public static void SendMail(ContactModel model, string HostEmail = "wawagruz@gmail.com", string HostPass = "Warszawka2021")
        {
            SmtpClient client = new SmtpClient(host, 587);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(HostEmail, HostPass);
            client.EnableSsl = true;

            MailAddress from = new MailAddress(HostEmail);
            MailAddress to = new MailAddress(Receivmaile);
            MailMessage message = new MailMessage(from, to);
            message.Body += model.Message + Environment.NewLine;
            message.Body += "Imie Wysylajacego" + model.Name + Environment.NewLine;
            message.Body += "Mail Wysylajacego" + model.Email;
            message.Subject = "Formularz Kontaktowy - " + model.Subject;
            client.Send(message);
            client.Dispose();
        }

        /// <summary>
        /// method used for sending veryfication code for main panel 
        /// </summary>
        /// <param name="Token">veryfication token</param>
        /// <param name="HostEmail">default value for host mail</param>
        /// <param name="HostPass">default value for host mail password</param>
        public static void SendMail(string Token, string HostEmail = "wawagruz@gmail.com", string HostPass = "Warszawka2021")
        {
            SmtpClient client = new SmtpClient(host, 587);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(HostEmail, HostPass);
            client.EnableSsl = true;

            MailAddress from = new MailAddress(HostEmail);
            MailAddress to = new MailAddress(Receivmaile);
            MailMessage message = new MailMessage(from, to);
            message.Body += Token + Environment.NewLine;

            message.Subject = "Kod weryfikacyjny ";
            client.Send(message);
            client.Dispose();
        }

        //todo
        /// <summary>
        /// method used to send veryfiaction code for side panel 
        /// </summary>
        /// <param name="Token">veryfication token</param>
        /// <param name="ReceivMail">receiving mail </param>
        /// <param name="HostEmail">default value for host mail</param>
        /// <param name="HostPass">default value for host mail password</param>
        public static void SendMail(string Token,string ReceivMail, string HostEmail = "wawagruz@gmail.com", string HostPass = "Warszawka2021")
        {
            SmtpClient client = new SmtpClient(host, 587);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(HostEmail, HostPass);
            client.EnableSsl = true;

            MailAddress from = new MailAddress(HostEmail);
            MailAddress to = new MailAddress(ReceivMail);
            MailMessage message = new MailMessage(from, to);
            message.Body += Token + Environment.NewLine;

            message.Subject = "Kod weryfikacyjny ";
            client.Send(message);
            client.Dispose();
        }
    }
}
