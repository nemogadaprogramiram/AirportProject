using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using PlaneProject.Procedures;

namespace PlaneProject.Models
{
    public class AccountModel
    {
        private List<Account> accounts = new List<Account>();

        public AccountModel()
        {
            List<UserModel> users = Procedures.Procedures.UserInfo();
            foreach (var item in users)
            {
                accounts.Add(new Account { Username = item.Username, Passsword = Decode(item.Password), Role =item.Role });
            }

        }
        public Account Find(string username)
        {
            return accounts.Where(acc => acc.Username.Equals(username)).FirstOrDefault();
        }

        public Account Login(string username, string password)
        {
            return accounts.Where(acc => acc.Username.Equals(username)&& acc.Passsword.Equals(password)).FirstOrDefault();
        }
        public string FindRole(string username)
        {
            return accounts.Where(acc => acc.Username.Equals(username)).FirstOrDefault().Role;
        }

        static string Decode(string password)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(password));
        }

    }
}