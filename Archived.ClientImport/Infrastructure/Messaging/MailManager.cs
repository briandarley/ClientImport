using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using RazorEngine;
using RazorEngine.Templating;

namespace Archived.ClientImport.Infrastructure.Messaging
{
    public class MailManager
    {
        public enum MailTemplateTypes
        {
            Success,
            MissingOrganizations,
            MultipleOrganizatonMatches
        }

        private readonly string _fromEmail;
        private readonly string _server;
        private readonly string _userId;
        private readonly string _password;
        public bool SuppressEmailMessages { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromEmail">Format : first name last name &lt;email@email.com&gt;</param>
        /// <param name="server"></param>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        public MailManager(string fromEmail, string server, string userId, string password)
        {
            _fromEmail = fromEmail;
            _server = server;
            _userId = userId;
            _password = password;
            SuppressEmailMessages = ConfigurationManager.AppSettings["suppress_email_messages"].ToBool();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="toEmail">Format : first name last name &lt;email@email.com&gt;</param>
        /// <param name="subject"></param>
        /// <param name="emailBody"></param>
        /// <param name="attachments"></param>
        /// <returns></returns>
        public void SendEmail(string toEmail, string subject, string emailBody, string[] attachments)
        {
            if (SuppressEmailMessages) return;
            // setup email header
            using (var mailMessage = new MailMessage { From = new MailAddress(_fromEmail,"Client Conversion") })
            {

                // Set the message sender
                // sets the from address for this e-mail message. 
                // Sets the address collection that contains the recipients of this e-mail message. 
                mailMessage.To.Add(new MailAddress(toEmail));

                // sets the message subject.
                mailMessage.Subject = subject;
                // sets the message body. 
                mailMessage.Body = emailBody;
                // sets a value indicating whether the mail message body is in Html. 
                // if this is false then ContentType of the Body content is "text/plain". 
                mailMessage.IsBodyHtml = true;

                mailMessage.AlternateViews.Add(EmbeddedImage(emailBody));

                // add all the file attachments if we have any
                if (attachments != null && attachments.Length > 0)
                {
                    foreach (var attachment in attachments)
                    {
                        mailMessage.Attachments.Add(new Attachment(attachment));
                    }
                }

                // SmtpClient Class Allows applications to send e-mail by using the Simple Mail Transfer Protocol (SMTP).
                var smtpClient = new SmtpClient(_server)
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false
                };

                //Specifies how email messages are delivered. Here Email is sent through the network to an SMTP server.

                // Some SMTP server will require that you first authenticate against the server.
                // Provides credentials for password-based authentication schemes such as basic, digest, NTLM, and Kerberos authentication.
                //smtpClient.EnableSsl = true;
                //smtpClient.UseDefaultCredentials = false;
                var networkCredential = new System.Net.NetworkCredential(_userId, _password);
                smtpClient.Credentials = networkCredential;

                //smtpClient.Port = int.Parse(Constants.Resources.SmtpPort);
                //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                
                //Let's send it
                smtpClient.Send(mailMessage);
            }


        }


        public void SendMailFromTemplate(MailTemplateTypes templateType, string toEmail, string subject, object model)
        {
            if (SuppressEmailMessages) return;

            var body = GenerateEmailBody(templateType, subject, model);

            try
            {
                SendEmail(toEmail, subject, body, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }



        }

        public static string GenerateEmailBody(MailTemplateTypes templateType, string subject, object model)
        {
            var templateContent = TemplatePath(templateType);

            
            var viewbag = new DynamicViewBag();
            
            //viewbag.AddValue("Url", Url);

            string templateCacheName = $"{templateType}-{subject}";
            
            
            var body = Engine.Razor.IsTemplateCached(templateCacheName, null)
                ? Engine.Razor.Run(templateCacheName, null, model, viewbag)
                : Engine.Razor.RunCompile(templateContent, templateCacheName, null, model, viewbag);


            return body;
        }

        private AlternateView EmbeddedImage(string bodyTemplate)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var imageStream = assembly.GetManifestResourceStream("ClientImport.Infrastructure.Messaging.pagebanner.jpg");
            var inline = new LinkedResource(imageStream, MediaTypeNames.Image.Jpeg)
            { ContentId = Guid.NewGuid().ToString() };
            string imageTag = $"<img src='cid:{inline.ContentId}'/>";
            bodyTemplate = bodyTemplate.Replace(@"<img/>", imageTag);
            var alternateView = AlternateView.CreateAlternateViewFromString(bodyTemplate, null, MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(inline);
            return alternateView;
        }


        private static string TemplatePath(MailTemplateTypes templateType)
        {
            var assembly = Assembly.GetExecutingAssembly();

            //  var imageStream = assembly.GetManifestResourceStream("ClientImport.Infrastructure.Messaging.pagebanner.jpg");
            string templatePath;
            switch (templateType)
            {
                case MailTemplateTypes.Success:
                    var rawFile = assembly.GetManifestResourceStream("ClientImport.Infrastructure.Messaging.Templates.SuccessTemplate.html");
                    var reader = new StreamReader(rawFile);
                    string result = reader.ReadToEnd();
                    return result;
                    
                case MailTemplateTypes.MissingOrganizations:
                    rawFile = assembly.GetManifestResourceStream("ClientImport.Infrastructure.Messaging.Templates.MissingMapping.html");
                    reader = new StreamReader(rawFile);
                    result = reader.ReadToEnd();
                    return result;
                case MailTemplateTypes.MultipleOrganizatonMatches:
                    rawFile = assembly.GetManifestResourceStream("ClientImport.Infrastructure.Messaging.Templates.MultipleMatches.html");
                    reader = new StreamReader(rawFile);
                    result = reader.ReadToEnd();
                    return result;
                default:
                    throw new ArgumentOutOfRangeException(nameof(templateType), templateType, null);
            }
           
        }
    }

}

