using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using E_Mart.Models;

namespace E_Mart.Controllers
{
  
    public class Customer_AddressController : ApiController
    {
        private E_martdb db = new E_martdb();

        

        // GET: api/Customer_Address/5
        [ResponseType(typeof(Customer_Address))]
        public IHttpActionResult GetCustomer_Address(int id)
        {
            Customer_Address customer_Address = db.Customer_Address.Find(id);
            if (customer_Address == null)
            {
                return NotFound();
            }

            return Ok(customer_Address);
        }

        // PUT: api/Customer_Address/Mobile no.      to add address
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer_Address(long id, Customer_Address customer_Address)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Customer_Address cust=new Customer_Address();
            cust.Add_1 = customer_Address.Add_1;
            cust.Add_2 = customer_Address.Add_2;
            cust.Zip_code = customer_Address.Zip_code;
            cust.Country = customer_Address.Country;


            db.Customer_Address.Add(cust);
            db.SaveChanges();

            string id_ = id.ToString();
            List<Customer_Details> list = db.Customer_Details.Where((c) => c.Mobile_no == id_).ToList();
            list.Reverse();
            Customer_Details customer_Details = list.FirstOrDefault();
            customer_Details.Add_Id = cust.Add_Id;

            db.Entry(customer_Details).State = EntityState.Modified;

            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cust.Add_Id }, cust);

        }

         //POST: api/Customer_Address/      
         [Authorize]
        [ResponseType(typeof(Customer_Address))]
        public IHttpActionResult PostCustomer_Address(Customer_Address customer_Address)
        {
            int C_id = 0;
            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    C_id = Convert.ToInt32(claims.Where(p => p.Type == "C_id").FirstOrDefault()?.Value);

                }

            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customer_Address.Add(customer_Address);
            db.SaveChanges();


            Customer_Details customer_Details = db.Customer_Details.Find(C_id);
            customer_Details.Add_Id = customer_Address.Add_Id;

            db.Entry(customer_Details).State = EntityState.Modified;

                db.SaveChanges();
           




            return CreatedAtRoute("DefaultApi", new { id = customer_Address.Add_Id }, customer_Address);
        }

        // DELETE: api/Customer_Address/5
        [ResponseType(typeof(Customer_Address))]
        public IHttpActionResult DeleteCustomer_Address(int id)
        {
            Customer_Address customer_Address = db.Customer_Address.Find(id);
            if (customer_Address == null)
            {
                return NotFound();
            }

            db.Customer_Address.Remove(customer_Address);
            db.SaveChanges();

            return Ok(customer_Address);
        }

        private bool Customer_AddressExists(int id)
        {
            return db.Customer_Address.Count(e => e.Add_Id == id) > 0;
        }

        private bool Customer_DetailsExists(int id)
        {
            return db.Customer_Details.Count(e => e.c_id == id) > 0;
        }
    }
}