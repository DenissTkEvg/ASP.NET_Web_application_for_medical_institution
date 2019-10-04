namespace WebMedApplication
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MEDICAMENT_EVENT
    {
        public Guid ID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IID { get; set; }

        public Guid EVENT_ID { get; set; }

        public Guid MEDICAMENT_ID { get; set; }

        public DateTime EVENT_DATE { get; set; }

        public int QUANTITY { get; set; }

        public virtual EVENTS EVENTS { get; set; }

        public virtual MEDICAMENT MEDICAMENT { get; set; }
    }
}
