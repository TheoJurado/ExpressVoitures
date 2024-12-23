﻿using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models.Entities
{
    public class Vehicule
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CodeVin { get; set; }
        public StatutVehicule Statut { get; set; }
        public int Annee { get; set; }
        
        public string Marque { get; set; }
        public string Model { get; set; }
        public string Finition { get; set; }

        public ICollection<Reparation> Reparations { get; set; } = new List<Reparation>();

        public Transaction TransactionAchat { get; set; }

        public Transaction? TransactionVente { get; set; }
    }

    public enum StatutVehicule
    {
        EnReparation = 0, EnVente = 1, Vendu = 2
    }
}
