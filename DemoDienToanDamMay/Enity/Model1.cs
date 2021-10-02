using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DemoDienToanDamMay.Enity
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<FileImg> FileImgs { get; set; }
        public virtual DbSet<FolderByUser> FolderByUsers { get; set; }
        public virtual DbSet<LoginEmail> LoginEmails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginEmail>()
                .Property(e => e.Code)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
