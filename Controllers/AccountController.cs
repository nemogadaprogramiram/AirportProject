using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlaneProject.Models;
using System.Web.Security;
using System.Text;

namespace PlaneProject.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        public ActionResult Login()
        {
            return View();
        }
        string Encode(string password)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        }
        [HttpPost]
        public ActionResult Login(UserTable model, string returnUrl)
        {
            AirportDatabaseEntities db = new AirportDatabaseEntities();
            model.Password = Encode(model.Password);
            var dataItem = db.UserTable.Where(x => x.Username == model.Username && x.Password ==model.Password).FirstOrDefault();
            if (dataItem != null)
            {
                FormsAuthentication.SetAuthCookie(dataItem.Username, false);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                         && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid user/pass");
                return View();
            }
        }
    }
}