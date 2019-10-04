namespace WebMedApplication
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MedDBContext : DbContext
    {
        public MedDBContext()
            : base("name=MedDBContext")
        {
        }

        public virtual DbSet<EVENTS> EVENTS { get; set; }
        public virtual DbSet<MEDICAMENT> MEDICAMENT { get; set; }
        public virtual DbSet<MEDICAMENT_EVENT> MEDICAMENT_EVENT { get; set; }
        public virtual DbSet<PKU_GROUP> PKU_GROUP { get; set; }
        public virtual DbSet<USERS> USERS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EVENTS>()
                .HasMany(e => e.MEDICAMENT_EVENT)
                .WithRequired(e => e.EVENTS)
                .HasForeignKey(e => e.EVENT_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MEDICAMENT>()
                .HasMany(e => e.MEDICAMENT_EVENT)
                .WithRequired(e => e.MEDICAMENT)
                .HasForeignKey(e => e.MEDICAMENT_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PKU_GROUP>()
                .HasMany(e => e.MEDICAMENT)
                .WithOptional(e => e.PKU_GROUP)
                .HasForeignKey(e => e.PKU_GROUP_ID);
        }
    }
}
