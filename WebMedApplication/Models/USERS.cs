namespace WebMedApplication
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class USERS
    {
        public Guid ID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IID { get; set; }

        [Required]
        [StringLength(50)]
        public string LOG_IN { get; set; }

        [Required]
        [MaxLength(20)]
        public byte[] PASS { get; set; }

        public DateTime DAT { get; set; }
    }
}
