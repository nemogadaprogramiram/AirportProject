using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PlaneProject.Models
{
    public class Account
    {
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Display(Name = "Passsword")]
        public string Passsword { get; set; }
        public string Role { get; set; }

    }
}