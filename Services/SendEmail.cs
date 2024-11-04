using System;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace InoaB3.Services.SendEmail
{
    public class SendEmail
    {

        public void Send(string email, string subject, string body)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();

                if(smtpClient == null)
                {
                    throw new InvalidOperationException("SmtpClient não foi inicializado.");
                }

                MailMessage mail = new MailMessage();

                //smtpClient.EnableSsl = true;
                //smtpClient.Timeout = 60*60;

                //smtpClient.UseDefaultCredentials = false;
                //smtpClient.Credentials = new NetworkCredential();

                Console.WriteLine($"O email de envio é: {email}, o assunto é: {subject} e o corpo é: {body}");

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

                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao enviar email: " + ex.Message);
                return;
            }
        }
    }
}