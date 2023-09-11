
using System;
using System.Collections.Generic;
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
  //  [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Customer_DetailsController : ApiController
    {
        private E_martdb db = new E_martdb();

        // GET: api/Customer_Details/anynumber        profile 
        [Authorize]
        [ResponseType(typeof(Customer_Details))]
        public IHttpActionResult GetCustomer_Details(int id)
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

            var cust_details = from cust in db.Customer_Details
                                      join adm in db.Customer_Address
                                      on cust.Add_Id equals adm.Add_Id
                                      select new {c_id= cust.c_id , Name = cust.Name , Email_id = cust.Email_id , Mobile_no= cust.Mobile_no , Alt_mobile = cust.Alt_Mobile , Add_1 = adm.Add_1, Add_2 = adm.Add_2 , Country = adm.Country , Zip_code = adm.Zip_code };

                  var customer = cust_details.Where(c => c.c_id== C_id).FirstOrDefault();
                
            

         
           return Ok(customer);
        }

        // PUT: api/Customer_Details/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer_Details(int id, Customer_Details customer_Details)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer_Details.c_id)
            {
                return BadRequest();
            }

            db.Entry(customer_Details).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Customer_DetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Customer_Details
        [ResponseType(typeof(Customer_Details))]
        public IHttpActionResult PostCustomer_Details(Customer_Details customer_Details)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customer_Details.Add(customer_Details);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customer_Details.c_id }, customer_Details);
        }

        // DELETE: api/Customer_Details/5
        [ResponseType(typeof(Customer_Details))]
        public IHttpActionResult DeleteCustomer_Details(int id)
        {
            Customer_Details customer_Details = db.Customer_Details.Find(id);
            if (customer_Details == null)
            {
                return NotFound();
            }

            db.Customer_Details.Remove(customer_Details);
            db.SaveChanges();

            return Ok(customer_Details);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Customer_DetailsExists(int id)
        {
            return db.Customer_Details.Count(e => e.c_id == id) > 0;
        }
    }
}