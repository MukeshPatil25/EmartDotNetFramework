namespace E_Mart.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Final_Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int inv_id { get; set; }

        public int? invdtl_id { get; set; }

        public int? C_id { get; set; }

        [StringLength(255)]
        public string inv_dt { get; set; }

        public double? Total_Amt { get; set; }

        public double? Tax { get; set; }

        public double? Payable_Amt { get; set; }
    }
}
