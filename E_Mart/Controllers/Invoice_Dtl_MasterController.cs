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
    public class Invoice_Dtl_MasterController : ApiController
    {
        private E_martdb db = new E_martdb();

        // GET: api/Invoice_Dtl_Master
        [Authorize]
        public IQueryable GetInvoice_Dtl_Master()
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
            var prices = db.Invoice_Dtl_Master.Where(c => c.C_id == C_id).Select(i => i.MRP);
            double total = 0;
            foreach (double p in prices)
            {
                total = total + p;
            }

            var inv_record = from i in db.Invoice_Dtl_Master
                             join p in db.Product_Master
                             on i.P_id equals p.P_id
                             select new { C_id = i.C_id, Catmaster_id = p.Catmaster_id, P_id = p.P_id, prod_short_desc = p.prod_short_desc, prod_long_desc = p.prod_long_desc, MRP_Price = p.MRP_Price, Cardholders_price = p.Cardholders_price, points_redeem = p.points_redeem, invdtl_id = i.invdtl_id, Total_price = total };

            var inv_dtl = from p in inv_record
                          join c in db.Category_Master
                          on p.Catmaster_id equals c.Catmaster_id
                          select new { C_id = p.C_id, Catmaster_id = p.Catmaster_id, P_id = p.P_id, prod_short_desc = p.prod_short_desc, prod_long_desc = p.prod_long_desc, MRP_Price = p.MRP_Price, Cardholders_price = p.Cardholders_price, points_redeem = p.points_redeem, invdtl_id = p.invdtl_id, Total_price = total, Cat_Name = c.Cat_Name, Cat_img_path = c.Cat_img_path };

            return (IQueryable)inv_dtl.Where(c => c.C_id == C_id);
        }

        // GET: api/Invoice_Dtl_Master/P_id                             add to cart from product 
        [Authorize]
        public IQueryable GetInvoice_Dtl_Master(int id)
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
            Product_Master product = db.Product_Master.Find(id);

            Invoice_Dtl_Master invoice = new Invoice_Dtl_Master();
            invoice.P_id = id;
            invoice.MRP = product.MRP_Price;
            invoice.card_holder_price = product.Cardholders_price;
            invoice.points_redeemed = product.points_redeem;
            invoice.C_id = C_id;

            db.Invoice_Dtl_Master.Add(invoice);
            db.SaveChanges();

            var prices = db.Invoice_Dtl_Master.Where(c => c.C_id==C_id).Select(i => i.MRP);
            double total = 0;
            foreach (double p in prices)
            {
                total = total + p;
            }

            var inv_record = from i in db.Invoice_Dtl_Master
                             join p in db.Product_Master
                             on i.P_id equals p.P_id
                             select new { C_id = i.C_id, Catmaster_id = p.Catmaster_id, P_id = p.P_id, prod_short_desc = p.prod_short_desc, prod_long_desc = p.prod_long_desc, MRP_Price = p.MRP_Price, Cardholders_price = p.Cardholders_price, points_redeem = p.points_redeem, invdtl_id = i.invdtl_id, Total_price = total };

            var inv_dtl = from p in inv_record
                          join c in db.Category_Master
                          on p.Catmaster_id equals c.Catmaster_id
                          select new { C_id = p.C_id, Catmaster_id = p.Catmaster_id, P_id = p.P_id, prod_short_desc = p.prod_short_desc, prod_long_desc = p.prod_long_desc, MRP_Price = p.MRP_Price, Cardholders_price = p.Cardholders_price, points_redeem = p.points_redeem, invdtl_id = p.invdtl_id, Total_price = total, Cat_Name = c.Cat_Name, Cat_img_path = c.Cat_img_path };

            return (IQueryable)inv_dtl.Where(c => c.C_id == C_id);
        }







        // PUT: api/Invoice_Dtl_Master/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInvoice_Dtl_Master(int id, Invoice_Dtl_Master invoice_Dtl_Master)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoice_Dtl_Master.invdtl_id)
            {
                return BadRequest();
            }

            db.Entry(invoice_Dtl_Master).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Invoice_Dtl_MasterExists(id))
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

        // POST: api/Invoice_Dtl_Master
        //[ResponseType(typeof(Invoice_Dtl_Master))]
        //public IHttpActionResult PostInvoice_Dtl_Master(Invoice_Dtl_Master invoice_Dtl_Master)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Invoice_Dtl_Master.Add(invoice_Dtl_Master);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = invoice_Dtl_Master.invdtl_id }, invoice_Dtl_Master);
        //}

        // DELETE: api/Invoice_Dtl_Master/5
        [Authorize]
        [ResponseType(typeof(Invoice_Dtl_Master))]
        public IHttpActionResult DeleteInvoice_Dtl_Master(int id)
        {
            Invoice_Dtl_Master invoice_Dtl_Master = db.Invoice_Dtl_Master.Find(id);
            if (invoice_Dtl_Master == null)
            {
                return NotFound();
            }

            db.Invoice_Dtl_Master.Remove(invoice_Dtl_Master);
            db.SaveChanges();

            return Ok(invoice_Dtl_Master);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Invoice_Dtl_MasterExists(int id)
        {
            return db.Invoice_Dtl_Master.Count(e => e.invdtl_id == id) > 0;
        }
    }
}