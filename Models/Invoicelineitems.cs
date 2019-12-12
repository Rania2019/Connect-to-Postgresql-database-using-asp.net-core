using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Invoicer.Models
{
    public partial class Invoicelineitems
    {
        public int InvoiceId { get; set; }

        [Required]
        [StringLength(30)]
        public string ServiceCode { get; set; }

        [Required]
        [StringLength(30)]
        [DataType(DataType.Currency)]
        public decimal Rate { get; set; }

        [Required]
        [StringLength(30)]
        public float Hours { get; set; }

        [Required]
        [StringLength(30)]
        [DataType(DataType.Currency)]
        public decimal TaxAmount { get; set; }

        [Required]
        [StringLength(30)]
        [DataType(DataType.Currency)]
        public decimal TotalInvoice { get; set; }
        

        public virtual Services ServiceCodeNavigation { get; set; }
    }
}
