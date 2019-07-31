using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Mail;
using System.Net.Mime;
using System.Net.Configuration;
using System.Net;
using ObjTransfer;

namespace AccessDB
{
    public class Email
    {
        MailMessage mail = new MailMessage();
        SmtpClient smtp = new SmtpClient();
        public string smtpEmailFrom { get; set; }
        public string smtpSenha { get; set; }
        public string Host { get; set; }

        public Email(string host, string email, string senha)
        {
            smtpEmailFrom = email;
            smtpSenha = senha;
            Host = host;
        }

        private SmtpClient EmailPadrao()
        {
            smtp.Host = Host;
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(smtpEmailFrom, smtpSenha);

            return smtp;
        }

        private MailMessage EmailMessage(EmailInfo email)
        {
            mail.From = new MailAddress(smtpEmailFrom);

            if (email.emailTo.Length > 0)
                foreach (string item in email.emailTo)
                    mail.To.Add(new MailAddress(item.Trim()));

            if (email.emailCC.Length > 0)
                foreach (string item in email.emailCC)
                    mail.CC.Add(new MailAddress(item.Trim()));

            if (email.emailCCo.Length > 0)
                foreach (string item in email.emailCCo)
                    mail.Bcc.Add(new MailAddress(item.Trim()));

            mail.Subject = email.emailAssunto;
            mail.Body = email.emailMessage;

            if (email.emailAnexo.Length > 0)
                foreach (string item in email.emailAnexo)
                    mail.Attachments.Add(new Attachment(item));

            return mail;
        }

        public bool EnviarEmail(EmailInfo emailInfo)
        {
            SmtpClient smtp = EmailPadrao();
            smtp.Send(EmailMessage(emailInfo));

            return true;
        }
    }
}
