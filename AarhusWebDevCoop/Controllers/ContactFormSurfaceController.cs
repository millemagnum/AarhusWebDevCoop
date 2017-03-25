using AarhusWebDevCoop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Core.Models;

namespace AarhusWebDevCoop.Controllers
{
    public class ContactFormSurfaceController : SurfaceController
    {
        // GET: ContactFormSurface
        public ActionResult Index()
        {
            return PartialView("ContactForm", new ContactForm());
        }

        [HttpPost]
        public ActionResult HandleFormSubmit(ContactForm model)
        {
            // viser fejlene og siden igen, hvis der er fejl
            if (!ModelState.IsValid) {
                return CurrentUmbracoPage();
            }
            else
            {
                // så man kan sende en mail
                MailMessage message = new MailMessage();
                message.To.Add("camilla.bambi14@gmail.com");
                message.Subject = model.Subject;
                message.From = new MailAddress(model.Email, model.Name);
                message.Body = model.Message;

                // er til gmail konto - plus sender beskeden
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential("camilla.bambi14@gmail.com", "Rozadragon24");
                    smtp.EnableSsl = true;
                    // send mail
                    smtp.Send(message);
                }

                // assigner true til en TempData variabel, hvis mailen bliver sendt og derved bliver p tagget vist fra ContactForm partial view - da TempData ikke er null længere
                TempData["success"] = true;


                // tilføjer beskeden til Umbraco
                IContent comment = Services.ContentService.CreateContent(model.Subject, CurrentPage.Id, "message");

                comment.SetValue("messageName", model.Name);
                comment.SetValue("email", model.Email);
                comment.SetValue("subject", model.Subject);
                comment.SetValue("messageContent", model.Message);

                // save
                Services.ContentService.Save(comment);



                // redirecter siden
                return RedirectToCurrentUmbracoPage();
            } // else slutter
        }
    }
}