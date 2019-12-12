using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invoicer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Invoicer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : ControllerBase
    {
        invoicerContext context = new invoicerContext();

        //getting the invoices and have the options og descending or ascending order according to invoice Date 
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

        ////Implementing paging

        //[HttpGet("[action]")]

        //public IActionResult PagingInvoices(int? pageNumber, int? pageSize)
        //{
        //    var invoices = context.Invoices;
        //    var currentPageNumber = pageNumber ?? 1;
        //    var currentPageSize = pageSize ?? 1;

        //    return Ok(invoices.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
        //}


        // Getting the invoice by id
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


        //post: invoices
        [HttpPost]
        public void Post([FromBody] Invoices invoices)
        {
            context.Invoices.Add(invoices);

            context.SaveChanges();
        }

        //Get clients
        //[HttpGet]
        //public IActionResult Get()
        //{

        //    return Ok(context.Clients.ToList());
        //}


        // implement search client by name

        [HttpGet]
        [Route("[action]")]

        public IActionResult SearchClients(string name)
        {
           var invoices = context.Clients.Where(c => c.Name.StartsWith(name));
        

            return Ok(invoices);
        }

        //post new client
        //[HttpPost]
        //public IActionResult Post([FromBody] Clients clients)
        //{
        //    context.Clients.Add(clients);

        //    context.SaveChanges();

        //    return StatusCode(StatusCodes.Status201Created);
        //}

        //put clients/ update 951
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Clients clients)
        {
            var entity = context.Clients.Find(id);
            if(entity == null)
            {
                return NotFound("No redcord found againest this id...");
            }
            else
            {
                entity.Name = clients.Name;
                entity.Address = clients.Address;
                entity.City = clients.City;
                entity.PostalCode = clients.PostalCode;
                entity.Country = clients.Country;
                entity.Phone = clients.Phone;
                entity.Email = clients.Email;
                entity.ContactAgent = clients.ContactAgent;
                context.SaveChanges();
                return Ok("Record Updated Successfully... ");
            }
            
        }

        //Delete: clients/551
        //[HttpDelete("{clientid}")]
        //public void Delete (int clientid)
        //{
        //    var client = context.Clients.Find(clientid);
        //    context.Clients.Remove(client);
        //    context.SaveChanges();
        //}

        //Delete: service/
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var service = context.Services.Find(id);
            if (service == null)
            {
                return NotFound("No redcord found againest this id...");
            }
            else
            {
                context.Services.Remove(service);
                context.SaveChanges();
                return Ok("Record Deleted... ");
            }
            
        }

    }
}