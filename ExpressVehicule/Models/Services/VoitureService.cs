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
    }
}
