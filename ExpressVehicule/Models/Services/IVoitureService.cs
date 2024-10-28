using ExpressVoitures.Models.Entities;
using Microsoft.Identity.Client;

namespace ExpressVoitures.Models.Services
{
    public interface IVoitureService
    {
        IEnumerable<Vehicule> GetAllVoitures();

        public Vehicule GetCarById(int i);

        void SaveCar(Vehicule car);
    }
}
