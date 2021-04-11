using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlaneProject.ViewModels;
using PlaneProject.Models;
using PlaneProject.Security;

namespace PlaneProject.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        public ActionResult Logout()
        {
            SessionPersister.Username = null;
            SessionPersister.Role = null;
            return Redirect("/Home/Index");
        }
        public ActionResult AccountLogin(string returnUrl)
        {
            ViewBag.Message = returnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult Login(AccountViewModel avm,string returnUrl)
        {
            AccountModel am = new AccountModel();
            if (string.IsNullOrEmpty(avm.Account.Username) || string.IsNullOrEmpty(avm.Account.Passsword)
                || am.Login(avm.Account.Username, avm.Account.Passsword) == null)
            {
                ViewBag.Error = "Invalid account!";
                return View("AccountLogin");
            }
            SessionPersister.Username = avm.Account.Username;

            SessionPersister.Role = am.FindRole(SessionPersister.Username);
            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }
            
            return Redirect("/Home/Index");
        }
    }
}