using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using E_Mart.Models;

namespace E_Mart.Controllers
{
  
    public class Category_MasterController : ApiController
    {
        private E_martdb db = new E_martdb();

        // GET: api/Category_Master       for all Main Categories
        public IQueryable<Category_Master> GetCategory_Master()
        {
            return db.Category_Master.Where((c) => c.Subcat_id == null);

        }
        

        // GET: api/Category_Master/Category            for All Subcategories of specific category
        public IQueryable GetCategory_Master(string id)
        {   if(id == "all")
            {
                var cat_details = from catm in db.Category_Master
                                  join prodm in db.Product_Master
                                  on catm.Catmaster_id equals prodm.Catmaster_id
                                  select new { Catmaster_id = catm.Catmaster_id, Flag = catm.Flag ,Cat_id = catm.cat_id, subcat_id = catm.Subcat_id, Cat_name = catm.Cat_Name, Cat_img_path = catm.Cat_img_path, P_id = prodm.P_id, prod_short_desc = prodm.prod_short_desc, prod_long_desc = prodm.prod_long_desc, MRP_Price = prodm.MRP_Price, Cardholders_price = prodm.Cardholders_price, points_redeem = prodm.points_redeem };

                return cat_details.Where((c) => c.Flag == true);

            }

            var cat_master = db.Category_Master.Where((c) => c.Subcat_id == id);

            foreach (Category_Master cat in cat_master)
            {
                if (cat.Flag == true)
                {
                    var cat_details = from catm in db.Category_Master
                                      join prodm in db.Product_Master
                                      on catm.Catmaster_id equals prodm.Catmaster_id
                                      select new { Catmaster_id=catm.Catmaster_id , Cat_id=catm.cat_id , subcat_id=catm.Subcat_id, Cat_name = catm.Cat_Name, Cat_img_path = catm.Cat_img_path , P_id=prodm.P_id, prod_short_desc = prodm.prod_short_desc, prod_long_desc = prodm.prod_long_desc, MRP_Price = prodm.MRP_Price, Cardholders_price = prodm.Cardholders_price, points_redeem = prodm.points_redeem };

                    return cat_details.Where( c => c.subcat_id == id);
                }
            }

            return cat_master;
        }



    }
}