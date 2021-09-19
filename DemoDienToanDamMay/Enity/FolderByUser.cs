namespace DemoDienToanDamMay.Enity
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("FolderByUser")]
    public partial class FolderByUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FolderByUser()
        {
            FileImgs = new HashSet<FileImg>();
        }

        [Required]
        [StringLength(50)]
        public string FolderName { get; set; }

        [Required]
        [StringLength(50)]
        public string Question { get; set; }

        [Required]
        [StringLength(50)]
        public string Answer { get; set; }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FileImg> FileImgs { get; set; }

        public virtual LoginEmail LoginEmail { get; set; }

    }
}
