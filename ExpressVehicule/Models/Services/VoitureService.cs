using ExpressVoitures.Data;
using ExpressVoitures.Models.Entities;
using Microsoft.EntityFrameworkCore;

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

        public Vehicule? GetCarById(int id)
        {/*
            List<Vehicule> cars = GetAllVoitures().ToList();
            return cars.Find(v => v.Id == id);/**/
            return _context.Vehicules
                .Include(v => v.Reparations)
                .Include(v => v.TransactionAchat)
                .Include(v => v.TransactionVente)
                .FirstOrDefault(v => v.Id == id);
        }

        public IEnumerable<Annonce> GetAllAnnonces()
        {
            IEnumerable<Annonce> allAnnonce = _context.Annonces.Where(a => a.Id > 0);
            return allAnnonce.ToList();
        }

        public Annonce? GetAnnonceById(int id)
        {/*
            IEnumerable<Annonce> allAnnonce = _context.Annonces.Where(a => a.Id > 0);
            return allAnnonce.First(a => a.Id == id);/**/
            return _context.Annonces
                .Include(a => a.Vehicule)
                .ThenInclude(v => v.TransactionAchat)
                .Include(a => a.Vehicule.TransactionVente)
                .Include(a => a.Vehicule.Reparations)
                .FirstOrDefault(a => a.Id == id);
        }

        public void SaveCar(Vehicule car, List<Reparation> reparations, Transaction transactionAchat, Annonce annonce, Transaction transactionVente)
        {
            if (transactionAchat != null)
            {
                transactionAchat.VehiculeAchat = car;
                car.TransactionAchat = transactionAchat;
            }
            if (transactionVente != null)
            {
                transactionVente.VehiculeVente = car;
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

        public bool UpdateVehicule(int idCar, Vehicule updatedVehicule)
        {
            var existingVehicule = GetCarById(idCar);

            if (existingVehicule == null)
                return false;

            existingVehicule.CodeVin = updatedVehicule.CodeVin;
            existingVehicule.Statut = updatedVehicule.Statut;
            existingVehicule.Annee = updatedVehicule.Annee;
            existingVehicule.Marque = updatedVehicule.Marque;
            existingVehicule.Model = updatedVehicule.Model;
            existingVehicule.Finition = updatedVehicule.Finition;

            _context.Vehicules.Update(existingVehicule);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateAnnonce(int idAnnonce, Annonce updatedAnnonce)
        {
            var existingAnnonce = GetAnnonceById(idAnnonce);

            if (existingAnnonce == null)
                return false;

            existingAnnonce.DateDispoVente = updatedAnnonce.DateDispoVente;
            existingAnnonce.Description = updatedAnnonce.Description;
            existingAnnonce.Price = updatedAnnonce.Price;

            if (updatedAnnonce.Photo != null && updatedAnnonce.Photo.Length > 0)
                existingAnnonce.Photo = updatedAnnonce.Photo;

            _context.Annonces.Update(existingAnnonce);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateReparations(int vehiculeId, List<Reparation> updatedReparations)
        {
            var existingVehicule = _context.Vehicules.Include(v => v.Reparations).FirstOrDefault(v => v.Id == vehiculeId);

            if (existingVehicule == null)
            {
                return false;
            }

            //remove existing repair
            var existingReparations = existingVehicule.Reparations.ToList();
            foreach (var rep in existingReparations)
            {
                if (!updatedReparations.Any(r => r.Id == rep.Id))
                {
                    _context.Reparations.Remove(rep);
                }
            }

            //add new repair
            foreach (var updatedRep in updatedReparations)
            {
                var existingRep = existingReparations.FirstOrDefault(r => r.Id == updatedRep.Id);

                if (existingRep != null)
                {
                    //update
                    existingRep.Type = updatedRep.Type;
                    existingRep.Prix = updatedRep.Prix;
                }
                else
                {
                    //add
                    updatedRep.VehiculeId = vehiculeId;
                    _context.Reparations.Add(updatedRep);
                }
            }

            _context.SaveChanges();
            return true;
        }

        public static IFormFile ByteArrayToFormFile(byte[] fileBytes, string fileName, string contentType)
        {
            var stream = new MemoryStream(fileBytes);
            return new FormFile(stream, 0, fileBytes.Length, "file", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType
            };
        }
    }
}
