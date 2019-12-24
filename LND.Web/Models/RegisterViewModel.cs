using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LND.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Ho ten không được để trống")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Tài khoản không được để trống")]
        public string UserName { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 kí tự")]
        public string Password { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Mật khẩu xác nhận phải trùng với Mật khẩu")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng")]
        public string Email { get; set; }

        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}