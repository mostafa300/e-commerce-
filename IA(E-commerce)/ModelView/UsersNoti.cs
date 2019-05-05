using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IA_E_commerce_.Models;
namespace IA_E_commerce_.ModelView
{
    public class UsersNoti
    {
        public Notification Notification { get; set; }
        public List<ApplicationUser> Users { get; set; }

    }
}