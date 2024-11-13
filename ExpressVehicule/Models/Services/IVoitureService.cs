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

        void SaveCar(Vehicule car, Reparation[] reparations, Transaction transactionAchat, Annonce annonce, Transaction transactionVente);

        public void DeleteTransactionAndDataLinked(int id);
        public void UpdateCar(int id, Annonce car);

    }
}
