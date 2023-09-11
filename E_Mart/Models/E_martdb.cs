using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace E_Mart.Models
{
    public partial class E_martdb : DbContext
    {
        public E_martdb() : base("name=E_martdb")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Category_Master> Category_Master { get; set; }
        public virtual DbSet<Customer_Address> Customer_Address { get; set; }
        public virtual DbSet<Customer_Details> Customer_Details { get; set; }
        public virtual DbSet<emCard_Holders> emCard_Holders { get; set; }
        public virtual DbSet<Final_Invoice> Final_Invoice { get; set; }
        public virtual DbSet<Invoice_Dtl_Master> Invoice_Dtl_Master { get; set; }
        public virtual DbSet<Prod_Dtl_Master> Prod_Dtl_Master { get; set; }
        public virtual DbSet<Product_Master> Product_Master { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category_Master>()
                .Property(e => e.cat_id)
                .IsUnicode(false);

            modelBuilder.Entity<Category_Master>()
                .Property(e => e.Subcat_id)
                .IsUnicode(false);

            modelBuilder.Entity<Category_Master>()
                .Property(e => e.Cat_img_path)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_Address>()
                .Property(e => e.Add_1)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_Address>()
                .Property(e => e.Add_2)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_Address>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_Details>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_Details>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_Details>()
                .Property(e => e.Email_id)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_Details>()
                .Property(e => e.Mobile_no)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_Details>()
                .Property(e => e.Alt_Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<Final_Invoice>()
                .Property(e => e.inv_dt)
                .IsUnicode(false);

            modelBuilder.Entity<Prod_Dtl_Master>()
                .Property(e => e.Config_Dtls)
                .IsUnicode(false);

            modelBuilder.Entity<Product_Master>()
                .Property(e => e.prod_short_desc)
                .IsUnicode(false);

            modelBuilder.Entity<Product_Master>()
                .Property(e => e.prod_long_desc)
                .IsUnicode(false);
        }
    }
}
