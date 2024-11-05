using ExpressVoitures.Data;
using ExpressVoitures.Models.Entities;

namespace ExpressVoitures.Models.Services
{
    public class VoitureService : IVoitureService
    {
        private static ApplicationDbContext _context;

        public VoitureService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Transaction> GetAllTransactions()
        {
            IEnumerable<Transaction> allTransaction = _context.Transactions.Where(t => t.Id > 0);
            foreach (Transaction transaction in allTransaction)
            {
                transaction.Vehicule = GetCarById(transaction.VehiculeId);
            }
            return allTransaction.ToList();
        }

        public Transaction GetTransactionById(int id)
        {
            List<Transaction> transactions = GetAllTransactions().ToList();
            return transactions.Find(t => t.Id == id);
        }

        public IEnumerable<Vehicule> GetAllVoitures()
        {
            IEnumerable<Vehicule> allVoitures = _context.Vehicules.Where(v => v.Id > 0);
            return allVoitures.ToList();
        }

        public Vehicule GetCarById(int id)
        {
            List<Vehicule> cars = GetAllVoitures().ToList();
            return cars.Find(v => v.Id == id);
        }

        public void SaveCar(Vehicule car)
        {
            _context.Vehicules.Add(car);
            _context.SaveChanges();
        }

        public void DeleteTransactionAndDataLinded(int id)
        {
            Transaction transaction = GetTransactionById(id);
            if (transaction != null)
            {
                _context.Vehicules.Remove(GetCarById(transaction.VehiculeId));
                foreach (Reparation r in transaction.Reparations)
                {
                    _context.Reparations.Remove(r);
                }
                _context.Transactions.Remove(transaction);
                _context.SaveChanges();
            }
        }

        public void UpdateCar(int id, Vehicule car)
        {
            //TODO
        }
    }
}
