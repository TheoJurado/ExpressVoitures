using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models.Entities
{
    public partial class Annonce
    {
        [Key]
        public int Id { get; set; }
        public DateOnly DateDispoVente { get; set; }
        public string? Description { get; set; }
        public byte[]? Photo { get; set; }
        public double? Price { get; set; }

        public int VehiculeId { get; set; }
        public Vehicule? Vehicule { get; set; }
    }
}
