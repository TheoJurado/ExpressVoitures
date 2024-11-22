namespace ExpressVoitures.Models.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public double Price { get; set; }

        public TransactionType Type { get; set; }

        public int? TransactionAchatId { get; set; }
        public Vehicule? VehiculeAchat { get; set; }

        public int? TransactionVenteId { get; set; }
        public Vehicule? VehiculeVente { get; set; }
    }

    public enum TransactionType
    {
        Sell = 0, Buy = 1
    }
}
