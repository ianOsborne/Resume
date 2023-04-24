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
            SmtpClient smtpClient = new SmtpClient("smtp.ionos.com");
            MailMessage message = new MailMessage();
            message.From = new MailAddress(contactMessage.Email);
            message.To.Add("mail@ianosborne.dev");
            message.Body = contactMessage.Name + " says " + contactMessage.Message;
            message.Subject = "Resume Website Contact";
            smtpClient.Port = 465;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("mail@ianosborne.dev", Environment.GetEnvironmentVariable("MailPassword"));
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Send(message);
        }

    }
}
