using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Factory.Models
{
    public class User : IdentityUser
    {
        public int? EmployeeId { get; set; }

        public Employee Employee { get; set; }
    }
}