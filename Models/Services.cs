using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Invoicer.Models
{
    public partial class Services
    {
        public Services()
        {
            Invoicelineitems = new HashSet<Invoicelineitems>();
        }

        public string ServiceCode { get; set; }
        [Required]
        [StringLength(30)]
        public string ServiceDescription { get; set; }
        [Required]
        [StringLength(50)]

        public virtual ICollection<Invoicelineitems> Invoicelineitems { get; set; }
    }
}
