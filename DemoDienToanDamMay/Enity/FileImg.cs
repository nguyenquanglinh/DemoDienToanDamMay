namespace DemoDienToanDamMay.Enity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FileImg")]
    public partial class FileImg
    {
        [Required]
        [StringLength(2000)]
        public string KeyImg { get; set; }

        [Required]
        [StringLength(2000)]
        public string ValueImg { get; set; }

        [Required]
        [StringLength(50)]
        public string FolderName { get; set; }

        [Key]
        [StringLength(50)]
        public string FileName { get; set; }
    }
}
