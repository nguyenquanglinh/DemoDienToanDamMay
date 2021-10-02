namespace DemoDienToanDamMay.Enity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoginEmail")]
    public partial class LoginEmail
    {
        [Key]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string PassWords { get; set; }

        [Required]
        [StringLength(8)]
        public string Code { get; set; }
    }
}
