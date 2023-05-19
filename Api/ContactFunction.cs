using System;
using System.Net;
using System.Net.Mail;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using ResumeWebsite.Shared;

namespace ApiIsolated
{
    public class ContactFunction
    {
        [Function("ContactFunction")]
        public void Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
            HttpRequestData req)
        {
            SendMail(JsonSerializer.Deserialize<ContactMessage>(req.Body));
        }
        public void SendMail(ContactMessage contactMessage)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("ianosborne.dev@gmail.com", Environment.GetEnvironmentVariable("MailPassword")),
            };
            MailMessage message = new();
            message.From = new MailAddress("ianosborne.dev@gmail.com");
            message.To.Add("ian.cosborne@yahoo.com");
            message.Body = contactMessage.Name + " \n " + contactMessage.Email + " \n " + contactMessage.Message;
            message.Subject = contactMessage.Subject;
            smtpClient.Send(message);
        }
    }
}
