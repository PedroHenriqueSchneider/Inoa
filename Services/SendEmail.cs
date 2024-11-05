using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace InoaB3.Services
{
    public class SendEmail
    {
        private static readonly SmtpClient _smtpClient;
        private static readonly string _smtpUserName;

        static SendEmail()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Lê as configurações
            string smtpServer = configuration["EmailSettings:SmtpServer"]
                ?? throw new InvalidOperationException("A configuração 'SmtpServer' não pode ser nula.");
            int smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"] 
                ?? throw new InvalidOperationException("A configuração 'SmtpPort' não pode ser nula."));
            _smtpUserName = configuration["EmailSettings:SmtpUserName"]
                ?? throw new InvalidOperationException("A configuração 'SmtpUserName' não pode ser nula.");
            string smtpPassword = configuration["EmailSettings:SmtpPassword"]
                ?? throw new InvalidOperationException("A configuração 'SmtpPassword' não pode ser nula.");
            bool enableSsl = bool.Parse(configuration["EmailSettings:EnableSsl"] 
                ?? throw new InvalidOperationException("A configuração 'EnableSsl' não pode ser nula."));
            int timeout = int.Parse(configuration["EmailSettings:Timeout"]
                ?? throw new InvalidOperationException("A configuração 'Timeout' não pode ser nula."));

            // Inicializa o _smtpClient com as configurações
            _smtpClient = new SmtpClient(smtpServer)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(_smtpUserName, smtpPassword),
                EnableSsl = enableSsl,
                Timeout = timeout
            };
        }

        public static void Send(string email, string subject, string body)
        {
            try
            {
                MailMessage message = new()
                {
                    From = new MailAddress(_smtpUserName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                    Priority = MailPriority.Normal
                };

                message.To.Add(email);
                Console.WriteLine($"Enviando e-mail para: {email}, Assunto: {subject}");

                _smtpClient.Send(message);
                Console.WriteLine("E-mail enviado com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao enviar e-mail: " + ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}