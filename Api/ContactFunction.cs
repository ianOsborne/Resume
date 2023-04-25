using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
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
        [Function("ContactFunction")]
        public void Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
            HttpRequestData req)
        {
            SendMail(JsonSerializer.Deserialize<ContactMessage>(req.Body));
        }


        public void SendMail(ContactMessage contactMessage)
        {
            if (contactMessage == null)
            {
                throw new ArgumentNullException("Contact Message was null :(");
            }
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("ianosborne.dev@gmail.com", Environment.GetEnvironmentVariable("MailPassword")),
            };
            MailMessage message = new MailMessage();
            message.From = new MailAddress("ianosborne.dev@gmail.com");
            message.To.Add("ian.cosborne@yahoo.com");
            message.Body = contactMessage.Name + " \n " + contactMessage.Email + " \n " + contactMessage.Message;
            message.Subject = "Resume Website";
            smtpClient.Send(message);
        }

    }
}
