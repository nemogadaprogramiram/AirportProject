using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using PlaneProject.Models;

namespace PlaneProject.Security
{
    public class CustomPrincipal : IPrincipal
    {
        private Account Account;
        public IIdentity Identity { get; set; }

        public CustomPrincipal(Account account)
        {
            this.Account = account;
            this.Identity = new GenericIdentity(account.Username);
        }

        public bool IsInRole(string role)
        {
            var roles = role.Split(new char[]{','});
            return roles.Any(r=>this.Account.Role.Contains(r));
        }
    }
}