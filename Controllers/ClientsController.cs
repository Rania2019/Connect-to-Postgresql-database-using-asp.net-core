using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invoicer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;


namespace Invoicer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        invoicerContext context = new invoicerContext();

        // get client/
        [HttpGet]
        public IActionResult Client(string sort)
        {
            IQueryable<Clients> clients;
            switch (sort)
            {
                case "desc":
                    //.Include("Invoices")
                    clients = context.Clients.OrderByDescending(i => i.Name);
                    break;
                case "asc":
                    clients = context.Clients.OrderBy(i => i.Name);
                    break;
                default:
                    clients = context.Clients;
                    break;
            }

            return Ok(clients);
        }


        //Implementing paging

        [HttpGet("[action]")]

        public IActionResult PagingClients(int? pageNumber, int? pageSize)
        {
            var clients = context.Clients;
            var currentPageNumber = pageNumber ?? 1;
            var currentPageSize = pageSize ?? 2;

            return Ok(clients.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
        }

        //Getting the client by id
        //invoice/client/id

        [HttpGet("{id}", Name = "Findclient")]
        public IActionResult Findclient(int id)
        {
            var client = context.Clients.Find(id);
            //var client = context.Clients.Include("Invoices").FirstOrDefault(c=>c.ClientId==id);
            if (client == null)
            {
                return NotFound("No redcord found...");
            }
            return Ok(client);
        }

        // implement search client by name

        [HttpGet]
        [Route("[action]")]

        public IActionResult SearchClients(string name)
        {
            var client = context.Clients.Where(c => c.Name.StartsWith(name));


            return Ok(client);
        }


        //post new client
        
        [HttpPost]
        public IActionResult Post([FromBody] Clients clients)
        {
            context.Clients.Add(clients);

            context.SaveChanges();

            return StatusCode(StatusCodes.Status201Created);
        }
        

        //put clients/ update 
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Clients clients)
        {
            var entity = context.Clients.Find(id);
            if (entity == null)
            {
                return NotFound("No record found againest this id...");
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

        //Delete: clients/
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var client = context.Clients.Find(id);
            if (client == null)
            {
                return NotFound("No redcord found againest this id...");
            }
            else
            {
                context.Clients.Remove(client);
                context.SaveChanges();
                return Ok("Record Deleted... ");
            }

        }

       
    }
}