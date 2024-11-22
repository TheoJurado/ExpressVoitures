using ExpressVoitures.Models.Entities;
using Microsoft.Identity.Client;

namespace ExpressVoitures.Models.Services
{
    public interface IVoitureService
    {
        IEnumerable<Vehicule> GetAllVoitures();

        IEnumerable<Transaction> GetAllTransactions();

        public Vehicule GetCarById(int i);

        public IEnumerable<Annonce> GetAllAnnonces();

        public Annonce GetAnnonceById(int id);

        public Transaction GetTransactionById(int i);

        void SaveCar(Vehicule car, List<Reparation> reparations, Transaction transactionAchat, Annonce annonce, Transaction transactionVente);

        public void DeleteAnnonce(int id);
        public bool UpdateVehicule(int idCar, Vehicule car);
        public bool UpdateAnnonce(int idAnnonce, Annonce annonce);
        public bool UpdateReparations(int vId, List<Reparation> allRepair);

    }
}
