using ExpressVoitures.Models.Entities;

namespace ExpressVoitures.Models.ViewModels
{
    public class DataAllInclusive
    {
        public Annonce dataAnnonce { get; set; } = new Annonce();
        public Transaction TransactionA { get; set; } = new Transaction();
        public Transaction? TransactionV { get; set; }
        public List<Reparation> dataReparations { get; set; } = new List<Reparation>();
        public Vehicule dataVehicule { get; set; } = new Vehicule();
        public IFormFile? Photo { get; set; }
    }
}
