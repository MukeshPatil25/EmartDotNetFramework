using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using E_Mart.Models;

namespace E_Mart.Controllers
{
    public class emCard_HoldersController : ApiController
    {
        private E_martdb db = new E_martdb();

        // GET: api/emCard_Holders
        public IQueryable<emCard_Holders> GetemCard_Holders()
        {
            return db.emCard_Holders;
        }

        // GET: api/emCard_Holders/5
        [ResponseType(typeof(emCard_Holders))]
        public IHttpActionResult GetemCard_Holders(int id)
        {
            emCard_Holders emCard_Holders = db.emCard_Holders.Find(id);
            if (emCard_Holders == null)
            {
                return NotFound();
            }

            return Ok(emCard_Holders);
        }

        // PUT: api/emCard_Holders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutemCard_Holders(int id, emCard_Holders emCard_Holders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != emCard_Holders.card_id)
            {
                return BadRequest();
            }

            db.Entry(emCard_Holders).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!emCard_HoldersExists(id))
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

        // POST: api/emCard_Holders
        [ResponseType(typeof(emCard_Holders))]
        public IHttpActionResult PostemCard_Holders(emCard_Holders emCard_Holders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.emCard_Holders.Add(emCard_Holders);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = emCard_Holders.card_id }, emCard_Holders);
        }

        // DELETE: api/emCard_Holders/5
        [ResponseType(typeof(emCard_Holders))]
        public IHttpActionResult DeleteemCard_Holders(int id)
        {
            emCard_Holders emCard_Holders = db.emCard_Holders.Find(id);
            if (emCard_Holders == null)
            {
                return NotFound();
            }

            db.emCard_Holders.Remove(emCard_Holders);
            db.SaveChanges();

            return Ok(emCard_Holders);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool emCard_HoldersExists(int id)
        {
            return db.emCard_Holders.Count(e => e.card_id == id) > 0;
        }
    }
}