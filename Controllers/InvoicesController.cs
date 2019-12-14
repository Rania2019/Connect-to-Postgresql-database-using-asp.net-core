using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invoicer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Invoicer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        invoicerContext context = new invoicerContext();

        //getting the invoices and have the options of descending or ascending order according to invoice Date 
        [HttpGet]
        public IActionResult Get(string sort)
        {
            IQueryable<Invoices> invoices;
            switch (sort)
            {
                case "desc":
                    invoices = context.Invoices.OrderByDescending(i => i.InvoiceDate);
                    break;
                case "asc":
                    invoices = context.Invoices.OrderBy(i => i.InvoiceDate);
                    break;
                default:
                    invoices = context.Invoices;
                    break;
            }

            return Ok(invoices);
        }

        //Implementing paging

        [HttpGet("[action]")]

        public IActionResult PagingInvoices(int? pageNumber, int? pageSize)
        {
            var invoices = context.Invoices;
            var currentPageNumber = pageNumber ?? 1;
            var currentPageSize = pageSize ?? 1;

            return Ok(invoices.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
        }


        //Getting the invoice by id

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var invoice = context.Invoices.Find(id);
            if (invoice == null)
            {
                return NotFound("No redcord found...");
            }
            return Ok(invoice);
        }

        // implement search invoice by invoice number

        [HttpGet]
        [Route("[action]")]

        public IActionResult SearchInvoices(int number)
        {
            var invoice = context.Invoices.Where(c => c.InvoiceNumber.Equals(number));
            return Ok(invoice);
        }


        //post: invoice
        [HttpPost]
        
        public IActionResult Post([FromBody] Invoices invoices)
        {
            context.Invoices.Add(invoices);

            context.SaveChanges();

            return StatusCode(StatusCodes.Status201Created);
        }


        //put Invoice/ update 
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Invoices invoices)
        {
            var entity = context.Invoices.Find(id);
            if (entity == null)
            {
                return NotFound("No record found againest this id...");
            }
            else
            {
                entity.InvoiceNumber = invoices.InvoiceNumber;
                entity.InvoiceDate = invoices.InvoiceDate;
                entity.InvoiceDueDate = invoices.InvoiceDueDate;
                entity.InvoiceAmount = invoices.InvoiceAmount;
                entity.TotalPayment = invoices.TotalPayment;
                entity.TotalCredit = invoices.TotalCredit;
                entity.PaymentDate = invoices.PaymentDate;
                context.SaveChanges();
                return Ok("Record Updated Successfully... ");
            }

        }


        //Delete: invoice/
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var invoice = context.Invoices.Find(id);
            if (invoice == null)
            {
                return NotFound("No redcord found againest this id...");
            }
            else
            {
                context.Invoices.Remove(invoice);
                context.SaveChanges();
                return Ok("Record Deleted... ");
            }

        }

    }
}