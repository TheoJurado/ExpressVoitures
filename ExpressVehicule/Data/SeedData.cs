using Microsoft.EntityFrameworkCore;
using ExpressVoitures.Models;

namespace ExpressVoitures.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider, IConfiguration config)
        {
            using var context = new P5Referential(
                serviceProvider.GetRequiredService<DbContextOptions<P5Referential>>(), config);

            if (context.Vehicule.Any())
            {
                return;
            }

            context.Vehicule.AddRange(
                 new Vehicule
                 {
                     CodeVIN = "B38CX23",
                     Annee = 2019,
                     Marque = "Mazda",
                     Model = "Miata",
                     Finition = "LE",
                     DateAchat = new DateOnly(2022, 01, 07),
                     PrixAchat = 1800,
                     Reparation = "Restauration complète",
                     CoutReparation = 7600,
                     DateDispoVente = new DateOnly(2022,04,07),
                     PrixVente =9900,
                     DateVente = new DateOnly(2022,04,08)
                 },

                 new Vehicule
                 {
                     CodeVIN = "TX84TP32",
                     Annee = 2007,
                     Marque = "Jeep",
                     Model = "Liberty",
                     Finition = "Sport",
                     DateAchat = new DateOnly(2022, 04, 02),
                     PrixAchat = 4500,
                     Reparation = "Roulements des roues avant",
                     CoutReparation = 350,
                     DateDispoVente = new DateOnly(2022, 04, 07),
                     PrixVente = 5350,
                     DateVente = new DateOnly(2022, 04, 09)
                 },

                 new Vehicule
                 {
                     CodeVIN = "R41P243",
                     Annee = 2007,
                     Marque = "Renault",
                     Model = "Scénic",
                     Finition = "TCe",
                     DateAchat = new DateOnly(2022, 04, 04),
                     PrixAchat = 1800,
                     Reparation = "Radiateur, freins",
                     CoutReparation = 690,
                     DateDispoVente = new DateOnly(2022, 04, 08),
                     PrixVente = 2990
                 }
            );
            context.SaveChanges();/**/
        }
    }
}
