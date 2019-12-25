using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invoicer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invoicer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        invoicerContext context = new invoicerContext();

        [HttpGet]
        public IActionResult Get()
        {
            using (invoicerContext context = new invoicerContext())
            {
                var invoice = (from l in context.Invoicelineitems
                               join i in context.Invoices
                                on l.InvoiceId equals i.InvoiceId
                               join c in context.Clients
                               on i.ClientId equals c.ClientId
                               where i.InvoiceNumber == 12345
                               select new
                               {
                                   InvoiceNumber = i.InvoiceNumber,
                                   ServiceDescription = c.Name,
                                   Hours = l.Hours,
                                   Rate = l.Rate,
                                   InvoiceAmount = i.InvoiceAmount,
                                  
                               }).ToList();

                return Ok(invoice);
            }
        }
        
    }
    
}