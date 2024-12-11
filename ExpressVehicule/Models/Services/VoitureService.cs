using ExpressVoitures.Data;
using ExpressVoitures.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ExpressVoitures.Models.Services
{
    public class VoitureService : IVoitureService
    {
        private static ApplicationDbContext _context;
        private readonly AppSettings _appSettings;

        public VoitureService(ApplicationDbContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }
        public IEnumerable<Transaction> GetAllTransactions()
        {
            IEnumerable<Transaction> allTransaction = _context.Transactions.Where(t => t.Id > 0);
            
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
        {
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
        {
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
                transactionAchat.VehiculeLinked = car;
                car.TransactionAchat = transactionAchat;
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
            annonce.Price = car.TransactionAchat.Price + priceAllRepar + _appSettings.Marge;

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

        public bool UpdateVehicule(int idCar, Vehicule updatedVehicule, bool isAdmin = false)
        {
            var existingVehicule = GetCarById(idCar);

            if (existingVehicule == null)
                return false;

            existingVehicule.Statut = updatedVehicule.Statut;
            if (isAdmin)
            {//admin part
                existingVehicule.CodeVin = updatedVehicule.CodeVin;
                existingVehicule.Annee = updatedVehicule.Annee;
                existingVehicule.Marque = updatedVehicule.Marque;
                existingVehicule.Model = updatedVehicule.Model;
                existingVehicule.Finition = updatedVehicule.Finition;
            }

            _context.Vehicules.Update(existingVehicule);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateAnnonce(int idAnnonce, Annonce updatedAnnonce, double allPrice)
        {
            var existingAnnonce = GetAnnonceById(idAnnonce);

            if (existingAnnonce == null)
                return false;

            existingAnnonce.DateDispoVente = updatedAnnonce.DateDispoVente;
            existingAnnonce.Description = updatedAnnonce.Description;
            existingAnnonce.Price = allPrice;

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
            
            var existingReparations = existingVehicule.Reparations.ToList();

            //add new repair
            foreach (var updatedRep in updatedReparations)
            {
                var existingRep = existingReparations.FirstOrDefault(r => r.Id == updatedRep.Id);

                if (existingRep != null)
                {//update
                    if (!(updatedRep.Type == null || updatedRep.Prix == 0))
                    {//reparation valid
                        existingRep.Type = updatedRep.Type;
                        existingRep.Prix = updatedRep.Prix;
                    }
                    else
                    {//reparation NOT valid
                        _context.Reparations.Remove(existingRep);
                    }
                }
                else
                {//add
                    if (!(updatedRep.Type == null || updatedRep.Prix == 0))
                    {//reparation valid
                        updatedRep.VehiculeId = vehiculeId;
                        _context.Reparations.Add(updatedRep);
                    }
                    else
                    {//reparation Not valid
                        //do nothing
                    }
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
