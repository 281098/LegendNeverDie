using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LND.Web.Models
{
    public class OrderViewModel
    {
        public int ID { set; get; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(256)]
        public string CustomerName { set; get; }

        [Required(ErrorMessage = "Address is Required")]
        [MaxLength(256)]
        public string CustomerAddress { set; get; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng")]
        public string CustomerEmail { set; get; }

        [Required(ErrorMessage = "Phai nhap sdt")]
        [MaxLength(50)]
        public string CustomerMobile { set; get; }

        [MaxLength(256, ErrorMessage = "Ghi chu khong duoc qua 256 ki tu")]
        public string CustomerMessage { set; get; }

        [MaxLength(256)]
        public string PaymentMethod { set; get; }

        public DateTime? CreatedDate { set; get; }
        public string CreatedBy { set; get; }
        public string PaymentStatus { set; get; }
        public bool Status { set; get; }

        [MaxLength(128)]
        public string CustomerId { set; get; }

        public IEnumerable<OrderDetailViewModel> OrderDetails { set; get; }
    }
}