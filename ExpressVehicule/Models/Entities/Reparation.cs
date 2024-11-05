namespace ExpressVoitures.Models.Entities
{
    public partial class Reparation
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public double Prix { get; set; }

        public int? TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}
