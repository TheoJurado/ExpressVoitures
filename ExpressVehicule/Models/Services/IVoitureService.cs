using ExpressVoitures.Models.Entities;
using Microsoft.Identity.Client;

namespace ExpressVoitures.Models.Services
{
    public interface IVoitureService
    {
        IEnumerable<Vehicule> GetAllVoitures();

        IEnumerable<Transaction> GetAllTransactions();

        public Vehicule GetCarById(int i);

        public Transaction GetTransactionById(int i);

        void SaveCar(Vehicule car);

        public void DeleteTransactionAndDataLinded(int id);
        public void UpdateCar(int id, Vehicule car);

    }
}
