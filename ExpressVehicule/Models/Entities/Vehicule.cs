namespace ExpressVoitures.Models.Entities
{
    public partial class Vehicule
    {
        public int Id { get; set; }
        public string CodeVIN { get; set; }
        public int Annee { get; set; }
        public string Marque { get; set; }
        public string Model { get; set; }
        public string Finition { get; set; }
        public DateOnly DateAchat { get; set; }
        public double PrixAchat { get; set; }
        public string Reparation { get; set; }
        public double CoutReparation { get; set; }
        public DateOnly DateDispoVente { get; set; }
        public double PrixVente { get; set; }
        public DateOnly DateVente { get; set; }
        //public Image Photo { get; set; }

    }
}
