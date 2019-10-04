namespace WebMedApplication
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MEDICAMENT")]
    public partial class MEDICAMENT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MEDICAMENT()
        {
            MEDICAMENT_EVENT = new HashSet<MEDICAMENT_EVENT>();
        }

        public Guid ID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IID { get; set; }

        public Guid? PKU_GROUP_ID { get; set; }

        [Required]
        [StringLength(1000)]
        public string TRADE_NAME { get; set; }

        [StringLength(1000)]
        public string ACTIVE_SUBSTANCE { get; set; }

        public int? QUANTITY_BEG { get; set; }

        [StringLength(50)]
        public string BARCODE { get; set; }

        public DateTime DAT { get; set; }

        public virtual PKU_GROUP PKU_GROUP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEDICAMENT_EVENT> MEDICAMENT_EVENT { get; set; }
    }
}
