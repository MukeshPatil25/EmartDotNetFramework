namespace E_Mart.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer_Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Add_Id { get; set; }

        [StringLength(255)]
        public string Add_1 { get; set; }

        [StringLength(255)]
        public string Add_2 { get; set; }


        public int Zip_code { get; set; }

        [StringLength(255)]
        public string Country { get; set; }
    }
}
