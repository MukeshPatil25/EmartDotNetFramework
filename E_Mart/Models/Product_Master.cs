namespace E_Mart.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product_Master
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product_Master()
        {
            Invoice_Dtl_Master = new HashSet<Invoice_Dtl_Master>();
            Prod_Dtl_Master = new HashSet<Prod_Dtl_Master>();
        }

        [Key]
        public int P_id { get; set; }

        public int? Catmaster_id { get; set; }

        [StringLength(255)]
        public string prod_short_desc { get; set; }

        [StringLength(255)]
        public string prod_long_desc { get; set; }

        public double? MRP_Price { get; set; }

        public double? Cardholders_price { get; set; }

        public int? points_redeem { get; set; }

        public virtual Category_Master Category_Master { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice_Dtl_Master> Invoice_Dtl_Master { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prod_Dtl_Master> Prod_Dtl_Master { get; set; }
    }
}
