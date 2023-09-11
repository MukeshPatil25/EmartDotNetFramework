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
    public class Prod_Dtl_MasterController : ApiController
    {
        private E_martdb db = new E_martdb();

        // GET: api/Prod_Dtl_Master/ P_id
        [ResponseType(typeof(Prod_Dtl_Master))]
        public IHttpActionResult GetProd_Dtl_Master(int id)
        {
            var cat_details = from catm in db.Category_Master
                              join prodm in db.Product_Master
                              on catm.Catmaster_id equals prodm.Catmaster_id
                              select new { Catmaster_id = catm.Catmaster_id, cat_id = catm.cat_id, Subcat_id = catm.Subcat_id, Cat_Name = catm.Cat_Name, Cat_img_path = catm.Cat_img_path, Flag = catm.Flag, P_id = prodm.P_id, prod_short_desc = prodm.prod_short_desc, prod_long_desc = prodm.prod_long_desc, MRP_Price = prodm.MRP_Price, Cardholders_price = prodm.Cardholders_price, points_redeem = prodm.points_redeem };

        

            var prod= cat_details.Where((p)=>p.P_id == id);

            return Ok(prod);
        }

        private bool Prod_Dtl_MasterExists(int id)
        {
            return db.Prod_Dtl_Master.Count(e => e.Prod_Dtl_id == id) > 0;
        }
    }
}