using ExpressVoitures.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitures.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<Vehicule> Vehicules { get; set; }
        public virtual DbSet<Reparation> Reparations { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }

        /// <summary>
        /// Add-migration -context ApplicationDbContext -Output Data/Migations InitDataBase
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relation Transaction -> Vehicule (0..1 : 0..*)
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Vehicule)
                .WithMany(v => v.Transactions)
                .HasForeignKey(t => t.VehiculeId)
                .IsRequired(false);

            // Relation Transaction -> Reparation (0..1 : 0..*)
            modelBuilder.Entity<Reparation>()
                .HasOne(r => r.Transaction)
                .WithMany(t => t.Reparations)
                .HasForeignKey(r => r.TransactionId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
