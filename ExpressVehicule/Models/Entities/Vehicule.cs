namespace ExpressVoitures.Models.Entities
{
    public partial class Vehicule
    {
        public int Id { get; set; }
        public int Annee { get; set; }
        public string Marque { get; set; }
        public string Model { get; set; }
        public string Finition { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
