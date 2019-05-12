using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LND.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Phai nhap tai khoan")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Phai nhap mat khau")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool Rememberme { get; set; }
    }
}