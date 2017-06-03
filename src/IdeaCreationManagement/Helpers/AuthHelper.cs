using IdeaCreationManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}