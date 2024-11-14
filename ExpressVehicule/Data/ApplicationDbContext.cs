using ExpressVoitures.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitures.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<Annonce> Annonces { get; set; }
        public virtual DbSet<Reparation> Reparations { get; set; }
        public virtual DbSet<Vehicule> Vehicules { get; set; }
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

            // Relation Vehicule -> Transaction (1..2 : 1..1)
            modelBuilder.Entity<Vehicule>()
                .HasOne(v => v.TransactionAchat)
                .WithOne(t => t.VehiculeAchat)
                .HasForeignKey<Transaction>(t => t.TransactionAchatId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vehicule>()
                .HasOne(v => v.TransactionVente)
                .WithOne(t => t.VehiculeVente)
                .HasForeignKey<Transaction>(t => t.TransactionVenteId)
                .OnDelete(DeleteBehavior.Restrict);
            /*
            modelBuilder.Entity<Transaction>()
                .Navigation(t => t.Vehicule)
                .IsRequired(false);/**/

            // Relation Vehicule -> Reparation (0..1 : 1..1)
            modelBuilder.Entity<Reparation>()
                .HasOne(r => r.Vehicule)
                .WithMany(v => v.Reparations)
                .HasForeignKey(r => r.VehiculeId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
