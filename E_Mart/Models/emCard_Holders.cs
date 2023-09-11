namespace E_Mart.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class emCard_Holders
    {
        [Key]
        public int card_id { get; set; }

        public int? C_id { get; set; }

        public virtual Customer_Details Customer_Details { get; set; }
    }
}
