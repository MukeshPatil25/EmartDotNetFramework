namespace E_Mart.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Invoice_Dtl_Master
    {
        [Key]
        public int invdtl_id { get; set; }

        public int? P_id { get; set; }

        public double? MRP { get; set; }

        public double? card_holder_price { get; set; }

        public int? points_redeemed { get; set; }

        public int? C_id { get; set; }
    }
}
