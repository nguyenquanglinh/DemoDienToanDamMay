namespace DemoDienToanDamMay.Enity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FolderByUser")]
    public partial class FolderByUser
    {
        [Key]
        [StringLength(50)]
        public string FolderName { get; set; }

        [Required]
        [StringLength(50)]
        public string Question { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Answer { get; set; }

        public virtual LoginEmail LoginEmail { get; set; }
    }
}
