using System;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace InoaB3.Services.SendEmail
{
    public class SendEmail
    {
        public static async Task SendAsync(string email, string subject, string body)
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

                using (var smtpClient = new SmtpClient(smtpServer)
                {
                    Port = smtpPort,
                    Credentials = new NetworkCredential(smtpUserName, smtpPassword),
                    EnableSsl = enableSsl,
                    Timeout = timeout
                })
                {
                    MailMessage message = new MailMessage
                    {
                        From = new MailAddress(smtpUserName),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true,
                        Priority = MailPriority.Normal
                    };

                    //Console.WriteLine($"SmtpServer: {smtpServer}, SmtpPort: {smtpPort}, SmtpUserName: {smtpUserName}, SmtpPassword: {smtpPassword}, EnableSsl: {enableSsl}, Timeout: {timeout}");

                    message.To.Add(email);

                    Console.WriteLine($"Enviando e-mail para: {email}, Assunto: {subject}");

                    // Envio assíncrono
                    //await smtpClient.SendMailAsync(message);
                    /* problemas com tls ou bloqueios do servidor smtp para assincronismo.
                    por isso está pegando de forma síncrona */
                    smtpClient.Send(message); 
                    Console.WriteLine("E-mail enviado com sucesso.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao enviar e-mail: " + ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}