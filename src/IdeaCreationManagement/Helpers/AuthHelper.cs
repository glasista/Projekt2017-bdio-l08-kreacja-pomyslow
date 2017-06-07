using IdeaCreationManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNet.Identity;



namespace IdeaCreationManagement.Helpers
{
    public static class AuthHelper
    {
        /// <summary>
        /// Zwraca true jeżeli użytkownik jest zalogowany, w przeciwnym wypadku false.
        /// </summary>
        public static bool UserIsLoggedIn()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }


        public static string HashPasswordWithExistingSalt(string password)
        {
            string hashedPassword = System.Web.Helpers.Crypto.SHA256(password);
            return hashedPassword;
        }

        /// <summary>
        /// Zwraca aktualnie zalogowanego użytkownika. Jeżeli user nie jest zalogowany 
        /// lub nie istnieje w bazie danych, zwraca null.
        /// </summary>
        public static User GetCurrentLoggedInUser()
        {
            if (!UserIsLoggedIn())
                return null;

            var username = HttpContext.Current.User.Identity.Name;
            using (var db = new AppContext())
            {
                if (username != String.Empty)
                    return db.Users.FirstOrDefault(p => p.UserName == username);
            }

            return null;
        }
       
        public static bool  SendEmail(string receiver, string title, string body)
        {
            {
                var sysLogin = "pomyslyiproblemy@gmail.com";
                var sysPass = "Informatyka1232";
                var sysAddress = new MailAddress(sysLogin, "Ocena użytkowników projektu");
                var receiverAddress = new MailAddress(receiver);

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(sysLogin, sysPass),
                    Timeout = 180000
                };

                using (var message = new MailMessage(sysAddress, receiverAddress) { Subject = title, Body = body })
                {
                    try
                    {
                        smtp.Send(message);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    return true;
                }
            }
            
        }

    }

}