using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Invoicer.Models
{
    public partial class Invoices
    {
        public int InvoiceId { get; set; }

        [Required]
        [StringLength(30)]
        public int ClientId { get; set; }

        [Required]
       // [StringLength(30)]
        public string InvoiceNumber { get; set; }

        //[Required]
        [StringLength(30)]
        public DateTime? InvoiceDate { get; set; }

        //[Required]
        [StringLength(30)]
        public DateTime? InvoiceDueDate { get; set; }

        [Required]
        [StringLength(30)]
        [DataType(DataType.Currency)]
        public decimal InvoiceAmount { get; set; }

        [Required]
        [StringLength(30)]
        [DataType(DataType.Currency)]
        public decimal TotalPayment { get; set; }

        //[Required]
        //[StringLength(30)]
        [DataType(DataType.Currency)]
        public decimal TotalCredit { get; set; }

        //[Required]
        //[StringLength(30)]
       
        public DateTime? PaymentDate { get; set; }

        public virtual Clients Client { get; set; }
    }
}
