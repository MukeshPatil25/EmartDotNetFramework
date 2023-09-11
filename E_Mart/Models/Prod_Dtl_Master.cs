namespace E_Mart.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Prod_Dtl_Master
    {
        [Key]
        public int Prod_Dtl_id { get; set; }

        public int? P_id { get; set; }

        public int? Config_id { get; set; }

        [StringLength(255)]
        public string Config_Dtls { get; set; }

        public virtual Product_Master Product_Master { get; set; }
    }
}
