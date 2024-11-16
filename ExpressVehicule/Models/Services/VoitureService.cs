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
            /*foreach (Transaction transaction in allTransaction)
            {
                transaction.Vehicule = GetCarById(transaction.VehiculeId);
            }/**/
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

        public IEnumerable<Annonce> GetAllAnnonces()
        {
            IEnumerable<Annonce> allAnnonce = _context.Annonces.Where(a => a.Id > 0);
            return allAnnonce.ToList();
        }

        public Annonce GetAnnonceById(int id)
        {
            IEnumerable<Annonce> allAnnonce = _context.Annonces.Where(a => a.Id > 0);
            return allAnnonce.First(a => a.Id == id);
        }

        public void SaveCar(Vehicule car, Reparation[] reparations, Transaction transactionAchat, Annonce annonce, Transaction transactionVente)
        {
            if (transactionAchat != null)
            {
                transactionAchat.VehiculeAchat = car;//
                car.TransactionAchat = transactionAchat;
            }
            if (transactionVente != null)
            {
                transactionVente.VehiculeVente = car;//
                car.TransactionVente = transactionVente;
            }
            if (reparations != null)
                foreach (Reparation r in reparations)
                {
                    r.Vehicule = car;
                    car.Reparations.Add(r);
                }
            if (annonce == null)
            {
                Annonce newAnnonce = new Annonce();
                newAnnonce.Description = "L'annonce n'as pas été correctement enregistrer";
                annonce = newAnnonce;
            }
            annonce.Vehicule = car;
            double priceAllRepar = 0;
            foreach (Reparation r in car.Reparations)
                priceAllRepar += r.Prix;
            annonce.Price = car.TransactionAchat.Price + priceAllRepar + 500;

            _context.Vehicules.Add(car);
            _context.Transactions.Add(transactionAchat);
            _context.Annonces.Add(annonce);
            _context.SaveChanges();
        }

        public void DeleteAnnonce(int id)
        {
            Annonce annonce = GetAnnonceById(id);
            if (annonce != null)
            {
                _context.Annonces.Remove(annonce);
                _context.SaveChanges();
            }
        }

        public void UpdateCar(int id, Annonce annonce)
        {
            //TODO
        }
    }
}
