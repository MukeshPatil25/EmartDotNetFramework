using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Description;
using E_Mart.Models;

namespace E_Mart.Controllers
{
    public class Orders_SearchController : ApiController
    {
        private E_martdb db = new E_martdb();

        // GET: api/Orders_Search           Total Orders
        [Authorize]
        public IQueryable GetCategory_Master()
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

           var a = from p in db.Invoice_Dtl_Master
                   join f in db.Final_Invoice
                   on p.invdtl_id equals f.invdtl_id
                   select new
                   {
                       C_id = p.C_id,
                       P_id = p.P_id,
                       MRP_Price = p.MRP,
                       Cardholders_price = p.card_holder_price,
                       points_redeem = p.points_redeemed,
                       invdtl_id = p.invdtl_id,
                       inv_dt = f.inv_dt,
                       inv_id = f.inv_id,
                       Total_amt = f.Total_Amt,
                       Tax = f.Tax,
                       Payable_amt = f.Payable_Amt
                   };


            var b = from p in a
                    join f in db.Product_Master
                    on p.P_id equals f.P_id
                    select new
                    {
                        C_id = p.C_id,
                        P_id = p.P_id,
                        Catmaster_id = f.Catmaster_id,
                        MRP_Price = p.MRP_Price,
                        Cardholders_price = p.Cardholders_price,
                        points_redeem = p.points_redeem,
                        invdtl_id = p.invdtl_id,
                        inv_dt = p.inv_dt,
                        inv_id = p.inv_id,
                        Total_amt = p.Total_amt,
                        Tax = p.Tax,
                        Payable_amt = p.Payable_amt
                    };


            var c = from p in b
                    join f in db.Category_Master
                    on p.Catmaster_id equals f.Catmaster_id
                    select new
                    {
                        C_id = p.C_id,
                        P_id = p.P_id,
                        Catmaster_id = f.Catmaster_id,
                        MRP_Price = p.MRP_Price,
                        Cat_Name = f.Cat_Name,
                        Cat_img_path = f.Cat_img_path,
                        Cardholders_price = p.Cardholders_price,
                        points_redeem = p.points_redeem,
                        invdtl_id = p.invdtl_id,
                        inv_dt = p.inv_dt,
                        inv_id = p.inv_id,
                        Total_amt = p.Total_amt,
                        Tax = p.Tax,
                        Payable_amt = p.Payable_amt
                    };



            return (IQueryable)c.Where((i) => i.C_id == C_id);

        

        }

        // GET: api/Orders_Search/search string         search bar
        [ResponseType(typeof(Category_Master))]
        public IQueryable GetCategory_Master(String id)
        {
            var cat_details = from catm in db.Category_Master
                              join prodm in db.Product_Master
                              on catm.Catmaster_id equals prodm.Catmaster_id
                              select new { Catmaster_id = catm.Catmaster_id, Flag = catm.Flag, Cat_id = catm.cat_id, subcat_id = catm.Subcat_id, Cat_name = catm.Cat_Name, Cat_img_path = catm.Cat_img_path, P_id = prodm.P_id, prod_short_desc = prodm.prod_short_desc, prod_long_desc = prodm.prod_long_desc, MRP_Price = prodm.MRP_Price, Cardholders_price = prodm.Cardholders_price, points_redeem = prodm.points_redeem };

            

            return cat_details.Where((c) => c.Cat_name.Contains(id) && c.Flag == true);
        }

        // PUT: api/Orders_Search/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategory_Master(int id, Category_Master category_Master)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category_Master.Catmaster_id)
            {
                return BadRequest();
            }

            db.Entry(category_Master).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Category_MasterExists(id))
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

        // POST: api/Orders_Search
        [ResponseType(typeof(Category_Master))]
        public IHttpActionResult PostCategory_Master(Category_Master category_Master)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Category_Master.Add(category_Master);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category_Master.Catmaster_id }, category_Master);
        }

        // DELETE: api/Orders_Search/5
        [ResponseType(typeof(Category_Master))]
        public IHttpActionResult DeleteCategory_Master(int id)
        {
            Category_Master category_Master = db.Category_Master.Find(id);
            if (category_Master == null)
            {
                return NotFound();
            }

            db.Category_Master.Remove(category_Master);
            db.SaveChanges();

            return Ok(category_Master);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Category_MasterExists(int id)
        {
            return db.Category_Master.Count(e => e.Catmaster_id == id) > 0;
        }
    }
}