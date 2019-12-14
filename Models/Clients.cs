using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Invoicer.Models
{
    public partial class Clients
    {
        public Clients()
        {
            Invoices = new HashSet<Invoices>();
        }

        public int ClientId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        [Required]
        [StringLength(30)]
        public string City { get; set; }

        [Required]
        [StringLength(30)]
        public string PostalCode { get; set; }

        [Required]
        [StringLength(30)]
        public string Country { get; set; }

        [Required]
        [StringLength(30)]
        [RegularExpression("^[0-9]*$")]
        public string Phone { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string ContactAgent { get; set; }

        public virtual ICollection<Invoices> Invoices { get; set; }
    }
}
