using System;
using System.Net;
using System.Net.Mail;

namespace InoaB3.Services.SendEmail
{
    public class SendEmail
    {
        private readonly SmtpClient? _smtpClient;

        public void Send(string email, string subject, string body)
        {
            try
            {
                if(_smtpClient == null)
                {
                    throw new InvalidOperationException("SmtpClient não foi inicializado.");
                }

                MailMessage mail = new MailMessage();

                //smtpClient.EnableSsl = true;
                //smtpClient.Timeout = 60*60;

                //smtpClient.UseDefaultCredentials = false;
                _smtpClient.Credentials = new NetworkCredential();

                // Create a new Attachment object
                //Attachment attachment = new Attachment("path_to_attachment_file.txt");

                // Add the attachment to the MailMessage object
                //message.Attachments.Add(another_attachment.pdf);

                mail.From = new MailAddress("julia.antiqueira1995@gmail.com");
                mail.Body = body; 
                mail.Subject = subject; 
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;
                mail.To.Add(email);

                _smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao enviar email: " + ex.Message);
                return;
            }
        }
    }
}