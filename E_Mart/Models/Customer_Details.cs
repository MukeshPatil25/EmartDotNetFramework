namespace E_Mart.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer_Details
    {
        [Key]
        public int c_id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(100)]
        public string Email_id { get; set; }

        [StringLength(100)]
        public string Mobile_no { get; set; }

        [StringLength(100)]
        public string Alt_Mobile { get; set; }

        public int? Add_Id { get; set; }

        public int? Points { get; set; }
    }
}
