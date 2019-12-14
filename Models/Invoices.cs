using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Invoicer.Models
{
    public partial class Invoices
    {
        public int InvoiceId { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int InvoiceNumber { get; set; }

        [Required]
        public DateTime? InvoiceDate { get; set; }

       // [Required]
        public DateTime? InvoiceDueDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal InvoiceAmount { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal TotalPayment { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal TotalCredit { get; set; }

        //[Required]
        public DateTime? PaymentDate { get; set; }

        public virtual Clients Client { get; set; }
    }
}
