namespace E_Mart.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Category_Master
    {
        [Key]
        public int Catmaster_id { get; set; }

        [StringLength(10)]
        public string cat_id { get; set; }

        [StringLength(10)]
        public string Subcat_id { get; set; }

        [StringLength(255)]
        public string Cat_img_path { get; set; }

        public bool? Flag { get; set; }

        public string Cat_Name { get; set; }
    }
}
