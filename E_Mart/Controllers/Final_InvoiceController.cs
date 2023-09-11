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
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using E_Mart.Models;

namespace E_Mart.Controllers
{
    public class Final_InvoiceController : ApiController
    {
        private E_martdb db = new E_martdb();


        // GET: api/Final_Invoice/any number             Checkout from cart
        [Authorize]
        public IQueryable GetFinal_Invoice(int id)
        {
            int C_id=0;
            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                     C_id =Convert.ToInt32( claims.Where(p => p.Type == "C_id").FirstOrDefault()?.Value);

                }
            
            }

            Final_Invoice f_inv = new Final_Invoice();


            f_inv.C_id = C_id;

            String date = DateTime.Now.ToString();
            f_inv.inv_dt = date;

            double Total = 0.00;
            var prices = db.Invoice_Dtl_Master.Where((c) => c.C_id == C_id).Select(c => c.MRP);
            foreach (double p in prices)
            {
                Total = Total + p;
            }

            f_inv.Total_Amt = Total;
            f_inv.Tax = (f_inv.Total_Amt / 100) * 1.8;
            f_inv.Payable_Amt = f_inv.Total_Amt + f_inv.Tax;

            db.Final_Invoice.Add(f_inv);
            db.SaveChanges();

            Final_Invoice invoice = db.Final_Invoice.Where((i) => i.inv_dt == date).FirstOrDefault();
            Customer_Details cust = db.Customer_Details.Where((c) => c.c_id == C_id ).FirstOrDefault();
            Customer_Address address = db.Customer_Address.Where((c) => c.Add_Id == cust.Add_Id).FirstOrDefault();

            var inv_record = from i in db.Invoice_Dtl_Master
                             join p in db.Product_Master
                             on i.P_id equals p.P_id
                             select new { C_id = i.C_id, Catmaster_id = p.Catmaster_id, P_id = p.P_id, prod_short_desc = p.prod_short_desc, prod_long_desc = p.prod_long_desc, MRP_Price = p.MRP_Price, Cardholders_price = p.Cardholders_price, points_redeem = p.points_redeem, invdtl_id = i.invdtl_id, Total_price = Total };

            var inv_dtl = from p in inv_record
                          join c in db.Category_Master
                          on p.Catmaster_id equals c.Catmaster_id
                          select new { C_id = p.C_id, 
                              P_id = p.P_id, 
                              Name= cust.Name,
                              prod_short_desc = p.prod_short_desc,
                              prod_long_desc = p.prod_long_desc,
                              MRP_Price = p.MRP_Price, 
                              Cardholders_price = p.Cardholders_price, 
                              points_redeem = p.points_redeem, 
                              invdtl_id = p.invdtl_id, 
                              Add_1 = address.Add_1,
                              Add_2 = address.Add_2,
                              Zip_code = address.Zip_code,
                              Country=address.Country,
                              inv_dt= invoice.inv_dt,
                              inv_id = invoice.inv_id,
                              Total_amt = invoice.Total_Amt,
                              Tax = invoice.Tax,
                              Payable_amt = invoice.Payable_Amt,
                              Cat_Name = c.Cat_Name,
                              Cat_img_path = c.Cat_img_path 
                          };

           
            return (IQueryable)inv_dtl.Where((c) => c.C_id ==C_id );

        }
    
    // PUT: api/Final_Invoice/5
    [ResponseType(typeof(void))]
    public IHttpActionResult PutFinal_Invoice(int id, Final_Invoice final_Invoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != final_Invoice.inv_id)
            {
                return BadRequest();
            }

            db.Entry(final_Invoice).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Final_InvoiceExists(id))
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

    IHttpActionResult StatusCode(HttpStatusCode noContent)
    {
        throw new NotImplementedException();
    }

    
    IHttpActionResult BadRequest(ModelState modelState)
    {
        throw new NotImplementedException();
    }

    // DELETE: api/Final_Invoice/5
    [ResponseType(typeof(Final_Invoice))]
        public IHttpActionResult DeleteFinal_Invoice(int id)
        {
            Final_Invoice final_Invoice = db.Final_Invoice.Find(id);
            if (final_Invoice == null)
            {
                return NotFound();
            }

            db.Final_Invoice.Remove(final_Invoice);
            db.SaveChanges();

            return Ok(final_Invoice);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Final_InvoiceExists(int id)
        {
            return db.Final_Invoice.Count(e => e.inv_id == id) > 0;
        }
    }
}