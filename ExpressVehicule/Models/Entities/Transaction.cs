namespace ExpressVoitures.Models.Entities
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public string CodeVin { get; set; }
        public DateOnly DateAchat { get; set; }
        public double PrixAchat { get; set; }
        public DateOnly DateDispoVente { get; set; }
        public double PrixVente { get; set; }
        public DateOnly DateVente { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }

        public int VehiculeId { get; set; }
        public Vehicule Vehicule { get; set; }

        public ICollection<Reparation> Reparations { get; set; } = new List<Reparation>();
    }
}
