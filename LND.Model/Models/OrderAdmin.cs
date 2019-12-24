using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LND.Model.Models
{
    public class OrderAdmin
    {
        public int Id { set; get; }
        public string CustomerName { set; get; }
        public string CustomerAddress { set; get; }
        public string CustomerEmail { set; get; }

        public string CustomerMobile { set; get; }
        public string CustomerMessage { set; get; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public decimal TotalPrice { get; set; }
        public string Quantitty { set; get; }
        public DateTime? CreatedDate { set; get; }
        public string PaymentMethod { set; get; }
        public string PaymentStatus { set; get; }
        public bool Status { set; get; }

    }
}