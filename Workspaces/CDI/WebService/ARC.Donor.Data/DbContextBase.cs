namespace ARC.Donor.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBContextBase : DbContext
    {
        public DBContextBase()
            : base("name=TDConnectionEF")
        {
            
        }

        public DBContextBase(string conn)
            : base(conn)
        {
            
        }

        public virtual DbSet<Entity.stwrd_dnr_prfle> stwrd_dnr_prfles { get; set; }

        //public virtual DbSet<DEPARTMENT> DEPARTMENTs { get; set; }
        //public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        //public virtual DbSet<ProductSubCategory> ProductSubCategories { get; set; }
        //public virtual DbSet<SOP> SOPs { get; set; }
        //public virtual DbSet<SOPBookManagement> SOPBookManagements { get; set; }
        //public virtual DbSet<SOPBookMap> SOPBookMaps { get; set; }
        //public virtual DbSet<SOPKeyword> SOPKeywords { get; set; }
        //public virtual DbSet<SOPKeywordMap> SOPKeywordMaps { get; set; }
        //public virtual DbSet<SOPRevision> SOPRevisions { get; set; }
        //public virtual DbSet<SOPTrainer> SOPTrainers { get; set; }
        //public virtual DbSet<SOPTraining> SOPTrainings { get; set; }
        //public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        //public virtual DbSet<Vendor> Vendors { get; set; }        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entity.stwrd_dnr_prfle>()
                .HasKey(e => e.constituent_id);
            //modelBuilder.Entity<Vendor>()
            //    .HasMany(e => e.SOPs)
            //    .WithOptional(e => e.Vendor)
            //    .HasForeignKey(e => e.VendorID);

            //modelBuilder.Entity<DEPARTMENT>()
            //    .Property(e => e.DEPARTMENTSHORTNAME)
            //    .IsUnicode(false);

            //modelBuilder.Entity<DEPARTMENT>()
            //    .Property(e => e.DEPARTMENTNAME)
            //    .IsUnicode(false);

            //modelBuilder.Entity<DEPARTMENT>()
            //    .HasMany(e => e.SOPs)
            //    .WithOptional(e => e.DEPARTMENT)
            //    .HasForeignKey(e => e.DepartmentID);

            //modelBuilder.Entity<ProductCategory>()
            //    .HasMany(e => e.ProductSubCategories)
            //    .WithRequired(e => e.ProductCategory1)
            //    .HasForeignKey(e => e.ProductCategory)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<ProductCategory>()
            //    .HasMany(e => e.SOPs)
            //    .WithOptional(e => e.ProductCategory1)
            //    .HasForeignKey(e => e.ProductCategory);

            //modelBuilder.Entity<ProductSubCategory>()
            //    .HasMany(e => e.SOPs)
            //    .WithOptional(e => e.ProductSubCategory1)
            //    .HasForeignKey(e => e.ProductSubCategory);

            //modelBuilder.Entity<SOP>()
            //    .HasMany(e => e.SOPBookMaps)
            //    .WithRequired(e => e.SOP)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<SOP>()
            //    .HasMany(e => e.SOPKeywordMaps)
            //    .WithRequired(e => e.SOP)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<SOP>()
            //    .HasMany(e => e.SOPRevisions)
            //    .WithRequired(e => e.SOP)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<SOP>()
            //    .HasMany(e => e.SOPTrainings)
            //    .WithRequired(e => e.SOP)
            //    .WillCascadeOnDelete(false);

            ////modelBuilder.Entity<SOP>()
            ////    .HasMany(e => e.SOPKeywords)
            ////    .WithMany(e => e.SOPs)
            ////    .Map(m => m.ToTable("SOPKeywordMap").MapLeftKey("SOPID").MapRightKey("SOPKeywordID"));

            //modelBuilder.Entity<SOPBookManagement>()
            //    .HasMany(e => e.SOPBookMaps)
            //    .WithRequired(e => e.SOPBookManagement)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<SOPKeyword>()
            //    .HasMany(e => e.SOPKeywordMaps)
            //    .WithRequired(e => e.SOPKeyword)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<SOPRevision>()
            //    .HasMany(e => e.SOPTrainings)
            //    .WithRequired(e => e.SOPRevision)
            //    .WillCascadeOnDelete(false);
        }
    }
}
