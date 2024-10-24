using ExpressVoitures.Models.Entities;

namespace ExpressVoitures.Models.Services
{
    public interface IVoitureService
    {
        IEnumerable<Vehicule> GetAllVoitures();
    }
}
