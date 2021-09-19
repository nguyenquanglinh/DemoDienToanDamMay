namespace DemoDienToanDamMay.Enity
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LoginEmail")]
    public partial class LoginEmail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoginEmail()
        {
            FolderByUsers = new HashSet<FolderByUser>();
        }

        [Key]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string PassWords { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FolderByUser> FolderByUsers { get; set; }
    }
}
