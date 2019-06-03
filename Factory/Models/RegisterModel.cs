using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Factory.Models
{
    public class RegisterModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string ConfirmedPassword { get; set; }
    }
}