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
using E_Mart.Models;

namespace E_Mart.Controllers
{
    public class Final_Invoice_BuyNowController : ApiController
    {
        private E_martdb db = new E_martdb();

      

        // GET: api/Final_Invoice_BuyNow/P_id
        [Authorize]
        [ResponseType(typeof(Final_Invoice))]
        public IHttpActionResult GetFinal_Invoice(int id)
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

            Product_Master prod = db.Product_Master.Find(id);

            Invoice_Dtl_Master invdtl = new Invoice_Dtl_Master();

            invdtl.C_id = C_id;
            invdtl.P_id = id;
            invdtl.MRP = prod.MRP_Price;
            invdtl.card_holder_price = prod.Cardholders_price;
            invdtl.points_redeemed = prod.points_redeem;

            Invoice_Dtl_Master inv = db.Invoice_Dtl_Master.Where((c) => c.C_id==C_id && c.P_id == id).FirstOrDefault();

            Final_Invoice f_inv = new Final_Invoice();
            f_inv.invdtl_id = inv.invdtl_id;
            f_inv.C_id = C_id;
            f_inv.inv_dt = DateTime.Now.ToString();
            f_inv.Total_Amt = invdtl.MRP;
            f_inv.Tax = (f_inv.Total_Amt / 100) * 1.8;
            f_inv.Payable_Amt = f_inv.Total_Amt + f_inv.Tax;

            db.Final_Invoice.Add(f_inv);
            db.SaveChanges();

            Customer_Details customer = db.Customer_Details.Where((c) => c.c_id == C_id).FirstOrDefault();
            Category_Master category = db.Category_Master.Where((c) => c.Catmaster_id == prod.Catmaster_id).FirstOrDefault();
            Customer_Address address = db.Customer_Address.Where((c) => c.Add_Id == customer.Add_Id).FirstOrDefault();

            var invoice = new
            {  
                inv_id = f_inv.inv_id,
                Name = customer.Name,
                prod_short_desc = prod.prod_short_desc,
                Cat_Name = category.Cat_Name,
                Add_1 = address.Add_1,
                Add_2 =address.Add_2,
                Zip_code = address.Zip_code,
                Country = address.Country,
                MRP_Price = prod.MRP_Price,
                Cardholder_price = prod.Cardholders_price,
                points_redeem = prod.points_redeem,
                inv_dt = f_inv.inv_dt,
                Total_amt = f_inv.Total_Amt,
                Tax = f_inv.Tax,
                Payable_amt = f_inv.Payable_Amt
            };

            return Ok(invoice);
        }

        // PUT: api/Final_Invoice_BuyNow/5
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

        // POST: api/Final_Invoice_BuyNow
        [ResponseType(typeof(Final_Invoice))]
        public IHttpActionResult PostFinal_Invoice(Final_Invoice final_Invoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Final_Invoice.Add(final_Invoice);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Final_InvoiceExists(final_Invoice.inv_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = final_Invoice.inv_id }, final_Invoice);
        }

        // DELETE: api/Final_Invoice_BuyNow/5
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