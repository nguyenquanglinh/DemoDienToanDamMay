namespace DemoDienToanDamMay.Enity
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("FileImg")]
    public partial class FileImg
    {
        public int id { get; set; }

        [Required]
        [StringLength(2000)]
        public string KeyImg { get; set; }

        [Required]
        [StringLength(2000)]
        public string ValueImg { get; set; }

        public int IdFolder { get; set; }

        [StringLength(2000)]
        public string FileName { get; set; }

        public virtual FolderByUser FolderByUser { get; set; }
    }
}
