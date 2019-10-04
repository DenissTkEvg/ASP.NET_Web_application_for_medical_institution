namespace WebMedApplication
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EVENTS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EVENTS()
        {
            MEDICAMENT_EVENT = new HashSet<MEDICAMENT_EVENT>();
        }

        public Guid ID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IID { get; set; }

        [Required]
        [StringLength(500)]
        public string EVENT_NAME { get; set; }

        public DateTime DAT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEDICAMENT_EVENT> MEDICAMENT_EVENT { get; set; }
    }
}
