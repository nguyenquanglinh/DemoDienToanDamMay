using System.Data.Entity;

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
            modelBuilder.Entity<FolderByUser>()
                .HasMany(e => e.FileImgs)
                .WithRequired(e => e.FolderByUser)
                .HasForeignKey(e => e.IdFolder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoginEmail>()
                .HasMany(e => e.FolderByUsers)
                .WithRequired(e => e.LoginEmail)
                .WillCascadeOnDelete(false);

        }
    }
}
