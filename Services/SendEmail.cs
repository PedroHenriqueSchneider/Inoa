using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace InoaB3.Services.SendEmail
{
    public class SendEmail
    {
        public void Send(string email, string subject, string body)
        {
            try
            {
               IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                // Lê as configurações
                string smtpServer = configuration["EmailSettings:SmtpServer"];
                int smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);  

                string smtpUserName = configuration["EmailSettings:SmtpUserName"];
                string smtpPassword = configuration["EmailSettings:SmtpPassword"];
                bool enableSsl = bool.Parse(configuration["EmailSettings:EnableSsl"]);
                int timeout = int.Parse(configuration["EmailSettings:Timeout"]);

                Console.WriteLine($"SMTP Server: {smtpServer}");
                Console.WriteLine($"SMTP Port: {smtpPort}");
                Console.WriteLine($"SMTP User Name: {smtpUserName}");
                Console.WriteLine($"Enable SSL: {enableSsl}");
                Console.WriteLine($"Timeout: {timeout}");

                SmtpClient smtpClient = new SmtpClient(smtpServer)
                {
                    Port = smtpPort,
                    Credentials = new System.Net.NetworkCredential(smtpUserName, smtpPassword),
                    EnableSsl = enableSsl,
                    Timeout = timeout
                };

                MailMessage mail = new MailMessage
                {
                    From = new MailAddress("pedro@d419d6c33b819176.maileroo.org"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                    Priority = MailPriority.Normal
                };

                mail.To.Add(email);
                
                Console.WriteLine($"Enviando e-mail para: {email}, Assunto: {subject}");

                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao enviar email: " + ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}