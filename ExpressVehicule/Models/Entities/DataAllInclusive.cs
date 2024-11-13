namespace ExpressVoitures.Models.Entities
{
    public class DataAllInclusive
    {
        public int Id { get; set; }
        public Annonce Annonce { get; set; } = new Annonce();
        public Transaction TransactionA { get; set; } = new Transaction();
        public Transaction TransactionV { get; set; } = new Transaction();
        public Reparation Reparation { get; set; } = new Reparation();
        public Vehicule Vehicule { get; set; } = new Vehicule();
        public IFormFile? Photo { get; set; }
    }
}
