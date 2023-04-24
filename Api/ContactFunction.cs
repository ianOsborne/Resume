using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using ResumeWebsite.Shared;

namespace ApiIsolated
{
    public class ContactFunction
    {
        private readonly ILogger _logger;

        public ContactFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HttpTrigger>();
        }

        [Function("ContactFunction")]
        public void Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
            JsonObject req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            ContactMessage data = JsonSerializer.Deserialize<ContactMessage>(req);
            SendMail(data);
        }

        public void SendMail(ContactMessage contactMessage)
        {
            SmtpClient smtpClient = new SmtpClient("email.ionos.com");
            MailMessage message = new MailMessage();
            message.From = new MailAddress(contactMessage.Email);
            message.To.Add("mail@ianosborne.dev");
            message.Body = contactMessage.Name + " says " + contactMessage.Message;
            message.Subject = "Resume Website Contact";
            smtpClient.Port = 465;
            smtpClient.Host = "smtp.ianosborne.dev";
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("mail@ianosborne.dev", "2002@))@IAN");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Send(message);
        }

    }
}
